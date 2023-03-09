import { take, map } from 'rxjs/operators';
import { Seed } from './../models/Seed';
import { PaginatedResult } from './../models/Pagination';
import { Observable } from 'rxjs';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class SeedService {

  baseURL = environment.apiURL + "api/seed";

  constructor(private http: HttpClient) {}

  public getSeedsForSelect():  Observable<Seed[]>{
    return this.http.get<Seed[]>(`${this.baseURL}/select`).pipe(take(1));
  }

  public getSeeds(
    page?: number,
    itemsPerPage?: number,
    term?: string
  ): Observable<PaginatedResult<Seed[]>> {
    const paginatedResult: PaginatedResult<Seed[]> = new PaginatedResult<
      Seed[]
    >();
    let params = new HttpParams();

    if (page != null && itemsPerPage != null) {
      params = params.append("pageNumber", page.toString());
      params = params.append("pageSize", itemsPerPage.toString());
    }

    if (term != null && term != "") params = params.append("term", term);

    return this.http
      .get<Seed[]>(this.baseURL, { observe: "response", params })
      .pipe(
        take(1),
        map((response) => {
          paginatedResult.result = response.body;
          if (response.headers.has("Pagination")) {
            paginatedResult.pagination = JSON.parse(
              response.headers.get("Pagination")
            );
          }
          return paginatedResult;
        })
      );
  }

  public getSeedById(id: number): Observable<Seed> {
    return this.http.get<Seed>(`${this.baseURL}/${id}`).pipe(take(1));
  }

  public post(seed: Seed): Observable<Seed> {
    return this.http.post<Seed>(this.baseURL, seed).pipe(take(1));
  }

  public put(seed: Seed): Observable<Seed> {
    return this.http
      .put<Seed>(`${this.baseURL}/${seed.id}`, seed)
      .pipe(take(1));
  }

  public deleteSeed(id: number): Observable<any> {
    return this.http.delete(`${this.baseURL}/${id}`).pipe(take(1));
  }

}

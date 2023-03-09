import { take, map } from 'rxjs/operators';
import { Planting } from './../models/Planting';
import { Observable } from 'rxjs';
import { PaginatedResult } from './../models/Pagination';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class PlantingService {
  baseURL = environment.apiURL + "api/planting";

  constructor(private http: HttpClient) {}

  public getPlantings(
    page?: number,
    itemsPerPage?: number,
    term?: string
  ): Observable<PaginatedResult<Planting[]>> {
    const paginatedResult: PaginatedResult<Planting[]> = new PaginatedResult<
      Planting[]
    >();
    let params = new HttpParams();

    if (page != null && itemsPerPage != null) {
      params = params.append("pageNumber", page.toString());
      params = params.append("pageSize", itemsPerPage.toString());
    }

    if (term != null && term != "") params = params.append("term", term);

    return this.http
      .get<Planting[]>(this.baseURL, { observe: "response", params })
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

  public getPlantingById(id: number): Observable<Planting> {
    return this.http.get<Planting>(`${this.baseURL}/${id}`).pipe(take(1));
  }

  public post(planting: Planting): Observable<Planting> {
    return this.http.post<Planting>(this.baseURL, planting).pipe(take(1));
  }

  public put(planting: Planting): Observable<Planting> {
    return this.http
      .put<Planting>(`${this.baseURL}/${planting.id}`, planting)
      .pipe(take(1));
  }

  public deletePlanting(id: number): Observable<any> {
    return this.http.delete(`${this.baseURL}/${id}`).pipe(take(1));
  }

}

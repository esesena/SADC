import { take, map } from "rxjs/operators";
import { Observable } from "rxjs";
import { PaginatedResult } from "./../models/Pagination";
import { Farm } from "./../models/Farm";
import { environment } from "./../../environments/environment";
import { HttpClient, HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";

@Injectable({
  providedIn: "root",
})
export class FarmService {
  baseURL = environment.apiURL + "api/farm";

  constructor(private http: HttpClient) {}

  public getFarms(
    page?: number,
    itemsPerPage?: number,
    term?: string
  ): Observable<PaginatedResult<Farm[]>> {
    const paginatedResult: PaginatedResult<Farm[]> = new PaginatedResult<
      Farm[]
    >();
    let params = new HttpParams();

    if (page != null && itemsPerPage != null) {
      params = params.append("pageNumber", page.toString());
      params = params.append("pageSize", itemsPerPage.toString());
    }

    if (term != null && term != "") params = params.append("term", term);

    return this.http
      .get<Farm[]>(this.baseURL, { observe: "response", params })
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

  public getFarmById(id: number): Observable<Farm> {
    return this.http.get<Farm>(`${this.baseURL}/${id}`).pipe(take(1));
  }

  public post(farm: Farm): Observable<Farm> {
    return this.http.post<Farm>(this.baseURL, farm).pipe(take(1));
  }

  public put(farm: Farm): Observable<Farm> {
    return this.http
      .put<Farm>(`${this.baseURL}/${farm.id}`, farm)
      .pipe(take(1));
  }

  public deleteFarm(id: number): Observable<any> {
    return this.http.delete(`${this.baseURL}/${id}`).pipe(take(1));
  }

  postUpload(farmId: number, file: File): Observable<Farm> {
    const fileToUpload = file[0] as File;
    const formData = new FormData();
    formData.append("file", fileToUpload);

    return this.http
      .post<Farm>(`${this.baseURL}/upload-image/${farmId}`, formData)
      .pipe(take(1));
  }
}

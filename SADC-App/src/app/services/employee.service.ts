import { take, map } from 'rxjs/operators';
import { Employee } from './../models/Employee';
import { PaginatedResult } from './../models/Pagination';
import { Observable } from 'rxjs';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {
  baseURL = environment.apiURL + 'api/employee';

  constructor(private http: HttpClient) {}

  public getEmployees(
    page?: number,
    itemsPerPage?: number,
    term?: string
  ): Observable<PaginatedResult<Employee[]>> {
    const paginatedResult: PaginatedResult<Employee[]> = new PaginatedResult<
      Employee[]
    >();

    let params = new HttpParams();

    if (page != null && itemsPerPage != null) {
      params = params.append('pageNumber', page.toString());
      params = params.append('pageSize', itemsPerPage.toString());
    }

    if (term != null && term != '') params = params.append('term', term);

    return this.http
      .get<Employee[]>(this.baseURL + '/all', { observe: 'response', params })
      .pipe(
        take(1),
        map((response) => {
          paginatedResult.result = response.body;
          if (response.headers.has('Pagination')) {
            paginatedResult.pagination = JSON.parse(
              response.headers.get('Pagination')
            );
          }
          return paginatedResult;
        })
      );
  }

  public getEmployee(): Observable<Employee> {
    return this.http
      .get<Employee>(`${this.baseURL}`)
      .pipe(take(1));
  }

  public getEmployeeById(id: number): Observable<Employee> {
    return this.http
      .get<Employee>(`${this.baseURL}/${id}`)
      .pipe(take(1));
  }

  public post(): Observable<Employee> {
    return this.http
      .post<Employee>(this.baseURL, {} as Employee)
      .pipe(take(1));
  }

  public put(employee: Employee): Observable<Employee> {
    return this.http
      .put<Employee>(`${this.baseURL}`, employee)
      .pipe(take(1));
  }

  postUpload(employeeId: number, file: File): Observable<Employee> {
    const fileToUpload = file[0] as File;
    const formData = new FormData();
    formData.append('file', fileToUpload);

    return this.http
      .post<Employee>(`${this.baseURL}/upload-image/${employeeId}`, formData)
      .pipe(take(1));
  }
}

import { take } from 'rxjs/operators';
import { Field } from '../models/Field';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class FieldService {
  baseURL = environment.apiURL + 'api/field';


  constructor(private http: HttpClient) {}

  public getFieldsByFarmId(farmId: number): Observable<Field[]> {
    return this.http.get<Field[]>(`${this.baseURL}/${farmId}`).pipe(take(1));
  }

  public saveField(farmId: number, fields: Field[]): Observable<Field[]> {
    return this.http
      .put<Field[]>(`${this.baseURL}/${farmId}`, fields)
      .pipe(take(1));
  }

  public deleteField(farmId: number, fieldId: number): Observable<any> {
    return this.http
      .delete(`${this.baseURL}/${farmId}/${fieldId}`)
      .pipe(take(1));
  }

}

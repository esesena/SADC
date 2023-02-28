import { take } from 'rxjs/operators';
import { Plot } from './../models/Plot';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class PlotService {
  baseURL = environment.apiURL + 'api/plot';


  constructor(private http: HttpClient) {}

  public getPlotsByFarmId(farmId: number): Observable<Plot[]> {
    return this.http.get<Plot[]>(`${this.baseURL}/${farmId}`).pipe(take(1));
  }

  public savePlot(farmId: number, plots: Plot[]): Observable<Plot[]> {
    return this.http
      .put<Plot[]>(`${this.baseURL}/${farmId}`, plots)
      .pipe(take(1));
  }

  public deletePlot(farmId: number, plotId: number): Observable<any> {
    return this.http
      .delete(`${this.baseURL}/${farmId}/${plotId}`)
      .pipe(take(1));
  }

}

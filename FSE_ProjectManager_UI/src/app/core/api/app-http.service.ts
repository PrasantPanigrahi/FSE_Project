import { Observable, throwError } from 'rxjs';
import { catchError, map, retry, tap } from 'rxjs/operators';

import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { environment } from '../../../environments/environment';
import { AppHttpConfig } from './app-http-config';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json'
  })
};

/** Service that wrap {HttpClient} of @angular/common/http' */
@Injectable({
  providedIn: 'root'
})
export class AppHttpService {

  private readonly apiUrl: string;

  constructor(private http: HttpClient) {
    this.apiUrl = environment.api.url;
  }

  get<T>(config: AppHttpConfig) {
    return this.http
      .get<T>(this.apiUrl + config.url, httpOptions)
      .pipe(
        catchError(this.handleError)
      );
  }

  getBlob(config: AppHttpConfig): Observable<Blob> {
    return this.http
      .get(this.apiUrl + config.url)
      .pipe(
        map((res: any) => {
          return new Blob([res.blob()]);
        }),
        catchError(this.handleError)
      );
  }

  post<T>(config: AppHttpConfig, data: any) {
    return this.http
      .post<T>(this.apiUrl + config.url, data, httpOptions)
      .pipe(
        tap((r) => { }),
        catchError(this.handleError)
      );
  }

  postForm<T>(config: AppHttpConfig, form: FormData) {
    return this.http
      .post<T>(this.apiUrl + config.url, form)
      .pipe(
        tap((r) => { }),
        catchError(this.handleError)
      );
  }

  update<T>(config: AppHttpConfig, data: any) {
    return this.http.put<T>(this.apiUrl + config.url, data, httpOptions)
      .pipe(
        tap((r) => { }),
        catchError(this.handleError)
      );
  }

  delete<T>(config: AppHttpConfig) {
    return this.http.delete<T>(this.apiUrl + config.url, httpOptions)
      .pipe(
        tap((r) => { }),
        catchError(this.handleError)
      );
  }

  private handleError(error: HttpErrorResponse) {
    // return an observable with a user-facing error message
    return throwError(error);
  }

}

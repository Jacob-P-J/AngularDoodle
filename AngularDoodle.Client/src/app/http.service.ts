import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class HttpService {
  private baseUrl = 'https://localhost:7010';
  constructor(private http: HttpClient) { }

  getFromServer<T>(url: string, params?: HttpParams): Observable<T> {
    return this.http.get<T>(`${this.baseUrl}/${url}`, {params});
  }
}

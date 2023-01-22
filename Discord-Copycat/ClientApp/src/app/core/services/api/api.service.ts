import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  private apiUrl = "";

  constructor(private readonly httpClient: HttpClient, @Inject('BASE_URL') baseUrl: String) {
    this.apiUrl = baseUrl + "api/";
  }

  get<T>(path: String, params = {}): Observable<any> {
    console.log(path);
    return this.httpClient.get<T>(`${this.apiUrl}${path}`, { params });
  }

  put<T>(path: String, body = {}): Observable<any> {
    return this.httpClient.put<T>(`${this.apiUrl}${path}`, body);
  }

  post<T>(path: String, body = {}): Observable<any> {
    return this.httpClient.post<T>(`${this.apiUrl}${path}`, body);
  }

  delete<T>(path: String): Observable<any> {
    return this.httpClient.delete<T>(`${this.apiUrl}${path}`);
  }
}

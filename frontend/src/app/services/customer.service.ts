import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CustomerModel } from '../models/customer.model';

@Injectable({
  providedIn: 'root'
})
export class CustomerService {

  private apiUrl = "https://localhost:44383/api/customer";

  constructor(private http: HttpClient) { }

  getAll(): Observable<CustomerModel[]> {
    return this.http
      .get<CustomerModel[]>(this.apiUrl);
  }

  getById(id: string): Observable<CustomerModel> {
    return this.http
      .get<CustomerModel>(`${this.apiUrl}/${id}`)
  }

  add(regiao: CustomerModel): Observable<boolean> {
    return this.http
      .post<boolean>(this.apiUrl, regiao)
  }

  update(regiao: CustomerModel): Observable<boolean> {
    return this.http
      .put<boolean>(this.apiUrl, regiao)
  }

  delete(id: string): Observable<boolean> {
    return this.http
      .delete<boolean>(`${this.apiUrl}/${id}`)
  }
}

import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { PostalCodeModel } from '../models/postalCode.model';

@Injectable({
  providedIn: 'root'
})
export class PostalCodeService {

  constructor(private http: HttpClient) { }

  getPostalCode(postalCode: string) {
    return this.http
          .get<PostalCodeModel>(`https://viacep.com.br/ws/${postalCode}/json/`)
  }
}

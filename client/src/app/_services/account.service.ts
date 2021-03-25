import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
// Injectable means this service can be injected in any component 
@Injectable({
  providedIn: 'root'
})
export class AccountService {
  // Base URL to API
  baseUrl = 'https://localhost:5001/api/';

  constructor(private http: HttpClient) { }
  // Login method to send our creds to API
  login(model: any) {
    return this.http.post(this.baseUrl + 'account/login', model);
  }
}

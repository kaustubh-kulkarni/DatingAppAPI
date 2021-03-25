import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ReplaySubject } from 'rxjs';
import { map } from 'rxjs/operators';
import { User } from '../_models/user';
// Injectable means this service can be injected in any component 
@Injectable({
  providedIn: 'root'
})
export class AccountService {
  // Base URL to API
  baseUrl = 'https://localhost:5001/api/';
  // Holds previous values
  private currentUserSource = new ReplaySubject<User>(1);
  currentUser$ = this.currentUserSource.asObservable();

  constructor(private http: HttpClient) { }
  // Login method to send our creds to API
  login(model: any) {
    return this.http.post(this.baseUrl + 'account/login', model).pipe(
      // Anything inside pipe would be rxjs
      map((response: User) => {
        const user = response;
        if(user) {
          localStorage.setItem('user', JSON.stringify(user));
          this.currentUserSource.next(user);
        }
      })
    );
  }

  setCurrentUser(user : User) {
    this.currentUserSource.next(user);
  }

  logout(){
    localStorage.removeItem('user');
    this.currentUserSource.next(null);
  }
}

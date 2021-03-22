import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
// decorator
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'The Dating App';
  // Users are type of any i.e we are turning of type safety
  users: any;
  // Dependency injection
  constructor(private http: HttpClient) {}

  // Lifecycle events after constructor are called initializers
  ngOnInit(){
    this.getUsers();
  }

  getUsers(){
    // http get request should consist of url and will get response in observables
    // we need to subscribe to observables to get the data
    this.http.get('https://localhost:5001/api/users').subscribe(response => {
      this.users = response;
    }, error => {
      console.log(error);
    })
  }

}

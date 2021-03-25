import { Component, OnInit } from '@angular/core';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model: any = {}
  loggedIn: boolean;
  // Inject accountService in this constructor
  constructor(private accountService: AccountService) { }

  ngOnInit(): void {
    this.getCurrentUser();
  }

  // Login method
  login(){
    this.accountService.login(this.model).subscribe(response => {
      console.log(response);
      this.loggedIn = true;
    }, error => {
      console.log(error);
    });
  }

  logout(){
    this.accountService.logout();
    this.loggedIn = false;
  }

  getCurrentUser(){
    this.accountService.currentUser$.subscribe(user => {
      // !! turns our obj into boolean
      this.loggedIn = !!user;
    }, error => {
      console.log(error);
    })
  }

}

import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { User } from '../_models/user';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model: any = {}
  // Inject accountService in this constructor
  // Inject router in constructor
  constructor(public accountService: AccountService, private router: Router) { }

  ngOnInit(): void {
    this.accountService.currentUser$;
  }

  // Login method
  login(){
    this.accountService.login(this.model).subscribe(response => {
      // When user is logged in will be redirected to members
      this.router.navigateByUrl('/members');
      console.log(response);
    }, error => {
      console.log(error);
    });
  }

  logout(){
    this.accountService.logout();
    this.router.navigateByUrl('/');
  }

}

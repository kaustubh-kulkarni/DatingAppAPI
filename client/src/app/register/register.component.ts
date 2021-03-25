import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  // Input when getting props from parent to child
  @Input() usersFromHomeComponent: any;
  // Output when sending prop to parent from child
  @Output() cancelRegister = new EventEmitter();
  model: any = {}

  constructor() { }

  ngOnInit(): void {
  }

  register(){
    console.log(this.model);
  }

  cancel(){
    // What we want to emit when button is clicked
    this.cancelRegister.emit(false);
  }

}

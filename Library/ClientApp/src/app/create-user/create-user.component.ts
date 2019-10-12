import { Component } from "@angular/core";
import { Router } from "@angular/router";
import { DataService } from '../data.service';
import { Observable } from 'rxjs';

@Component({
  selector: "app-create-user",
  templateUrl: "./create-user.component.html",
  styleUrls: ["./create-user.component.scss"]
})
export class CreateUserComponent {
  firstName: string = "";
  lastName: string = "";
  email: string = "";
  password: string = "";
  createFailed: boolean = false;
  createSuccess: boolean = false;

  constructor(private router: Router, private data: DataService) {}

  createUser() {
    if (this.firstName === "" || this.lastName === "" || this.email === "" || this.password === "") {
      this.createFailed = true;
      this.createSuccess = false;
      return;
    }

    const user = { firstName: this.firstName, lastName: this.lastName, email: this.email, password: this.password };
    this.data.createUser(user).subscribe(result => {
      if (result.status === 201) {
        this.createFailed = false;
        this.createSuccess = true;
        this.firstName = "";
        this.lastName = "";
        this.email = "";
        this.password = "";
      }
    });
  }
}

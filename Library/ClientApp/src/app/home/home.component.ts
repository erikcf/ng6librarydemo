import { Component, Inject, OnInit } from '@angular/core';
import { SESSION_STORAGE, WebStorageService } from 'angular-webstorage-service';
import { DataService } from '../data.service';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  inputEmail: string = "";
  inputPassword: string = "";
  currentUser: string = "";
  user: IUser;
  loans: ILoan[];
  isLoginFailed: boolean = false;

  constructor(@Inject(SESSION_STORAGE) private storage: WebStorageService, private data: DataService) {}
  ngOnInit() {
    if (typeof this.storage.get("currentUser") !== 'undefined' && this.storage.get("currentUser") !== null) {
      this.currentUser = this.storage.get("currentUser");
      this.getLoanInfo();
    }
  }

  logInUser() {    
    this.data.logInUser(this.inputEmail, this.inputPassword).subscribe(result => {
      this.user = result.body as IUser;
      this.currentUser = this.user.email;
      this.storage.set('currentUser', this.user.email);
      this.storage.set('currentFirstName', this.user.firstName);
      this.storage.set('currentLastName', this.user.lastName);
      this.storage.set('currentId', this.user.userId);
      this.isLoginFailed = false;
      this.getLoanInfo();
    }, (): void => {
        this.isLoginFailed = true;
    });
  }

  logout() {
    this.storage.remove("currentUser");
    this.storage.remove("currentFirstName");
    this.storage.remove("currentLastName");
    this.storage.remove("currentId");
    this.currentUser = "";
    this.loans = [];
  }

  getLoanInfo() {
    this.data.getLoanInfo(this.storage.get("currentId")).subscribe(result => {
        this.loans = result as ILoan[];
    });
  }

    returnBook(id: number) {
      const loan = { active: false };
      this.data.returnBook(id, loan).subscribe(result => {
      this.getLoanInfo();
    });    
  }
}

interface IUser {
  firstName: string;
  lastName: string;
  email: string;
  userId: number;
}

interface ILoan {
  loanId: number;
  active: string;
  created: Date;
  finished: Date;
  bookName: string;
}

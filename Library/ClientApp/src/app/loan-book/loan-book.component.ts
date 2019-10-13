import { Component, Inject, OnInit } from '@angular/core';
import { SESSION_STORAGE, WebStorageService } from 'angular-webstorage-service';
import { DataService } from '../data.service';
import { Observable } from 'rxjs';
import { FormControl, Validators, FormGroup } from "@angular/forms";

@Component({
    selector: 'app-loan-book',
    templateUrl: './loan-book.component.html',
    styleUrls: ['./loan-book.component.scss']
})
export class LoanBookComponent implements OnInit {
  currentUserId: string = "";
    books$: Object;
    formGroup = new FormGroup({
      nameFormControl: new FormControl("", [Validators.required])
    });

    constructor(@Inject(SESSION_STORAGE) private storage: WebStorageService, private data: DataService) { }

    ngOnInit() {
        if (typeof this.storage.get("currentId") !== 'undefined' && this.storage.get("currentId") !== null) {
            this.currentUserId = this.storage.get("currentId");
        }
        this.getAllBooks();
    }
    getAllBooks() {
        this.data.getAllBooks(this.formGroup.get("nameFormControl").value).subscribe(
            result => this.books$ = result
        );
    }
    searchBook() {
        if (this.formGroup.get("nameFormControl").value.length > 2) {
            this.getAllBooks();
        }
    }
    getAvailabilityText(book: any) {
        if (book.available) {
            return "Free";
        } else {
            return "Borrowed by " + book.firstName + " " + book.lastName;
        }
    }
    loanBook(id: number) {
        const loan = { bookId: id, userId: this.currentUserId };
        this.data.loanBook(loan).subscribe(result => {
            if (result.status === 201) {
                this.getAllBooks();
            }
        });
    }
}

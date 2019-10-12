import { Component, Inject, OnInit } from '@angular/core';
import { SESSION_STORAGE, WebStorageService } from 'angular-webstorage-service';
import { DataService } from '../data.service';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-loan-book',
  templateUrl: './loan-book.component.html',
  styleUrls: ['./loan-book.component.scss']
})
export class LoanBookComponent implements OnInit {
  bookName: string = "";
  currentUserId: string = "";
  books$: Object;

  constructor(@Inject(SESSION_STORAGE) private storage: WebStorageService, private data: DataService) { }

  ngOnInit() {
    if (typeof this.storage.get("currentId") !== 'undefined' && this.storage.get("currentId") !== null) {
      this.currentUserId = this.storage.get("currentId");
    }
    this.getAllBooks();
  }
  getAllBooks() {
    this.data.getAllBooks(this.bookName).subscribe(
      result => this.books$ = result
    );
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

import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from "@angular/router";
import { DataService } from '../data.service';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-book',
  templateUrl: './book.component.html',
  styleUrls: ['./book.component.scss']
})
export class BookComponent implements OnInit {
  bookId: number;
  bookName: string;
  loans$: Object;

  constructor(private readonly route: ActivatedRoute, private data: DataService) { }

  ngOnInit() {
    this.route.params.subscribe(params => {
      this.bookId = params["id"] as number;
    });
    this.getBookName();
    this.getBookInfo();
  }

  getBookInfo() {
    this.data.getBookInfo(this.bookId).subscribe(
      result => this.loans$ = result
    );
  }
  getBookName() {
    this.data.getBookName(this.bookId).subscribe(
      result => this.bookName = (result as IBook).name
    );
  }
}

interface IBook {
  name: string;
}

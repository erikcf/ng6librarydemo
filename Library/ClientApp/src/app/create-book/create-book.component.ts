import { Component } from '@angular/core';
import { DataService } from '../data.service';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-create-book',
  templateUrl: './create-book.component.html',
  styleUrls: ['./create-book.component.scss']
})
export class CreateBookComponent {
  name: string = "";
  bookCreated: boolean = false;
  bookFailed: boolean = false;
  constructor(private data: DataService) {}
  createBook() {
    const book = { name: this.name };
    this.data.createBook(book).subscribe(result => {
      if (result.status === 201) {
        this.bookCreated = true;
      } else {
        this.bookFailed = true;
      }
    });
  }
}

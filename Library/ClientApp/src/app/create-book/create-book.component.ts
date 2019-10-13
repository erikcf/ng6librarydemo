import { Component } from '@angular/core';
import { DataService } from '../data.service';
import { Observable } from 'rxjs';
import { FormControl, Validators, FormGroup } from "@angular/forms";

@Component({
    selector: 'app-create-book',
    templateUrl: './create-book.component.html',
    styleUrls: ['./create-book.component.scss']
})
export class CreateBookComponent {
    bookCreated: boolean;
    bookFailed: boolean;
    formGroup = new FormGroup({
        nameFormControl: new FormControl("", [Validators.required])
    });
    constructor(private data: DataService) {
        this.bookCreated = false;
        this.bookFailed = false;
    }
    onSubmit() {
        const book = { name: this.formGroup.get("nameFormControl").value };
        this.data.createBook(book).subscribe(result => {
            if (result.status === 201) {
                this.bookFailed = false;
                this.bookCreated = true;
                this.formGroup.reset();
                Object.keys(this.formGroup.controls).forEach(key => {
                  this.formGroup.get(key).setErrors(null);
                });
            }
        }, () => {
            this.bookFailed = true;
            this.bookCreated = false;
        });
    }
}

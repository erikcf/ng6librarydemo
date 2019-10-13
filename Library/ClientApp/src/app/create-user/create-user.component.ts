import { Component } from "@angular/core";
import { Router } from "@angular/router";
import { DataService } from '../data.service';
import { Observable } from 'rxjs';
import { FormControl, Validators, FormGroup } from "@angular/forms";

@Component({
    selector: "app-create-user",
    templateUrl: "./create-user.component.html",
    styleUrls: ["./create-user.component.scss"]
})
export class CreateUserComponent {
    createSuccess: boolean;
    createFailed: boolean;
    formGroup = new FormGroup({
        firstNameFormControl: new FormControl("", [Validators.required]),
        lastNameFormControl: new FormControl("", [Validators.required]),
        emailFormControl: new FormControl("", [
            Validators.required,
            Validators.email
        ]),
        passwordFormControl: new FormControl("", [Validators.required])
    });

    constructor(private router: Router, private data: DataService) {
        this.createSuccess = false;
        this.createFailed = false;
    }

    onSubmit(): void {
        const user = {
            firstName: this.formGroup.get("firstNameFormControl").value,
            lastName: this.formGroup.get("lastNameFormControl").value,
            email: this.formGroup.get("emailFormControl").value,
            password: this.formGroup.get("passwordFormControl").value,
        };
        this.data.createUser(user).subscribe(result => {
            if (result.status === 201) {
                this.createFailed = false;
                this.createSuccess = true;
                this.formGroup.reset();
                Object.keys(this.formGroup.controls).forEach(key => {
                  this.formGroup.get(key).setErrors(null);
                });
            }
        }, (): void => {
          this.createFailed = true;
          this.createSuccess = false;
        });
    }
}

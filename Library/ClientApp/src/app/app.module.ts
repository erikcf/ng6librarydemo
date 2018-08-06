import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { StorageServiceModule } from 'angular-webstorage-service';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CreateBookComponent } from './create-book/create-book.component';
import { LoanBookComponent } from './loan-book/loan-book.component';
import { CreateUserComponent } from './create-user/create-user.component';
import { BookComponent } from './book/book.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CreateBookComponent,
    LoanBookComponent,
    CreateUserComponent,
    BookComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    StorageServiceModule,
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'createbook', component: CreateBookComponent},
      { path: 'loan', component: LoanBookComponent},
      { path: 'createuser', component: CreateUserComponent},
      { path: 'book/:id', component: BookComponent}
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }

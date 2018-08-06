import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class DataService {

  constructor(private http: HttpClient) { }

  getAllBooks(name) {
    return this.http.get("/api/BookData/GetAllBooks?name=" + name);
  }
  loanBook(id, userid) {
    return this.http.post("/api/BookData/CreateLoan?userId=" + userid + "&bookId=" + id, null, { observe: 'response'});
  }
  logInUser(email, password) {
    return this.http.get("/api/BookData/LogInUser?email=" + email + "&password=" + password);
  }
  getLoanInfo(id) {
    return this.http.get("/api/BookData/GetLoansByUserId/" + id);
  }
  returnBook(id) {
    return this.http.put("/api/BookData/ReturnLoan/" + id, null);
  }
  createUser(user) {
    return this.http.post("/api/BookData/CreateUser", user, { observe: 'response' });
  }
  createBook(book) {
    return this.http.post("/api/BookData/CreateBook", book, { observe: 'response' });
  }
  getBookInfo(id) {
    return this.http.get("/api/BookData/GetLoansForBookById/" + id);
  }
  getBookName(id) {
    return this.http.get("/api/BookData/GetBookById/" + id);
  }
}

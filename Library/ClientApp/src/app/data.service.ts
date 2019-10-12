import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class DataService {

  constructor(private http: HttpClient) { }

  getAllBooks(name) {
    return this.http.get(`/api/Book/GetAllBooks?name=${name}`);
  }
  loanBook(loan) {
    return this.http.post(`/api/Loan/CreateLoan`, loan, { observe: 'response'});
  }
  logInUser(email, password) {
      return this.http.get(`/api/User/LogInUser?email=${email}&password=${password}`, { observe: 'response' });
  }
  getLoanInfo(id) {
    return this.http.get(`/api/Loan/GetLoansByUserId/${id}`);
  }
  returnBook(id, loan) {
      return this.http.put(`/api/Loan/UpdateLoan/${id}`, loan);
  }
  createUser(user) {
    return this.http.post("/api/User/CreateUser", user, { observe: 'response' });
  }
  createBook(book) {
    return this.http.post("/api/Book/CreateBook", book, { observe: 'response' });
  }
  getBookInfo(id) {
    return this.http.get(`/api/Loan/GetLoansForBookById/${id}`);
  }
  getBookName(id) {
    return this.http.get(`/api/Book/GetBookById/${id}`);
  }
}

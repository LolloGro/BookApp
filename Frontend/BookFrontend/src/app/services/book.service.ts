import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable, tap} from 'rxjs';

export interface CreateBook {
  title: string;
  author: string;
  publishDate: string;
}

export interface Book {
  id: number;
  title: string;
  author: string;
  publishDate: string;
}

@Injectable({
  providedIn: 'root'
})

export class BookService {
  private api = 'http://localhost:5249/api/Auth';

  constructor(private http: HttpClient) { }

  getAllBooks():Observable<Book[]> {
    return this.http.get<Book[]>(`${this.api}/Book`);
  }

  getBookByID(id :number):Observable<Book>{
    return this.http.get<Book>(`${this.api}/${id}`);
  }

  createBook({title, author, publishDate}: CreateBook):Observable<Book>{
    return this.http.post<Book>(`${this.api}/Book`, {title, author, publishDate});
  }

  updateBook({title, author, publishDate}: CreateBook):Observable<Book> {
    return this.http.put<Book>(`${this.api}/Book`, {title, author, publishDate});
  }

  deleteBook(id:number):Observable<Book>{
    return this.http.delete<Book>(`${this.api}/${id}`);
  }

}

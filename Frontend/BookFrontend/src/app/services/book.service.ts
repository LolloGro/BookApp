import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {BehaviorSubject, Observable, tap} from 'rxjs';

export interface CreateBook {
  title: string;
  author: string;
  publishDate: string;
}

export interface BookType {
  id: number;
  title: string;
  author: string;
  publishDate: string;
}

@Injectable({
  providedIn: 'root'
})

export class BookService {
  private api = 'http://localhost:5249/api/Book';
  private bookSubject = new BehaviorSubject<BookType[]>([]);
  books$ =  this.bookSubject.asObservable();

  constructor(private http: HttpClient) { }

  getAllBooks(){
    this.http.get<BookType[]>(`${this.api}`)
      .subscribe(books => this.bookSubject.next(books));
  }

  //use?
  getBookByID(id :number):Observable<BookType>{
    return this.http.get<BookType>(`${this.api}/${id}`);
  }

  createBook({title, author, publishDate}: CreateBook):Observable<BookType>{
    return this.http.post<BookType>(`${this.api}`, {title, author, publishDate});
  }

  //Add id
  updateBook({title, author, publishDate}: CreateBook):Observable<BookType> {
    return this.http.put<BookType>(`${this.api}`, {title, author, publishDate});
  }

  deleteBook(id:number){
    return this.http.delete<BookType>(`${this.api}/${id}`)
      .pipe(tap(() => {
          const currentBook = this.bookSubject.value;
          this.bookSubject.next(currentBook.filter(book => book.id !== id));
        }));
  }

}

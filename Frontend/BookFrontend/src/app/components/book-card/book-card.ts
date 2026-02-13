import {Component, inject, signal, OnInit} from '@angular/core';
import {BookService} from '../../services/book.service';
import {BookType} from '../../services/book.service';
import {RouterModule} from '@angular/router';
import {DatePipe, AsyncPipe} from '@angular/common';


@Component({
  selector: 'app-book-card',
  standalone: true,
  imports: [RouterModule, AsyncPipe],
  templateUrl: './book-card.html',
  styleUrl: './book-card.css',
  providers: [DatePipe]
})
export class BookCard implements OnInit {

  errorMessage = signal('');

private bookService = inject(BookService);
private datePipe = inject(DatePipe);

books$ = this.bookService.books$;

ngOnInit() {
  this.bookService.getAllBooks();
}

  deleteBook(book: BookType) {
  if(!confirm('Are you sure you want to delete this book?')) return;

  this.bookService.deleteBook(book.id).subscribe({
    error: err => {
      this.errorMessage.set(err.error?.message || 'Failed to delete book');
    }
  })
}

updateBook(book: BookType) {
  console.log("Open edit module", book);
}

formatDate(date: string) {
  return this.datePipe.transform(date, 'yyyy-MM-dd');
}

}

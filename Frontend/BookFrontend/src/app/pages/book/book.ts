import { Component } from '@angular/core';
import { BookForm} from '../../components/book-form/book-form';

@Component({
  selector: 'app-book',
  imports: [BookForm],
  templateUrl: './book.html',
  styleUrl: './book.css',
})
export class Book {

}

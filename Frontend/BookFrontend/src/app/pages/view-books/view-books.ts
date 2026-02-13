import {Component} from '@angular/core';
import {BookCard} from '../../components/book-card/book-card';

@Component({
  selector: 'app-view-books',
  imports: [BookCard],
  templateUrl: './view-books.html',
  styleUrl: './view-books.css',
})
export class ViewBooks{

}

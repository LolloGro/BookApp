import { Routes } from '@angular/router';
import {Login} from './pages/login/login';
import {Home} from './pages/home/home';
import {Register} from './pages/register/register';
import {ViewBooks} from './pages/view-books/view-books';
import {UpdateBookForm} from './components/update-book-form/update-book-form';
import {BookForm} from './components/book-form/book-form';
import {QuoteForm} from './components/quote-form/quote-form';
import {QuoteList} from './components/quote-list/quote-list';
import {UpdateQuoteForm} from './components/update-quote-form/uppdate-quote-form';


export const routes: Routes = [
  {
    path:'',
    component: Home,
  },
  {
    path: 'login',
    component: Login
  },
  {
    path: 'register',
    component: Register
  },
  {
    path: 'book',
    component: ViewBooks
  },
  {
    path: 'book/create',
    component: BookForm
  },
  {
    path: 'book/update/:id',
    component: UpdateBookForm
  },
  {
    path: 'quote',
    component: QuoteList
  },
  {
    path: 'quote/create',
    component: QuoteForm
  },
  {
    path: 'quote/update/:id',
    component: UpdateQuoteForm
  }
];

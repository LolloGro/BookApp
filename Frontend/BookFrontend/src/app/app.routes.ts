import { Routes } from '@angular/router';
import {Home} from './pages/home/home';
import {UpdateBookForm} from './components/update-book-form/update-book-form';
import {BookForm} from './components/book-form/book-form';
import {QuoteForm} from './components/quote-form/quote-form';
import {QuoteList} from './components/quote-list/quote-list';
import {UpdateQuoteForm} from './components/update-quote-form/uppdate-quote-form';
import {LoginForm} from './components/login-form/login-form';
import {RegisterForm} from './components/register-form/register-form';
import {BookCard} from './components/book-card/book-card';


export const routes: Routes = [
  {
    path:'',
    component: Home,
  },
  {
    path: 'login',
    component: LoginForm
  },
  {
    path: 'register',
    component: RegisterForm
  },
  {
    path: 'logout',
    component: Home
  },
  {
    path: 'book',
    component: BookCard
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

import { Routes } from '@angular/router';
import {Login} from './pages/login/login';
import {Home} from './pages/home/home';
import {Register} from './pages/register/register';
import {Book} from './pages/book/book';
import {ViewBooks} from './pages/view-books/view-books';

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
    component: Book
  },
  {
    path: 'books',
    component: ViewBooks
  }
];

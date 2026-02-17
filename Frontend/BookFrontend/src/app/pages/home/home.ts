import { Component, inject } from '@angular/core';
import {RouterLink} from '@angular/router';
import {AsyncPipe} from '@angular/common';
import {LoginService} from '../../services/login.service';

@Component({
  selector: 'app-home',
  imports: [
    RouterLink,
    AsyncPipe
  ],
  templateUrl: './home.html',
  styleUrl: './home.css',
})
export class Home {
private loginService = inject(LoginService);
isLoggedIn$ = this.loginService.loggedIn$
}

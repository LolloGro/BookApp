import { Component, inject} from '@angular/core';
import { RouterModule, Router } from '@angular/router';
import {LoginService} from '../../services/login.service';
import {AsyncPipe} from '@angular/common';
import {ThemeService} from '../../services/theme.service';

@Component({
  selector: 'app-app-header',
  imports: [RouterModule, AsyncPipe],
  templateUrl: './app-header.html',
  styleUrl: './app-header.css',
})
export class AppHeader{
  private loginService = inject(LoginService);
  public themeService = inject(ThemeService);
  private router = inject(Router);

  isLoggedIn$ = this.loginService.loggedIn$;

  onLogout(){
    this.loginService.logout();
    this.router.navigate(['logout']);
  }

  toggleTheme(){
    this.themeService.toggleDarkMode()
  }
}

import { Component, inject, AfterViewInit} from '@angular/core';
import { RouterModule, Router } from '@angular/router';
import {LoginService} from '../../services/login.service';
import {AsyncPipe} from '@angular/common';
import {ThemeService} from '../../services/theme.service';

declare var bootstrap: any;
@Component({
  selector: 'app-app-header',
  imports: [RouterModule, AsyncPipe],
  templateUrl: './app-header.html',
  styleUrl: './app-header.css',
})
export class AppHeader implements AfterViewInit {
  private onClose: any;

  private loginService = inject(LoginService);
  public themeService = inject(ThemeService);
  private router = inject(Router);

  isLoggedIn$ = this.loginService.loggedIn$;

  ngAfterViewInit() {
    const close = document.getElementById('navbarSupportedContent');
    if(close){
      this.onClose = new bootstrap.Collapse(close, {toggle: false});
    }
  }

  closeNavbar(){
    if(this.onClose){
      this.onClose.hide();
    }
  }


  onLogout(){
    this.loginService.logout();
    this.router.navigate(['logout']);
  }

  toggleTheme(){
    this.themeService.toggleDarkMode()
  }
}

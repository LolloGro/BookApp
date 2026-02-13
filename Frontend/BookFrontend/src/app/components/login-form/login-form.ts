import { Component, inject, signal } from '@angular/core';
import {ReactiveFormsModule, Validators, NonNullableFormBuilder} from '@angular/forms';
import {LoginService} from '../../services/login.service';
import {Router, RouterModule} from '@angular/router';

@Component({
  selector: 'app-login-form',
  standalone: true,
  imports: [ReactiveFormsModule, RouterModule],
  templateUrl: './login-form.html',
  styleUrl: './login-form.css',
})

export class LoginForm {

errorMessage = signal('');
loading = signal(false);

private formBuilder= inject(NonNullableFormBuilder);
private loginService = inject(LoginService);
private router = inject(Router);

 loginForm = this.formBuilder.group({
    username: ['', Validators.required],
    password: ['', Validators.required],
  })

  onLogin() {

    if (this.loginForm.invalid) {
      return;
    }

    this.loading.set(true);
    this.errorMessage.set('');

    const {username, password} = this.loginForm.getRawValue();

    this.loginService.loginUser(username, password)
    .subscribe(
      {
        next: () => this.router.navigate(['/books']),
        error: err => {
          this.errorMessage.set(err.error?.message || 'Login failed');
          this.loading.set(false);
        }
      });
  }
}

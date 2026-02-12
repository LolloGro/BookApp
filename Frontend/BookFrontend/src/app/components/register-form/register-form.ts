import { Component, inject, signal } from '@angular/core';
import {ReactiveFormsModule, Validators, NonNullableFormBuilder} from '@angular/forms';
import {RegisterService} from '../../services/register.service';
import {Router} from '@angular/router';

@Component({
  selector: 'app-register-form',
  standalone: true,
  imports: [ReactiveFormsModule ],
  templateUrl: './register-form.html',
  styleUrl: './register-form.css',
})
export class RegisterForm{

  errorMessage = signal('');
  loading = signal(false);

  private formBuilder= inject(NonNullableFormBuilder);
  private registerService = inject(RegisterService);
  private router = inject(Router);


    registerForm = this.formBuilder.group({
      username: ['',[ Validators.required, Validators.minLength(3)]],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]],});

  onRegister() {
    if(this.registerForm.invalid)
    {
      return;
    }

    this.loading.set(true);
    this.errorMessage.set('');

    const {username, email, password} = this.registerForm.getRawValue();

    this.registerService.register({username, email, password}).subscribe({
      next: () => this.router.navigate(['/login']),
      error: err => {
        this.errorMessage.set(err.error?.message || 'Login failed');
        this.loading.set(false);
      }
    })
  }
}

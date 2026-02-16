import { Component, inject, signal } from '@angular/core';
import {ReactiveFormsModule, Validators, NonNullableFormBuilder} from '@angular/forms';
import {QuoteService} from '../../services/quote.service';
import {Router, RouterModule} from '@angular/router';

@Component({
  selector: 'app-quote-form',
  standalone: true,
  imports: [ReactiveFormsModule, RouterModule],
  templateUrl: './quote-form.html',
  styleUrl: './quote-form.css',
})

export class QuoteForm {
  errorMessage = signal('');
  loading = signal(false);

  private formBuilder= inject(NonNullableFormBuilder);
  private quoteService = inject(QuoteService);
  private router = inject(Router);

  quoteForm = this.formBuilder.group({
    quoteText: ['', Validators.required],
  })

  onCreateQuote(){
    if(this.quoteForm.invalid){
      this.quoteForm.markAllAsTouched();
      return;
    }

    this.loading.set(true);
    this.errorMessage.set('');

    const quote =  this.quoteForm.getRawValue();

    this.quoteService.createQuote(quote).subscribe({
      next: () => this.router.navigate(['/quote']),
      error: err => {
        this.errorMessage.set(err.error?.message || 'Creating of book failed');
        this.loading.set(false);
      }
    });
  }
}

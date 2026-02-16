import { Component, inject, signal, OnInit } from '@angular/core';
import {ReactiveFormsModule, Validators, NonNullableFormBuilder} from '@angular/forms';
import {ActivatedRoute, Router} from '@angular/router';
import {QuoteService} from '../../services/quote.service';

@Component({
  selector: 'app-update-quote-form',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './update-quote-form.html',
  styleUrl: './update-quote-form.css',
})
export class UpdateQuoteForm implements  OnInit {

  errorMessage = signal('');
  loading = signal(false);
  quoteId!: number;

  private route = inject(ActivatedRoute);
  private router  = inject(Router);
  private quoteService = inject(QuoteService);
  private formBuilder = inject(NonNullableFormBuilder);

  updateForm = this.formBuilder.group({
    quoteText: ["",[Validators.required]],
  });

  ngOnInit() {
    this.quoteId = Number(this.route.snapshot.paramMap.get('id'));

    this.quoteService.getQuoteById(this.quoteId)
      .subscribe(quote => {this.updateForm.patchValue({
        quoteText: quote.quoteText,
      });
      });
  }

  updateQuote(){
    if(this.updateForm.invalid){
      return;
    }

    this.loading.set(true);
    this.errorMessage.set('');

    const text = this.updateForm.getRawValue();

    this.quoteService.updateQuote(this.quoteId, text)
      .subscribe({
        next: () => {
          void this.router.navigate(['/quote']);
        },
        error: err => {
          this.errorMessage.set(err.error?.message || 'Update of quote failed');
          this.loading.set(false);
        }
      });
  }

}

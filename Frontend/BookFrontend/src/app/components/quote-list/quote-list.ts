import { Component, inject, signal, OnInit } from '@angular/core';
import {QuoteService, QuoteType} from '../../services/quote.service';
import {RouterModule, Router} from '@angular/router';
import {AsyncPipe} from '@angular/common';
import {combineLatest, map} from 'rxjs';

@Component({
  selector: 'app-quote-list',
  standalone: true,
  imports: [RouterModule,
    AsyncPipe
  ],
  templateUrl: './quote-list.html',
  styleUrl: './quote-list.css',
})
export class QuoteList implements OnInit {
  private quoteService = inject(QuoteService);
  private router = inject(Router);

  quotes$ = this.quoteService.quotes$;
  totalCount$ = this.quoteService.totalCount$;
  page$ = this.quoteService.page$;
  pageSize$ = this.quoteService.pageSize$;

  page = 1;

  errorMessage = signal('');

  pageInfo$ =combineLatest([this.page$, this.totalCount$, this.pageSize$]).pipe(
    map(([page, totalCount, pageSize]) => ({
      page,
      totalPages: Math.ceil(totalCount / pageSize)}))
  );

  ngOnInit() {
    this.getPaginatedQuotes(this.page);
  }

  getPaginatedQuotes(page:number) {
    this.quoteService.getAllQuotes(page);
  }

  nextPage() {
    this.quoteService.getNextQuote();
  }

  previousPage() {
   this.quoteService.getPreviousQuote();
  }

  updateQuote(quote: QuoteType) {
    this.router.navigate(['quote/update', quote.id]);
  }

  deleteQuote(quote: QuoteType) {
    if(!confirm('Are you sure you want to delete this book?')) return;
    this.quoteService.deleteQuote(quote.id).subscribe({
      error: err => {
        this.errorMessage.set(err.error?.message || 'Failed to delete quote');
      }
    });
  }

}

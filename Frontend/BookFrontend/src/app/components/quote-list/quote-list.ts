import { Component, inject, signal, OnInit } from '@angular/core';
import {QuoteService} from '../../services/quote.service';
import {RouterModule} from '@angular/router';
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
  quotes$ = this.quoteService.quotes$;
  totalCount$ = this.quoteService.totalCount$;
  page$ = this.quoteService.page$;
  pageSize$ = this.quoteService.pageSize$;

  page = 1;

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

  updateQuote() {
    console.log("Update")
  }

  deleteQuote() {
    console.log("Delete");
  }

}

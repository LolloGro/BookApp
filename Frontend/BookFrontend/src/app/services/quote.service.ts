import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {BehaviorSubject, Observable, tap} from 'rxjs';

export interface Quote{
  quoteText: string;
}
export interface QuoteType{
  id: number,
  quoteText:string,
}

export interface PaginationResult<T>{
  listItems: T[];
  totalCount: number;
  page : number;
  pageSize: number;
}

@Injectable({
  providedIn: 'root'
})

export class QuoteService {
  private api = 'http://localhost:5249/api/Quote';

  private quoteSubject = new BehaviorSubject<QuoteType[]>([]);
  private totalCountSubject = new BehaviorSubject<number>(0);
  private pageSubject = new BehaviorSubject<number>(1);
  private pageSizeSubject = new BehaviorSubject<number>(5);

  quotes$ =  this.quoteSubject.asObservable();
  totalCount$ =  this.totalCountSubject.asObservable();
  page$ =  this.pageSubject.asObservable();
  pageSize$ =  this.pageSizeSubject.asObservable();
  constructor(private http: HttpClient) { }

  getAllQuotes(page: number){
    this.http.get<PaginationResult<QuoteType>>(`${this.api}?page=${page}&pageSize=${this.pageSizeSubject.value}`)
      .subscribe(res => {
        this.quoteSubject.next(res.listItems)
        this.totalCountSubject.next(res.totalCount)
        this.pageSubject.next(res.page)
        this.pageSizeSubject.next(res.pageSize)
      });
  }

  getNextQuote(){
    const page = this.pageSubject.value;
    const total = Math.ceil(this.totalCountSubject.value / this.pageSizeSubject.value);
    if(page < total){
      this.getAllQuotes(page + 1);
    }
  }

  getPreviousQuote(){
    const page = this.pageSubject.value;
    if(page > 1){
      this.getAllQuotes(page - 1);
    }
  }

  getQuoteById(id: number){
    return this.http.get<Quote>(`${this.api}/${id}`);
  }

  createQuote(quote: Quote): Observable<QuoteType>{
    return this.http.post<QuoteType>(`${this.api}`, quote);
  }

  updateQuote(id: number, quote: Quote):Observable<QuoteType> {
    return this.http.put<QuoteType>(`${this.api}/${id}`, quote);
  }

  deleteQuote(id: number){
    return this.http.delete<Quote>(`${this.api}/${id}`)
      .pipe(tap(() => {
        const currentQuote = this.quoteSubject.value;
        this.quoteSubject.next(currentQuote.filter(quote => quote.id !== id));
    }));
  }

}

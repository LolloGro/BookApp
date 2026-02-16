import { Component, inject, signal} from '@angular/core';
import {ReactiveFormsModule, Validators, NonNullableFormBuilder} from '@angular/forms';
import {BookService} from '../../services/book.service';
import {Router, RouterModule} from '@angular/router';

@Component({
  selector: 'app-book-form',
  standalone: true,
  imports: [ReactiveFormsModule, RouterModule],
  templateUrl: './book-form.html',
  styleUrl: './book-form.css',
})

export class BookForm {
  errorMessage = signal('');
  loading = signal(false);

  private formBuilder= inject(NonNullableFormBuilder);
  private bookService = inject(BookService);
  private router = inject(Router);

  bookForm = this.formBuilder.group({
    title: ['', Validators.required],
    author: ['', Validators.required],
    publishDate: ['', Validators.required],
  })

  onCreateBook(){
    if(this.bookForm.invalid){
      return;
    }

    this.loading.set(true);
    this.errorMessage.set('');

    const {title, author, publishDate} = this.bookForm.getRawValue();

    this.bookService.createBook({title, author, publishDate}).subscribe({
      next: () => this.router.navigate(['/book']),
      error: err => {
        this.errorMessage.set(err.error?.message || 'Creating of book failed');
        this.loading.set(false);
      }
    });
  }
}

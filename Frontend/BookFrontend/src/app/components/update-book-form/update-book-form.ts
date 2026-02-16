import { Component, inject, signal, OnInit } from '@angular/core';
import {ReactiveFormsModule, Validators, NonNullableFormBuilder} from '@angular/forms';
import {BookService} from '../../services/book.service';
import {ActivatedRoute, Router} from '@angular/router';

@Component({
  selector: 'app-update-book-form',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './update-book-form.html',
  styleUrl: './update-book-form.css',
})
export class UpdateBookForm implements OnInit {
  errorMessage = signal('');
  loading = signal(false);
  bookId!: number;

  private route = inject(ActivatedRoute);
  private router  = inject(Router);
  private bookService = inject(BookService);
  private formBuilder = inject(NonNullableFormBuilder);

  updateForm = this.formBuilder.group({
    title: ["",[Validators.required]],
    author: ["",[Validators.required]],
    publishDate: ["", [Validators.required]],
  });

  ngOnInit() {
    this.bookId = Number(this.route.snapshot.paramMap.get('id'));

    this.bookService.getBookByID(this.bookId)
      .subscribe(book => { this. updateForm.patchValue({
        title: book.title,
        author: book.author,
        publishDate: book.publishDate.split('T')[0]
      });
    });
  }

  updateBook(){
    if(this.updateForm.invalid){
      this.updateForm.markAllAsTouched();
      return;
    }

    this.loading.set(true);
    this.errorMessage.set('');

    const {title, author, publishDate} = this.updateForm.getRawValue();

    this.bookService.updateBook(this.bookId, {title, author, publishDate})
    .subscribe({
      next: () => {
        void this.router.navigate(['/book']);
      },
      error: err => {
        this.errorMessage.set(err.error?.message || 'Update of book failed');
        this.loading.set(false);
      }
    });

  }

}

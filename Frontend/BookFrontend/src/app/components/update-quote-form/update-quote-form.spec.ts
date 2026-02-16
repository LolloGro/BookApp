import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UpdateQuoteForm } from './uppdate-quote-form';

describe('UpdateQuoteForm', () => {
  let component: UpdateQuoteForm;
  let fixture: ComponentFixture<UpdateQuoteForm>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [UpdateQuoteForm]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UpdateQuoteForm);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

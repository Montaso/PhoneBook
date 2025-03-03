import { Component } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { ContactService } from '../services/contact.service';
import { PutContact } from '../models/put-contact.model';
import { CategoryService } from '../../categories/services/category.service';
import { Categories } from '../../categories/models/categories.model';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-contact-add',
  imports: [CommonModule, ReactiveFormsModule, RouterLink],
  templateUrl: './contact-add.component.html',
  styleUrls: ['./contact-add.component.scss'],
})
export class ContactAddComponent {
  contactForm: FormGroup;
  categories: Categories | undefined;

  constructor(
    private fb: FormBuilder,
    private contactService: ContactService,
    private categoryService: CategoryService,
    private router: Router
  ) {
    this.contactForm = this.fb.group({
      name: ['', Validators.required],
      surname: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]],
      phoneNumber: ['', Validators.required],
      birthDate: ['', Validators.required],
      subcategory: ['', Validators.required],
    });
  }

  ngOnInit(): void {
    this.categoryService.getCategories().subscribe({
      next: (categories) => (this.categories = categories),
      error: (err) => console.error('Error fetching categories:', err),
    });
  }

  onSubmit(): void {
    if (this.contactForm.valid) {
      const newContact: PutContact = this.contactForm.value;

      this.contactService.postContact(newContact).subscribe(
        () => {
          console.log('Contact successfully added');
          this.router.navigate(['']);
        },
        (error) => {
          console.error('Error adding contact:', error);
          alert('Failed to add contact');
        }
      );
    }
  }
}

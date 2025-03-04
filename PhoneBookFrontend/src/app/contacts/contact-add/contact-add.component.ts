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
import { Subcategories } from '../../categories/models/subcategories.model';

@Component({
  selector: 'app-contact-add',
  imports: [CommonModule, ReactiveFormsModule, RouterLink],
  templateUrl: './contact-add.component.html',
  styleUrls: ['./contact-add.component.scss'],
})
export class ContactAddComponent {
  contactForm: FormGroup;
  categories: Categories | undefined;
  subcategories: Subcategories | undefined;

  showSubcategory: boolean = false;
  isBusinessCategory: boolean = false;
  isOtherCategory: boolean = false;

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
      category: ['', Validators.required],
      subcategory: [''],
    });
  }

  ngOnInit(): void {
    this.categoryService.getCategories().subscribe({
      next: (categories) => (this.categories = categories),
      error: (err) => console.error('Error fetching categories:', err),
    });

    //send subcategory get request on changing the category
    this.contactForm
      .get('category')
      ?.valueChanges.subscribe((selectedCategory) => {
        console.log(selectedCategory);
        this.categoryService
          .getCategorySubcategories(selectedCategory)
          .subscribe({
            next: (subcategories) => {
              this.subcategories = subcategories;
            },
            error: (err) => console.error('Error fetching subcategories:', err),
          });

        this.updateSubcategoryField(selectedCategory);
      });
  }

  updateSubcategoryField(selectedCategory: string): void {
    if (selectedCategory === 'Służbowy') {
      this.showSubcategory = true;
      this.isBusinessCategory = true;
      this.isOtherCategory = false;
      this.contactForm.get('subcategory')?.setValue('');
      this.contactForm.get('subcategory')?.setValidators([Validators.required]);
    } else if (selectedCategory === 'Inny') {
      this.showSubcategory = true;
      this.isBusinessCategory = false;
      this.isOtherCategory = true;
      this.contactForm.get('subcategory')?.setValue('');
      this.contactForm.get('subcategory')?.setValidators([Validators.required]);
    } else {
      this.showSubcategory = false;
      this.isBusinessCategory = false;
      this.isOtherCategory = false;
      this.contactForm.get('subcategory')?.setValue('prywatny');
      this.contactForm.get('subcategory')?.clearValidators();
    }

    this.contactForm.get('subcategory')?.updateValueAndValidity();
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

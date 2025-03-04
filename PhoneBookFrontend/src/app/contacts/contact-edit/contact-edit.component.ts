import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { Router, ActivatedRoute, RouterLink } from '@angular/router';
import { ContactService } from '../services/contact.service';
import { Contact } from '../models/contact.model';
import { CategoryService } from '../../categories/services/category.service';
import { Categories } from '../../categories/models/categories.model';
import { PutContact } from '../models/put-contact.model';
import { Subcategories } from '../../categories/models/subcategories.model';

@Component({
  selector: 'app-contact-edit',
  imports: [CommonModule, ReactiveFormsModule, RouterLink],
  templateUrl: './contact-edit.component.html',
  styleUrl: './contact-edit.component.scss',
})
export class ContactEditComponent {
  contactForm: FormGroup;
  contactId: string = '';
  categories: Categories | undefined;
  contact: Contact | undefined;

  subcategories: Subcategories | undefined;

  showSubcategory: boolean = false;
  isBusinessCategory: boolean = false;
  isOtherCategory: boolean = false;

  constructor(
    private route: ActivatedRoute,
    private fb: FormBuilder,
    private contactService: ContactService,
    private categoryService: CategoryService,
    private router: Router
  ) {
    this.contactForm = this.fb.group({
      name: ['', Validators.required],
      surname: ['', Validators.required],
      email: ['', Validators.required],
      password: ['', Validators.required],
      phoneNumber: ['', Validators.required],
      birthDate: ['', Validators.required],
      category: ['', Validators.required],
      subcategory: [''],
    });
  }

  ngOnInit(): void {
    this.contactId = String(this.route.snapshot.paramMap.get('id'));

    this.categoryService.getCategories().subscribe({
      next: (categories) => (this.categories = categories),
      error: (err) => console.error('Error fetching contacts:', err),
    });

    this.contactService
      .getContact(this.contactId)
      .subscribe((contact: Contact) => {
        this.contact = contact;
      });

    console.log(this.contact);

    this.contactService
      .getContact(this.contactId)
      .subscribe((contact: Contact) => {
        this.contactForm.patchValue({
          name: contact.name,
          surname: contact.surname,
          email: contact.email,
          password: '',
          birthDate: contact.birthDate,
          phoneNumber: contact.phoneNumber,
          subcategory: contact.subcategory,
        });
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
      console.log(this.contactForm.value);
      const updatedContact: Contact = this.contactForm.value;

      updatedContact.id = this.contactId;

      const putContact: PutContact = {
        name: updatedContact.name,
        surname: updatedContact.surname,
        email: updatedContact.email,
        password: updatedContact.password,
        phoneNumber: updatedContact.phoneNumber,
        birthDate: updatedContact.birthDate,
        subcategory: updatedContact.subcategory,
        category: '',
      };

      this.contactService.putContact(this.contactId, putContact).subscribe(
        () => {
          console.log('Citizen successfully updated');
          this.router.navigate(['/']);
        },
        (error) => {
          console.error('Error updating contact:', error);
          alert('Failed to update contact');
        }
      );
    }
  }
}

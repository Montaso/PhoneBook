import { Component, inject, OnInit } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';
import { ContactService } from '../services/contact.service';
import { Contacts } from '../models/contacts.model';
import { Contact } from '../models/contact.model';

@Component({
  selector: 'app-contacts-view',
  standalone: true,
  imports: [RouterLink, CommonModule],
  templateUrl: './contacts-view.component.html',
  styleUrl: './contacts-view.component.scss',
})
export class ContactsViewComponent implements OnInit {
  private service = inject(ContactService);
  contacts: Contacts | undefined;
  constructor(private router: Router) {}

  ngOnInit(): void {
    this.service.getContacts().subscribe({
      next: (contacts) => (this.contacts = contacts),
      error: (err) => console.error('Error fetching contacts:', err),
    });
  }

  onDetails(contact: Contact): void {
    this.router.navigate([`contact-details/${contact.id}`]);
  }

  onEdit(contact: Contact): void {
    this.router.navigate([`contact-edit/${contact.id}`]);
  }

  onDelete(contact: Contact): void {
    this.service.deleteContact(contact.id).subscribe(() => this.ngOnInit());
  }

  onAdd(): void {
    this.router.navigate(['contact-add/']);
  }
}

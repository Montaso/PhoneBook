import { Component } from '@angular/core';
import { Contact } from '../models/contact.model';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { ContactService } from '../services/contact.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-contact-details',
  imports: [CommonModule, RouterLink],
  templateUrl: './contact-details.component.html',
  styleUrl: './contact-details.component.scss',
})
export class ContactDetailsComponent {
  contactId: string = '';
  contact: Contact | undefined;

  constructor(
    private route: ActivatedRoute,
    private contactService: ContactService
  ) {}

  ngOnInit(): void {
    this.contactId = String(this.route.snapshot.paramMap.get('id'));

    this.contactService
      .getContact(this.contactId)
      .subscribe((contact: Contact) => {
        this.contact = contact;
      });

      
  }
}

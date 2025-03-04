import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { Contacts } from '../models/contacts.model';
import { Contact } from '../models/contact.model';
import { PutContact } from '../models/put-contact.model';

@Injectable({
  providedIn: 'root',
})
export class ContactService {
  constructor(private http: HttpClient) {}

  getContacts(): Observable<Contacts> {
    return this.http.get<Contacts>('http://localhost:5234/api/Contact/');
  }

  getContact(guid: string): Observable<Contact> {
    return this.http.get<Contact>('http://localhost:5234/api/Contact/' + guid);
  }

  putContact(guid: string, contact: PutContact): Observable<Contact> {
    return this.http.put<Contact>(
      'http://localhost:5234/api/Contact/' + guid,
      contact
    );
  }

  deleteContact(guid: string): Observable<Contact> {
    return this.http.delete<Contact>(
      'http://localhost:5234/api/Contact/' + guid
    );
  }

  postContact(contact: PutContact): Observable<Contact> {
    return this.http.post<Contact>(
      'http://localhost:5234/api/Contact',
      contact
    );
  }
}

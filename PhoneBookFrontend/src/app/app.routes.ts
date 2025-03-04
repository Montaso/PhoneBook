import { Routes } from '@angular/router';
import { ContactsViewComponent } from './contacts/contacts-view/contacts-view.component';
import { ContactDetailsComponent } from './contacts/contact-details/contact-details.component';
import { ContactEditComponent } from './contacts/contact-edit/contact-edit.component';
import { ContactAddComponent } from './contacts/contact-add/contact-add.component';
import { LoginComponent } from './auth/login/login.component';
import { RenderMode } from '@angular/ssr';

export const routes: Routes = [
  { path: '', component: ContactsViewComponent },
  {
    path: 'contact-details/:id',
    component: ContactDetailsComponent,
  },
  { path: 'contact-edit/:id', component: ContactEditComponent },
  { path: 'contact-add', component: ContactAddComponent },
  { path: 'auth', component: LoginComponent },
];

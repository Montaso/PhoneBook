import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ContactsViewComponent } from './contacts-view.component';

describe('ContactsViewComponent', () => {
  let component: ContactsViewComponent;
  let fixture: ComponentFixture<ContactsViewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ContactsViewComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ContactsViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

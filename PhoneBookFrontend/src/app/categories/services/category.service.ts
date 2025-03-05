import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Categories } from '../models/categories.model';
import { Subcategories } from '../models/subcategories.model';

@Injectable({
  providedIn: 'root',
})
export class CategoryService {
  constructor(private http: HttpClient) {}

  getCategories(): Observable<Categories> {
    return this.http.get<Categories>('http://localhost:8080/api/Category/');
  }

  getCategorySubcategories(name: string): Observable<Subcategories> {
    return this.http.get<Subcategories>(
      'http://localhost:8080/api/Category/' + name
    );
  }
}

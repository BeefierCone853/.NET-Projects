import { Injectable } from '@angular/core';
import { AddCategoryRequest } from '../models/add-category-request.model';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Category } from '../models/category.model';
import { environment } from '../../../../environments/environment.development';

@Injectable({
  providedIn: 'root',
})
export class CategoryService {
  constructor(private httpClient: HttpClient) {}
  getAllCategories(): Observable<Category[]> {
    return this.httpClient.get<Category[]>(
      `${environment.apiBaseUrl}/api/categories`
    );
  }
  addCategory(model: AddCategoryRequest): Observable<void> {
    return this.httpClient.post<void>(
      `${environment.apiBaseUrl}/api/categories`,
      model
    );
  }
}

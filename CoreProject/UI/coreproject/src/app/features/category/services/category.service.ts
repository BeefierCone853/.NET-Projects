import { Injectable } from '@angular/core';
import { AddCategoryRequest } from '../models/add-category-request.model';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Category } from '../models/category.model';
import { environment } from '../../../../environments/environment.development';
import { UpdateCategoryRequest } from '../models/update-category-request.model';
import { CookieService } from 'ngx-cookie-service';

@Injectable({
  providedIn: 'root',
})
export class CategoryService {
  constructor(
    private httpClient: HttpClient,
    private cookieService: CookieService
  ) {}
  getAllCategories(): Observable<Category[]> {
    return this.httpClient.get<Category[]>(
      `${environment.apiBaseUrl}/api/categories`
    );
  }
  addCategory(model: AddCategoryRequest): Observable<void> {
    return this.httpClient.post<void>(
      `${environment.apiBaseUrl}/api/categories?addAuth=true`,
      model,
    );
  }
  getCategoryById(id: string): Observable<Category> {
    return this.httpClient.get<Category>(
      `${environment.apiBaseUrl}/api/categories/${id}`
    );
  }
  editCategoryById(
    id: string,
    updateCategoryRequest: UpdateCategoryRequest
  ): Observable<Category> {
    return this.httpClient.put<Category>(
      `${environment.apiBaseUrl}/api/categories/${id}?addAuth=true`,
      updateCategoryRequest,
    );
  }
  deleteCategoryById(id: string): Observable<Category> {
    return this.httpClient.delete<Category>(
      `${environment.apiBaseUrl}/api/categories/${id}?addAuth=true`,
    );
  }
}

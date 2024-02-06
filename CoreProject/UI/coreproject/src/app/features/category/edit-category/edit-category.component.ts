import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { CategoryService } from '../services/category.service';
import { Category } from '../models/category.model';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { UpdateCategoryRequest } from '../models/update-category-request.model';
import { response } from 'express';

@Component({
  selector: 'app-edit-category',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './edit-category.component.html',
  styleUrl: './edit-category.component.css',
})
export class EditCategoryComponent implements OnInit, OnDestroy {
  id: string | null = null;
  paramsSubscription?: Subscription;
  getCategorySubscription?: Subscription;
  editCategorySubscription?: Subscription;
  deleteCategorySubscription?: Subscription;
  category?: Category;

  constructor(
    private route: ActivatedRoute,
    private categoryService: CategoryService,
    private router: Router
  ) {}
  ngOnInit(): void {
    this.paramsSubscription = this.route.paramMap.subscribe({
      next: (params) => {
        this.id = params.get('id');
        if (this.id) {
          this.getCategorySubscription = this.categoryService
            .getCategoryById(this.id)
            .subscribe({
              next: (response) => {
                this.category = response;
              },
            });
        }
      },
    });
  }
  ngOnDestroy(): void {
    this.paramsSubscription?.unsubscribe();
    this.getCategorySubscription?.unsubscribe();
    this.editCategorySubscription?.unsubscribe();
    this.deleteCategorySubscription?.unsubscribe();
  }
  onEditCategory(): void {
    const updateCategoryRequest: UpdateCategoryRequest = {
      name: this.category?.name ?? '',
      urlHandle: this.category?.urlHandle ?? '',
    };
    if (this.category && this.category?.id) {
      this.categoryService
        .editCategoryById(this.category.id, updateCategoryRequest)
        .subscribe({
          next: (response) => {
            this.router.navigateByUrl('/admin/categories');
          },
        });
    }
  }
  onDeleteCategory(): void {
    if (this.id) {
      this.deleteCategorySubscription = this.categoryService
        .deleteCategoryById(this.id)
        .subscribe({
          next: (response) => {
            this.router.navigateByUrl('/admin/categories');
          },
        });
    }
  }
}

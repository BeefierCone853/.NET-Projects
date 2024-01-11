import { Component, OnInit } from '@angular/core';
import { AddCategoryComponent } from '../add-category/add-category.component';
import { RouterLink, RouterLinkActive } from '@angular/router';
import { CategoryService } from '../services/category.service';
import { Category } from '../models/category.model';
import { CommonModule } from '@angular/common';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-category-list',
  standalone: true,
  imports: [CommonModule, RouterLink, RouterLinkActive, AddCategoryComponent],
  templateUrl: './category-list.component.html',
  styleUrl: './category-list.component.css',
})
export class CategoryListComponent implements OnInit {
  categories$?: Observable<Category[]>;

  constructor(private categoryService: CategoryService) {}
  ngOnInit(): void {
    this.categories$ = this.categoryService.getAllCategories();
  }
}

import { Component, OnInit } from '@angular/core';
import { AddBlogPost } from '../models/add-blog-post.model';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { BlogPostService } from '../services/blog-post.service';
import { Router } from '@angular/router';
import { MarkdownModule } from 'ngx-markdown';
import { CategoryService } from '../../category/services/category.service';
import { Category } from '../../category/models/category.model';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-add-blog-post',
  standalone: true,
  imports: [CommonModule, FormsModule, MarkdownModule],
  templateUrl: './add-blog-post.component.html',
  styleUrl: './add-blog-post.component.css',
})
export class AddBlogPostComponent implements OnInit {
  model: AddBlogPost;
  categories$?: Observable<Category[]>;

  constructor(
    private blogPostService: BlogPostService,
    private categoryService: CategoryService,
    private router: Router
  ) {
    this.model = {
      title: '',
      shortDescription: '',
      urlHandle: '',
      content: '',
      featuredImageUrl: '',
      author: '',
      isVisible: true,
      publishedDate: new Date(),
      categories: [],
    };
  }
  ngOnInit(): void {
    this.categories$ = this.categoryService.getAllCategories();
  }
  onAddBlogPost() {
    this.blogPostService.addBlogPost(this.model).subscribe({
      next: (response) => {
        this.router.navigateByUrl('/admin/blogposts');
      },
    });
  }
}

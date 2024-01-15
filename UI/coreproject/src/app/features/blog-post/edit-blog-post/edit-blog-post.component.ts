import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { Observable, Subscription } from 'rxjs';
import { BlogPostService } from '../services/blog-post.service';
import { BlogPost } from '../models/blog-post.model';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MarkdownModule } from 'ngx-markdown';
import { CategoryService } from '../../category/services/category.service';
import { Category } from '../../category/models/category.model';
import { EditBlogPost } from '../models/edit-blog-post.model';
import { response } from 'express';

@Component({
  selector: 'app-edit-blog-post',
  standalone: true,
  imports: [CommonModule, FormsModule, MarkdownModule],
  templateUrl: './edit-blog-post.component.html',
  styleUrl: './edit-blog-post.component.css',
})
export class EditBlogPostComponent implements OnInit, OnDestroy {
  id: string | null = null;
  routeSubscription?: Subscription;
  getBlogPostSubscription?: Subscription;
  editBlogPostSubscription?: Subscription;
  deleteBlogPostSubscription?: Subscription;
  blogPost?: BlogPost;
  categories$?: Observable<Category[]>;
  selectedCategories?: string[];

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private blogPostService: BlogPostService,
    private categoryService: CategoryService
  ) {}
  ngOnInit(): void {
    this.categories$ = this.categoryService.getAllCategories();
    this.routeSubscription = this.route.paramMap.subscribe({
      next: (params) => {
        this.id = params.get('id');
        if (this.id) {
          this.getBlogPostSubscription = this.blogPostService
            .getBlogPostById(this.id)
            .subscribe({
              next: (response) => {
                this.blogPost = response;
                this.selectedCategories = response.categories.map(
                  (category) => category.id
                );
              },
            });
        }
      },
    });
  }
  ngOnDestroy(): void {
    this.routeSubscription?.unsubscribe();
    this.getBlogPostSubscription?.unsubscribe();
    this.editBlogPostSubscription?.unsubscribe();
    this.deleteBlogPostSubscription?.unsubscribe();
  }
  onEditBlogPost() {
    if (this.blogPost && this.id) {
      var updateBlogPost: EditBlogPost = {
        author: this.blogPost.author,
        content: this.blogPost.content,
        shortDescription: this.blogPost.shortDescription,
        featuredImageUrl: this.blogPost.featuredImageUrl,
        isVisible: this.blogPost.isVisible,
        publishedDate: this.blogPost.publishedDate,
        title: this.blogPost.title,
        urlHandle: this.blogPost.urlHandle,
        categories: this.selectedCategories ?? [],
      };
      this.editBlogPostSubscription = this.blogPostService
        .editBlogPostById(this.id, updateBlogPost)
        .subscribe({
          next: (response) => {
            this.router.navigateByUrl('/admin/blogposts');
          },
        });
    }
  }
  onDeleteBlogPost() {
    if (this.id && this.blogPost) {
      this.deleteBlogPostSubscription = this.blogPostService
        .deleteBlogPostById(this.id)
        .subscribe({
          next: (response) => {
            this.router.navigateByUrl('/admin/blogposts');
          },
        });
    }
  }
}

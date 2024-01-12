import { Component, OnInit } from '@angular/core';
import { RouterLink } from '@angular/router';
import { Observable } from 'rxjs';
import { BlogPost } from '../models/blog-post.model';
import { BlogPostService } from '../services/blog-post.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-blog-post-list',
  standalone: true,
  imports: [RouterLink, CommonModule],
  templateUrl: './blog-post-list.component.html',
  styleUrl: './blog-post-list.component.css',
})
export class BlogPostListComponent implements OnInit {
  blogPosts$?: Observable<BlogPost[]>;
  constructor(private blogPostService: BlogPostService) {}
  ngOnInit(): void {
    this.blogPosts$ = this.blogPostService.getAllBlogPosts();
  }
}

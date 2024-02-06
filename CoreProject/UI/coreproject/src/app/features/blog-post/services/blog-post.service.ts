import { Injectable } from '@angular/core';
import { AddBlogPost } from '../models/add-blog-post.model';
import { Observable } from 'rxjs';
import { BlogPost } from '../models/blog-post.model';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../../environments/environment.development';
import { Category } from '../../category/models/category.model';
import { EditBlogPost } from '../models/edit-blog-post.model';

@Injectable({
  providedIn: 'root',
})
export class BlogPostService {
  constructor(private httpClient: HttpClient) {}
  getAllBlogPosts(): Observable<BlogPost[]> {
    return this.httpClient.get<BlogPost[]>(
      `${environment.apiBaseUrl}/api/blogpost`
    );
  }
  addBlogPost(data: AddBlogPost): Observable<BlogPost> {
    return this.httpClient.post<BlogPost>(
      `${environment.apiBaseUrl}/api/blogpost?addAuth=true`,
      data
    );
  }
  getBlogPostById(id: string): Observable<BlogPost> {
    return this.httpClient.get<BlogPost>(
      `${environment.apiBaseUrl}/api/blogpost/${id}`
    );
  }
  getBlogPostByUrlHandle(urlHandle: string): Observable<BlogPost> {
    return this.httpClient.get<BlogPost>(
      `${environment.apiBaseUrl}/api/blogpost/${urlHandle}`
    );
  }
  editBlogPostById(
    id: string,
    updatedBlogPost: EditBlogPost
  ): Observable<BlogPost> {
    return this.httpClient.put<BlogPost>(
      `${environment.apiBaseUrl}/api/blogpost/${id}?addAuth=true`,
      updatedBlogPost
    );
  }
  deleteBlogPostById(id: string): Observable<BlogPost> {
    return this.httpClient.delete<BlogPost>(
      `${environment.apiBaseUrl}/api/blogpost/${id}?addAuth=true`
    );
  }
}

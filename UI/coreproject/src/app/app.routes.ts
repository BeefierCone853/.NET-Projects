import { Routes } from '@angular/router';
import { CategoryListComponent } from './features/category/category-list/category-list.component';
import { AddCategoryComponent } from './features/category/add-category/add-category.component';
import { EditCategoryComponent } from './features/category/edit-category/edit-category.component';
import { BlogPostListComponent } from './features/blog-post/blog-post-list/blog-post-list.component';
import { AddBlogPostComponent } from './features/blog-post/add-blog-post/add-blog-post.component';
import { EditBlogPostComponent } from './features/blog-post/edit-blog-post/edit-blog-post.component';
import { HomeComponent } from './public/home/home.component';
import { BlogDetailsComponent } from './public/blog-details/blog-details.component';

export const routes: Routes = [
  {
    path: '',
    title: 'Home',
    component: HomeComponent,
  },
  {
    path: 'blog/:url',
    title: 'Blog Details',
    component: BlogDetailsComponent,
  },
  {
    path: 'admin/categories',
    title: 'Categories',
    component: CategoryListComponent,
  },
  {
    path: 'admin/categories/add',
    title: 'Add Category',
    component: AddCategoryComponent,
  },
  {
    path: 'admin/categories/:id',
    title: 'Edit Category',
    component: EditCategoryComponent,
  },
  {
    path: 'admin/blogposts',
    title: 'Blog Posts',
    component: BlogPostListComponent,
  },
  {
    path: 'admin/blogposts/add',
    title: 'Add Blog Post',
    component: AddBlogPostComponent,
  },
  {
    path: 'admin/blogposts/:id',
    title: 'Edit Blog Post',
    component: EditBlogPostComponent,
  },
];

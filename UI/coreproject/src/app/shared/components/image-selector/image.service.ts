import { Injectable, OnInit } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { BlogImage } from '../../models/blog-image.model';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../../environments/environment.development';

@Injectable({
  providedIn: 'root',
})
export class ImageService {
  selectedImage: BehaviorSubject<BlogImage> = new BehaviorSubject<BlogImage>({
    id: '',
    fileExtension: '',
    fileName: '',
    title: '',
    url: '',
  });
  constructor(private httpClient: HttpClient) {}
  getAllImages(): Observable<BlogImage[]> {
    return this.httpClient.get<BlogImage[]>(
      `${environment.apiBaseUrl}/api/images`
    );
  }
  uploadImage(
    file: File,
    fileName: string,
    title: string
  ): Observable<BlogImage> {
    const formData = new FormData();
    formData.append('file', file);
    formData.append('fileName', fileName);
    formData.append('title', title);
    return this.httpClient.post<BlogImage>(
      `${environment.apiBaseUrl}/api/images`,
      formData
    );
  }
  selectImage(image: BlogImage): void {
    this.selectedImage.next(image)
  }
  onSelectImage(): Observable<BlogImage> {
    return this.selectedImage.asObservable()
  }
}

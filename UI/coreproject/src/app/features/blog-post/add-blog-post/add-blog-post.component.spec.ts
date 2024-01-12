import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddBlogPostComponent } from './add-blog-post.component';

describe('AddBlogPostComponent', () => {
  let component: AddBlogPostComponent;
  let fixture: ComponentFixture<AddBlogPostComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AddBlogPostComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AddBlogPostComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

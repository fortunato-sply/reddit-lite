import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ImgForumUploaderComponent } from './img-forum-uploader.component';

describe('ImgForumUploaderComponent', () => {
  let component: ImgForumUploaderComponent;
  let fixture: ComponentFixture<ImgForumUploaderComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ImgForumUploaderComponent]
    });
    fixture = TestBed.createComponent(ImgForumUploaderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

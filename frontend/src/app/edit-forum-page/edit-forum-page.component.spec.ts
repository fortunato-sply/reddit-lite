import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditForumPageComponent } from './edit-forum-page.component';

describe('EditForumPageComponent', () => {
  let component: EditForumPageComponent;
  let fixture: ComponentFixture<EditForumPageComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [EditForumPageComponent]
    });
    fixture = TestBed.createComponent(EditForumPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

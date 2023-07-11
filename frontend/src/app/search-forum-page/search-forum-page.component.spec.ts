import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SearchForumPageComponent } from './search-forum-page.component';

describe('SearchForumPageComponent', () => {
  let component: SearchForumPageComponent;
  let fixture: ComponentFixture<SearchForumPageComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [SearchForumPageComponent]
    });
    fixture = TestBed.createComponent(SearchForumPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

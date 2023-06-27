import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FavForumComponent } from './fav-forum.component';

describe('FavForumComponent', () => {
  let component: FavForumComponent;
  let fixture: ComponentFixture<FavForumComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [FavForumComponent]
    });
    fixture = TestBed.createComponent(FavForumComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

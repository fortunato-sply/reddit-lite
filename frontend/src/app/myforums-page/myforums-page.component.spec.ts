import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MyforumsPageComponent } from './myforums-page.component';

describe('MyforumsPageComponent', () => {
  let component: MyforumsPageComponent;
  let fixture: ComponentFixture<MyforumsPageComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [MyforumsPageComponent]
    });
    fixture = TestBed.createComponent(MyforumsPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginPageComponent } from './login-page/login-page.component';
import { NewAccountPageComponent } from './new-account-page/new-account-page.component';
import { PasswordComponent } from './password/password.component';
import { FormsModule } from '@angular/forms';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { FeedPageComponent } from './feed-page/feed-page.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {MatButtonModule} from '@angular/material/button';
import {MatDatepickerModule} from '@angular/material/datepicker';
import {MatInputModule} from '@angular/material/input';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatNativeDateModule} from '@angular/material/core';
import { HeaderComponent } from './header/header.component';
import { CommentComponent } from './comment/comment.component';
import { PostComponent } from './post/post.component';
import { MyforumsPageComponent } from './myforums-page/myforums-page.component';
import { FavoritesPageComponent } from './favorites-page/favorites-page.component';
import { ForumCardComponent } from './forum-card/forum-card.component';
import { SearchBoxComponent } from './search-box/search-box.component';
import { FavForumComponent } from './fav-forum/fav-forum.component';
import { EditForumPageComponent } from './edit-forum-page/edit-forum-page.component';
import { ModalComponent } from './modal/modal.component';
import { ModalModule } from './modal/modal.module';
import { ForumPageComponent } from './forum-page/forum-page.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginPageComponent,
    NewAccountPageComponent,
    PasswordComponent,
    FeedPageComponent,
    HeaderComponent,
    CommentComponent,
    PostComponent,
    MyforumsPageComponent,
    FavoritesPageComponent,
    ForumCardComponent,
    SearchBoxComponent,
    FavForumComponent,
    EditForumPageComponent,
    ForumPageComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    FontAwesomeModule,
    BrowserAnimationsModule,
    MatFormFieldModule,
    MatInputModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatButtonModule,
    ModalModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})

export class AppModule { }

import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginPageComponent } from './login-page/login-page.component';
import { NewAccountPageComponent } from './new-account-page/new-account-page.component';
import { FeedPageComponent } from './feed-page/feed-page.component';
import { FavoritesPageComponent } from './favorites-page/favorites-page.component';
import { MyforumsPageComponent } from './myforums-page/myforums-page.component';
import { EditForumPageComponent } from './edit-forum-page/edit-forum-page.component';
import { ForumPageComponent } from './forum-page/forum-page.component';
import { SearchForumPageComponent } from './search-forum-page/search-forum-page.component';

const routes: Routes = [
  {
    path: "login",
    title: "Login",
    component: LoginPageComponent
  },
  {
    path: "newaccount",
    title: "Nova Conta",
    component: NewAccountPageComponent
  },
  {
    path: "feed",
    title: "Feed",
    component: FeedPageComponent
  },
  {
    path: "favorites",
    title: "Favoritos",
    component: FavoritesPageComponent
  },
  {
    path: "myforums",
    title: "Meus Fóruns",
    component: MyforumsPageComponent
  },
  {
    path: "forum/:id",
    title: "Reddit Lite",
    component: ForumPageComponent
  },
  {
    path: "search/:name",
    title: "Procurar fórum",
    component: SearchForumPageComponent
  }
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

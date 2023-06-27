import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginPageComponent } from './login-page/login-page.component';
import { NewAccountPageComponent } from './new-account-page/new-account-page.component';
import { FeedPageComponent } from './feed-page/feed-page.component';
import { FavoritesPageComponent } from './favorites-page/favorites-page.component';
import { MyforumsPageComponent } from './myforums-page/myforums-page.component';
import { EditForumPageComponent } from './edit-forum-page/edit-forum-page.component';

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
    path: "editforum",
    title: "Editar fórum",
    component: EditForumPageComponent
  }
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

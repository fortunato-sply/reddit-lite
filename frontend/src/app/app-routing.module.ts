import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginPageComponent } from './login-page/login-page.component';
import { NewAccountPageComponent } from './new-account-page/new-account-page.component';

const routes: Routes = [
  { 
    path: "login", 
    title: "Login", 
    component: LoginPageComponent, 
    children: [
      { path: "newaccount", component: NewAccountPageComponent }
    ]},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

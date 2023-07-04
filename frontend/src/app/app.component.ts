import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'Reddit Lite';

  isLoggedIn: boolean = false;
  
  constructor(private router: Router) { }
  
  verifyLogin() {
    var token = sessionStorage.getItem('jwt');
    
    if(token == '' && location.pathname != '/newaccount') {
      console.log('token é nulo')
      this.isLoggedIn = false;
      this.router.navigate(['/login']);
      return;
    }
    
    //this.router.url == '/' ? this.router.navigate(['/feed']) : '';
    console.log(token);
    this.isLoggedIn = true;
  }

  ngOnInit() {
    this.verifyLogin();
    console.log('logado: ' + this.isLoggedIn)
  }
}

import { Component } from '@angular/core';
import { Router } from '@angular/router';


@Component({
  selector: 'app-new-account-page',
  templateUrl: './new-account-page.component.html',
  styleUrls: ['./new-account-page.component.css']
})

export class NewAccountPageComponent {
  email = "";
  user = "";
  password = "";
  born = ""

  constructor(private router: Router) { }

  passwordChanged(event: any) {
    this.password = event;
  }

  createAccount() {
    if (this.email == "email@email.com" && this.password == "123") {
      // Isso evidentemente não é seguro, mas a ideia é bom e será melhorada no futuro
      sessionStorage.setItem('user', 'pamella');
      this.router.navigate(["/login"])
    }
    this.router.navigate(["/login"])
  }
}

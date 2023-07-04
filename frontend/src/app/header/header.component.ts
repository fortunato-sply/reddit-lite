import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { faRightFromBracket } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent {
  icon = faRightFromBracket;

  constructor(private router: Router) { }

  logoff() {
    sessionStorage.setItem('jwt', '');
    this.router.navigate(['/login']);
  }
}

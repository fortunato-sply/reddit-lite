import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { faCircleRight } from '@fortawesome/free-solid-svg-icons';
@Component({
  selector: 'app-search-box',
  templateUrl: './search-box.component.html',
  styleUrls: ['./search-box.component.css']
})
export class SearchBoxComponent {
  constructor(private router: Router) { }

  circleRight = faCircleRight;
  value: string = "";

  search() {
    this.router.navigateByUrl(`search/${this.value}`);
  }
}

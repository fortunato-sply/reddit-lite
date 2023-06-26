import { Component } from '@angular/core';
import { faCircleRight } from '@fortawesome/free-solid-svg-icons';
@Component({
  selector: 'app-search-box',
  templateUrl: './search-box.component.html',
  styleUrls: ['./search-box.component.css']
})
export class SearchBoxComponent {
  value=""
  circleRight = faCircleRight;
}

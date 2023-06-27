import { Component } from '@angular/core';
import { faArrowUp, faArrowDown, faStar } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-feed-page',
  templateUrl: './feed-page.component.html',
  styleUrls: ['./feed-page.component.css']
})
export class FeedPageComponent {
  arrowUp = faArrowUp;
  arrowDown = faArrowDown;
  star = faStar;

  Likes = 0;
  newComment = "";

  changeLikes() {

  }

  changeComment() {

  }

  comment() {
    
  }
}

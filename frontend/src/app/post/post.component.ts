import { Component } from '@angular/core';
import { faArrowUp, faArrowDown } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-post',
  templateUrl: './post.component.html',
  styleUrls: ['./post.component.css']
})
export class PostComponent {
  arrowUp = faArrowUp;
  arrowDown = faArrowDown;

  Likes = 0;
  newComment = "";

  changeLikes() {

  }

  changeComment() {

  }

  comment() {
    
  }
}

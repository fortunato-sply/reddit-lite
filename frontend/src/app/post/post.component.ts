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

  showComments: boolean = false;

  Likes = 0;
  newComment = "";

  toggleComments() {
    this.showComments = !this.showComments;
    if (this.showComments) {
      setTimeout(() => {
        const commentContainer = document.querySelector('.comment-container');
        console.log(commentContainer)
        
        if (commentContainer != null)
          commentContainer.classList.add('show');
      }, 10);
    } else {
      const commentContainer = document.querySelector('.comment-container');

      if (commentContainer != null)
        commentContainer.classList.remove('show');
  }
  }

  changeLikes() {

  }

  changeComment() {

  }

  comment() {
    
  }
}

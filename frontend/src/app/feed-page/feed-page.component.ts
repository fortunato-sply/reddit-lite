import { Component, Output } from '@angular/core';
import { Router } from '@angular/router';
import { faArrowUp, faArrowDown, faStar } from '@fortawesome/free-solid-svg-icons';
import { JWT, UserToken } from 'src/services/DTO/user';
import { UserService } from 'src/services/Http/user.service';

@Component({
  selector: 'app-feed-page',
  templateUrl: './feed-page.component.html',
  styleUrls: ['./feed-page.component.css']
})
export class FeedPageComponent {
  constructor(private router: Router, private service: UserService) { }
  
  jwt: JWT = { value: '' };

  @Output() user: UserToken | null = null;
  image: string = "http://localhost:5241/img/";

  ngOnInit() {
    this.jwt.value = sessionStorage.getItem('jwt');
    console.log("jwt no feed: ", this.jwt.value);

    this.service.validate(this.jwt).subscribe((res: UserToken) => {
      console.log(res);
      this.user = res;
      this.image += res.photoID;
    })


  }

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

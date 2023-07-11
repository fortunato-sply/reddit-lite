import { ChangeDetectorRef, Component, Output } from '@angular/core';
import { Router } from '@angular/router';
import { faArrowUp, faArrowDown, faStar } from '@fortawesome/free-solid-svg-icons';
import { ForumDTO } from 'src/services/DTO/Forum';
import { CompletePost } from 'src/services/DTO/Post';
import { JWT, UserToken } from 'src/services/DTO/user';
import { ForumService } from 'src/services/Http/forum.service';
import { PostService } from 'src/services/Http/post.service';
import { UserService } from 'src/services/Http/user.service';

@Component({
  selector: 'app-feed-page',
  templateUrl: './feed-page.component.html',
  styleUrls: ['./feed-page.component.css']
})
export class FeedPageComponent {
  constructor(
    private router: Router, 
    private userService: UserService,
    private forumService: ForumService,
    private postService: PostService,
    private changeDetection: ChangeDetectorRef
  ) { }

  jwt: JWT = { value: '' };

  @Output() user: UserToken | null = null;
  image: string = "http://localhost:5241/img/";

  favorites: ForumDTO[] = [];
  emptyFavorites: boolean = true;
  
  posts: CompletePost[] = [];

  verifyFavorites() {
    this.emptyFavorites = this.favorites.length < 1;
  }

  ngOnInit() {
    this.postService.getFeedPosts()
      .subscribe((res: CompletePost[]) => {
        this.posts = res;
        this.changeDetection.detectChanges();
      })

    this.jwt.value = sessionStorage.getItem('jwt');
    console.log("jwt no feed: ", this.jwt.value);

    this.userService.validate(this.jwt).subscribe((res: UserToken) => {
      console.log(res);
      this.user = res;
      this.image += res.photoID;

      this.forumService.getUserFavoriteForums(res.id)
        .subscribe((res: ForumDTO[]) => {
          this.favorites = res;
          this.verifyFavorites();
        })

      
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

import { Component, Output } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ForumService } from 'src/services/Http/forum.service';
import { ForumDTO } from 'src/services/DTO/Forum';
import { 
  faCalendarAlt, 
  faUser, 
  faTrash, 
  faPencil,
  faHeart,
  faHeartBroken
} from '@fortawesome/free-solid-svg-icons';
import { UserService } from 'src/services/Http/user.service';
import { JWT, UserToken } from 'src/services/DTO/user';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-forum-page',
  templateUrl: './forum-page.component.html',
  styleUrls: ['./forum-page.component.css']
})
export class ForumPageComponent {
  calendar = faCalendarAlt;
  user = faUser;
  trash = faTrash;
  pencil = faPencil;
  heart = faHeart;
  heartbroken = faHeartBroken;

  formattedDate: string = '';

  id: number = 0;
  @Output() forum: ForumDTO = {
    id: '0',
    createdAt: new Date,
    title: '',
    description: '',
    owner: '',
    photo: ''
  };

  isUserFollowing: boolean = false;

  jwt: JWT = {
    value: ''
  }
  userToken: UserToken = {
    id: 0,
    authenticated: false,
    born: new Date,
    email: '',
    photoID: 0,
    username: ''
  };
  isOwner: boolean = false;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private forumService: ForumService, 
    private userService: UserService
  ) { }

  deleteForum() {
    this.forumService.deleteForum(this.forum.id, this.jwt)
      .subscribe({
        next: (res) => {
          this.router.navigate(['/myforums']);
        },
        error: (res: HttpErrorResponse) => {
          window.alert("erro: " + res.message);
        }
      })
  }

  startFollowingForum() {
    this.forumService.startFollowingForum(this.forum.id, this.jwt)
      .subscribe({
        next: (res) => {
          console.log(res);
        }
      });
    
    window.location.reload();
  }

  stopFollowingForum() {
    this.forumService.stopFollowingForum(this.forum.id, this.jwt)
      .subscribe((res) => {
        this.router.navigate([`/forum/${this.forum.id}`])
      });

    window.location.reload();
  }

  ngOnInit() {
    var token = sessionStorage.getItem('jwt');
    if (token != null)
      this.jwt.value = token;

    this.route.params.subscribe(params => {
      this.id = +params['id'];

      this.forumService.getForumById(this.id)
        .subscribe((res: ForumDTO) => {
          this.forum = res;

          const date = new Date(res.createdAt);
          const formattedDate = `${date.getDate().toString().padStart(2, '0')}/${(date.getMonth() + 1).toString().padStart(2, '0')}/${date.getFullYear()} às ${date.getHours().toString().padStart(2, '0')}:${date.getMinutes().toString().padStart(2, '0')}`;
          this.formattedDate = formattedDate;

          this.userService.validate(this.jwt)
            .subscribe((res: UserToken) => {
              this.userToken = res;
              this.isOwner = res.username == this.forum.owner;

              this.forumService.checkIfUserIsFollowingForum(this.forum.id, this.jwt)
                .subscribe((res: boolean) => {
                  console.log(res);
                  this.isUserFollowing = res;
                });
            });
        });
    });
  }

}

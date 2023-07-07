import { Component, Output } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ForumService } from 'src/services/Http/forum.service';
import { ForumDTO, UpdateForumDTO } from 'src/services/DTO/Forum';
import { CompletePost, PostDTO } from 'src/services/DTO/Post';
import { 
  faCalendarAlt, 
  faUser, 
  faTrash, 
  faPencil,
  faHeart,
  faHeartBroken,
  faStar
} from '@fortawesome/free-solid-svg-icons';
import { UserService } from 'src/services/Http/user.service';
import { JWT, UserMemberDTO, UserToken } from 'src/services/DTO/user';
import { HttpErrorResponse } from '@angular/common/http';
import { ImageService } from 'src/services/Http/image.service';
import { PostService } from 'src/services/Http/post.service';

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
  star = faStar;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private forumService: ForumService, 
    private userService: UserService,
    private imageService: ImageService,
    private postService: PostService
  ) { }

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
  isFavorite: boolean = false;

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

  imgForm?: FormData;

  members: UserMemberDTO[] = [];
  haveMembers: boolean = false;

  postContent: string = '';

  sendPost() {
    var post: PostDTO = {
      content: this.postContent,
      forumId: this.forum.id,
      userId: this.userToken.id
    }

    console.log("id passado: " + post.userId);
    console.log("forum id passado: " + post.forumId);

    this.postService.sendPost(post)
      .subscribe((res) => { })

    window.location.reload();
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

  favoriteForum() {
    this.forumService.favoriteForum(this.forum.id, this.jwt)
      .subscribe({ next: (res) => { console.log(res) } });

    window.location.reload();
  }

  unfavoriteForum() {
    this.forumService.unfavoriteForum(this.forum.id, this.jwt)
      .subscribe({ next: (res) => { } });

    window.location.reload();
  }

  handleFileUpload(value: FormData) {
    this.imgForm = value;
  }

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

  attTitle: string = '';
  attDescription: string = '';
  updateForum() {
    const data: UpdateForumDTO = {
      title: this.attTitle != '' ? this.attTitle : this.forum.title,
      description: this.attDescription != '' ? this.attDescription : this.forum.description
    }

    this.forumService.updateForum(this.forum.id, data)
      .subscribe(() => {
        if(this.imgForm) {
          this.imageService
            .updateForumImage(this.imgForm, this.forum.id)
            .subscribe((res) => {
              console.log("sucesso");
            })
        }

        window.location.reload();
      })
  }

  // posts
  posts: CompletePost[] = [];
  getPosts() {
    this.postService.getForumPosts(this.forum.id)
      .subscribe((res: CompletePost[]) => {
        this.posts = res;
      })
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
          
          this.getPosts();

          this.userService.validate(this.jwt)
            .subscribe((res: UserToken) => {
              this.userToken = res;
              this.isOwner = res.username == this.forum.owner;

              this.forumService.checkIfUserIsFollowingForum(this.forum.id, this.jwt)
                .subscribe((res: boolean) => {
                  console.log(res);
                  this.isUserFollowing = res;
                });
              
              this.forumService.checkIfIsForumFavorite(this.forum.id, this.jwt)
                .subscribe((res: boolean) => {
                  console.log(res);
                  this.isFavorite = res;
                });

              this.forumService.getMembers(this.forum.id)
                .subscribe((res: UserMemberDTO[]) => {
                  this.members = res;
                });
            });
        });
    });
  }

}

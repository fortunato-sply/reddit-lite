import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { faPlus, faPlusCircle } from '@fortawesome/free-solid-svg-icons';
import { ImageService } from 'src/services/Http/image.service';
import { CreateForumDTO, ForumDTO } from 'src/services/DTO/Forum';
import { JWT, UserToken } from 'src/services/DTO/user';
import { ForumService } from 'src/services/Http/forum.service';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-myforums-page',
  templateUrl: './myforums-page.component.html',
  styleUrls: ['./myforums-page.component.css']
})
export class MyforumsPageComponent {
  plus = faPlusCircle

  title: string = '';
  description: string = '';
  imgForm?: FormData;
  jwt: string = sessionStorage.getItem('jwt') ?? '';

  myforums: ForumDTO[] = [];
  isEmpty: boolean = this.myforums.length == 0;

  constructor(
    private router: Router,
    private imageService: ImageService,
    private forumService: ForumService
    ) { }

  handleFileUpload(value: FormData) {
    this.imgForm = value;
  }

  verifyIsEmpty() {
    this.isEmpty = this.myforums.length == 0;
  }

  ngOnInit() {
    this.forumService.getUserForums(this.jwt)
      .subscribe(list => {
        var forums: ForumDTO[] = []
        list.forEach(forum => {
          forums.push({
            id: forum.id,
            title: forum.title,
            description: forum.description,
            createdAt: forum.createdAt,
            owner: forum.owner,
            photo: forum.photo
          })
        });

        this.myforums = forums;
        this.verifyIsEmpty();
      });
  }

  async createForum() {
    const data: CreateForumDTO = {
      jwtToken: this.jwt,
      title: this.title,
      description: this.description
    }

    this.forumService.createForum(data)
      .subscribe((res) => {
        var id = res.body?.toString();
        if(this.imgForm) {
          this.imageService
            .updateForumImage(this.imgForm, id)
            .subscribe((res) => { 
              this.router.navigate(['/myforums']);
             });
        }
      })
  }
}

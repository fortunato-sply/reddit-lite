import { Component, Output } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ForumService } from 'src/services/Http/forum.service';
import { ForumDTO } from 'src/services/DTO/Forum';

@Component({
  selector: 'app-forum-page',
  templateUrl: './forum-page.component.html',
  styleUrls: ['./forum-page.component.css']
})
export class ForumPageComponent {
  id: number = 0;
  @Output() forum: ForumDTO = {
    id: '0',
    createdAt: new Date,
    title: '',
    description: '',
    owner: '',
    photo: ''
  };

  constructor(
    private route: ActivatedRoute,
    private forumService: ForumService
  ) { }

  ngOnInit() {
    this.route.params.subscribe(params => {
      this.id = +params['id'];

      this.forumService.getForumById(this.id)
        .subscribe((res: ForumDTO) => {
          this.forum = res;
        })
    })
  }

}

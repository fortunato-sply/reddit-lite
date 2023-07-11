import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ForumDTO } from 'src/services/DTO/Forum';
import { ForumService } from 'src/services/Http/forum.service';

@Component({
  selector: 'app-search-forum-page',
  templateUrl: './search-forum-page.component.html',
  styleUrls: ['./search-forum-page.component.css']
})
export class SearchForumPageComponent {
  constructor(
    private route: ActivatedRoute, 
    private forumService: ForumService,
    private router: Router
  ) { }

  search: string = '';

  forums: ForumDTO[] = []
  forumsIsEmpty: boolean = true;

  verifyForumsIsEmpty () {
    this.forumsIsEmpty = this.forums.length < 1;
  }

  ngOnInit() {
    this.route.params.subscribe(params => {
      this.search = params['name'];
      
      this.forumService.searchForum(this.search)
        .subscribe((res: ForumDTO[]) => {
          this.forums = res;
          this.verifyForumsIsEmpty();
        })
    })
  }
}
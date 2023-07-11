import { Component, Output } from '@angular/core';
import { ForumDTO } from 'src/services/DTO/Forum';
import { JWT, UserToken } from 'src/services/DTO/user';
import { ForumService } from 'src/services/Http/forum.service';
import { UserService } from 'src/services/Http/user.service';

@Component({
  selector: 'app-favorites-page',
  templateUrl: './favorites-page.component.html',
  styleUrls: ['./favorites-page.component.css']
})
export class FavoritesPageComponent {
  constructor(private userService: UserService, private forumService: ForumService) { }

  forums: ForumDTO[] = [];
  emptyForums: boolean = true;

  @Output() user: UserToken | null = null;
  jwt: JWT = { value: '' };
  
  verifyForums() {
    this.emptyForums = this.forums.length < 1;
  }

  ngOnInit() {
    this.jwt.value = sessionStorage.getItem('jwt');

    this.userService.validate(this.jwt).subscribe((res: UserToken) => {
      console.log(res);
      this.user = res;

      this.forumService.getForums()
        .subscribe((res: ForumDTO[]) => {
          this.forums = res;
        })
    })
  }
}

import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-fav-forum',
  templateUrl: './fav-forum.component.html',
  styleUrls: ['./fav-forum.component.css']
})
export class FavForumComponent {
  @Input() id: string = '';
  @Input() title: string = '';
  @Input() photo: string = '';
}

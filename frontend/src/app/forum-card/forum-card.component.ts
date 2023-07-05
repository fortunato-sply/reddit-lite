import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-forum-card',
  templateUrl: './forum-card.component.html',
  styleUrls: ['./forum-card.component.css']
})
export class ForumCardComponent {
  @Input() id: string = '0';
  @Input() title: string = '';
  @Input() image: string = '';
}

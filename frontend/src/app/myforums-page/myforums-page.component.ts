import { Component } from '@angular/core';
import { faPlus, faPlusCircle } from '@fortawesome/free-solid-svg-icons';
import { ModalComponent } from '../modal/modal.component';

@Component({
  selector: 'app-myforums-page',
  templateUrl: './myforums-page.component.html',
  styleUrls: ['./myforums-page.component.css']
})
export class MyforumsPageComponent {
  plus = faPlusCircle
  
}

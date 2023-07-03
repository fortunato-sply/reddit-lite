import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { faPlus, faPlusCircle } from '@fortawesome/free-solid-svg-icons';
import { ImageService } from 'src/services/Http/image.service';

@Component({
  selector: 'app-myforums-page',
  templateUrl: './myforums-page.component.html',
  styleUrls: ['./myforums-page.component.css']
})
export class MyforumsPageComponent {
  plus = faPlusCircle
  imgForm?: FormData;

  constructor(
    private router: Router,
    private imageService: ImageService
    ) { }

  handleFileUpload(value: FormData) {
    this.imgForm = value;
  }

  //TODO
  //enviar a foto pelo imgForm, receber no back igual a de user
}

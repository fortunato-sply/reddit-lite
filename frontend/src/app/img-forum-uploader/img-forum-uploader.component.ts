import { Component, EventEmitter, OnInit, Output, Input } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-forum-uploader',
  templateUrl: './img-forum-uploader.component.html',
  styleUrls: ['./img-forum-uploader.component.css'],
})
export class ImgForumUploaderComponent implements OnInit {
  @Output() public onUploadFinished = new EventEmitter<any>();
  @Input() public value: FormData = new FormData();

  route = new Router();
  ngOnInit(): void { }

  imgUrl: string = '';

  uploadFile = (files: any) => {
    if (files.length == 0) {
      return;
    }

    let fileToUpload = <File>files[0];

    this.value = new FormData();
    this.value.append('file', fileToUpload, fileToUpload.name);
    this.imgUrl = URL.createObjectURL(fileToUpload);
    
    console.log(this.value)

    this.onUploadFinished.emit(this.value);
  };

  getImgSrc() {
    if (this.imgUrl !== '') return this.imgUrl;

    return '../assets/plus.png';
  }
}
import { HttpClient } from "@angular/common/http";
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ImageService {
  constructor(private http: HttpClient) { }

  updateUserImage(form: FormData, jwt: string) {
    form.append('jwt', jwt);
    console.log(form.getAll);
    return this.http.post(
      'http://localhost:5241/img/user/update',
      form,
      { observe: 'response' }
    );
  }

  updateForumImage(form: FormData, code: string | undefined) {
    var id = '';
    if(code != undefined) {
      id = code
    }
    
    form.append('id', id);
    console.log(form.getAll);
    return this.http.post(
      'http://localhost:5241/img/forum/update',
      form,
      { observe: 'response' }
    );
  }

  addImage(form: FormData) {
    console.log(form)

    return this.http.post(
      'http://localhost:5241/img',
      form,
      { observe: 'response' }
    )
  }
}
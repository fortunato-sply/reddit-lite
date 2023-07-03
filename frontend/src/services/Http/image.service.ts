import { HttpClient } from "@angular/common/http";
import { Injectable } from '@angular/core';
import { JWT, LoginDTO, SignUpDTO, UserToken } from "./user";

@Injectable({
  providedIn: 'root'
})
export class ImageService {
  constructor(private http: HttpClient) { }

  updateUserImage(form: FormData) {
    let jwt = sessionStorage.getItem('jwt') ?? '';

    form.append('jwt', jwt)
    console.log(form.getAll);
    return this.http.post(
      'http://localhost:5241/img/user/update',
      form,
      { observe: 'response' }
    );
  }
}
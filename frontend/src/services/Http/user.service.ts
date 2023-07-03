import { HttpClient } from "@angular/common/http";
import { Injectable } from '@angular/core';
import { JWT, LoginDTO, SignUpDTO, UserToken } from "./user";

@Injectable({
  providedIn: 'root'
})
export class UserService {
  constructor(private http: HttpClient) { }

  login(data: LoginDTO) {
    return this.http.post<JWT>('http://localhost:5241/user/signin', data);
  }

  signUp(data: SignUpDTO) {
    return this.http.post<JWT>('http://localhost:5241/user/signup', data);
  }

  validate(jwt: JWT) {
    return this.http.post<UserToken>('http://localhost:5241/user/validate', jwt);
  }
}
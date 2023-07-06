import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from '@angular/core';
import { CreateForumDTO, ForumDTO } from "../DTO/Forum";
import { JWT } from "../DTO/user";

@Injectable({
  providedIn: 'root'
})
export class ForumService {
  constructor(private http: HttpClient) { }

  createForum(data: CreateForumDTO) {
    return this.http.post('http://localhost:5241/forum/create', data, { observe: 'response' });
  }

  getUserForums(jwt: string) {
    const headers = new HttpHeaders().set('jwt', jwt);
    return this.http.get<ForumDTO[]>('http://localhost:5241/forum/myforums', { headers });
  }

  getForumById(id: number) {
    return this.http.get<ForumDTO>('http://localhost:5241/forum/' + id)
  }

  deleteForum(id: string, jwt: JWT) {
    return this.http.post(`http://localhost:5241/forum/del/${id}`, jwt);
  }

  checkIfUserIsFollowingForum(id: string, jwt: JWT) {
    return this.http.post<boolean>(`http://localhost:5241/forum/checkfollow/${id}`, jwt);
  }

  startFollowingForum(id: string, jwt: JWT) {
    return this.http.post(`http://localhost:5241/forum/startfollow/${id}`, jwt);
  }

  stopFollowingForum(id: string, jwt: JWT) {
    return this.http.post(`http://localhost:5241/forum/stopfollow/${id}`, jwt);
  }
}

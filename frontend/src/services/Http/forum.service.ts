import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from '@angular/core';
import { CreateForumDTO, ForumDTO } from "../DTO/Forum";

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
}

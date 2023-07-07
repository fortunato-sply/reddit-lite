import { HttpClient } from "@angular/common/http";
import { Injectable } from '@angular/core';
import { CompletePost, PostDTO } from "../DTO/Post";

@Injectable({
  providedIn: 'root'
})
export class PostService {
  constructor(private http: HttpClient) { }

  sendPost(data: PostDTO) {
      return this.http.post('http://localhost:5241/post/send', data, { observe: 'response' });
  }

  getForumPosts(id: string) {
    return this.http.get<CompletePost[]>('http://localhost:5241/post/get/forumposts/' + id);
  }

  getFeedPosts() {
    return this.http.get<CompletePost[]>('http://localhost:5241/post/get/feedposts');
  }
}
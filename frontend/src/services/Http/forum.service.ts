import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from '@angular/core';
import { CreateForumDTO, ForumDTO, UpdateForumDTO } from "../DTO/Forum";
import { JWT, UserMemberDTO } from "../DTO/user";

@Injectable({
  providedIn: 'root'
})
export class ForumService {
  constructor(private http: HttpClient) { }

  searchForum(name: string) {
    return this.http.get<ForumDTO[]>(`http://localhost:5241/forum/search/${name}`);
  }

  createForum(data: CreateForumDTO) {
    return this.http.post('http://localhost:5241/forum/create', data, { observe: 'response' });
  }

  getForums() {
    return this.http.get<ForumDTO[]>('http://localhost:5241/forum/getforums');
  }

  getUserForums(jwt: string) {
    const headers = new HttpHeaders().set('jwt', jwt);
    return this.http.get<ForumDTO[]>('http://localhost:5241/forum/myforums', { headers });
  }

  getUserFavoriteForums(userId: number) {
    return this.http.get<ForumDTO[]>('http://localhost:5241/forum/favorites/' + userId);
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

  checkIfIsForumFavorite(id: string, jwt: JWT) {
    return this.http.post<boolean>(`http://localhost:5241/forum/checkfavorite/${id}`, jwt);
  }

  favoriteForum(id: string, jwt: JWT) {
    return this.http.post(`http://localhost:5241/forum/favorite/${id}`, jwt);
  }

  unfavoriteForum(id: string, jwt: JWT) {
    return this.http.post(`http://localhost:5241/forum/unfavorite/${id}`, jwt);
  }

  updateForum(id: string, updates: UpdateForumDTO) {
    return this.http.post(`http://localhost:5241/forum/update/${id}`, updates);
  }

  getMembers(id: string) {
    return this.http.get<UserMemberDTO[]>(`http://localhost:5241/forum/members/${id}`);
  }
}

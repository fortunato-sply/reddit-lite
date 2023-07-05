export interface CreateForumDTO {
  jwtToken: string,
  title: string,
  description: string
}

export interface ForumDTO {
  id: string,
  title: string,
  description: string,
  photo: string,
  createdAt: Date,
  owner: string
}
interface UserData {
  id: number;
  username: string;
  email: string;
  born: Date;
  photoID: number;
}

interface UserToken {
  authenticated: boolean,
  id: number,
  username: string,
  email: string,
  born: Date,
  photoId: number
}

interface LoginDTO {
  username: string;
  password: string;
}

interface SignUpDTO {
  username: string;
  email: string;
  password: string;
  born: Date;
}

interface JWT {
  value?: string;
}

export { UserData, LoginDTO, JWT, SignUpDTO, UserToken };
interface UserData {
  id: number;
  username: string;
  email: string;
  born: Date;
  photoID: number;
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
  photo: File | null,
  photoName: string
}

interface JWT {
  value?: string;
}

export { UserData, LoginDTO, JWT, SignUpDTO };
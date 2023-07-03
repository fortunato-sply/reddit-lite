import { HttpErrorResponse } from '@angular/common/http';
import { Component, Input } from '@angular/core';
import { Router } from '@angular/router';
import { JWT, SignUpDTO } from 'src/services/Http/user';
import { UserService } from 'src/services/Http/user.service';
import { UploadFileValues } from 'src/services/UploadFile';


@Component({
  selector: 'app-new-account-page',
  templateUrl: './new-account-page.component.html',
  styleUrls: ['./new-account-page.component.css']
})

export class NewAccountPageComponent {
  email = "";
  user = "";
  password = "";
  born: Date = new Date();
  photo: File | null = null;
  photoName: string = '';

  error: string = '';

  constructor(private router: Router, private service: UserService) { }

  passwordChanged(event: any) {
    this.password = event;
  }

  createAccount() {
    if (this.email == "email@email.com" && this.password == "123") {
      // Isso evidentemente não é seguro, mas a ideia é bom e será melhorada no futuro
      sessionStorage.setItem('user', 'pamella');
      this.router.navigate(["/login"])
    }
    this.router.navigate(["/login"])
  }

  emitAlert() {
    console.log(this.error);
  }

  handleFileUpload(value: FormData) {
    const file: File | null = value.get('file') as File | null;
    const fileName: string | null = file ? file.name : null;

    console.log('File:', file);
    console.log('File name: ', fileName);
  }

  submitLogin() {
    const data: SignUpDTO = {
      email: this.email,
      username: this.user,
      password: this.password,
      born: this.born,
      photo: this.photo,
      photoName: this.photoName
    }

    this.service.signUp(data)
      .subscribe({
        next: (res: JWT) => {
          sessionStorage.setItem('jwt', res.value ?? "")
          this.router.navigate(['/feed'])
        },
        error: (err: HttpErrorResponse) => {
          switch(err.status) {
            case 400:
              this.error = "Usuário ja existe."
              break;
          }

          this.emitAlert();
        },
        complete: () => {

        }
      })
  }
}

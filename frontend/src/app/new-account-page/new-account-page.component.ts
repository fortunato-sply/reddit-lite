import { HttpErrorResponse } from '@angular/common/http';
import { Component, Input } from '@angular/core';
import { Router } from '@angular/router';
import { ImageService } from 'src/services/Http/image.service';
import { JWT, SignUpDTO, UserToken } from 'src/services/Http/user';
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
  photo = '';
  imgForm?: FormData;

  error: string = '';

  constructor(
    private router: Router, 
    private userService: UserService,
    private imageService: ImageService
    ) { }

  passwordChanged(event: any) {
    this.password = event;
  }

  emitAlert() {
    console.log(this.error);
  }

  handleFileUpload(value: FormData) {
    this.imgForm = value;
  }

  submitLogin() {
    const data: SignUpDTO = {
      email: this.email,
      username: this.user,
      password: this.password,
      born: this.born
    }

    this.userService.signUp(data)
      .subscribe({
        next: (res: JWT) => {
          sessionStorage.setItem('jwt', res.value ?? "")

          this.userService.validate(res)
            .subscribe((res: UserToken) => {
              if (this.imgForm) {
                this.imageService
                  .updateUserImage(this.imgForm)
                  .subscribe((res) => { });
              }
            })

          window.alert('sucesso. jwt: ' + res)
          this.router.navigate(['/login'])
        },
        error: (err: HttpErrorResponse) => {
          switch(err.status) {
            case 400:
              this.error = "Usuário ja existe."
              break;
            case 200:
              
          }

          this.emitAlert();
        }
      })
  }
}

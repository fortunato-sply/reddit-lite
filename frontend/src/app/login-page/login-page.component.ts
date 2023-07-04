import { HttpErrorResponse } from '@angular/common/http';
import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { JWT, LoginDTO } from 'src/services/Http/user';
import { UserService } from 'src/services/Http/user.service';

@Component({
  selector: 'app-login-page',
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.css']
})
export class LoginPageComponent {
  user: string = "";
  password: string = "";

  emitError: boolean = false;
  error: string = "";

  constructor(private router: Router, private service: UserService) { }

  onValueChanged() {
    this.emitError = false;
  }

  passwordChanged(event: any) {
    this.emitError = false;
    this.password = event;
  }

  emitAlert() {
    window.alert("Erro: " + this.error)
  }

  submitLogin() {
    const data: LoginDTO = {
      username: this.user,
      password: this.password
    }

    this.service.login(data)
      .subscribe({
        next: (res: JWT) => {
          sessionStorage.setItem('jwt', res.value ?? "")
          window.alert("jwt: " + res.value);
          this.router.navigate(['/feed'])
        },
        error: (err: HttpErrorResponse) => {
          switch(err.status) {
            case 404:
              this.error = "Not found"
              break;
            case 400:
              this.error = "Usuário ou senha inválidos."
              break;
          }

          this.emitError = true;
        },
        complete: () => {

        }
      })
  }
}

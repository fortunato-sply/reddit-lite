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
  user = "";
  password = "";
  error = "";

  constructor(private router: Router, private service: UserService) { }

  passwordChanged(event: any) {
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

          this.emitAlert();
        },
        complete: () => {

        }
      })
  }
}

import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { IconProp } from '@fortawesome/fontawesome-svg-core';
import { faEye, faEyeSlash } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-password',
  templateUrl: './password.component.html',
  styleUrls: ['./password.component.css']
})

export class PasswordComponent implements OnInit {
  faEye = faEye;
  faEyeSlash = faEyeSlash;
  icon = faEye;

  // Inputs podem ser acessados de fora do componente como propriedades HTML
  // Outputs podem ser acessados de fora do componente como eventos no estilo onclick
  @Output() valueChanged = new EventEmitter<string>();
  @Input() breakLineOnInput = true;
  @Input() canSeePassword = true;
  @Input() seePassword = false;
  @Output() seePasswordChanged = new EventEmitter<boolean>();
  // Membros protegidos não podem ser usados fora da classe, apenas no html
  protected inputType = "text";
  protected inputStyle = "color: black;"
  protected inputText = "";
  protected initialState = true;
  // Implmentamos OnInit para executar algum comportamento quando o componente inicializa
  ngOnInit(): void {
    // Atualizamos o inputType que aparece na tela
    this.updateInput()
  }
  /* Aqui o evento foi pedido usando checkBoxToogle($event) no html e foi recuperado aqui.
  * Isso não necessariamente precisa ser feito. Neste caso poderiamos usar apenas checkBoxToogle()
  * e deixar essa função sem parâmetros já que não usamos o resultado do evento 'newValue'.
  */
  protected checkBoxToogle(newValue: any) {
    this.updateInput()
    this.seePasswordChanged.emit(this.seePassword);
  }
  protected updateInput() {
    if (this.initialState) {
      this.inputText = "Senha"
      this.inputType = "text"
      this.inputStyle = "color: gray;"
      return
    }
    this.inputStyle = "color: black;"
    this.inputType = this.seePassword ? "text" : "password";
  }
  protected passwordChanged() {
    this.updateInput()
    this.valueChanged.emit(this.inputText)
  }
  protected passwordClick() {
    if (!this.initialState)
      return
    this.initialState = false;
    this.inputText = "";
    this.updateInput();
  }
  protected passwordFocusout() {
    if (this.inputText !== "")
      return
    this.initialState = true
    this.updateInput();
  }
  protected eyeBtnClick() {
    if(this.seePassword) {
      this.seePassword = false;
      this.icon = faEyeSlash;
    }
    else {
      this.seePassword = true;
      this.icon = faEye;
    }

    this.updateInput();
  }
}
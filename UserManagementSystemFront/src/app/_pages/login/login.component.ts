import { Component } from '@angular/core';
import { FooterComponent } from '../../_components/footer/footer.component';
import { ViewLoginComponent } from '../../_components/view-login/view-login.component';

@Component({
  selector: 'app-login',
  imports: [ FooterComponent, ViewLoginComponent ],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent {

}

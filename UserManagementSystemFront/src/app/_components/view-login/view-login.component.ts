import { Component } from '@angular/core';
import { TranslateModule, TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-view-login',
  imports: [
    TranslateModule
  ],
  templateUrl: './view-login.component.html',
  styleUrl: './view-login.component.scss'
})
export class ViewLoginComponent {
  constructor(private translate: TranslateService)
  {}
}

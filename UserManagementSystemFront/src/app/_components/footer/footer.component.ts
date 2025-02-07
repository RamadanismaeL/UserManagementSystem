import { NgIf } from '@angular/common';
import { Component } from '@angular/core';
import { TranslateModule, TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-footer',
  imports: [
    TranslateModule
],
  templateUrl: './footer.component.html',
  styleUrl: './footer.component.scss'
})
export class FooterComponent {
  localStor = localStorage.getItem('language');
  constructor(private translate: TranslateService)
  {}

  changeLanguage(event: Event) {
    console.log("working")
    const target = event.target as HTMLSelectElement;
    const selectedLang = target.value;

    if (selectedLang) {
      this.translate.use(selectedLang);
      localStorage.setItem('language', selectedLang);
    }
  }
}

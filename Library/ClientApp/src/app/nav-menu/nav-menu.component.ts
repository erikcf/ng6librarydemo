import { Component, Inject } from '@angular/core';
import { SESSION_STORAGE, WebStorageService } from "angular-webstorage-service";

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.scss']
})
export class NavMenuComponent {
  isExpanded = false;
  storage: WebStorageService;
  constructor(@Inject(SESSION_STORAGE) storage: WebStorageService) {
    this.storage = storage;
  }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
}

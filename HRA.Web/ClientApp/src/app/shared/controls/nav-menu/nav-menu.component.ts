import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { UserToken } from 'src/app/models/UserToken';
import { AuthenticationService } from 'src/app/services/authentication.service';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.scss']
})
export class NavMenuComponent {
  isExpanded = false;
  currentUser: UserToken;
  isAdmin = false;

  constructor(
    private router: Router,
    private authenticationService: AuthenticationService) {
    this.authenticationService.currentUser.subscribe(x => {
      this.currentUser = x;
      if (!!this.currentUser) {
        this.isAdmin = this.currentUser.roles.some(role => role === 'SuperAdmin' || role === 'Admin');
      }
    });
  }

  logout() {
    this.authenticationService.logout();
    this.router.navigate(['/login']);
  }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
}

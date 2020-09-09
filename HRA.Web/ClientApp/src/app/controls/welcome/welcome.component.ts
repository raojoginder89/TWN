import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { UserToken } from 'src/app/models/UserToken';

@Component({
  templateUrl: 'welcome.component.html',
  styleUrls: ['./welcome.component.scss']
})
export class WelcomeComponent {
  currentUser: UserToken;
  hraFilled = false;
  constructor(
    private _router: Router,
    private authenticationService: AuthenticationService) {
    this.authenticationService.currentUser.subscribe(x => {
      this.currentUser = x;
      if (!!this.currentUser) {
        this.hraFilled = !!this.currentUser.isHRACompleted;
      }
    });
  }

  gotoQuestionnaire() {
    const route = `/${this.currentUser.groupId}/hra-form`;
    this._router.navigate([route]);
  }
}

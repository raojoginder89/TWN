import { Component } from '@angular/core';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { UserToken } from 'src/app/models/UserToken';
@Component({
    // tslint:disable-next-line: component-selector
    selector: 'hra-footer',
    templateUrl: './footer.ctrl.html',
    styleUrls: ['./footer.ctrl.scss']
})
// tslint:disable-next-line: component-class-suffix
export class FooterControl {
    currentUser: UserToken;
    constructor(
        private authenticationService: AuthenticationService) { 
        this.authenticationService.currentUser.subscribe(x => {
            this.currentUser = x;
        });
    }
}

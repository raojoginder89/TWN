import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
    selector: 'app-update-group',
    templateUrl: './update-group.component.html'
})
export class UpdateGroupComponent {
    selectedGroupId: string;
    constructor(private _route: ActivatedRoute, private _router: Router) {
        this._route.params.subscribe(params => { this.selectedGroupId = params['groupId']; });
    }

    showAllGroups() {
        this._router.navigate(['/groups']);
    }
}

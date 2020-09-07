import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
    selector: 'app-update-video',
    templateUrl: './update-video.component.html'
})
export class UpdateVideoComponent {
    selectedVideoId: string;
    constructor(private _route: ActivatedRoute, private _router: Router) {
        this._route.params.subscribe(params => { this.selectedVideoId = params['videoId']; });
    }

    showAllVideos() {
        this._router.navigate(['/videos']);
    }
}

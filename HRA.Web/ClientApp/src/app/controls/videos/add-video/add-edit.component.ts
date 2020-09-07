import { Component, OnInit, Input } from '@angular/core';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { AlertService } from 'src/app/services/AlertService.service';
import { first } from 'rxjs/operators';
import { Router } from '@angular/router';
import { VideoService } from 'src/app/services/video.service';

@Component({
    selector: 'app-add-video',
    templateUrl: './add-edit.component.html'
})
export class AddEditVideoComponent implements OnInit {
    videoForm: FormGroup;
    loading = false;
    submitted = false;

    @Input()
    videoId: string;

    constructor(
        private formBuilder: FormBuilder,
        private videoService: VideoService,
        private alertService: AlertService,
        private _router: Router
    ) { }

    ngOnInit() {
        // referenceId: string;
        this.videoForm = this.formBuilder.group({
            url: ['', Validators.required],
            title: ['', Validators.required],
            description: ['', Validators.required],
            extraInfo: ['', Validators.required]
        });

        if (!!this.videoId) {
            this.videoService.getVideoById(this.videoId).subscribe(video => {
                this.videoForm.patchValue(video);
            });
        }
    }

    // convenience getter for easy access to form fields
    get f() { return this.videoForm.controls; }

    onSubmit() {
        this.submitted = true;

        // reset alerts on submit
        this.alertService.clear();

        // stop here if form is invalid
        if (this.videoForm.invalid) {
            return;
        }

        this.loading = true;
        const videoRefrenceId = this.videoId;

        if (!!videoRefrenceId) {
            this.videoService.updateVideo(videoRefrenceId, this.videoForm.value)
                .pipe(first())
                .subscribe(
                    () => {
                        // return to list
                        this.alertService.success('Video updated uccessfully.');

                        this.loading = false;
                    },
                    error => {
                        if (error.error instanceof Array) {
                            error.error.map(err => this.alertService.error(err.value));
                        } else if (error.error instanceof Array) {
                            this.alertService.error(error.error);
                        } else {
                            this.alertService.error(error);
                        }
                        this.loading = false;
                    });
        } else {
            this.videoService.addVideo(this.videoForm.value)
                .pipe(first())
                .subscribe(
                    (video) => {
                        // return to list
                        this.alertService.success('Video added successfully.');
                        this.loading = false;
                        this.videoId = video.referenceId;
                        // this._router.navigate([`/videos/${video.referenceId}`])
                    },
                    error => {
                        if (error.error instanceof Array) {
                            error.error.map(err => this.alertService.error(err.value));
                        } else if (error.error instanceof Array) {
                            this.alertService.error(error.error);
                        } else {
                            this.alertService.error(error);
                        }
                        this.loading = false;
                    });
        }
    }
}



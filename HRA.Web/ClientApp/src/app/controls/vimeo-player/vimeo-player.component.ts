import { Component, Input, EventEmitter, Output, OnChanges, SimpleChanges } from '@angular/core';
import Player from '@vimeo/player';
import { VideoWithUserDetails } from 'src/app/models/VideoWithUserDetails';
import { UserVideoService } from 'src/app/services/user-video.service';
import { first } from 'rxjs/operators';
import { ModalService } from 'src/app/shared/controls/modal/modal.service';
import { ModalOptions } from 'src/app/models/modalOptions';
import { Router } from '@angular/router';

@Component({
    selector: 'app-vimeo-player',
    templateUrl: './vimeo-player.component.html'
})
export class VimeoPlayerComponent implements OnChanges {

    @Input()
    public videoDetails: VideoWithUserDetails;

    @Output()
    public videoCompletedEvent: EventEmitter<boolean> = new EventEmitter<boolean>();

    @Output()
    public showVideoGallary: EventEmitter<boolean> = new EventEmitter<boolean>();

    title: string;
    state = 'play'; // play when auto play is true, else stop.
    isMuted = false;
    isVideoSeeked = false;
    private _player;

    constructor(private _userVideoService: UserVideoService, private _modalService: ModalService, private _router: Router) { }

    ngOnChanges(changes: SimpleChanges) {
        for (const propName in changes) {
            if (changes.hasOwnProperty(propName)) {
                switch (propName) {
                    case 'videoDetails': {
                        this.initPlayer();
                    }
                }
            }
        }
    }

    private initPlayer() {
        if (this._player) {
            this._player.destroy();
        }

        this._player = new Player('vimeoPlayer', {
            url: this.videoDetails.url,
            controls: false,
            title: false,
            byline: false,
            potrait: false,
            transparent: false,
            loop: false,
            autoplay: true,
            width: 640
        });

        this.title = this.videoDetails.title;
        const _this = this;
        this._player.getVideoTitle().then(function (title) {
            _this.title = title;
        });

        if (this.videoDetails.status !== 3) {
            this._player.on('ended', this._handleVideoEndedEvent(this));
            this._player.on('seeked', this._handleSeekedEvent(this));
        }
    }

    public buttonMutePress() {
        this.isMuted = !this.isMuted;
        this._player.setMuted(this.isMuted).then(function () {
        }).catch(function (error) {
            console.log(error.name);
            console.log(error);
        });
    }

    public buttonPlayPress(): void {
        if (this.state === 'stop') {
            this.state = 'play';
            this._playVideo();

        } else if (this.state === 'play' || this.state === 'resume') {
            this.state = 'pause';
            this._pauseVideo();
        } else if (this.state === 'pause') {
            this.state = 'resume';
            this._playVideo();
        }
        console.log('button play pressed, play was ' + this.state);
    }

    public buttonStopPress() {
        this.state = 'stop';
        const outerThis = this;
        this._player.unload().then(function () {
            outerThis.isVideoSeeked = false;
        }).catch(function (error) {
            console.log(error.name);
            console.log(error);
        });
    }

    public setVolume(volume: number) {
        this._player.setVolume(volume).then(function () {
        }).catch(function (error) {
            console.log(error.name);
            console.log(error);
        });
    }

    public buttonFullScreenPress() {
        this._player.requestFullscreen().then(function () {
        }).catch(function (error) {
            console.log(error.name);
            console.log(error);
        });
    }

    private _playVideo() {
        this._player.play().then(function () {
        }).catch(function (error) {
            console.log(error.name);
            console.log(error);
        });
    }

    private _pauseVideo() {
        this._player.pause().then(function () {
        }).catch(function (error) {
            console.log(error.name);
            console.log(error);
        });
    }

    private _handleVideoEndedEvent(outerThis: VimeoPlayerComponent) {
        return function () {
            if (outerThis.videoDetails.status === 3 || outerThis.isVideoSeeked) {
                return;
            }
            outerThis._userVideoService.addVideoToUserLibrary(outerThis.videoDetails.referenceId, 'Completed')
                .pipe(first())
                .subscribe(
                    (): void => {
                        outerThis.videoDetails.status = 3;
                        outerThis.videoCompletedEvent.emit(true);
                        const opt = new ModalOptions();
                        opt.title = 'Success';
                        opt.content = 'Video completed successfully';
                        opt.okBtnText = 'Go To Library';
                        opt.cancelBtnText = 'Cancel';
                        opt.showCancelButton = true;

                        const modalRef = outerThis._modalService.open(opt);
                        modalRef.then((result) => {
                            if (!!result) {
                                outerThis.showVideoGallary.emit(true);
                            }
                        }, (reason) => {

                        });
                    });
        };
    }

    private _handleSeekedEvent(outerThis: VimeoPlayerComponent) {
        return function () {
            if (outerThis.videoDetails.status === 3) {
                return;
            }
            outerThis.isVideoSeeked = true;
        };
    }
}

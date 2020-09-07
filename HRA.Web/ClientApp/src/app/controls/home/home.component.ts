import { Component, OnInit } from '@angular/core';
import { VideoWithUserDetails } from 'src/app/models/VideoWithUserDetails';
import { Observable } from 'rxjs';
import { DynamicControlBase } from 'src/app/data-driven/dynamic-control.base';
import { VideoService } from 'src/app/services/video.service';
import { VimeoService } from 'src/app/services/vimeo.service';
import { QuestionService } from 'src/app/data-driven/question.service';
import { first } from 'rxjs/operators';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  videos: VideoWithUserDetails[] = [];
  currentSelectedVideo: VideoWithUserDetails;
  playVideo = false;
  questions$: Observable<DynamicControlBase<any>[]>;

  constructor(private _videoService: VideoService, private _vimeoService: VimeoService, service: QuestionService) {
    this.questions$ = service.getQuestions();
  }

  ngOnInit() {
    this._videoService.getAllVideosWithUserStatus().pipe(first()).subscribe(x => x.map(y => this.getVimeoThmbnail(y)));
  }

  playthisVideo(video: VideoWithUserDetails) {
    this.currentSelectedVideo = video;
    this.playVideo = true;
  }

  getDate(date: string) {
    return date == null ? null : new Date(date + 'Z');

  }

  async showList() {
    this.playVideo = false;
  }

  videoCompleted() {
    this.currentSelectedVideo.status = 3; // completed
    this.videos[this.currentSelectedVideo.referenceId] = this.currentSelectedVideo;
  }

  getVimeoThmbnail(video: VideoWithUserDetails) {
    const _this = this;
    this._vimeoService.getOEmbedData(video.url).then(function (data) {
      video.videoThumnail = data['thumbnail_url'].toString();
      video.duration = _this._toHoursMinutesSeconds(data['duration']);
      video.videoThumnailWithPlayButton = data['thumbnail_url_with_play_button'].toString();
      _this.videos.push(video);
    });
  }

  private _toHoursMinutesSeconds(totalSeconds: number) {
    const hours = Math.floor(totalSeconds / 3600);
    const minutes = Math.floor((totalSeconds % 3600) / 60);
    const seconds = totalSeconds % 60;

    return `${hours.toString()}:${minutes
      .toString()
      .padStart(2, '0')}:${seconds.toString().padStart(2, '0')}`;
  }
}

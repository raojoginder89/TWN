import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { getApiUrl } from '../helpers/get-url';
import { VideoWithUserDetails } from '../models/VideoWithUserDetails';
import { Observable } from 'rxjs';
import { Videos } from '../models/videos';

@Injectable({ providedIn: 'root' })
export class VideoService {

  constructor(private http: HttpClient) { }

  getAllVideos(): Observable<Videos[]> {
    return this.http.get<Videos[]>(getApiUrl('videos'));
  }

  getVideoById(videoReferenceId: string) {
    return this.http.get<Videos>(getApiUrl(`videos/${videoReferenceId}`));
  }

  getAllVideosWithUserStatus(): Observable<VideoWithUserDetails[]> {
    return this.http.get<VideoWithUserDetails[]>(getApiUrl('videos/user'));
  }

  addVideo(group: Videos): Observable<Videos> {
    return this.http.post<Videos>(getApiUrl('videos'), group);
  }

  updateVideo(groupReferenceId: string, group: Videos): Observable<Videos> {
    return this.http.put<Videos>(getApiUrl(`videos/${groupReferenceId}`), group);
  }

  deleteVideo(videoReferenceId: string) {
    return this.http.get(getApiUrl(`videos/${videoReferenceId}`));
  }

  getVimeoJson(url: string) {
    return this.http.get(`https://vimeo.com/api/oembed.json?url=${url}`);
  }
}

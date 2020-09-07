import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { getApiUrl } from '../helpers/get-url';

@Injectable({ providedIn: 'root' })
export class UserVideoService {
  constructor(private http: HttpClient) { }
  getAllUserVideos() {
    return this.http.get(getApiUrl('user/videos'));
  }

  getUserVideoById(videoRefernceId: string) {
    return this.http.get(getApiUrl(`user/videos/${videoRefernceId}`));
  }

  getAllUserVideosByStatus(videoStatus: number) {
    return this.http.get(getApiUrl(`user/videos/status/${videoStatus}`));
  }

  addVideoToUserLibrary(videoRefernceId: string, videoStatus: string) {
    return this.http.post(getApiUrl(`user/videos/${videoRefernceId}/${videoStatus}`), {});
  }

  updateVideoStatus(videoRefernceId: string, videoStatus: string) {
    return this.http.put(getApiUrl(getApiUrl(`user/videos/${videoRefernceId}/${videoStatus}`)), {});
  }
}

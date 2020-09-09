import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User } from '../models/User';
import { getApiUrl } from '../helpers/get-url';

@Injectable({ providedIn: 'root' })
export class UserService {
  constructor(private http: HttpClient) { }

  getUserDetails(): Observable<User> {
    return this.http.get<User>(getApiUrl('user'));
  }

  changePassword(value: any) {
    return this.http.post(getApiUrl('user/change-password'), value);
  }

  forgotPassword(value: any) {
    return this.http.post(getApiUrl('user/forgot-password'), value);
  }

  resetPassword(value: any) {
    return this.http.post(getApiUrl('user/reset-password'), value);
  }
}

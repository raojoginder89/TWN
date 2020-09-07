import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { HRADetails } from '../models/hraDetails';
import { getApiUrl } from '../helpers/get-url';

@Injectable({ providedIn: 'root' })
export class HRAService {
  constructor(private http: HttpClient) { }

  addHRA(hraDetails: HRADetails) {
    return this.http.post(getApiUrl(`hra/${hraDetails.groupId}`), hraDetails);
  }
}

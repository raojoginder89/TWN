import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { getApiUrl } from '../helpers/get-url';
import { Group } from '../models/group';
import { Observable } from 'rxjs/internal/Observable';
import { StringResponse } from '../models/stringResponse';

@Injectable({ providedIn: 'root' })
export class GroupService {
  constructor(private http: HttpClient) { }

  getAllGroups(): Observable<Group[]> {
    return this.http.get<Group[]>(getApiUrl('groups'));
  }

  getGroup(groupReferenceId: string): Observable<Group> {
    return this.http.get<Group>(getApiUrl(`groups/${groupReferenceId}`));
  }

  getGroupName(groupReferenceId: string): Observable<StringResponse> {
    return this.http.get<StringResponse>(getApiUrl(`groups/${groupReferenceId}/name`));
  }

  addGroup(group: Group): Observable<Group> {
    return this.http.post<Group>(getApiUrl('groups'), group);
  }

  updateGroup(groupReferenceId: string, group: Group): Observable<Group> {
    return this.http.put<Group>(getApiUrl(`groups/${groupReferenceId}`), group);
  }

  deleteGroup(groupReferenceId: string) {
    return this.http.delete(getApiUrl(`groups/${groupReferenceId}`));
  }
}

import { Injectable } from '@angular/core';
import { AppHttpService } from '../core/api/app-http.service';
import { Observable } from 'rxjs';
import { FilterState } from '../shared/filter/models/filter-state';
import { DataResult } from '../shared/filter/models/data-result';
import { Project } from './Project';
import { appSettings } from 'src/app-settings';
@Injectable({
  providedIn: 'root'
})
export class ProjectService {

  constructor(private httpService: AppHttpService) { }
  private readonly url: string = appSettings.api.project.path;

  query(state: FilterState): Observable<DataResult<Project>> {
    return this.httpService.post<DataResult<Project>>({ url: this.url + '/search' }, state);
  }

  getAll(): Observable<Project[]> {
    return this.httpService.get<Project[]>({ url: this.url + '/getProjects' });
  }

  create(project: Project): Observable<Project> {
    return this.httpService.post<Project>({ url: `${this.url}/update` }, project);
  }

  update(project: Project): Observable<Project> {
    return this.httpService.post<Project>({ url: `${this.url}/update` }, project);
  }

  get(id: string): Observable<Project> {
    return this.httpService.get<Project>({ url: `${this.url}/${id}` });
  }

  delete(id: number): Observable<Project> {
    return this.httpService.delete<Project>({ url: `${this.url}/delete/${id}` });
  }

  updateProjectState(project: Project): Observable<any> {
    return this.httpService.post<any>({ url: `${this.url}/updateProjectStatus` }, project);
  }
}

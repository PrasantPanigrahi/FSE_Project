import { appSettings } from 'src/app-settings';
import { Injectable } from '@angular/core';
import { AppHttpService } from '../core/api/app-http.service';
import { FilterState } from '../shared/filter/models/filter-state';
import { DataResult } from '../shared/filter/models/data-result';
import { Task } from './task';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class TaskService {

  constructor(private httpService: AppHttpService) { }
  private readonly url: string = appSettings.api.task.path;

  query(state: FilterState): Observable<DataResult<Task>> {
    return this.httpService.post<DataResult<Task>>({ url: this.url + '/search' }, state);
  }

  create(task: Task): Observable<Task> {
    return this.httpService.post<Task>({ url: `${this.url}/update` }, task);
  }

  update(task: Task): Observable<Task> {
    return this.httpService.post<Task>({ url: `${this.url}/update` }, task);
  }

  get(id: string): Observable<Task> {
    return this.httpService.get<Task>({ url: `${this.url}/${id}` });
  }

  delete(id: number): Observable<Task> {
    return this.httpService.delete<Task>({ url: `${this.url}/delete/${id}` });
  }

  updateTaskState(task: Task): Observable<any> {
    return this.httpService.post<any>({ url: `${this.url}/updateTaskStatus` }, task);
  }
}

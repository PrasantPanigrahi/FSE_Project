import { appSettings } from 'src/app-settings';
import { Injectable } from '@angular/core';
import { AppHttpService } from '../core/api/app-http.service';
import { Observable } from 'rxjs';
import { Task } from './task';

@Injectable({
  providedIn: 'root'
})
export class ParentTaskService {

  constructor(private httpService: AppHttpService) { }
  private readonly url: string = appSettings.api.parentTask.path;

  getAll(): Observable<Task[]> {
    return this.httpService.get<Task[]>({ url: this.url + '/getTasks' });
  }

  create(task: Task): Observable<Task> {
    return this.httpService.post<Task>({ url: `${this.url}/update` }, task);
  }
}

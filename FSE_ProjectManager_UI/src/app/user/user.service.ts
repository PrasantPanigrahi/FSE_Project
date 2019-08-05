import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { User } from './user';
import { AppHttpService } from '../shared/app-http.service';
import { appSettings } from 'src/app-settings';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  private readonly url = appSettings.api.user.path;
  constructor(private appHttpService: AppHttpService) { }

  get(id:number):Observable<User>{
  return this.appHttpService.get<User>({url: `${this.url}/${id}` });
  }
}

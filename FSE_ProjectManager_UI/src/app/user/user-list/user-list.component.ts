import { Component, OnInit } from '@angular/core';
import { User } from '../user';
import{ UserService} from '../user.service';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.scss']
})
export class UserListComponent implements OnInit {
  user:User;
  constructor(private userService:UserService) { }

  ngOnInit() {
    this.userService.get(1).subscribe(result=>{this.user= result }, error=>{ alert(error);})
  }

}

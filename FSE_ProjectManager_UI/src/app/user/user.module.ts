import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserListComponent } from './user-list/user-list.component';
import { UserDetailComponent } from './user-detail/user-detail.component';
import { Routes, RouterModule } from '@angular/router';
import { SharedModule } from '../shared/shared.module';
import { NameValidatorDirective } from './user-detail/name-validator.directive';

const routes: Routes = [
  { path: 'list', component: UserListComponent },
  { path: 'new', component: UserDetailComponent },
  { path: ':id', component: UserDetailComponent },
];


@NgModule({
  declarations: [UserListComponent, UserDetailComponent, NameValidatorDirective],
  imports: [
    CommonModule,
    SharedModule,
    RouterModule.forChild(routes)
  ]
})
export class UserModule { }

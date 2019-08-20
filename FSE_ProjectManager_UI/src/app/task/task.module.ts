import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TaskListComponent } from './task-list/task-list.component';
import { TaskDetailComponent } from './task-detail/task-detail.component';
import { SharedModule } from '../shared/shared.module';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  { path: 'list', component: TaskListComponent },
  { path: 'new', component: TaskDetailComponent },
  { path: ':id', component: TaskDetailComponent },
];


@NgModule({
  declarations: [TaskListComponent, TaskDetailComponent],
  imports: [
    CommonModule,
    SharedModule,
    RouterModule.forChild(routes)
  ]
})
export class TaskModule { }

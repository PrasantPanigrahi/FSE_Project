import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TaskListComponent } from './task-list/task-list.component';
import { TaskDetailComponent } from './task-detail/task-detail.component';



@NgModule({
  declarations: [TaskListComponent, TaskDetailComponent],
  imports: [
    CommonModule
  ]
})
export class TaskModule { }

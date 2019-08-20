import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProjectListComponent } from './project-list/project-list.component';
import { ProjectDetailComponent } from './project-detail/project-detail.component';
import { SharedModule } from '../shared/shared.module';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  { path: 'list', component: ProjectListComponent },
  { path: 'new', component: ProjectDetailComponent },
  { path: ':id', component: ProjectDetailComponent },
];


@NgModule({
  declarations: [ProjectListComponent, ProjectDetailComponent],
  imports: [
    CommonModule,
    SharedModule,
    RouterModule.forChild(routes)
  ]
})
export class ProjectModule { }

import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import{LayoutComponent} from './layout/layout.component';


const routes: Routes = [
  {
    path: '',
    component: LayoutComponent,
    children: [
      { path: 'user', loadChildren: './user/user.module#UserModule' },
      { path: 'project', loadChildren: './project/project.module#ProjectModule' },
      { path: 'task', loadChildren: './task/task.module#TaskModule' },
    ]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

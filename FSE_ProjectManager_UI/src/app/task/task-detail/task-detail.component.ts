import { UserService } from 'src/app/user/user.service';
import { User } from '../../user/user';
import { ParentTaskService } from '../../task/parent-task.service';
import { ProjectService } from '../../project/project.service';
import { TaskService } from '../../task/task.service';
import { Task } from './../task';
import { MessageService } from 'primeng/api';
import { common } from 'src/app/core/common';
import {
  ConfirmationDialogService
} from 'src/app/shared/confirm-dialog/confirmation-dialog.service';

import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { Project } from 'src/app/project/Project';


@Component({
  selector: 'app-task-detail',
  templateUrl: './task-detail.component.html',
  styleUrls: ['./task-detail.component.scss']
})
export class TaskDetailComponent implements OnInit {
  _startDate: Date;
  _endDate: Date;
  projectOptions: Project[];
  ownerOptions: User[];
  parentTaskOptions: Task[];
  ready = true;
  isParentTask = false;
  public currentTask: Task;
  public selectedProject: Project;
  public selectedOwner: User;
  public selectedParentTask: Task;

  get readonly(): boolean {
    return !common.isNil(this.currentTask.id);
  }

  get startDate(): any {
    if (common.isNil(this._startDate)) {
      this._startDate = new Date();
    }
    return this._startDate;
  }

  set startDate(value: any) {
    this._startDate = value;
    if (this._startDate >= this._endDate) {
      this._endDate = value;
    }
  }

  get endDate(): any {
    if (common.isNil(this._endDate)) {
      this.endDate = new Date();
      this._endDate.setDate(this.startDate.getDate() + 1);
    }
    return this._endDate;
  }

  set endDate(value: any) {
    if (value < this.startDate) {
      this.messageService.add({
        severity: 'error',
        summary: value,
        detail: 'end date cannot be prior to start date.'
      });

      this.endDate = undefined;
    } else {
      this._endDate = value;
    }
  }


  constructor(private router: ActivatedRoute,
    private route: Router,
    public userService: UserService,
    private messageService: MessageService,
    private confirmationDialogService: ConfirmationDialogService,
    private taskService: TaskService,
    private projectService: ProjectService,
    private parentTaskService: ParentTaskService) { }

  ngOnInit() {
    this.instantiateTask(undefined);
    this.load();
  }

  loadOwners() {
    this.userService.getAll()
      .subscribe(users => {
        this.ownerOptions = users;
        this.selectedOwner = this.ownerOptions.find(m => m.id === this.currentTask.ownerId);
      });
  }

  loadProjects() {
    this.projectService.getAll()
      .subscribe(projects => {
        this.projectOptions = projects;
        this.selectedProject = this.projectOptions.find(m => m.id === this.currentTask.projectId);
      });
  }

  loadParentTasks() {
    this.parentTaskService.getAll()
      .subscribe(tasks => {
        this.parentTaskOptions = tasks;
        this.selectedParentTask = this.parentTaskOptions.find(m => m.id === this.currentTask.parentTaskId);
      });
  }

  load() {
    const id = this.router.snapshot.paramMap.get('id');
    if (!common.isNil(id)) {
      this.taskService.get(id)
        .subscribe(task => {
          this.instantiateTask(task);
          this.ready = true;
          this.startDate = common.YYYYMMDDToDate(this.currentTask.startDate);
          this.endDate = common.YYYYMMDDToDate(this.currentTask.endDate);
          this.loadProjects();
          this.loadOwners();
          this.loadParentTasks();
        });
    } else {
      this.loadProjects();
      this.loadOwners();
      this.loadParentTasks();
      this.ready = true;
    }
  }
  save() {
    const dto = this.extractDto();
    const action = common.isNil(dto.id) ? 'create' : 'update';
    this.confirmationDialogService.confirm(`Proceed to ${action} this task?`,
      (result) => {

        if (this.isParentTask) {
          this.saveParentTask(dto);
        } else {
          this.saveTask(dto);
        }
      });
  }

  saveTask(dto: Task) {
    if (common.isNil(this.selectedOwner)) {
      this.messageService.add({
        severity: 'error',
        summary: this.currentTask.name,
        detail: 'Please choose owner.'
      });
    } else if (common.isNil(this.selectedProject)) {
      this.messageService.add({
        severity: 'error',
        summary: this.currentTask.name,
        detail: 'Please choose project.'
      });
    } else {
      this.taskService
        .update(dto)
        .subscribe(() => {
          // clear form
          this.instantiateTask(null);
          this.messageService.add({
            severity: 'success',
            summary: this.currentTask.name,
            detail: 'Saved successfully.'
          });
          this.back();
        }, (error) => {
          this.messageService.add({
            severity: 'error',
            summary: this.currentTask.name,
            detail: 'Task Could not be saved.'
          });
        });
    }
  }

  saveParentTask(dto: Task) {
    this.parentTaskService
      .create(dto)
      .subscribe(result => {
        // clear form
        this.instantiateTask(null);
        this.messageService.add({
          severity: 'success',
          summary: this.currentTask.name,
          detail: 'Saved successfully.'
        });
        this.back();
      }, (error) => {
        this.messageService.add({
          severity: 'error',
          summary: this.currentTask.name,
          detail: 'Parent task Could not be saved.'
        });
      });
  }

  back() {
    this.route.navigate(['task/list']);
  }

  instantiateTask(task: Task) {
    if (common.isNil(task)) {
      this.currentTask = {
        id: undefined,
        name: '',
        startDate: '',
        endDate: '',
        priority: '0',
        ownerName: '',
        ownerId: undefined,
        projectName: '',
        projectId: undefined,
        parentTaskName: '',
        parentTaskId: undefined,
      };
    } else {
      this.currentTask = common.cloneDeep(task);
    }
  }

  extractDto(): Task {
    return {
      id: this.currentTask.id,
      name: this.currentTask.name,
      startDate: common.isNil(this.startDate) ? '' : common.dateToYYYYMMDD(this.startDate),
      endDate: common.isNil(this.endDate) ? '' : common.dateToYYYYMMDD(this.endDate),
      priority: this.currentTask.priority,
      ownerId: common.isNil(this.selectedOwner) ? undefined : this.selectedOwner.id,
      projectId: common.isNil(this.selectedProject) ? undefined : this.selectedProject.id,
      parentTaskId: common.isNil(this.selectedParentTask) ? undefined : this.selectedParentTask.id,
      ownerName: undefined,
      projectName: undefined,
      parentTaskName: undefined,
    };
  }

  onOwnerChange($event: any) {
    this.selectedOwner = $event.value;
  }

  onProjectChange($event: any) {
    this.selectedProject = $event.value;
  }

  onParentTaskChange($event: any) {
    this.selectedParentTask = $event.value;
  }
}


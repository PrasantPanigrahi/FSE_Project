import { UserService } from '../../user/user.service';
import { MessageService } from 'primeng/api';
import { common } from 'src/app/core/common';
import {
  ConfirmationDialogService
} from 'src/app/shared/confirm-dialog/confirmation-dialog.service';

import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { Project } from '../Project';
import { User } from 'src/app/user/user';
import { ProjectService } from '../project.service';

@Component({
  selector: 'app-project-details',
  templateUrl: './project-detail.component.html',
  styleUrls: ['./project-detail.component.scss']
})
export class ProjectDetailComponent implements OnInit {
  _startDate: Date;
  _endDate: Date;
  managerOptions: User[];
  ready = true;
  public currentProject: Project;
  public selectedManager: User;
  _isSetDate: boolean;

  get startDate(): any {
    return this._startDate;
  }
  set startDate(value: any) {
    this._startDate = value;
    if (this._startDate >= this._endDate) {
      this._endDate = value;
    }
  }

  get isSetDate(): any {
    return this._isSetDate;
  }

  set isSetDate(value: any) {
    this._isSetDate = value;
    if (this._isSetDate) {
      if (common.isNil(this.startDate)) {
        this.startDate = new Date();
        this._endDate = new Date();
        this._endDate.setDate(this.startDate.getDate() + 1);
      }
    } else {
      if (common.dateToYYYYMMDD(this.startDate) !== (this.currentProject.startDate)) {
        this.startDate = common.YYYYMMDDToDate(this.currentProject.startDate);
        this._endDate = common.YYYYMMDDToDate(this.currentProject.endDate);
      }
    }
  }

  get endDate(): any {
    return this._endDate;
  }

  set endDate(value: any) {
    if (value < this.startDate) {
      this.messageService.add({
        severity: 'error',
        summary: value,
        detail: 'end date cannot be prior to start date.'
      });
    } else {
      this._endDate = value;
    }
  }

  constructor(private router: ActivatedRoute,
    private route: Router,
    public userService: UserService,
    private messageService: MessageService,
    private confirmationDialogService: ConfirmationDialogService,
    private projectService: ProjectService) { }

  ngOnInit() {
    this.instantiateProject(undefined);
    this.load();
  }
  loadManagers() {
    this.userService.getAll()
      .subscribe(users => {
        this.managerOptions = users;
        this.selectedManager = this.managerOptions.find(m => m.id === this.currentProject.managerId);
      });
  }

  load() {
    const id = this.router.snapshot.paramMap.get('id');
    if (!common.isNil(id)) {
      this.projectService.get(id)
        .subscribe(project => {
          this.instantiateProject(project);
          this.ready = true;
          this.startDate = common.YYYYMMDDToDate(this.currentProject.startDate);
          this.endDate = common.YYYYMMDDToDate(this.currentProject.endDate);
          this.isSetDate = !common.isNil(this.startDate);
          this.loadManagers();
        });
    } else {
      this.ready = true;
      this.loadManagers();
    }
  }
  save() {
    if (common.isNil(this.selectedManager)) {
      this.messageService.add({
        severity: 'error',
        summary: this.currentProject.name,
        detail: 'Please select Project manager first.'
      });
    } else {
      const dto = this.extractDto();
      const action = common.isNil(dto.id) ? 'create' : 'update';
      this.confirmationDialogService.confirm(`Proceed to ${action} this project?`,
        () => {
          this.projectService
            .update(dto)
            .subscribe(result => {
              // clear form
              this.instantiateProject(null);
              this.messageService.add({
                severity: 'success',
                summary: this.currentProject.name,
                detail: 'Saved successfully.'
              });
              this.back();
            }, error => {
              this.messageService.add({
                severity: 'error',
                summary: this.currentProject.name,
                detail: 'Project Could not be saved.'
              });
            });
        });
    }
  }
  back() {
    this.route.navigate(['project/list']);
  }

  instantiateProject(project: Project) {

    if (common.isNil(project)) {
      this.currentProject = {
        id: undefined,
        name: '',
        startDate: '',
        endDate: '',
        priority: '0',
        managerDisplayName: '',
        managerId: 0
      };
    } else {
      this.currentProject = common.cloneDeep(project);
    }
  }

  public extractDto(): Project {
    return {
      id: this.currentProject.id,
      name: this.currentProject.name,
      startDate: common.isNil(this.startDate) ? '' : common.dateToYYYYMMDD(this.startDate),
      endDate: common.isNil(this.endDate) ? '' : common.dateToYYYYMMDD(this.endDate),
      priority: this.currentProject.priority,
      managerDisplayName: common.isNil(this.selectedManager) ? '' : this.selectedManager.displayName,
      managerId: common.isNil(this.selectedManager) ? undefined : this.selectedManager.id
    };
  }
  onManagerChange($event: any) {
    this.selectedManager = $event.value;
  }
}

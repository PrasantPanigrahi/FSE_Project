import { MessageService } from 'primeng/api';
import { ProjectService } from './../project.service';
import { Component, OnInit, OnDestroy } from '@angular/core';
import { Project } from '../Project';
import { Subscription } from 'rxjs';
import { FilterListHelper } from 'src/app/shared/filter/filter-list-helper';
import { Router } from '@angular/router';
import { TableFilterStateService } from 'src/app/shared/filter/table-filter-state.service';
import { FilterList } from 'src/app/shared/filter/filter-list';
import { ConfirmationDialogService } from 'src/app/shared/confirm-dialog/confirmation-dialog.service';
import { common } from 'src/app/core/common';

@Component({
  selector: 'app-project-list',
  templateUrl: './project-list.component.html',
  styleUrls: ['./project-list.component.scss']
})
export class ProjectListComponent implements FilterList, OnInit, OnDestroy {
  projects: Project[];
  selectedProject: Project;
  cols: any[];
  private subscription: Subscription;
  totalRecords = 0;
  loading: boolean;
  priorityFilter: number;
  public filterListHelper: FilterListHelper;
  constructor(private router: Router,
    public filterStateService: TableFilterStateService,
    private projectService: ProjectService,
    private confirmationDialogService: ConfirmationDialogService,
    private messageService: MessageService) {
    this.filterListHelper = new FilterListHelper(this, this.filterStateService);
    this.filterStateService.onSort('id', -1);
  }

  ngOnInit() {
    this.cols = [
      { field: 'name', header: 'Project Name' },
      { field: 'isSuspendedText', header: 'Suspended?' },
      { field: 'totalTasks', header: 'Total Tasks #' },
      { field: 'totalCompletedTasks', header: 'Completed Tasks #' },
      { field: 'priority', header: 'Priority' },
      { field: 'startDate', header: 'Start Date' },
      { field: 'endDate', header: 'End Date' },
    ];
    this.loading = true;
    this.priorityFilter = 0;
  }

  edit(project: Project): void {
    this.router.navigate([`/project/${project.id}`]);
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }

  refresh() {
    this.subscription = this.projectService
      .query(this.filterStateService.extract())
      .subscribe(dataResult => {
        this.projects = dataResult.data;
        this.format(this.projects);
        this.totalRecords = dataResult.total;
        this.loading = false;
      });
  }
  format(projects: Project[]) {
    projects.forEach(p => {
      if (p.isSuspended) {
        p.isSuspendedText = 'Yes';
      } else {
        p.isSuspendedText = 'No';
      }
    });
  }

  loadProjectLazy($event: any) {
    this.loading = true;
    this.filterStateService.onPaginate($event.first, $event.rows);
    let sortField = $event.sortField;
    let sortOrder = $event.sortOrder;
    if (!sortField) {
      sortField = 'id';
      sortOrder = -1;
    }
    this.filterStateService.onSort(sortField, sortOrder);
    this.refresh();
  }

  addNewProject(): void {
    this.router.navigate([`/project/new`]);
  }

  changeProjectState(project: Project, isSuspended: boolean): void {
    const projectDto = common.cloneDeep(project);
    projectDto.isSuspended = isSuspended;
    const action = isSuspended ? 'suspend' : 'activate';
    const actionResult = isSuspended ? 'Suspended' : 'Activated';
    this.confirmationDialogService.confirm(`Proceed to ${action}  this project?`,
      () => {
        this.projectService
          .updateProjectState(projectDto)
          .subscribe(result => {
            this.messageService.add({
              severity: 'success',
              summary: projectDto.name,
              detail: `${actionResult} successfully.`
            });
            this.refresh();
          }, error => {
            this.messageService.add({
              severity: 'error',
              summary: projectDto.name,
              detail: `Project Could not be ${actionResult}`
            });
          });
      });
  }
}

import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs';
import { FilterListHelper } from 'src/app/shared/filter/filter-list-helper';
import { Router } from '@angular/router';
import { TableFilterStateService } from 'src/app/shared/filter/table-filter-state.service';
import { FilterList } from 'src/app/shared/filter/filter-list';
import { Task } from '../task';
import { ConfirmationDialogService } from 'src/app/shared/confirm-dialog/confirmation-dialog.service';
import { MessageService } from 'primeng/api';
import { FilterOperatorType } from 'src/app/shared/filter/models/filter-enums';
import { common } from 'src/app/core/common';
import { TaskService } from '../../task/task.service';


@Component({
  selector: 'app-task-list',
  templateUrl: './task-list.component.html',
  styleUrls: ['./task-list.component.scss']
})
export class TaskListComponent implements FilterList, OnInit, OnDestroy {
  tasks: Task[];
  selectedTask: Task;
  cols: any[];
  private subscription: Subscription;
  totalRecords = 0;
  loading: boolean;
  priorityFilter: number;
  public filterListHelper: FilterListHelper;
  constructor(private router: Router,
    public filterStateService: TableFilterStateService,
    // tslint:disable-next-line:align
    private taskService: TaskService,
    private confirmationDialogService: ConfirmationDialogService,
    private messageService: MessageService) {
    this.filterListHelper = new FilterListHelper(this, this.filterStateService);
    this.filterStateService.onSort('id', -1);
    // this.filterStateService.onFilter('status', '3', FilterOperatorType.notEqualTo);
  }

  ngOnInit() {
    this.cols = [
      { field: 'name', header: 'Task' },
      { field: 'isCompleted', header: 'Completed?' },
      { field: 'parentTaskName', header: 'Parent Task' },
      { field: 'priority', header: 'Priority#' },
      { field: 'projectName', header: 'Project' },
      { field: 'ownerName', header: 'Owner' },
      { field: 'startDate', header: 'Start Date' },
      { field: 'endDate', header: 'End Date' },
    ];
    this.loading = true;
    this.priorityFilter = 0;
  }

  edit(task: Task): void {
    this.router.navigate([`/task/${task.id}`]);
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }

  refresh() {
    this.subscription = this.taskService
      .query(this.filterStateService.extract())
      .subscribe(dataResult => {
        this.tasks = dataResult.data;
        this.format(this.tasks);
        this.totalRecords = dataResult.total;
        this.loading = false;
      });
  }
  format(tasks: Task[]) {
    tasks.forEach(t => {
      if (t.statusId === 3) {
        t.isCompleted = 'Yes';
      } else {
        t.isCompleted = 'No';
      }
    });
  }

  loadTaskLazy($event: any) {
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

  addNewTask(): void {
    this.router.navigate([`/task/new`]);
  }

  changeTaskState(task: Task, isCompleted: boolean): void {
    const taskDto = common.cloneDeep(task);
    if (isCompleted) {
      taskDto.statusId = 3;
    } else {
      taskDto.statusId = 2;
    }
    const action = isCompleted ? 'complete' : 'incomplete';
    const actionResult = isCompleted ? 'Completed' : 'Incomplete';
    this.confirmationDialogService.confirm(`Proceed to ${action} this task?`,
      () => {
        this.taskService
          .updateTaskState(taskDto)
          .subscribe(result => {
            this.messageService.add({
              severity: 'success',
              summary: taskDto.name,
              detail: `Task updated to ${actionResult} successfully.`
            });
            this.refresh();
          }, error => {
            this.messageService.add({
              severity: 'error',
              summary: taskDto.name,
              detail: `Task Could not be ${actionResult}`
            });
          });
      });
  }
}

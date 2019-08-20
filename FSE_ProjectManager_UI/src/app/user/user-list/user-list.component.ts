import { User } from './../user';
import { Component, OnInit, OnDestroy } from '@angular/core';
import { UserService } from '../user.service';
import { TableFilterStateService } from 'src/app/shared/filter/table-filter-state.service';
import { Subscription } from 'rxjs';
import { FilterListHelper } from 'src/app/shared/filter/filter-list-helper';
import { Router } from '@angular/router';
import { FilterList } from 'src/app/shared/filter/filter-list';
import { ConfirmationDialogService } from 'src/app/shared/confirm-dialog/confirmation-dialog.service';
import { MessageService } from 'primeng/api';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.scss'],
  providers: [TableFilterStateService]
})
export class UserListComponent implements FilterList, OnInit, OnDestroy {
  users: User[];
  selectedUser: User;
  cols: any[];
  private subscription: Subscription;
  totalRecords = 0;
  loading: boolean;
  public filterListHelper: FilterListHelper;
  constructor(private router: Router,
    public filterStateService: TableFilterStateService,
    private userService: UserService,
    private confirmationDialogService: ConfirmationDialogService,
    private messageService: MessageService) {
    this.filterListHelper = new FilterListHelper(this, this.filterStateService);
    this.filterStateService.onSort('id', -1);
  }

  ngOnInit() {
    this.cols = [
      { field: 'firstName', header: 'Fist Name' },
      { field: 'lastName', header: 'Last Name' },
      { field: 'employeeId', header: 'Employee Id' },
    ];

    this.loading = true;
  }

  edit(user: User): void {
    this.router.navigate([`/user/${user.id}`]);
  }
  removeUser(user: User): void {
    this.confirmationDialogService.confirm(`Proceed to delete this user, Please make sure it does not have Project/task assigned?`,
      () => {
        this.userService
          .delete(user.id)
          .subscribe(result => {
            // clear form
            this.messageService.add({
              severity: 'success',
              summary: user.firstName,
              detail: 'Deleted successfully.'
            });
            this.refresh();
          }, error => {
            this.messageService.add({
              severity: 'error',
              summary: user.firstName,
              detail: `User ${user.firstName} Could not be deleted. Possibly projects/tasks are mapped to it.`
            });
          });
      });
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }

  refresh() {
    this.subscription = this.userService
      .query(this.filterStateService.extract())
      .subscribe(dataResult => {
        this.users = dataResult.data;
        this.totalRecords = dataResult.total;
        this.loading = false;
      });
  }


  loadUserLazy($event: any) {
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

  addNewUser(): void {
    this.router.navigate([`/user/new`]);
  }
}

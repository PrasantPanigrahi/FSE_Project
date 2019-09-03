import { MessageService } from 'primeng/api';
import { common } from 'src/app/core/common';
import {
  ConfirmationDialogService
} from 'src/app/shared/confirm-dialog/confirmation-dialog.service';

import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { UserService } from '../user.service';
import { User } from '../user';
import { FormGroup, FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'app-user-details',
  templateUrl: './user-detail.component.html',
  styleUrls: ['./user-detail.component.scss']
})
export class UserDetailComponent implements OnInit {

  ready = true;
  id:any;
  userForm: FormGroup;
  private firstName: FormControl;
  private lastName: FormControl;
  private employeeId: FormControl;
  public currentUser: User;

  get readonly(): boolean {
    return false;
  }

  constructor(private router: ActivatedRoute,
    private route: Router,
    public userService: UserService,
    private messageService: MessageService,
    private confirmationDialogService: ConfirmationDialogService) {
  }

  ngOnInit() {
    this.loadUser();
  }

  // create user form group
  createUserForm() {
    this.firstName = new FormControl(this.currentUser.firstName);
    this.lastName = new FormControl(this.currentUser.lastName);
    this.employeeId = new FormControl(this.currentUser.employeeId, Validators.required);

    this.userForm = new FormGroup({
      firstName: this.firstName,
      lastName: this.lastName,
      employeeId: this.employeeId
    });
  }

  loadUser() {
     this.id = this.router.snapshot.paramMap.get('id');
    if (!common.isNil(this.id)) {
      this.userService.get(this.id)
        .subscribe(user => {
          this.instantiateUser(user);
          this.ready = true;
        });
    } else {
      this.ready = true;
      this.instantiateUser(undefined);
    }
  }

  save(user: User) {
    if (this.userForm.valid) {
      const dto = {
        id: this.currentUser.id,
        firstName: user.firstName,
        lastName: user.lastName,
        employeeId: user.employeeId
      };
      const action = common.isNil(dto.id) ? 'create' : 'update';
      this.confirmationDialogService.confirm(`Proceed to ${action} this user?`,
        () => {
          this.userService
            .update(dto)
            .subscribe(result => {
              // clear form
              this.instantiateUser(null);
              this.messageService.add({
                severity: 'success',
                summary: this.currentUser.firstName,
                detail: 'Saved successfully.'
              });
              this.back();
            });
        });
    }
  }
  back() {
    this.route.navigate(['user/list']);
  }

  validateEmployeeId() {
    return this.employeeId.valid || this.employeeId.untouched;
  }

  validateName() {
    return this.firstName.valid || this.firstName.untouched || this.lastName.untouched;
  }

  instantiateUser(user: User) {

    if (common.isNil(user)) {
      this.currentUser = {
        id: undefined,
        firstName: undefined,
        lastName: undefined,
        employeeId: undefined
      };
    } else {
      this.currentUser = common.cloneDeep(user);
    }

    // after instantiating user, create user form group
    this.createUserForm();
  }

  onLastNameChange() {
    this.userForm.controls['firstName'].updateValueAndValidity();
  }
}

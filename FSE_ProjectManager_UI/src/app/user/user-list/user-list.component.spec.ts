import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { UserListComponent } from './user-list.component';
import { RouterTestingModule } from '@angular/router/testing';
import { By } from '@angular/platform-browser';
import { Observable } from 'rxjs/Observable';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { HTTP_INTERCEPTORS, HttpClient, HttpClientModule } from '@angular/common/http';
import { ConfirmationService, MessageService } from 'primeng/api';
import { ActivatedRoute, Router } from '@angular/router';
import { UserService } from '../user.service';
import { User } from '../user';
import { TableFilterStateService } from 'src/app/shared/filter/table-filter-state.service';
import { FilterListHelper } from 'src/app/shared/filter/filter-list-helper';
import { observable } from 'rxjs';
//import { SharedModule } from '../../shared/shared.module';

describe('UserListComponent', () => {
  let component: UserListComponent;
  let fixture: ComponentFixture<UserListComponent>;
  let service: UserService;
  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UserListComponent ],
      imports: [FormsModule,ReactiveFormsModule, RouterTestingModule,HttpClientModule],
      schemas: [
        CUSTOM_ELEMENTS_SCHEMA
      ],
      providers: [UserService,MessageService,ConfirmationService,TableFilterStateService]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UserListComponent);
    component = fixture.componentInstance;
    service = TestBed.get(UserService);
    component.ngOnInit();
    fixture.detectChanges();
  });

  it('should  create UserList', () => {
    expect(component).toBeTruthy();
  });

  it('should  addNewUser', () => {
    const router = TestBed.get(Router);
    const spy = spyOn(router, 'navigate');

    fixture.detectChanges();

    const button = fixture.debugElement.query(By.css('#AddNew'));
    button.triggerEventHandler('click', null);
    //button.nativeElement.click();
    
    expect(spy).toHaveBeenCalledWith(['/user/new']);

  });

  it('should redirect the user to `User Details` component when Edit button is clicked', () => {
    const router = TestBed.get(Router);
    const spy = spyOn(router, 'navigate');

    component.selectedUser = {
      id: 1,
      firstName: "FirstName1",
      lastName: "LastName1",
      employeeId: "1"
    };

    //fixture.detectChanges();

    //const button = fixture.debugElement.query(By.css('.edit ui-button ui-widget ui-state-default ui-corner-all ui-button-icon-only'));
   // button.triggerEventHandler('click', null);

   // expect(spy).toHaveBeenCalledWith(['/user', component.selectedUser.id]);
  });

  it('should  Delete', () => {
    let currentUser:User[]=[{
      id: 1,
      firstName: "FirstName1",
      lastName: "LastName1",
      employeeId: "1"
    },
  
    {
      id: 2,
      firstName: "FirstName2",
      lastName: "LastName2",
      employeeId: "2"
    },
    {
      id: 1,
      firstName: "FirstName3",
      lastName: "LastName3",
      employeeId: "3"
    }]; 
    component.users=currentUser;

    spyOn(window, 'confirm').and.returnValue(true);
    const spy = spyOn(service, 'delete').and.callThrough();
    const UserId = 2;
    const deluser=currentUser.find(x=>x.id===UserId)
    service.delete(deluser.id);
//component.removeUser(deluser);
    const index = component.users.findIndex(
      product => product.id === UserId
    );
    //expect(index).toBe();

    //  const button = fixture.debugElement.query(By.css('#AddNew'));
    //  button.triggerEventHandler('click', null);
     //button.nativeElement.click();
    // expect(spy).toHaveBeenCalledWith(['/user/new']);

  });


  it('should NOT call the server to delete a product if the user cancels', () => {
    spyOn(window, 'confirm').and.returnValue(false);
    const spy = spyOn(service, 'delete').and.callThrough();
  
    const user={
      id: 1,
      firstName: "FirstName1",
      lastName: "LastName1",
      employeeId: "1"
    }
    const Userid = 1;
    service.delete(Userid);

    expect(spy).not.toHaveBeenCalledWith(user);
  });
});

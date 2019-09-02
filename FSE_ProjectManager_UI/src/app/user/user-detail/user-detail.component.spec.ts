import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { UserDetailComponent } from './user-detail.component';
import { UserService } from '../user.service';
import { User } from '../user';
import { By } from '@angular/platform-browser';
import { Observable } from 'rxjs/Observable';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { HTTP_INTERCEPTORS, HttpClient, HttpClientModule } from '@angular/common/http';
import { ConfirmationService, MessageService } from 'primeng/api';
import { ActivatedRoute, Router } from '@angular/router';
describe('UserDetailComponent', () => {
  let component: UserDetailComponent;
  let fixture: ComponentFixture<UserDetailComponent>;
  let service: UserService;
  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UserDetailComponent ],
      imports: [FormsModule,ReactiveFormsModule, RouterTestingModule,HttpClientModule],
   
      schemas: [
        CUSTOM_ELEMENTS_SCHEMA
      ],
      providers: [UserService,MessageService,ConfirmationService]
    
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UserDetailComponent);
    component = fixture.componentInstance;
    component.ngOnInit();
    service = TestBed.get(UserService);
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });


  it('should be blank in case of new user', () => {
    const id=component.id;
    component.loadUser() 
    // Arrange - Setup
    var curentU:User= {
      id: 1,
      firstName: null,
      lastName: null,
      employeeId: null
    };
    if(id!=null)
    {
      curentU={
        id: 1,
        firstName: "FirstName1",
        lastName: "LastName1",
        employeeId: "1"
      };
    }
    //component.currentUser =curentU;
    fixture.detectChanges();

    // const idElement: HTMLElement = fixture.debugElement.query(
    //   By.css('#ud_firstName')
    // ).nativeElement;
    fixture.whenStable().then(() => {
    const firstNameElement=component.userForm.controls['firstName'].value;
    const alastNameElement=component.userForm.controls['lastName'].value;
    const employeeIdElement=component.userForm.controls['employeeId'].value;

    console.log("Data",component.currentUser);
    // expect(firstNameElement).toContain(curentU.firstName);
    // expect(alastNameElement).toContain(curentU.lastName);
    // expect(employeeIdElement).toContain(curentU.employeeId);
    expect(firstNameElement).toBe(curentU.firstName);
    expect(alastNameElement).toBe(curentU.lastName);
    expect(employeeIdElement).toBe(curentU.employeeId);
    });
    //expect(priceElement.innerText).toContain('70,000');
  });


  it('form invalid when empty', () => {
    expect(component.userForm.valid).toBeFalsy();
  });

  it('should redirect  to `User List` component when Cancel button is clicked', () => {
    const router = TestBed.get(Router);
    const spy = spyOn(router, 'navigate');



    fixture.detectChanges();

    const button = fixture.debugElement.query(By.css('.ui-button-danger'));
    button.triggerEventHandler('click', null);
    //button.nativeElement.click();
    expect(spy).toHaveBeenCalledWith(['user/list']);
    
   
  });
  

  it('should save User details when form is submitted', () => {
    let currentUser={
      id: 1,
      firstName: "FirstName5",
      lastName: "LastName5",
      employeeId: "5"
    };
    // const spy = spyOn(service, 'update').and.returnValue(
    //   Observable.length
    // );
    component.instantiateUser(currentUser);
    component.save(currentUser);
    const form = fixture.debugElement.query(By.css('form'));
    form.triggerEventHandler('submit', null);
    console.log(component.currentUser)
    // const button = fixture.debugElement.query(By.css('#save'));
    // button.nativeElement.click();

  //  expect(spy).toHaveBeenCalled();

  // const firstNameElement: HTMLInputElement = fixture.debugElement.query(
  //   By.css('.firstName')
  // ).nativeElement;
  // const alastNameElement: HTMLInputElement = fixture.debugElement.query(
  //   By.css('#ud_lastName')
  // ).nativeElement;
  // const employeeIdElement: HTMLInputElement = fixture.debugElement.query(
  //   By.css('#ud_employeeId')
  // ).nativeElement;


  const firstNameElement=component.userForm.controls['firstName'].value;
  const alastNameElement=component.userForm.controls['lastName'].value;
  const employeeIdElement=component.userForm.controls['employeeId'].value;
  expect(firstNameElement).toBe(currentUser.firstName);
  expect(alastNameElement).toBe(currentUser.lastName);
  expect(employeeIdElement).toBe(currentUser.employeeId);
  // expect(firstNameElement.value).toContain(currentUser.firstName);
  // expect(alastNameElement.value).toContain(currentUser.lastName);
  // expect(employeeIdElement.value).toContain(currentUser.employeeId);
  });

  

});


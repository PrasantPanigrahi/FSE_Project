import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { ProjectDetailComponent } from './project-detail.component';



import { RouterTestingModule } from '@angular/router/testing';

import { Project } from '../Project';
import { User } from 'src/app/user/user';
import { ProjectService } from '../project.service';

import { By } from '@angular/platform-browser';
import { Observable } from 'rxjs/Observable';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { HTTP_INTERCEPTORS, HttpClient, HttpClientModule } from '@angular/common/http';
import { ConfirmationService, MessageService } from 'primeng/api';
import { ActivatedRoute, Router } from '@angular/router';


describe('ProjectDetailComponent', () => {
  let component: ProjectDetailComponent;
  let fixture: ComponentFixture<ProjectDetailComponent>;  
  let service: ProjectService;


  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ProjectDetailComponent ],      
      imports: [FormsModule,ReactiveFormsModule, RouterTestingModule,HttpClientModule],   
      schemas: [
        CUSTOM_ELEMENTS_SCHEMA
      ],
      providers: [ProjectService,MessageService,ConfirmationService]

    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProjectDetailComponent);
    component = fixture.componentInstance;
    component.ngOnInit();
    service = TestBed.get(ProjectService);
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  //====  

  it('should redirect  to `Project List` component when Cancel button is clicked', () => {
    const router = TestBed.get(Router);
    const spy = spyOn(router, 'navigate');

    fixture.detectChanges();
    const button = fixture.debugElement.query(By.css('.ui-button-danger'));
    button.triggerEventHandler('click', null);
    //button.nativeElement.click();
    expect(spy).toHaveBeenCalledWith(['project/list']);       
  });
  
});

import { TestBed } from '@angular/core/testing';
import { TaskService } from './task.service';


import { RouterTestingModule } from '@angular/router/testing';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HTTP_INTERCEPTORS, HttpClient, HttpClientModule } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AppHttpService } from '../core/api/app-http.service';

describe('TaskService', () => {
  //beforeEach(() => TestBed.configureTestingModule({}));

  beforeEach(() => TestBed.configureTestingModule({
    imports: [FormsModule,ReactiveFormsModule, RouterTestingModule,HttpClientModule],    
    providers: [TaskService] 
  }));

  it('should be created', () => {
    const service: TaskService = TestBed.get(TaskService);
    expect(service).toBeTruthy();
  });
});

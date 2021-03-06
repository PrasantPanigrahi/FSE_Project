import { TestBed } from '@angular/core/testing';
import { AppHttpService } from './app-http.service';

import { RouterTestingModule } from '@angular/router/testing';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HTTP_INTERCEPTORS, HttpClient, HttpClientModule } from '@angular/common/http';
import { Injectable } from '@angular/core';


describe('AppHttpService', () => {
  //beforeEach(() => TestBed.configureTestingModule({}));

  beforeEach(() => TestBed.configureTestingModule({

    imports: [FormsModule,ReactiveFormsModule, RouterTestingModule,HttpClientModule],    
    providers: [AppHttpService]
 
  }));

  it('should be created', () => {
    const service: AppHttpService = TestBed.get(AppHttpService);
    expect(service).toBeTruthy();
  });
});

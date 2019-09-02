import { TestBed, inject } from '@angular/core/testing';
import { MenuService } from './menu.service';

//import { RouterTestingModule } from '@angular/router/testing';
//import { FormsModule, ReactiveFormsModule } from '@angular/forms';
//import { HTTP_INTERCEPTORS, HttpClient, HttpClientModule } from '@angular/common/http';
//import { Injectable } from '@angular/core';
//import { AppHttpService } from '../api/app-http.service';

describe('MenuService', () => {
  beforeEach(() => TestBed.configureTestingModule({
    //imports: [FormsModule,ReactiveFormsModule, RouterTestingModule,HttpClientModule],    
    providers: [MenuService] 
  }));

  it('should be created', inject([MenuService], (service: MenuService) => {
    expect(service).toBeTruthy();
  }));
});

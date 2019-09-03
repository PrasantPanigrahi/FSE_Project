import { TestBed } from '@angular/core/testing';
import { UserService } from './user.service';
import { RouterTestingModule } from '@angular/router/testing';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
//import { HTTP_INTERCEPTORS, HttpClient, HttpClientModule } from '@angular/common/http';
// import { Injectable } from '@angular/core';
// import { AppHttpService } from '../core/api/app-http.service';

import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import {  HttpClientModule } from '@angular/common/http';
import { appSettings } from 'src/app-settings';
import { environment } from 'src/environments/environment';

describe('UserService', () => {
  beforeEach(() => TestBed.configureTestingModule({
    imports: [HttpClientModule,HttpClientTestingModule],
    providers: [UserService]
  }));

  function setup() {
    const sharedService = TestBed.get(UserService);
    const httpTestingController = TestBed.get(HttpTestingController);
    return { sharedService, httpTestingController };
  }

  it('should be created', () => {
    const service: UserService = TestBed.get(UserService);
    expect(service).toBeTruthy();
  });


  it('should call the  all UserList Service', () => {
    const { sharedService, httpTestingController } = setup();
    const getUser = {
      id: 2,
      firstName: "FirstName2",
      lastName: "LastName2",
      employeeId: "2"
    };
    
    sharedService.getAll().subscribe(data => {
      console.log("UserData",data);
      expect(data.mapData).toEqual(getUser);
    });
    
    const url=environment.api.url+''+appSettings.api.user.path;
    const req = httpTestingController.expectOne(url + '/getUserList');

    expect(req.request.method).toBe('GET');

    req.flush({
      mapData: getUser
    });

  });



  it('should call the  delete User Service', () => {
    const { sharedService, httpTestingController } = setup();
    const DelUser = {
      id: 2,
      firstName: "FirstName2",
      lastName: "LastName2",
      employeeId: "2"
    };
    sharedService.delete(DelUser.id).subscribe(data => {
    
      expect(data.mapData).toEqual(DelUser);
    });
    const url=environment.api.url+''+appSettings.api.user.path;
    const req = httpTestingController.expectOne(url + '/delete/'+DelUser.id);

    expect(req.request.method).toBe('DELETE');

    req.flush({
      mapData: DelUser
    });

  });



  it('should call the  Create User Service', () => {
    const { sharedService, httpTestingController } = setup();
    const SaveUser = {
      id: 6,
      firstName: "FirstName6",
      lastName: "LastName6",
      employeeId: "6"
    };
    sharedService.create(SaveUser).subscribe(data => {
      console.log("UserData",data);
      expect(data.mapData).toEqual(SaveUser);
    });
    const url=environment.api.url+''+appSettings.api.user.path;
    const req = httpTestingController.expectOne(url + '/update');

    expect(req.request.method).toBe('POST');

    req.flush({
      mapData: SaveUser
    });

  });


});
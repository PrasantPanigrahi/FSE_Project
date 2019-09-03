import { TestBed } from '@angular/core/testing';
import { ParentTaskService } from './parent-task.service';


import { HttpClientTestingModule } from '@angular/common/http/testing';
import {  HttpClientModule } from '@angular/common/http';


describe('ParentTaskService', () => {
  //beforeEach(() => TestBed.configureTestingModule({})); 
    beforeEach(() => TestBed.configureTestingModule({
      imports: [HttpClientModule,HttpClientTestingModule],
      providers: [ParentTaskService]
    }));
  

  it('should be created', () => {
    const service: ParentTaskService = TestBed.get(ParentTaskService);
    expect(service).toBeTruthy();
  });


});

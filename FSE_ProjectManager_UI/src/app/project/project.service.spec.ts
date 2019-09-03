import { TestBed } from '@angular/core/testing';
import { ProjectService } from './project.service';

import { HttpClientTestingModule } from '@angular/common/http/testing';
import {  HttpClientModule } from '@angular/common/http';

describe('ProjectService', () => {
  //beforeEach(() => TestBed.configureTestingModule({}));
  beforeEach(() => TestBed.configureTestingModule({
    imports: [HttpClientModule,HttpClientTestingModule],
    providers: [ProjectService]
  }));

  it('should be created', () => {
    const service: ProjectService = TestBed.get(ProjectService);
    expect(service).toBeTruthy();
  });
});

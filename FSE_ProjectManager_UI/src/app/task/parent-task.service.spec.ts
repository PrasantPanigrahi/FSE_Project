import { TestBed } from '@angular/core/testing';

import { ParentTaskService } from './parent-task.service';

describe('ParentTaskService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: ParentTaskService = TestBed.get(ParentTaskService);
    expect(service).toBeTruthy();
  });
});

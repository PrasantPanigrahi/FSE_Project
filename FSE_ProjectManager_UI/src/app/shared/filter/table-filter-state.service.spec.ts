import { TestBed } from '@angular/core/testing';

import { TableFilterStateService } from './table-filter-state.service';

describe('TableFilterStateService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  xit('should be created', () => {
    const service: TableFilterStateService = TestBed.get(TableFilterStateService);
    expect(service).toBeTruthy();
  });
});

import { TestBed } from '@angular/core/testing';

import { ConfirmationDialogServiceService } from './confirmation-dialog-service.service';

describe('ConfirmationDialogServiceService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: ConfirmationDialogServiceService = TestBed.get(ConfirmationDialogServiceService);
    expect(service).toBeTruthy();
  });
});

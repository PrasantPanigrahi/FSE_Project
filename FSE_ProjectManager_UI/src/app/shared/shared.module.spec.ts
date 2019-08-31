import { SharedModule } from './shared.module';

describe('SharedModule', () => {
  let sharedModule: SharedModule;

  beforeEach(() => {
    sharedModule = new SharedModule();
  });

  xit('should create an instance', () => {
    expect(sharedModule).toBeTruthy();
  });
});

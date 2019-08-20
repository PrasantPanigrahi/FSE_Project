/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { FooterComponent } from './footer.component';


describe('Component: Footer', () => {

    beforeEach(() => {
        TestBed.configureTestingModule({
            providers: [SettingsService]
        }).compileComponents();
    });

    it('should create an instance', async(inject([SettingsService], (settingsService) => {

        expect(component).toBeTruthy();
    })));
});

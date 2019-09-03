/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { LayoutComponent } from './layout.component';


  describe('Component: Layout', () => {
    beforeEach(() => {
        TestBed.configureTestingModule({  
           //imports: [FormsModule,ReactiveFormsModule, RouterTestingModule,HttpClientModule],    
           //providers: [MenuService,  SettingsService]
           //providers: [ SettingsService]
        }).compileComponents();
    });

    it('should create an instance', () => {
      let component = new LayoutComponent();
      expect(component).toBeTruthy();
    });  

});

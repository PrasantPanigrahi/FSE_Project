import { TestBed, async, inject } from '@angular/core/testing';
import { FooterComponent } from './footer.component';

describe('Component: Footer', () => {    

    beforeEach(() => {
        TestBed.configureTestingModule({
           // imports: [FormsModule,ReactiveFormsModule, RouterTestingModule,HttpClientModule],    
           //providers: [MenuService,  SettingsService]
           //providers: [ SettingsService]
        }).compileComponents();
    });   

    it('should be created', () => {
        let component = new FooterComponent();
        expect(component).toBeTruthy();
      });
});

import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { ProjectListComponent } from './project-list.component';

describe('ProjectListComponent', () => {
  let component: ProjectListComponent;
  let fixture: ComponentFixture<ProjectListComponent>;


  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ProjectListComponent ]     
      // imports: [FormsModule,ReactiveFormsModule, RouterTestingModule,HttpClientModule],
      // schemas: [
      //   CUSTOM_ELEMENTS_SCHEMA
      // ],
      // providers: [ProjectService,MessageService,ConfirmationService,TableFilterStateService]

    })
    .compileComponents();
  }));


  xit('should create', () => {
    expect(component).toBeTruthy();
  });
});

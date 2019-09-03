import { TestBed, async, inject } from '@angular/core/testing';
import { FooterComponent } from './footer.component';
//import { SettingsService } from '../../core/settings/settings.service';
//import { MenuService } from '../../core/menu/menu.service';
// import { FormsModule, ReactiveFormsModule } from '@angular/forms';
// import { RouterTestingModule } from '@angular/router/testing';
// import { HttpClientModule } from '@angular/common/http';
//// import { DialogService } from 'primeng/api';
////import { Component, OnInit, ViewChild } from '@angular/core';


describe('Component: Footer', () => {    

    beforeEach(() => {
        TestBed.configureTestingModule({
           // imports: [FormsModule,ReactiveFormsModule, RouterTestingModule,HttpClientModule],    
           //providers: [MenuService,  SettingsService]
           //providers: [ SettingsService]
        });//.compileComponents();
    });

    ////it('should create an instance', async(inject([MenuService,  SettingsService], (menuService, userblockService, settingsService) => {
    // it('should create an instance', async(inject([SettingsService], (settingsService: SettingsService) => {
    //     let component = new FooterComponent();
    //     expect(component).toBeTruthy();
    // })));

    it('should be created', () => {
        let component = new FooterComponent();
        expect(component).toBeTruthy();
      });
});

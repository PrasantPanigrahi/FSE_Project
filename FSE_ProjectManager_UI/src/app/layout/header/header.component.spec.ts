import { TestBed, async, inject } from '@angular/core/testing';
import { HeaderComponent } from './header.component';
import { SettingsService } from '../../core/settings/settings.service';
import { MenuService } from '../../core/menu/menu.service';

//import { FormsModule, ReactiveFormsModule } from '@angular/forms';
//import { RouterTestingModule } from '@angular/router/testing';
//import { HttpClientModule } from '@angular/common/http';
//// import { DialogService } from 'primeng/api';
////import { Component, OnInit, ViewChild } from '@angular/core';

describe('Component: Header', () => {
    beforeEach(() => {
        TestBed.configureTestingModule({  
           //imports: [FormsModule,ReactiveFormsModule, RouterTestingModule,HttpClientModule],    
           //providers: [MenuService,  SettingsService]
           //providers: [ SettingsService]
        }).compileComponents();
    });

    //it('should create an instance', async(inject([MenuService,  SettingsService], (menuService, userblockService, settingsService) => {
    it('should create an instance', async(inject([MenuService,  SettingsService], (menuService: MenuService, userblockService: SettingsService, settingsService: SettingsService) => {
        let component = new HeaderComponent(menuService, userblockService);
        expect(component).toBeTruthy();
    })));   

  });

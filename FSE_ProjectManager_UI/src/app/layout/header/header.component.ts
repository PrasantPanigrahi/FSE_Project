import { DialogService } from 'primeng/api';

import { Component, OnInit, ViewChild } from '@angular/core';

import { MenuService } from '../../core/menu/menu.service';
import { SettingsService } from '../../core/settings/settings.service';

declare var $: any;

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss'],
  providers: [DialogService]
})
export class HeaderComponent implements OnInit {

  navCollapsed = true; // for horizontal layout
  menuItems = []; // for horizontal layout

  isNavSearchVisible: boolean;

  isWarehouse: boolean;
  isCreditLimit: boolean;

  constructor(public menu: MenuService,
    public settings: SettingsService) { }

  ngOnInit() {
    this.isNavSearchVisible = false;
    this.isWarehouse = true;
    this.isCreditLimit = false;
  }

  toggleUserBlock(event) {
    event.preventDefault();
  }

  openNavSearch(event) {
    event.preventDefault();
    event.stopPropagation();
    this.setNavSearchVisible(true);
  }

  setNavSearchVisible(stat: boolean) {
    // console.log(stat);
    this.isNavSearchVisible = stat;
  }

  getNavSearchVisible() {
    return this.isNavSearchVisible;
  }

  toggleOffSidebar() {
    this.settings.toggleLayoutSetting('offsidebarOpen');
  }

  toggleCollapsedSidebar() {
    this.settings.toggleLayoutSetting('isCollapsed');
  }

  isCollapsedText() {
    return this.settings.getLayoutSetting('isCollapsedText');
  }

  toggleFullScreen(event) {
  }
}

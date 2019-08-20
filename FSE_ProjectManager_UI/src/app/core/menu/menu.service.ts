import { Injectable } from '@angular/core';
import {
  Menu, menus
} from './menu';

@Injectable({
  providedIn: 'root'
})
export class MenuService {

  constructor() {
  }

  addMenu() {
    // not requires
  }

  getMenu(): Array<Menu> {

    const displayMenus: Array<Menu> = [];
    displayMenus.push(menus.users);
    displayMenus.push(menus.projects);
    displayMenus.push(menus.tasks);
    return displayMenus;
  }
}

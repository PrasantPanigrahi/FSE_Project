import { MenuItem } from './menu-item';
export interface MenuItem {
  text: string;
  heading?: boolean;
  link?: string;     // internal route links
  elink?: string;    // used only for external links
  target?: string;   // anchor target="_blank|_self|_parent|_top|frame-name"
  icon?: string;
  alert?: string;
  submenu?: Array<MenuItem>;
  label?: string;
  translate?: string;
}

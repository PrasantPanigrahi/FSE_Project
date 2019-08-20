export interface Menu {
  text: string;
  heading?: boolean;
  link?: string;     // internal route links
  elink?: string;    // used only for external links
  target?: string;   // anchor target="_blank|_self|_parent|_top|framename"
  icon?: string;
  alert?: string;
  submenu?: Array<Menu>;
}


const Users = {
  text: 'Users',
  link: '/user/list',
  icon: 'fas fa-user',
};

const Projects = {
  text: 'Projects',
  link: '/project/list',
  icon: 'fas fa-project-diagram'
};

const Tasks = {
  text: 'Tasks',
  link: '/task/list',
  icon: 'fa fa-tasks'
};

export const menus = {
  users: Users,
  tasks: Tasks,
  projects: Projects,
};

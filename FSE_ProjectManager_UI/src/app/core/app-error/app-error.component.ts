import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/api';

import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-app-error',
  templateUrl: './app-error.component.html',
  styleUrls: ['./app-error.component.scss']
})
export class AppErrorComponent implements OnInit {

  error: any;

  constructor(public ref: DynamicDialogRef, public config: DynamicDialogConfig) { }

  ngOnInit() {
    this.error = this.config.data;
  }

}

import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/api';
import { common } from 'src/app/core/common';

import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-remark-dialog',
  templateUrl: './remark-dialog.component.html',
  styleUrls: ['./remark-dialog.component.scss']
})
export class RemarkDialogComponent implements OnInit {

  label: string;
  remark: string;
  isRequired: boolean;
  requiredErrorMessage: string;
  invalid: boolean;

  constructor(public ref: DynamicDialogRef,
    public config: DynamicDialogConfig) { }

  ngOnInit() {
    this.invalid = false;
    this.label = this.config.data.label;
    this.requiredErrorMessage = this.config.data.requiredErrorMessage;
    if (!common.isNil(this.requiredErrorMessage)) {
      this.isRequired = true;
    }
  }

  confirm(action: string) {
    if (this.isRequired && common.isNil(this.remark)) {
      this.invalid = true;
    } else {
      this.ref.close({
        action: action,
        remarks: this.remark
      });
    }
  }
}

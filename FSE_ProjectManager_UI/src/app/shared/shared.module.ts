import { TabsModule } from 'ngx-bootstrap';
import { AutoCompleteModule } from 'primeng/autocomplete';
import { CalendarModule } from 'primeng/calendar';
import { CardModule } from 'primeng/card';
import { CheckboxModule } from 'primeng/checkbox';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { ContextMenuModule } from 'primeng/contextmenu';
import { DataViewModule } from 'primeng/dataview';
import { DialogModule } from 'primeng/dialog';
import { DropdownModule } from 'primeng/dropdown';
import { DynamicDialogModule } from 'primeng/dynamicdialog';
import { EditorModule } from 'primeng/editor';
import { FileUploadModule } from 'primeng/fileupload';
import { InputMaskModule } from 'primeng/inputmask';
import { InputTextModule } from 'primeng/inputtext';
import { KeyFilterModule } from 'primeng/keyfilter';
import { ListboxModule } from 'primeng/listbox';
import { MessageModule } from 'primeng/message';
import { MessagesModule } from 'primeng/messages';
import { MultiSelectModule } from 'primeng/multiselect';
import { ProgressSpinnerModule } from 'primeng/progressspinner';
import { RadioButtonModule } from 'primeng/radiobutton';
import { SelectButtonModule } from 'primeng/selectbutton';
import { SliderModule } from 'primeng/slider';
import { SpinnerModule } from 'primeng/spinner';
import { SplitButtonModule } from 'primeng/splitbutton';
import { TableModule } from 'primeng/table';
import { TabMenuModule } from 'primeng/tabmenu';
import { ToastModule } from 'primeng/toast';
import { ToggleButtonModule } from 'primeng/togglebutton';
import { ToolbarModule } from 'primeng/toolbar';

import { CommonModule } from '@angular/common';
import { ModuleWithProviders } from '@angular/compiler/src/core';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { TranslateModule } from '@ngx-translate/core';
import { RemarkDialogComponent } from './remark-dialog/remark-dialog.component';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,

    /** ngx-bootstrap */
    TabsModule.forRoot(),

    /** PrimeNG */
    CalendarModule,
    DropdownModule,
    SliderModule,
    CheckboxModule,
    RadioButtonModule,
    SpinnerModule,
    SelectButtonModule,
    ListboxModule,
    ToastModule,
    EditorModule,
    InputTextModule,
    MessageModule,
    MessagesModule,
    ConfirmDialogModule,
    TableModule,
    DialogModule
  ],
  exports: [
    CommonModule,
    RouterModule,
    FormsModule,
    ReactiveFormsModule,
    TranslateModule,

    /** ngx-bootstrap */
    TabsModule,

    /** PrimeNG */
    CalendarModule,
    AutoCompleteModule,
    DropdownModule,
    SliderModule,
    SpinnerModule,
    ToggleButtonModule,
    ToastModule,
    EditorModule,
    TabMenuModule,
    InputTextModule,
    MessageModule,
    MessagesModule,
    ProgressSpinnerModule,
    ConfirmDialogModule,
    TableModule,
    ToolbarModule,
    DialogModule,
    CheckboxModule,
  ],
  declarations: [
    RemarkDialogComponent,
  ],
  entryComponents: [
    RemarkDialogComponent,
  ]
})
export class SharedModule {
  static forRoot(): ModuleWithProviders {
    return {
      ngModule: SharedModule
    };
  }
}

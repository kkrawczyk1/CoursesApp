import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatTableModule } from '@angular/material/table';
import { MatToolbarModule } from '@angular/material/toolbar';

import { ConfirmDialog } from './confirm-dialog/confirm-dialog';

const materialModules = [
  MatInputModule,
  MatFormFieldModule,
  MatButtonModule,
  MatToolbarModule,
  MatIconModule,
  MatDialogModule,
  MatTableModule,
  MatProgressSpinnerModule,
];

@NgModule({
  imports: [ReactiveFormsModule, FormsModule, CommonModule, ...materialModules],
  exports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    ...materialModules,
  ],
  declarations: [ConfirmDialog],
  entryComponents: [ConfirmDialog],
  providers: [],
})
export class SharedModule {}

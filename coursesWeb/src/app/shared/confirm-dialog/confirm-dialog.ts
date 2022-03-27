import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';

export class ConfirmDialogData {
  public title: string;
  public content: string;

  constructor(title: string, content: string) {
    this.title = title;
    this.content = content;
  }
}
@Component({
  selector: 'confirm-dialog',
  templateUrl: 'confirm-dialog.html',
  styleUrls: ['confirm-dialog.scss'],
})
export class ConfirmDialog {
  constructor(
    public dialogRef: MatDialogRef<ConfirmDialog>,
    @Inject(MAT_DIALOG_DATA) public data: ConfirmDialogData
  ) {}

  public abort() {
    this.dialogRef.close();
  }

  public save() {
    this.dialogRef.close(true);
  }
}

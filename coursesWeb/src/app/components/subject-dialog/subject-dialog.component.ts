import { Component, Inject } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { Observable } from 'rxjs';
import { BehaviorSubject } from 'rxjs/internal/BehaviorSubject';
import { SubjectDTO } from 'src/app/core/service-proxies';

export interface DialogData {
  subject: SubjectDTO;
  modalState: ModalState;
  saveAction: (outData: SubjectDTO) => Observable<SubjectDTO>;
}

export enum ModalState {
  Create = 0,
  Edit = 1,
}

@Component({
  templateUrl: './subject-dialog.component.html',
  styleUrls: ['./subject-dialog.component.scss'],
})
export class SubjectDialog {
  #inParamData: DialogData;
  modalState: ModalState;
  subject: SubjectDTO;
  dialogTopic$ = new BehaviorSubject<string>('');

  get subjectNameControl() {
    return this.form.get('subjectName');
  }
  get subjectNumberControl() {
    return this.form.get('subjectNumber');
  }

  constructor(
    public dialogRef: MatDialogRef<SubjectDialog>,
    @Inject(MAT_DIALOG_DATA) public data: DialogData
  ) {
    this.setInitData(data);
  }

  form = new FormGroup({
    // Randomowe id dodane tylko na czas operacji EDYCJI tematu zajęć (course-dialog.component.ts:158)
    // ponieważ zanim trafi do bazy jest nullem i nie można rozróżnić tematów (2 pola i 2 edytowalne)
    // przed requestem do API zostaje wyczyszczone(course-dialog.component.ts:158).
    id: new FormControl(Math.floor(Math.random() * 100).toString()),
    subjectName: new FormControl('', [
      Validators.required,
      Validators.maxLength(40),
    ]),
    subjectNumber: new FormControl(null, Validators.required),
  });

  setInitData(data: DialogData) {
    this.#inParamData = data;
    this.modalState = data.modalState;
    this.subject = data.subject;
    this.setDialogTitle(this.modalState);
  }

  ngOnInit() {
    if (this.modalState === ModalState.Edit) {
      this.setForm();
    }
  }

  setForm() {
    this.form.patchValue(this.subject);
  }
  save() {
    this.#inParamData.saveAction(this.form.getRawValue()).subscribe();
    this.dialogRef.close();
  }
  abort() {
    this.dialogRef.close();
  }

  private setDialogTitle(modalState: ModalState) {
    const dialogTopics: Record<ModalState, string> = {
      [ModalState.Create]: 'Nowy temat',
      [ModalState.Edit]: 'Edycja tematu',
    };
    this.dialogTopic$.next(dialogTopics[modalState]);
  }
}

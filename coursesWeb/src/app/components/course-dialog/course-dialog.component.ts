import { Component, Inject } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialog, MatDialogRef } from '@angular/material/dialog';
import { MatTableDataSource } from '@angular/material/table';
import { filter, of, tap } from 'rxjs';
import { BehaviorSubject } from 'rxjs/internal/BehaviorSubject';
import { CourseDTO, CoursesApi, SubjectDTO } from 'src/app/core/service-proxies';

import { CoursesService } from '../courses.service';
import { SubjectDialog } from '../subject-dialog/subject-dialog.component';

export interface DialogData {
  courseId: number;
  modalState: ModalState;
}

export enum ModalState {
  Create = 0,
  Edit = 1,
}

@Component({
  templateUrl: './course-dialog.component.html',
  styleUrls: ['./course-dialog.component.scss'],
})
export class CourseDialog {
  displayedColumns: string[] = ['subjectName', 'subjectNumber', 'action'];
  isLoading = true;
  course: CourseDTO;
  courseId: number;
  modalState: ModalState;
  modalStateEnum = ModalState;
  dialogTopic$ = new BehaviorSubject<string>('');
  subjectsTemp: MatTableDataSource<SubjectDTO>;

  get nameControl() {
    return this.form.get('name');
  }
  get descriptionControl() {
    return this.form.get('description');
  }

  constructor(
    public dialogRef: MatDialogRef<CourseDialog>,
    private readonly coursesApi: CoursesApi,
    public dialog: MatDialog,
    private readonly coursesService: CoursesService,
    @Inject(MAT_DIALOG_DATA) public data: DialogData
  ) {
    this.setInitData(data);
    this.subjectsTemp = new MatTableDataSource();
  }

  form = new FormGroup({
    name: new FormControl('', [Validators.required, Validators.maxLength(30)]),
    description: new FormControl(''),
  });

  setInitData(data: DialogData) {
    this.modalState = data.modalState;
    this.courseId = data.courseId;
    this.setDialogTitle(this.modalState);
  }

  ngOnInit() {
    if (this.modalState === ModalState.Edit) {
      this.loadCourse();
    }
  }

  loadCourse() {
    this.coursesApi.getById(this.data.courseId).subscribe((response) => {
      this.course = response.result;
      this.setForm();
      this.isLoading = false;
    });
  }

  setForm() {
    this.form.patchValue(this.course);
    this.course.subjects.forEach((subject) => {
      this.subjectsTemp.data.push(subject);
      this.subjectsTemp._updateChangeSubscription();
    });
  }

  addSubject() {
    this.dialog.open(SubjectDialog, {
      width: '40vw',
      maxHeight: '80vh',
      data: {
        modalState: ModalState.Create,
        saveAction: (outData: SubjectDTO) =>
          of(outData).pipe(
            filter((onClose) => !!onClose),
            tap(() => this.addSubjectToTable(outData))
          ),
      },
    });
  }

  editSubject(subject: SubjectDTO) {
    this.dialog.open(SubjectDialog, {
      width: '40vw',
      maxHeight: '80vh',
      data: {
        modalState: ModalState.Edit,
        subject,
        saveAction: (outData: SubjectDTO) =>
          of(outData).pipe(
            filter((onClose) => !!onClose),
            tap(() => this.addEditSubjectToTable(outData))
          ),
      },
    });
  }
  deleteSubject(subject: SubjectDTO) {
    const index = this.subjectsTemp.data.indexOf(subject);
    this.subjectsTemp.data.splice(index, 1);
    this.subjectsTemp._updateChangeSubscription();
  }

  save() {
    let course = new CourseDTO();
    course = this.form.getRawValue();
    this.clearIdsInSubjectsTemp();
    course.subjects = this.subjectsTemp.data;

    if (this.modalState === this.modalStateEnum.Create) {
      this.coursesApi.create(course as CourseDTO).subscribe(() => {
        this.dialogRef.close();
        return this.coursesService.reloadGrid$.next(true);
      });
    } else {
      this.coursesApi.edit(this.courseId, course as CourseDTO).subscribe(() => {
        this.dialogRef.close();
        return this.coursesService.reloadGrid$.next(true);
      });
    }
  }
  abort() {
    this.dialogRef.close();
  }

  private setDialogTitle(modalState: ModalState) {
    const dialogTopics: Record<ModalState, string> = {
      [ModalState.Create]: 'Nowy kurs',
      [ModalState.Edit]: 'Edycja kursu',
    };
    this.dialogTopic$.next(dialogTopics[modalState]);
  }
  private addSubjectToTable(subjectModel: SubjectDTO) {
    let subject = new SubjectDTO();

    subject.id = subjectModel.id;
    subject.subjectName = subjectModel.subjectName;
    subject.subjectNumber = subjectModel.subjectNumber;

    this.subjectsTemp.data.push(subject);
    this.subjectsTemp._updateChangeSubscription();
  }
  private addEditSubjectToTable(subjectDTO: SubjectDTO) {
    const { subjectName, subjectNumber } = subjectDTO;
    let subject = this.subjectsTemp.data.find(
      (x) => x.id === subjectDTO.id
    ) as SubjectDTO;

    subject.subjectName = subjectName;
    subject.subjectNumber = subjectNumber;

    this.subjectsTemp._updateChangeSubscription();
  }
  private clearIdsInSubjectsTemp() {
    this.subjectsTemp.data.forEach((subject) => {
      const subjectId: any = subject.id;
      if (typeof subjectId === 'string' || subjectId instanceof String) {
        subject.id = null;
      }
    });
  }
}

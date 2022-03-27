import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { CourseDTO, CoursesApi, SubjectDTO } from 'src/app/core/service-proxies';

export interface DialogData {
  courseId: number;
}

@Component({
  templateUrl: './course-detail-dialog.component.html',
  styleUrls: ['./course-detail-dialog.component.scss'],
})
export class CourseDetailsDialog {
  displayedColumns: string[] = ['subjectName', 'subjectNumber'];
  isLoading = true;
  course: CourseDTO;
  subjects: SubjectDTO[];

  constructor(
    public dialogRef: MatDialogRef<CourseDetailsDialog>,
    private coursesApi: CoursesApi,
    @Inject(MAT_DIALOG_DATA) public data: DialogData
  ) {}

  ngOnInit() {
    this.coursesApi.getById(this.data.courseId).subscribe((response) => {
      this.course = response.result;
      this.subjects = response.result.subjects;
      this.isLoading = false;
    });
  }
}

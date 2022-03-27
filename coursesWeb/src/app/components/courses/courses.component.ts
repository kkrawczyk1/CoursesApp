import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { CourseDTO, CoursesApi } from 'src/app/core/service-proxies';
import { ConfirmDialog, ConfirmDialogData } from 'src/app/shared/confirm-dialog/confirm-dialog';

import { CourseDetailsDialog } from '../course-detail-dialog/course-detail-dialog.component';
import { CourseDialog, ModalState } from '../course-dialog/course-dialog.component';
import { CoursesService } from '../courses.service';

@Component({
  selector: 'courses',
  templateUrl: './courses.component.html',
  styleUrls: ['./courses.component.scss'],
})
export class CoursesComponent implements OnInit {
  displayedColumns: string[] = ['name', 'description', 'action'];
  courses: CourseDTO[] = [];
  isLoading = true;

  constructor(
    private coursesApi: CoursesApi,
    public dialog: MatDialog,
    private readonly coursesService: CoursesService
  ) {}

  ngOnInit(): void {
    this.loadData();

    this.coursesService.reloadGrid$.subscribe(() => {
      this.isLoading = true;
      this.loadData();
    });
  }

  loadData() {
    this.coursesApi.getAll().subscribe((response) => {
      this.courses = response.result;
      this.isLoading = false;
    });
  }

  viewDetails(courseId: number) {
    this.dialog.open(CourseDetailsDialog, {
      width: '85vw',
      maxHeight: '80vh',
      data: {
        courseId: courseId,
      },
    });
  }

  addCourse() {
    this.dialog.open(CourseDialog, {
      width: '55vw',
      maxHeight: '80vh',
      data: {
        modalState: ModalState.Create,
      },
    });
  }

  editCourse(courseId: number) {
    this.dialog.open(CourseDialog, {
      width: '55vw',
      maxHeight: '80vh',
      data: {
        courseId: courseId,
        modalState: ModalState.Edit,
      },
    });
  }

  deleteCourse(courseId: number) {
    const dialogRef = this.dialog.open(ConfirmDialog, {
      width: '700px',
      maxHeight: '20vh',
      data: new ConfirmDialogData(
        'Potwierdzenie',
        'Czy na pewno chcesz usunąć kurs?'
      ),
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result != undefined && result === true) {
        this.coursesApi.delete(courseId).subscribe((_) => {
          return this.coursesService.reloadGrid$.next(true);
        });
      }
    });
  }
}

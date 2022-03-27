import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './components/app.component';
import { CourseDetailsDialog } from './components/course-detail-dialog/course-detail-dialog.component';
import { CourseDialog } from './components/course-dialog/course-dialog.component';
import { CoursesService } from './components/courses.service';
import { CoursesComponent } from './components/courses/courses.component';
import { SubjectDialog } from './components/subject-dialog/subject-dialog.component';
import { API_BASE_URL } from './core/service-proxies';
import { SwaggerModule } from './core/services/swagger.module';
import { SharedModule } from './shared/shared.module';

@NgModule({
  declarations: [
    AppComponent,
    CoursesComponent,
    CourseDetailsDialog,
    CourseDialog,
    SubjectDialog,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    SharedModule,
    SwaggerModule,
  ],
  exports: [SharedModule],
  entryComponents: [CourseDetailsDialog],
  providers: [
    { provide: API_BASE_URL, useFactory: getBaseApiUrl },
    CoursesService,
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
export function getBaseApiUrl() {
  return 'https://localhost:44321';
}

import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable()
export class CoursesService {
  reloadGrid$ = new BehaviorSubject<boolean>(false);
}

import { NgModule } from '@angular/core';

import * as swagger from '../service-proxies';

@NgModule({
  providers: [swagger.CoursesApi],
})
export class SwaggerModule {}

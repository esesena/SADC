import { EmployeeComponent } from './../../pages/employee/employee.component';
import { PlantingComponent } from './../../pages/planting/planting.component';
import { SeedComponent } from './../../pages/seed/seed.component';
import { AccountService } from './../../services/account.service';
import { JwtInterceptor } from './../../interceptors/jwt.interceptor';
import { FarmDetailsComponent } from './../../pages/farm/farm-details/farm-details.component';
import { FarmListComponent } from './../../pages/farm/farm-list/farm-list.component';
import { FarmComponent } from './../../pages/farm/farm.component';
import { FarmService } from './../../services/farm.service';
import { NgModule } from '@angular/core';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { ClipboardModule } from 'ngx-clipboard';
import { NgbModule, NgbPaginationModule } from '@ng-bootstrap/ng-bootstrap';

import { AdminLayoutRoutes } from './admin-layout.routing';
import { DashboardComponent } from '../../pages/dashboard/dashboard.component';
import { IconsComponent } from '../../pages/icons/icons.component';
import { MapsComponent } from '../../pages/maps/maps.component';
import { UserProfileComponent } from '../../pages/user-profile/user-profile.component';
import { TablesComponent } from '../../pages/tables/tables.component';
import { ToastrModule } from 'ngx-toastr';

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(AdminLayoutRoutes),
    FormsModule,
    HttpClientModule,
    NgbModule,
    ClipboardModule,
    NgbPaginationModule,
    ReactiveFormsModule,
    ToastrModule.forRoot({
      timeOut: 4000,
      positionClass: 'toast-bottom-right',
      preventDuplicates: true,
      progressBar: true,
    }),
  ],
  declarations: [
    DashboardComponent,
    UserProfileComponent,
    TablesComponent,
    IconsComponent,
    MapsComponent,
    FarmComponent,
    FarmListComponent,
    FarmDetailsComponent,
    SeedComponent,
    PlantingComponent,
    EmployeeComponent,
  ],
  providers: [
    FarmService,
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
  ],
})

export class AdminLayoutModule {}

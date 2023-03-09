import { NgbDateAdapter, NgbDateParserFormatter } from '@ng-bootstrap/ng-bootstrap';
import { NgMultiSelectDropDownModule } from 'ng-multiselect-dropdown';
import { PlantingDetailsComponent } from './../../pages/planting/planting-details/planting-details.component';
import { PlantingListComponent } from './../../pages/planting/planting-list/planting-list.component';
import { SeedDetailsComponent } from './../../pages/seed/seed-details/seed-details.component';
import { SeedListComponent } from './../../pages/seed/seed-list/seed-list.component';
import { EmployeeService } from './../../services/employee.service';
import { RegisterComponent } from './../../pages/register/register.component';
import { NgModule } from '@angular/core';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { GoogleMapsModule } from '@angular/google-maps';

import { ClipboardModule } from 'ngx-clipboard';
import { ToastrModule } from 'ngx-toastr';

import { NgbAccordionModule, NgbDatepickerModule, NgbNavModule, NgbPaginationModule } from '@ng-bootstrap/ng-bootstrap';
import { NgbCollapseModule, NgbModule, } from '@ng-bootstrap/ng-bootstrap';

import { JwtInterceptor } from './../../interceptors/jwt.interceptor';

import { AdminLayoutRoutes } from './admin-layout.routing';

import { NavbarComponent } from './../../components/navbar/navbar.component';
import { CardComponent } from './../../components/card/card.component';

import { EmployeeComponent } from './../../pages/employee/employee.component';
import { EmployeeListComponent } from './../../pages/employee/employee-list/employee-list.component';
import { EmployeeDetailsComponent } from './../../pages/employee/employee-details/employee-details.component';
import { FarmComponent } from './../../pages/farm/farm.component';
import { FarmListComponent } from './../../pages/farm/farm-list/farm-list.component';
import { FarmDetailsComponent } from './../../pages/farm/farm-details/farm-details.component';
import { SeedComponent } from './../../pages/seed/seed.component';
import { PlantingComponent } from './../../pages/planting/planting.component';

import { FarmService } from './../../services/farm.service';

import { DashboardComponent } from '../../pages/dashboard/dashboard.component';
import { IconsComponent } from '../../pages/icons/icons.component';
import { MapsComponent } from '../../pages/maps/maps.component';
import { UserProfileComponent } from '../../pages/user-profile/user-profile.component';
import { TablesComponent } from '../../pages/tables/tables.component';

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(AdminLayoutRoutes),
    FormsModule,
    HttpClientModule,
    NgbModule,
    ClipboardModule,
    ReactiveFormsModule,
    NgbPaginationModule,
    GoogleMapsModule,
    NgbCollapseModule,
    NgbAccordionModule,
    NgbNavModule,
    NgbDatepickerModule,
    NgMultiSelectDropDownModule.forRoot(),
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
    SeedListComponent,
    SeedDetailsComponent,
    PlantingComponent,
    PlantingListComponent,
    PlantingDetailsComponent,
    EmployeeComponent,
    EmployeeListComponent,
    EmployeeDetailsComponent,
    CardComponent,
    NavbarComponent,
    RegisterComponent,
  ],
  providers: [
    FarmService,
    EmployeeService,
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
  ],
})

export class AdminLayoutModule {}

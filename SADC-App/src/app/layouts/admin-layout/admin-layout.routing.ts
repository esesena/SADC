import { FarmListComponent } from './../../pages/farm/farm-list/farm-list.component';
import { FarmDetailsComponent } from './../../pages/farm/farm-details/farm-details.component';
import { FarmComponent } from './../../pages/farm/farm.component';
import { Routes } from '@angular/router';

import { DashboardComponent } from '../../pages/dashboard/dashboard.component';
import { IconsComponent } from '../../pages/icons/icons.component';
import { MapsComponent } from '../../pages/maps/maps.component';
import { UserProfileComponent } from '../../pages/user-profile/user-profile.component';
import { TablesComponent } from '../../pages/tables/tables.component';

export const AdminLayoutRoutes: Routes = [
    { path: 'dashboard',       component: DashboardComponent },
    { path: 'user-profile',    component: UserProfileComponent },
    { path: 'tables',          component: TablesComponent },
    { path: 'icons',           component: IconsComponent },
    { path: 'maps',            component: MapsComponent },
    { path: 'fazenda',         component: FarmComponent },
    { path: 'fazenda/lista',         redirectTo: 'fazenda' },
    { path: 'fazenda/lista', component: FarmListComponent },
    { path: 'fazenda/detalhe', component: FarmDetailsComponent },
    { path: 'fazenda/detalhe/:id', component: FarmDetailsComponent },
];

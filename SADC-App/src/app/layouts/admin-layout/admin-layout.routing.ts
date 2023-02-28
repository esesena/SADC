import { EmployeeComponent } from './../../pages/employee/employee.component';
import { PlantingComponent } from './../../pages/planting/planting.component';
import { SeedComponent } from './../../pages/seed/seed.component';
import { AuthGuard } from "./../../guard/auth.guard";
import { Routes } from "@angular/router";

import { DashboardComponent } from "../../pages/dashboard/dashboard.component";
import { IconsComponent } from "../../pages/icons/icons.component";
import { MapsComponent } from "../../pages/maps/maps.component";
import { UserProfileComponent } from "../../pages/user-profile/user-profile.component";
import { TablesComponent } from "../../pages/tables/tables.component";
import { FarmListComponent } from "./../../pages/farm/farm-list/farm-list.component";
import { FarmDetailsComponent } from "./../../pages/farm/farm-details/farm-details.component";
import { FarmComponent } from "./../../pages/farm/farm.component";

export const AdminLayoutRoutes: Routes = [
  {
    path: "",
    runGuardsAndResolvers: "always",
    canActivate: [AuthGuard],
    children: [
      { path: "dashboard", component: DashboardComponent },
      { path: "user-profile", component: UserProfileComponent },
      { path: "tables", component: TablesComponent },
      { path: "icons", component: IconsComponent },
      { path: "maps", component: MapsComponent },
      { path: "fazenda", redirectTo: "fazenda/lista" },
      {
        path: "fazenda",
        component: FarmComponent,
        children: [
          { path: "lista", component: FarmListComponent },
          { path: "detalhe", component: FarmDetailsComponent },
          { path: "detalhe/:id", component: FarmDetailsComponent },
        ],
      },
      { path: "sementes", component: SeedComponent },
      { path: "plantacoes", component: PlantingComponent },
      { path: "funcionarios", component:  EmployeeComponent},

    ],
  },
];

import { PlantingDetailsComponent } from "./../../pages/planting/planting-details/planting-details.component";
import { PlantingListComponent } from "./../../pages/planting/planting-list/planting-list.component";
import { SeedDetailsComponent } from "./../../pages/seed/seed-details/seed-details.component";
import { SeedListComponent } from "./../../pages/seed/seed-list/seed-list.component";
import { EmployeeDetailsComponent } from "./../../pages/employee/employee-details/employee-details.component";
import { EmployeeListComponent } from "./../../pages/employee/employee-list/employee-list.component";
import { EmployeeComponent } from "./../../pages/employee/employee.component";
import { PlantingComponent } from "./../../pages/planting/planting.component";
import { SeedComponent } from "./../../pages/seed/seed.component";
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
      { path: "funcionarios", redirectTo: "funcionarios/lista" },
      {
        path: "funcionarios",
        component: EmployeeComponent,
        children: [
          { path: "lista", component: EmployeeListComponent },
          { path: "detalhe", component: EmployeeDetailsComponent },
          { path: "detalhe/:id", component: EmployeeDetailsComponent },
        ],
      },
      { path: "sementes", redirectTo: "sementes/lista" },
      {
        path: "sementes",
        component: SeedComponent,
        children: [
          { path: "lista", component: SeedListComponent },
          { path: "detalhe", component: SeedDetailsComponent },
          { path: "detalhe/:id", component: SeedDetailsComponent },
        ],
      },
      { path: "plantacoes", redirectTo: "plantacoes/lista" },
      {
        path: "plantacoes",
        component: PlantingComponent,
        children: [
          { path: "lista", component: PlantingListComponent },
          { path: "detalhe", component: PlantingDetailsComponent },
          { path: "detalhe/:id", component: PlantingDetailsComponent },
        ],
      },
    ],
  },
];

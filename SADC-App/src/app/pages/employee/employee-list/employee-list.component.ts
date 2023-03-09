import { environment } from './../../../../environments/environment';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { EmployeeService } from './../../../services/employee.service';
import { Subject, debounceTime } from 'rxjs';
import { Pagination, PaginatedResult } from './../../../models/Pagination';
import { Employee } from './../../../models/Employee';
import { Component, OnInit, TemplateRef } from '@angular/core';

@Component({
  selector: 'app-employee-list',
  templateUrl: './employee-list.component.html',
  styleUrls: ['./employee-list.component.scss']
})
export class EmployeeListComponent implements OnInit {

  public employees: Employee[] = [];
  public eventoId = 0;
  public pagination = {} as Pagination;

  constructor(
    private employeeService: EmployeeService,
    private toastr: ToastrService,
    private router: Router
  ) { }

  public ngOnInit(): void {
    this.pagination = {
      currentPage: 1,
      itemsPerPage: 3,
      totalItems: 1,
    } as Pagination;

    this.loadEmployees();
  }

  searchTermChanged: Subject<string> = new Subject<string>();

  public filterEmployees(evt: any): void {
    if (this.searchTermChanged.observers.length === 0) {
      this.searchTermChanged
        .pipe(debounceTime(1000))
        .subscribe((filter) => {        
          this.employeeService
            .getEmployees(
              this.pagination.currentPage,
              this.pagination.itemsPerPage,
              filter
            )
            .subscribe(
              (paginatedResult: PaginatedResult<Employee[]>) => {
                this.employees = paginatedResult.result;
                this.pagination = paginatedResult.pagination;
              },
              (error: any) => {                
                this.toastr.error('Erro ao Carregar os Palestrantes', 'Erro!');
              }
            );
        });
    }
    this.searchTermChanged.next(evt.value);
  }

  // public getImageURL(imageName: string): string {
  //   if (imageName)
  //     return environment.apiURL + `resources/perfil/${imageName}`;
  //   else
  //     return './assets/img/perfil.png';
  // }

  public loadEmployees(): void {  

    this.employeeService
      .getEmployees(this.pagination.currentPage, this.pagination.itemsPerPage)
      .subscribe(
        (paginatedResult: PaginatedResult<Employee[]>) => {
          this.employees = paginatedResult.result;
          this.pagination = paginatedResult.pagination;
        },
        (error: any) => {          
          this.toastr.error('Erro ao Carregar os Funcionários', 'Erro!');
        }
      );
  }

  employeeDetail(id: number): void {
    this.router.navigate([`funcionarios/detalhe/${id}`]);
  }


  public pageChange(page: number): void {
    this.pagination.currentPage = page;
    this.loadEmployees();
  }

}

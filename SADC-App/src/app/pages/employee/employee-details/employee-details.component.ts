import { PaginatedResult, Pagination } from './../../../models/Pagination';
import { FarmService } from './../../../services/farm.service';
import { Farm } from './../../../models/Farm';
import { UserUpdate } from "./../../../models/identity/UserUpdate";
import { environment } from "./../../../../environments/environment";
import { Employee } from "./../../../models/Employee";
import { ToastrService } from "ngx-toastr";
import { EmployeeService } from "./../../../services/employee.service";
import { ActivatedRoute, Router } from "@angular/router";
import { Component, EventEmitter, OnInit, Output } from "@angular/core";
import {
  AbstractControl,
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from "@angular/forms";
import { NgbCalendar, NgbDateAdapter } from "@ng-bootstrap/ng-bootstrap";

@Component({
  selector: "app-employee-details",
  templateUrl: "./employee-details.component.html",
  styleUrls: ["./employee-details.component.scss"],
})
export class EmployeeDetailsComponent implements OnInit {
  @Output() changeFormValue = new EventEmitter();

  employeeId: number;
  employee = {} as Employee;
  form: FormGroup;
  saveState = "post";
  file: File;
  imageURL = "assets/img/brand/upload.png";
  lastField = { id: 0, name: "", indice: 0 };
  public user = {} as UserUpdate;
  farms: Farm[] = [];
  public pagination = {} as Pagination;

  get editMode(): boolean {
    return this.saveState === "put";
  }

  get f(): any {
    return this.form.controls;
  }

  public get isUser(): boolean {
    return (
      this.employee.function === "Admin" ||
      this.employee.function === "Director" ||
      this.employee.function === "Support"
    );
  }

  constructor(
    private fb: FormBuilder,
    private activatedRouter: ActivatedRoute,
    private employeeService: EmployeeService,
    private farmService: FarmService,
    private toastr: ToastrService,
    private router: Router
  ) {}

  ngOnInit() {
    this.loadEmployee();
    this.validation();
    this.loadFarms();
  }

  public validation(): void {
    this.form = this.fb.group({
      firstName: ["", [Validators.required]],
      lastName: ["", [Validators.required]],
      cpf: ["", Validators.required],
      function: ["", Validators.required],
      workload: ["", Validators.required],
      birthDate: ["", Validators.required],
      address: ["", Validators.required],
      cep: ["", Validators.required],
      city: ["", Validators.required],
      state: ["", Validators.required],
      imageURL: ["", Validators.required],
      userId: ["", Validators.required],
      user: ["", Validators.required],
      farmId: ["", Validators.required],
    });
  }

  public loadFarms(): void {
    this.farmService
      .getFarms(this.pagination.currentPage, this.pagination.itemsPerPage)
      .subscribe(
        (paginatedResult: PaginatedResult<Farm[]>) => {
          this.farms = paginatedResult.result;
          this.pagination = paginatedResult.pagination;
          console.log("Fazendas: ", this.farms);
        },
        (error: any) => {
          this.toastr.error("Erro ao Carregar os Fazendas", "Erro!");
        }
      );
  }

  public cssValidator(campoForm: FormControl | AbstractControl): any {
    return { "is-invalid": campoForm.errors && campoForm.touched };
  }

  public returnEmpName(name: string): string {
    return name === null || name === "" ? "Nome do Funcionário" : name;
  }

  public loadEmployee(): void {
    this.employeeId = +this.activatedRouter.snapshot.paramMap.get("id");

    if (this.employeeId !== null && this.employeeId !== 0) {
      this.saveState = "put";

      this.employeeService.getEmployeeById(this.employeeId).subscribe(
        (employee: Employee) => {
          this.employee = { ...employee };
          this.form.patchValue(this.employee);
          if (this.employee.imageURL !== "") {
            this.imageURL =
              environment.apiURL +
              "resources/employeeimage/" +
              this.employee.imageURL;
          }
        },
        (error: any) => {
          this.toastr.error("Erro ao tentar carregar Funcionário.", "Erro!");
          console.error(error);
        }
      );
    }
  }

  public saveEmployee(): void {
    if (this.form.valid) {
      this.employee =
        this.saveState === "post"
          ? { ...this.form.value }
          : { id: this.employee.id, ...this.form.value };

      this.employeeService[this.saveState](this.employee).subscribe(
        (employeeRetorno: Employee) => {
          this.toastr.success("Funcionário salva com Sucesso!", "Sucesso");
          this.router.navigate([`funcionario/detalhe/${employeeRetorno.id}`]);
        },
        (error: any) => {
          console.error(error);
          this.toastr.error("Error ao salvar Funcionário", "Erro");
        }
      );
    }
  }

  onFileChange(ev: any): void {
    const reader = new FileReader();

    reader.onload = (event: any) => (this.imageURL = event.target.result);

    this.file = ev.target.files;
    reader.readAsDataURL(this.file[0]);

    this.uploadImage();
  }

  uploadImage(): void {
    this.employeeService.postUpload(this.employeeId, this.file).subscribe(
      () => {
        this.loadEmployee();
        this.toastr.success("Imagem atualizada com Sucesso", "Sucesso!");
      },
      (error: any) => {
        this.toastr.error("Erro ao fazer upload de imagem", "Erro!");
        console.log(error);
      }
    );
  }
}

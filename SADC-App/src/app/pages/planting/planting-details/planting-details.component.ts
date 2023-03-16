import { Farm } from "./../../../models/Farm";
import { FarmService } from "./../../../services/farm.service";
import { FieldService } from "./../../../services/field.service";
import { Field } from "./../../../models/Field";
import { Seed } from "src/app/models/Seed";
import { Pagination, PaginatedResult } from "./../../../models/Pagination";
import { Observable } from "rxjs";
import { SeedService } from "./../../../services/seed.service";
import { ToastrService } from "ngx-toastr";
import { PlantingService } from "./../../../services/planting.service";
import { ActivatedRoute, Router } from "@angular/router";
import {
  Validators,
  FormBuilder,
  FormGroup,
  FormControl,
  AbstractControl,
} from "@angular/forms";
import { Planting } from "./../../../models/Planting";
import { Component, OnInit } from "@angular/core";
import { IDropdownSettings } from "ng-multiselect-dropdown";
import { NgbDate, NgbDateStruct } from "@ng-bootstrap/ng-bootstrap";

@Component({
  selector: "app-planting-details",
  templateUrl: "./planting-details.component.html",
  styleUrls: ["./planting-details.component.scss"],
})
export class PlantingDetailsComponent implements OnInit {
  plantingId: number;
  planting = {} as Planting;
  form: FormGroup;
  saveState = "post";
  dateConverter: Date;

  seeds: Seed[] = [];
  farms: Farm[] = [];
  fields: Field[] = [];
  public pagination = {} as Pagination;

  selectedItems: any = [];
  dropdownSettings: any = {};

  get editMode(): boolean {
    return this.saveState === "put";
  }

  get f(): any {
    return this.form.controls;
  }

  constructor(
    private fb: FormBuilder,
    private activatedRouter: ActivatedRoute,
    private plantingService: PlantingService,
    private farmService: FarmService,
    private fieldService: FieldService,
    private seedService: SeedService,
    private toastr: ToastrService,
    private router: Router
  ) {}

  ngOnInit() {
    this.loadPlanting();
    this.validation();
    this.pagination = {
      currentPage: 1,
      itemsPerPage: 100,
      totalItems: 1,
    } as Pagination;
    this.loadSeeds();
    this.loadFarms();

    this.dropdownSettings = {
      singleSelection: false,
      idField: "id",
      textField: "name",
      selectAllText: "Marcar Todos",
      unSelectAllText: "Desmarcar Todos",
      itemsShowLimit: 5,
      allowSearchFilter: true,
    };

    // if (this.saveState == "put") {
    //   for (let i = 0; i < this.planting.fieldId.length; i++) {
    //     const item = this.planting.fieldId[i];
    //     if (!this.selectedItems.includes(item)) {
    //       this.selectedItems.push(item);
    //     }
    //   }
    //   console.log(this.selectedItems)
    // }
  }

  // onFilterChange(ev: any){

  // }

  // onItemSelect(item: any) {
  //   if (!this.selectedItems.includes(item.id)) {
  //     this.selectedItems.push(item.id);
  //   }
  //   this.planting.fieldId = this.selectedItems;
  // }

  // onItemDeSelect(item: any) {
  //   const index = this.selectedItems.indexOf(item.id);
  //   if (index !== -1) {
  //     this.selectedItems.splice(index, 1);
  //   }
  //   this.planting.fieldId = this.selectedItems;
  // }

  // onSelectAll(items: any) {
  //   for (let i = 0; i < items.length; i++) {
  //     const item = items[i];
  //     if (!this.selectedItems.includes(item.id)) {
  //       this.selectedItems.push(item.id);
  //     }
  //   }
  //   this.planting.fieldId = this.selectedItems;
  // }

  // onItemDeselectAll(items: any) {
  //   (this.selectedItems = []), (this.planting.fieldId = this.selectedItems);
  // }

  public validation(): void {
    this.form = this.fb.group({
      plantingDate: ["", Validators.required],
      harvest: ["", Validators.required],
      rainAmount: ["", Validators.required],
      typePlanting: ["", Validators.required],
      weatherPlanting: ["", Validators.required],
      airMoisture: ["", Validators.required],
      seedId: ["", Validators.required],
      seedAmount: ["", Validators.required],
      fertilizing: ["", Validators.required],
      farmId: ["", Validators.required],
      fieldId: [""],
    });
  }

  public cssValidator(campoForm: FormControl | AbstractControl): any {
    return { "is-invalid": campoForm.errors && campoForm.touched };
  }

  public returnPlantingName(description: string): string {
    return description === null || description === ""
      ? "Nome da semente"
      : description;
  }

  public loadFarms(): void {
    this.farmService
      .getFarms(this.pagination.currentPage, this.pagination.itemsPerPage)
      .subscribe(
        (paginatedResult: PaginatedResult<Farm[]>) => {
          this.farms = paginatedResult.result;
          this.pagination = paginatedResult.pagination;
          if (this.saveState == "put") {
            this.loadFields(this.planting.farm.id);
          }
          console.log("Fazendas: ", this.farms);
        },
        (error: any) => {
          this.toastr.error("Erro ao Carregar os Fazendas", "Erro!");
        }
      );
  }

  public loadSeeds(): void {
    this.seedService
      .getSeeds(this.pagination.currentPage, this.pagination.itemsPerPage)
      .subscribe(
        (paginatedResult: PaginatedResult<Seed[]>) => {
          this.seeds = paginatedResult.result;
          this.pagination = paginatedResult.pagination;
          console.log("Sementes: ", this.seeds);
        },
        (error: any) => {
          this.toastr.error("Erro ao Carregar os Sementes", "Erro!");
        }
      );
  }

  public loadPlanting(): void {
    this.plantingId = +this.activatedRouter.snapshot.paramMap.get("id");

    if (this.plantingId !== null && this.plantingId !== 0) {
      this.saveState = "put";
      this.plantingService.getPlantingById(this.plantingId).subscribe(
        (planting: Planting) => {
          this.planting = { ...planting };
          this.form.patchValue(this.planting);
        },
        (error: any) => {
          this.toastr.error("Erro ao tentar carregar Funcionário.", "Erro!");
          console.error(error);
        }
      );
    }
  }

  public loadFields(farmId: any): void {
    if (farmId >= 1) {
      this.fieldService.getFieldsByFarmId(farmId).subscribe(
        (fieldsRetorno: Field[]) => {
          this.fields = fieldsRetorno;
          console.log("Talhões: ", this.fields);
        },
        (error: any) => {
          this.toastr.error("Erro ao tentar carregar talhões", "Erro");
          console.error(error);
        }
      );
    } else {
      this.toastr.error("Fazenda não selecionada", "Erro");
    }
  }

  public savePlanting(): void {
    if (this.form.valid) {
      this.planting =
        this.saveState === "post"
          ? { ...this.form.value }
          : { id: this.planting.id, ...this.form.value };

      this.plantingService[this.saveState](
        this.planting.farm,
        this.planting.seed,
        this.planting
      ).subscribe(
        (plantingRetorno: Planting) => {
          this.toastr.success("Plantio salva com Sucesso!", "Sucesso");
          this.router.navigate([`plantacoes/lista `]);
        },
        (error: any) => {
          console.error(error);
          this.toastr.error("Error ao salvar evento", "Erro");
        }
      );
    }
  }
}

import { Farm } from './../../../models/Farm';
import { FarmService } from './../../../services/farm.service';
import { PlotService } from './../../../services/plot.service';
import { Plot } from './../../../models/Plot';
import { Pagination, PaginatedResult } from './../../../models/Pagination';
import { Observable } from 'rxjs';
import { SeedService } from './../../../services/seed.service';
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
import { Seed } from 'src/app/models/Seed';

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

  seeds: Seed[] = [];
  farms: Farm[] = [];
  plots: Plot[] = [];
  public pagination = {} as Pagination;

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
    private plotService: PlotService,
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
    this.loadFarms()
  }

  public validation(): void {
    this.form = this.fb.group({
      plantingDate: ["", Validators.required],
      harvest: ["", Validators.required],
      rainAmount: ["", Validators.required],
      typePlanting: ["", Validators.required],
      weatherPlanting: ["", Validators.required],
      airMoisture: ["", Validators.required],
      seed: ["", Validators.required],
      seedAmount: ["", Validators.required],
      fertilizing: ["", Validators.required],
      farm: ["", Validators.required],
      plot: ["", Validators.required],
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
          console.log("Fazendas: ",this.farms);
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
          console.log("Sementes: ",this.seeds);
        },
        (error: any) => {
          this.toastr.error("Erro ao Carregar os Sementes", "Erro!");
        }
      );
  }

  public loadPlots(farmId: any ): void {
    this.plotService.getPlotsByFarmId(farmId).subscribe(
      (plotsRetorno: Plot[]) => {
        this.plots = plotsRetorno
        console.log("Talhões: ",this.plots);
      },
      (error: any) => {
        this.toastr.error("Erro ao tentar carregar talhões", "Erro");
        console.error(error);
      }
    );
  }

  public loadPlanting(): void {
    this.plantingId = +this.activatedRouter.snapshot.paramMap.get("id");

    if (this.plantingId !== null && this.plantingId !== 0) {
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

  public savePlanting(): void {
    if (this.form.valid) {
      this.planting =
        this.saveState === "post"
          ? { ...this.form.value }
          : { id: this.planting.id, ...this.form.value };

      this.plantingService[this.saveState](this.planting).subscribe(
        (plantingRetorno: Planting) => {
          this.toastr.success("Plantio salva com Sucesso!", "Sucesso");
          this.router.navigate([`plantios / detalhe /${plantingRetorno.id}`]);
        },
        (error: any) => {
          console.error(error);
          this.toastr.error("Error ao salvar evento", "Erro");
        }
      );
    }
  }
}

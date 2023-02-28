import { Plot } from "./../../../models/Plot";
import { PlotService } from "./../../../services/plot.service";
import { ToastrService } from "ngx-toastr";
import { FarmService } from "./../../../services/farm.service";
import { Farm } from "./../../../models/Farm";
import { Component, OnInit, TemplateRef } from "@angular/core";
import {
  AbstractControl,
  FormArray,
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from "@angular/forms";
import { ActivatedRoute, Router } from "@angular/router";
import { environment } from "src/environments/environment";
import { NgbModalRef } from "@ng-bootstrap/ng-bootstrap";

@Component({
  selector: "app-farm-details",
  templateUrl: "./farm-details.component.html",
  styleUrls: ["./farm-details.component.scss"],
})
export class FarmDetailsComponent implements OnInit {
  farmId: number;
  farm = {} as Farm;
  form: FormGroup;
  saveState = "post";
  file: File;
  imageURL = 'assets/img/brand/upload.png';
  lastPlot = { id: 0, name: "", indice: 0 };

  get editMode(): boolean {
    return this.saveState === "put";
  }

  get plots(): FormArray {
    return this.form.get("plots") as FormArray;
  }

  get f(): any {
    return this.form.controls;
  }

  constructor(
    private fb: FormBuilder,
    private activatedRouter: ActivatedRoute,
    private farmService: FarmService,
    private plotService: PlotService,
    private toastr: ToastrService,
    private router: Router,
    // private modalRef: NgbModalRef,
    // private modalService: NgbModal,
  ) {}

  public loadFarm(): void {
    this.farmId = +this.activatedRouter.snapshot.paramMap.get("id");

    if (this.farmId !== null && this.farmId !== 0) {
      this.saveState = "put";

      this.farmService.getFarmById(this.farmId).subscribe(
        (farm: Farm) => {
          this.farm = { ...farm };
          this.form.patchValue(this.farm);
          if (this.farm.imageURL !== "") {
            this.imageURL =
              environment.apiURL + "resources/images/" + this.farm.imageURL;
          }
          this.loadPlots();
        },
        (error: any) => {
          this.toastr.error("Erro ao tentar carregar Fazenda.", "Erro!");
          console.error(error);
        }
      );
    }
  }

  public loadPlots(): void {
    this.plotService.getPlotsByFarmId(this.farmId).subscribe(
      (plotsRetorno: Plot[]) => {
        plotsRetorno.forEach((plot) => {
          this.plots.push(this.postPlot(plot));
        });
      },
      (error: any) => {
        this.toastr.error("Erro ao tentar carregar talhões", "Erro");
        console.error(error);
      }
    );
  }

  ngOnInit() {
    this.loadFarm();
    this.validation();
  }

  public validation(): void {
    this.form = this.fb.group({
      name: [
        "",
        [
          Validators.required,
          Validators.minLength(4),
          Validators.maxLength(50),
        ],
      ],
      location: ["", Validators.required],
      size: ["", Validators.required],
      imageURL: [''],
      plots: this.fb.array([]),
    });
  }

  addPlots(): void {
    this.plots.push(this.postPlot({ id: 0 } as Plot));
  }

  postPlot(plot: Plot): FormGroup {
    return this.fb.group({
      id: [plot.id],
      name: [plot.name, Validators.required],
      location: [plot.location, Validators.required],
      size: [plot.size, Validators.required],
      soilType: [plot.soilType],
    });
  }

  public cssValidator(campoForm: FormControl | AbstractControl): any {
    return { "is-invalid": campoForm.errors && campoForm.touched };
  }

  public returnFarmName(name: string): string {
    return name === null || name === "" ? "Nome da Fazenda" : name;
  }

  public returnPlotName(name: string): string {
    return name === null || name === "" ? "Nome do talhão" : name;
  }

  public saveFarm(): void {
    if (this.form.valid) {
      this.farm =
        this.saveState === "post"
          ? { ...this.form.value }
          : { id: this.farm.id, ...this.form.value };

      this.farmService[this.saveState](this.farm).subscribe(
        (farmRetorno: Farm) => {
          this.toastr.success("Fazenda salva com Sucesso!", "Sucesso");
          this.router.navigate([`fazenda/detalhe/${farmRetorno.id}`]);
        },
        (error: any) => {
          console.error(error);
          this.toastr.error("Error ao salvar evento", "Erro");
        }
      );
    }
  }

  public savePlots(): void {
    if (this.form.controls.plots.valid) {
      this.plotService
        .savePlot(this.farmId, this.form.value.plots)
        .subscribe(
          () => {
            this.toastr.success('Talhões salvos com Sucesso!', 'Sucesso!');
          },
          (error: any) => {
            this.toastr.error('Erro ao tentar salvar talhões.', 'Erro');
            console.error(error);
          }
        );
    }
  }

  onFileChange(ev: any): void {
    const reader = new FileReader();

    reader.onload = (event: any) => this.imageURL = event.target.result;

    this.file = ev.target.files;
    reader.readAsDataURL(this.file[0]);

    this.uploadImage();
  }

  uploadImage(): void {
    this.farmService.postUpload(this.farmId, this.file).subscribe(
      () => {
        this.loadFarm();
        this.toastr.success('Imagem atualizada com Sucesso', 'Sucesso!');
      },
      (error: any) => {
        this.toastr.error('Erro ao fazer upload de imagem', 'Erro!');
        console.log(error);
      }
    );
  }

  // public removePlot(template: TemplateRef<any>, indice: number): void {
  //   this.lastPlot.id = this.plots.get(indice + '.id').value;
  //   this.lastPlot.name = this.plots.get(indice + '.name').value;
  //   this.lastPlot.indice = indice;

  //   this.modalRef = this.modalService.show(template, { class: 'modal-sm' });
  // }
}

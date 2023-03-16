import { Field } from "./../../../models/Field";
import { FieldService } from "./../../../services/field.service";
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
  lastField = { id: 0, name: "", indice: 0 };

  get editMode(): boolean {
    return this.saveState === "put";
  }

  get fields(): FormArray {
    return this.form.get("fields") as FormArray;
  }

  get f(): any {
    return this.form.controls;
  }

  constructor(
    private fb: FormBuilder,
    private activatedRouter: ActivatedRoute,
    private farmService: FarmService,
    private fieldService: FieldService,
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
              environment.apiURL + "resources/farmimage/" + this.farm.imageURL;
          }
          this.loadFields();
        },
        (error: any) => {
          this.toastr.error("Erro ao tentar carregar Fazenda.", "Erro!");
          console.error(error);
        }
      );
    }
  }

  public loadFields(): void {
    this.fieldService.getFieldsByFarmId(this.farmId).subscribe(
      (fieldsRetorno: Field[]) => {
        fieldsRetorno.forEach((field) => {
          this.fields.push(this.postField(field));
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
      fields: this.fb.array([]),
    });
  }

  addFields(): void {
    this.fields.push(this.postField({ id: 0 } as Field));
  }

  postField(field: Field): FormGroup {
    return this.fb.group({
      id: [field.id],
      name: [field.name, Validators.required],
      location: [field.location, Validators.required],
      size: [field.size, Validators.required],
      soilType: [field.soilType],
    });
  }

  public cssValidator(campoForm: FormControl | AbstractControl): any {
    return { "is-invalid": campoForm.errors && campoForm.touched };
  }

  public returnFarmName(name: string): string {
    return name === null || name === "" ? "Nome da Fazenda" : name;
  }

  public returnFieldName(name: string): string {
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

  public saveFields(): void {
    if (this.form.controls.fields.valid) {
      this.fieldService
        .saveField(this.farmId, this.form.value.fields)
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

  // public removeField(template: TemplateRef<any>, indice: number): void {
  //   this.lastField.id = this.fields.get(indice + '.id').value;
  //   this.lastField.name = this.fields.get(indice + '.name').value;
  //   this.lastField.indice = indice;

  //   this.modalRef = this.modalService.show(template, { class: 'modal-sm' });
  // }
}

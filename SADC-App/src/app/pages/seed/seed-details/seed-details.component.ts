import { ToastrService } from 'ngx-toastr';
import { SeedService } from './../../../services/seed.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Seed } from './../../../models/Seed';
import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-seed-details',
  templateUrl: './seed-details.component.html',
  styleUrls: ['./seed-details.component.scss']
})
export class SeedDetailsComponent implements OnInit {
  seedId: number;
  seed = {} as Seed;
  form: FormGroup;
  saveState = "post";

  get editMode(): boolean {
    return this.saveState === "put";
  }

  get f(): any {
    return this.form.controls;
  }

  constructor(
    private fb: FormBuilder,
    private activatedRouter: ActivatedRoute,
    private seedService: SeedService,
    private toastr: ToastrService,
    private router: Router
  ) {}

  ngOnInit() {
    this.loadSeed();
    this.validation();
  }

  public validation(): void {
    this.form = this.fb.group({
      cultivation: ["", Validators.required],
      description: ["", Validators.required],
      growthHabit: ["", Validators.required],
      height: ["", Validators.required],
      flowering: ["", Validators.required],
      resistence: ["", Validators.required],
      maturationGroup: ["", Validators.required],
      seedConsumption: ["", Validators.required],
    });
  }

  public cssValidator(campoForm: FormControl | AbstractControl): any {
    return { "is-invalid": campoForm.errors && campoForm.touched };
  }

  public returnSeedName(description: string): string {
    return description === null || description === "" ? "Nome da semente" : description;
  }

  public loadSeed(): void {
    this.seedId = +this.activatedRouter.snapshot.paramMap.get("id");

    if (this.seedId !== null && this.seedId !== 0) {

      this.seedService.getSeedById(this.seedId).subscribe(
        (seed: Seed) => {
          this.seed = { ...seed };
          this.form.patchValue(this.seed);
        },
        (error: any) => {
          this.toastr.error("Erro ao tentar carregar Funcionário.", "Erro!");
          console.error(error);
        }
      );
    }
  }

  public saveSeed(): void {
    if (this.form.valid) {
      this.seed =
        this.saveState === "post"
          ? { ...this.form.value }
          : { id: this.seed.id, ...this.form.value };

      this.seedService[this.saveState](this.seed).subscribe(
        (seedRetorno: Seed) => {
          this.toastr.success("Semente salva com Sucesso!", "Sucesso");
          this.router.navigate([`sementes/detalhe/${seedRetorno.id}`]);
        },
        (error: any) => {
          console.error(error);
          this.toastr.error("Error ao salvar evento", "Erro");
        }
      );
    }
  }
}

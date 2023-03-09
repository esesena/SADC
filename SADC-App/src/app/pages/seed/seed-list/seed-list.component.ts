import { Router } from "@angular/router";
import { ToastrService } from "ngx-toastr";
import { Subject, debounceTime } from "rxjs";
import { Pagination, PaginatedResult } from "./../../../models/Pagination";
import { SeedService } from "./../../../services/seed.service";
import { Seed } from "./../../../models/Seed";
import { Component, OnInit, TemplateRef } from "@angular/core";
import { environment } from "src/environments/environment";

@Component({
  selector: "app-seed-list",
  templateUrl: "./seed-list.component.html",
  styleUrls: ["./seed-list.component.scss"],
})
export class SeedListComponent implements OnInit {
  public seeds: Seed[] = [];
  public seedId = 0;
  public pagination = {} as Pagination;

  searchTermChanged: Subject<string> = new Subject<string>();

  constructor(
    private seedService: SeedService,
    // private modalRef: NgbModalRef,
    private toastr: ToastrService,
    private router: Router
  ) {}

  public ngOnInit(): void {
    this.pagination = {
      currentPage: 1,
      itemsPerPage: 5,
      totalItems: 1,
    } as Pagination;

    this.loadSeeds();
  }

  public loadSeeds(): void {
    this.seedService
      .getSeeds(this.pagination.currentPage, this.pagination.itemsPerPage)
      .subscribe(
        (paginatedResult: PaginatedResult<Seed[]>) => {
          this.seeds = paginatedResult.result;
          this.pagination = paginatedResult.pagination;
        },
        (error: any) => {
          this.toastr.error("Erro ao Carregar os Sementes", "Erro!");
        }
      );
  }

  openModal(event: any, template: TemplateRef<any>, seedId: number): void {
    event.stopPropagation();
    this.seedId = seedId;
  }

  public pageChange(page: number): void {
    this.pagination.currentPage = page;
    this.loadSeeds();
  }

  // confirm(): void {
  //   this.modalRef.hidden;

  //   this.seedService
  //     .deleteSeed(this.seedId)
  //     .subscribe(
  //       (result: any) => {
  //         if (result.message === 'Deletado') {
  //           this.toastr.success(
  //             'O Evento foi deletado com Sucesso.',
  //             'Deletado!'
  //           );
  //           this.loadSeeds();
  //         }
  //       },
  //       (error: any) => {
  //         console.error(error);
  //         this.toastr.error(
  //           `Erro ao tentar deletar o evento ${this.seedId}`,
  //           'Erro'
  //         );
  //       }
  //     );
  // }

  // decline(): void {
  //   this.modalRef.hidden;
  // }

  detalheSeed(id: number): void {
    this.router.navigate([`sementes/detalhe/${id}`]);
  }

  public filterSeeds(evt: any): void {
    if (this.searchTermChanged.observers.length === 0) {
      this.searchTermChanged.pipe(debounceTime(1000)).subscribe((filter) => {
        this.seedService
          .getSeeds(
            this.pagination.currentPage,
            this.pagination.itemsPerPage,
            filter
          )
          .subscribe(
            (paginatedResult: PaginatedResult<Seed[]>) => {
              this.seeds = paginatedResult.result;
              this.pagination = paginatedResult.pagination;
            },
            (error: any) => {
              this.toastr.error("Erro ao Carregar os Sementes", "Erro!");
            }
          );
      });
    }
    this.searchTermChanged.next(evt.value);
  }
}

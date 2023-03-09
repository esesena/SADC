import { Router } from '@angular/router';
import { PlantingService } from './../../../services/planting.service';
import { ToastrService } from 'ngx-toastr';
import { PaginatedResult, Pagination } from './../../../models/Pagination';
import { Planting } from './../../../models/Planting';
import { debounceTime, Subject } from 'rxjs';
import { Component, OnInit, TemplateRef } from '@angular/core';

@Component({
  selector: 'app-planting-list',
  templateUrl: './planting-list.component.html',
  styleUrls: ['./planting-list.component.scss']
})
export class PlantingListComponent implements OnInit {
  public plantings: Planting[] = [];
  public plantingId = 0;
  public pagination = {} as Pagination;

  searchTermChanged: Subject<string> = new Subject<string>();

  constructor(
    private plantingService: PlantingService,
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

    this.loadPlantings();
  }

  public loadPlantings(): void {
    this.plantingService
      .getPlantings(this.pagination.currentPage, this.pagination.itemsPerPage)
      .subscribe(
        (paginatedResult: PaginatedResult<Planting[]>) => {
          this.plantings = paginatedResult.result;
          this.pagination = paginatedResult.pagination;
        },
        (error: any) => {
          this.toastr.error("Erro ao Carregar os Plantações", "Erro!");
        }
      );
  }

  openModal(event: any, template: TemplateRef<any>, plantingId: number): void {
    event.stopPropagation();
    this.plantingId = plantingId;
  }

  public pageChange(page: number): void {
    this.pagination.currentPage = page;
    this.loadPlantings();
  }

  // confirm(): void {
  //   this.modalRef.hidden;

  //   this.plantingService
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
    this.router.navigate([`semente/detalhe/${id}`]);
  }

  public filterPlantings(evt: any): void {
    if (this.searchTermChanged.observers.length === 0) {
      this.searchTermChanged.pipe(debounceTime(1000)).subscribe((filter) => {
        this.plantingService
          .getPlantings(
            this.pagination.currentPage,
            this.pagination.itemsPerPage,
            filter
          )
          .subscribe(
            (paginatedResult: PaginatedResult<Planting[]>) => {
              this.plantings = paginatedResult.result;
              this.pagination = paginatedResult.pagination;
            },
            (error: any) => {
              this.toastr.error("Erro ao Carregar os Plantações", "Erro!");
            }
          );
      });
    }
    this.searchTermChanged.next(evt.value);
  }
}

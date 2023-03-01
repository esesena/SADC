import { NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { Router } from '@angular/router';
import { Component, OnInit, TemplateRef } from '@angular/core';

import { ToastrService } from 'ngx-toastr';
import { debounceTime, Subject } from 'rxjs';

import { environment } from './../../../../environments/environment';
import { FarmService } from './../../../services/farm.service';
import { Pagination, PaginatedResult } from './../../../models/Pagination';
import { Farm } from './../../../models/Farm';

@Component({
  selector: 'app-farm-list',
  templateUrl: './farm-list.component.html',
  styleUrls: ['./farm-list.component.scss'],

})
export class FarmListComponent implements OnInit {

  public farms: Farm[] = [];
  public farmId = 0;
  public pagination = {} as Pagination;

  public larguraImagem = 10;
  public margemImagem = 2;
  public showImage = true;

searchTermChanged: Subject<string> = new Subject<string>();

  constructor(
    private farmService: FarmService,
    // private modalRef: NgbModalRef,
    private toastr: ToastrService,
    private router: Router) { }

    public ngOnInit(): void {
      this.pagination = {
        currentPage: 1,
        itemsPerPage: 5,
        totalItems: 1,
      } as Pagination;

      this.loadFarms();
    }

  page = this.pagination.currentPage
  pageSize = this.pagination.itemsPerPage
  collectionSize = this.pagination.totalItems
  
    public changeImage(): void {
      this.showImage = !this.showImage;
    }
  
    public revelImage(imageURL: string): string {
      return imageURL !== ''
        ? `${environment.apiURL}resources/farmimage/${imageURL}`
        : 'assets/img/brand/semImagem.jpeg';
    }
  
    public loadFarms(): void {
      this.farmService
        .getFarms(this.pagination.currentPage, this.pagination.itemsPerPage)
        .subscribe(
          (paginatedResult: PaginatedResult<Farm[]>) => {
            this.farms = paginatedResult.result;
            this.pagination = paginatedResult.pagination;
          },
          (error: any) => {
            this.toastr.error('Erro ao Carregar os Eventos', 'Erro!');
          }
        );
    }
  
    openModal(event: any, template: TemplateRef<any>, farmId: number): void {
      event.stopPropagation();
      this.farmId = farmId;
    }
  
    public pageChange(page: number): void {
      this.pagination.currentPage = page;
      this.loadFarms();
    }
  
    // confirm(): void {
    //   this.modalRef.hidden;
  
    //   this.farmService
    //     .deleteFarm(this.farmId)
    //     .subscribe(
    //       (result: any) => {
    //         if (result.message === 'Deletado') {
    //           this.toastr.success(
    //             'O Evento foi deletado com Sucesso.',
    //             'Deletado!'
    //           );
    //           this.loadFarms();
    //         }
    //       },
    //       (error: any) => {
    //         console.error(error);
    //         this.toastr.error(
    //           `Erro ao tentar deletar o evento ${this.farmId}`,
    //           'Erro'
    //         );
    //       }
    //     );
    // }
  
    // decline(): void {
    //   this.modalRef.hidden;
    // }
  
    detalheFarm(id: number): void {
      this.router.navigate([`fazenda/detalhe/${id}`]);
    }

    public filterFarms(evt: any): void {
      if (this.searchTermChanged.observers.length === 0) {
        this.searchTermChanged
          .pipe(debounceTime(1000))
          .subscribe((filter) => {
            this.farmService
              .getFarms(
                this.pagination.currentPage,
                this.pagination.itemsPerPage,
                filter
              )
              .subscribe(
                (paginatedResult: PaginatedResult<Farm[]>) => {
                  this.farms = paginatedResult.result;
                  this.pagination = paginatedResult.pagination;
                },
                (error: any) => {
                  this.toastr.error('Erro ao Carregar os Fazendas', 'Erro!');
                }
              );
          });
      }
      this.searchTermChanged.next(evt.value);
    }

}

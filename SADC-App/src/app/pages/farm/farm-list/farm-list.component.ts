import { environment } from './../../../../environments/environment.prod';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { FarmService } from './../../../services/farm.service';
import { Pagination, PaginatedResult } from './../../../models/Pagination';
import { Farm } from './../../../models/Farm';
import { Component, OnInit, TemplateRef } from '@angular/core';
import { Subject } from 'rxjs';

@Component({
  selector: 'app-farm-list',
  templateUrl: './farm-list.component.html',
  styleUrls: ['./farm-list.component.scss']
})
export class FarmListComponent implements OnInit {

  public farms: Farm[] = [];
  public farmId = 0;
  public pagination = {} as Pagination;

  public larguraImagem = 150;
  public margemImagem = 2;
  public exibirImagem = true;

  termoBuscaChanged: Subject<string> = new Subject<string>();

  constructor(    private farmService: FarmService,
    private toastr: ToastrService,
    private router: Router) { }

    public ngOnInit(): void {
      this.pagination = {
        currentPage: 1,
        itemsPerPage: 3,
        totalItems: 1,
      } as Pagination;
  
      this.loadFarms();
    }
  
    public alterarImagem(): void {
      this.exibirImagem = !this.exibirImagem;
    }
  
    public showImage(imageURL: string): string {
      return imageURL !== ''
        ? `${environment.apiURL}resources/images/${imageURL}`
        : 'assets/img/semImagem.jpeg';
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
      this.modalRef = this.modalService.show(template, { class: 'modal-sm' });
    }
  
    public pageChanged(event): void {
      this.pagination.currentPage = event.page;
      this.loadFarms();
    }
  
    confirm(): void {
      this.modalRef.hide();
      this.spinner.show();
  
      this.farmService
        .deleteEvento(this.eventoId)
        .subscribe(
          (result: any) => {
            if (result.message === 'Deletado') {
              this.toastr.success(
                'O Evento foi deletado com Sucesso.',
                'Deletado!'
              );
              this.loadFarms();
            }
          },
          (error: any) => {
            console.error(error);
            this.toastr.error(
              `Erro ao tentar deletar o evento ${this.eventoId}`,
              'Erro'
            );
          }
        )
        .add(() => this.spinner.hide());
    }
  
    decline(): void {
      this.modalRef.hide();
    }
  
    detalheEvento(id: number): void {
      this.router.navigate([`eventos/detalhe/${id}`]);
    }

}

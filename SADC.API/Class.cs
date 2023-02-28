< div class= "card rounded shadow-sm mt-3" * ngIf = "editMode" >
      < div class= "p-3" >
        < div class= "d-flex border-bottom" >
          < h2 class= "mr-auto" > Talhões da Fazenda </ h2 >
          < h2 >
            < i class= "fas fa-money-bill-wave" ></ i >
          </ h2 >
        </ div >
        < p > Clique em Talhão para adicionar e preencher novos talhões</p>
        <div class= "form-row p-1" >
          < div[formGroup] = "form" class= "col" >
            < div
              formArrayName = "plots"
              * ngFor = "let plot of plots.controls; let i = index"
            >
              < fieldset[formGroupName] = "i" class= "form-group" >
                < legend class= "d-flex justify-content-between capitalize" >
                  { { retornaTituloPlot(plots.get(i + ".nome").value) } }
                  < button
                    (click) = "removerPlot(template, i)"
                    class= "p-2 btn btn-sm btn-outline-warning mb-1 d-flex"
                    tooltip = "Excluir Talhão"
                    [adaptivePosition] = "false"
                    [delay] = "500"
                    placement = "left"
                  >
                    < i class= "fa fa-window-close my-1" ></ i >
                    < b class= "ml-1 d-none d-sm-block" > Excluir </ b >
                  </ button >
                </ legend >
                < div class= "row" >
                  < div class= "form-group col-md-4" >
                    < label > Nome </ label >
                    < input
                      [ngClass] = "cssValidator(plots.get(i + '.nome'))"
                      type = "text"
                      class= "form-control"
                      formControlName = "nome"
                      placeholder = "Lote"
                    />
                  </ div >
                  < div class= "form-group col-md-4" >
                    < label > Quantidade </ label >
                    < input
                      type = "text"
                      class= "form-control"
                      [ngClass] = "cssValidator(plots.get(i + '.quantidade'))"
                      formControlName = "quantidade"
                      placeholder = "000"
                    />
                  </ div >
                  < div class= "form-group col-md-4" >
                    < label > Preço </ label >
                    < input
                      type = "text"
                      currencyMask
                      [options] = "{
                        prefix: 'R$ ',
                        thousands: '.',
                        decimal: ',',
                        align: 'left'
                      }"
                      class= "form-control"
                      [ngClass] = "cssValidator(plots.get(i + '.preco'))"
                      formControlName = "preco"
                      placeholder = "R$ 0,00"
                    />
                  </ div >
                  < div class= "form-group col-md-4" >
                    < label > Data Início </ label >
                    < input
                      type = "datetime"
                      class= "form-control"
                      bsDatepicker
                      value = "{{
                        plots.get(i + '.dataInicio').value | date : 'dd/MM/yyyy'
                      }}"
                      placeholder = "01/01/2019"
                      (bsValueChange) = "mudarValorData($event, i, 'dataInicio')"
                    />
                  </ div >
                  < div class= "form-group col-md-4" >
                    < label > Data Fim </ label >
                    < input
                      type = "datetime"
                      class= "form-control"
                      bsDatepicker
                      value = "{{
                        plots.get(i + '.dataFim').value | date : 'dd/MM/yyyy'
                      }}"
                      placeholder = "01/01/2019"
                      (bsValueChange) = "mudarValorData($event, i, 'dataFim')"
                    />
                  </ div >
                </ div >
              </ fieldset >
            </ div >
          </ div >
        </ div >
        < button
          (click) = "adicionarPlot()"
          class= "d-flex btn btn-outline-primary"
        >
          < i class= "fa fa-plus-circle my-1" ></ i >
          < b class= "ml-1" > Talhão </ b >
        </ button >
      </ div >
      < div class= "card-footer" >
        < div class= "d-flex" >
          < button class= "btn btn-outline-secondary mr-auto border" >
            Cancelar Alteração
          </ button >
          < button
            [disabled] = "!form.controls.plots.valid"
            (click) = "salvarPlots()"
            class= "btn btn-success"
          >
Salvar Talhões
          </ button >
        </ div >
      </ div >
    </ div >
  </ div >
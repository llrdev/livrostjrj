import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { LivrosRoutingModule } from './livros-routing.module';
import { ListarComponent } from './listar/listar.component';
import { AdicionarComponent } from './adicionar/adicionar.component';
import { EditarComponent } from './editar/editar.component';


@NgModule({
  declarations: [
    ListarComponent,
    AdicionarComponent,
    EditarComponent
  ],
  imports: [
    CommonModule,
    LivrosRoutingModule,
    FormsModule 
  ]
})
export class LivrosModule { }

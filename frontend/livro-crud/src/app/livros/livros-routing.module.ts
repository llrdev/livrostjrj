import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ListarComponent } from './listar/listar.component';
import { AdicionarComponent } from './adicionar/adicionar.component';
import { EditarComponent } from './editar/editar.component';
import { RelatorioComponent } from '../relatorio/relatorio.component';

const routes: Routes = [
  { path: '', component: ListarComponent }, // Adicione isso para que a lista apareça na rota base
  { path: 'adicionar', component: AdicionarComponent },
  { path: 'editar/:id', component: EditarComponent },
  { path: 'relatorio', component: RelatorioComponent }, // Adicione a rota aqui

];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class LivrosRoutingModule { }
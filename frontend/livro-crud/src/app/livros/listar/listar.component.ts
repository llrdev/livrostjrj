import { Component, OnInit } from '@angular/core';
import { LivroService, Livro } from '../livro.service';

@Component({
  selector: 'app-listar',
  templateUrl: './listar.component.html',
  styleUrls: ['./listar.component.css']
})
export class ListarComponent implements OnInit {
  livros: Livro[] = [];

  constructor(private livroService: LivroService) { }

  ngOnInit(): void {
    this.carregarLivros();
  }

  carregarLivros(): void {
    this.livroService.getLivros().subscribe((response: any) => {
      this.livros = response.data;
    });
  }

  excluirLivro(id: number): void {
    this.livroService.deleteLivro(id).subscribe(() => {
      this.carregarLivros();
    });
  }
}
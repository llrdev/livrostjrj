import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { LivroService, Livro } from '../livro.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-adicionar',
  templateUrl: './adicionar.component.html',
  styleUrls: ['./adicionar.component.css']
})
export class AdicionarComponent {
  livro: Livro = {
    codi:0,
    titulo: '',
    editora: '',
    anoPublicacao: '',
    valor: 0,
    autorCodAus:[],
    assuntoCodAs: []
  };
  constructor(private livroService: LivroService, private router: Router, private toastr: ToastrService) { }

  adicionarAutor() {
    this.livro.autorCodAus.push({ nome: '' }); 
  }

  adicionarAssunto() {
    this.livro.assuntoCodAs.push({ descricao: '' }); 
  }

  removerAutor(index: number) {
    this.livro.autorCodAus.splice(index, 1); 
  }

  removerAssunto(index: number) {
    this.livro.assuntoCodAs.splice(index, 1); 
  }

  

  adicionarLivro(): void {
    this.livroService.adicionarLivro(this.livro).subscribe({
      next: (response) => { // Altere para usar 'next'
        if (response.success) {
          this.toastr.success(response.success, 'Sucesso'); // Mensagem de sucesso do backend
          this.router.navigate(['/livros']);
        } else {
          this.toastr.error('Erro ao adicionar o livro!', 'Erro'); // Mensagem de erro
        }
      },
      error: (error) => {
        this.toastr.error('Erro ao adicionar o livro!', 'Erro'); // Mensagem de erro padrão
        console.error('Erro ao adicionar o livro:', error);
      }
    });
  }

  trackByAutor(index: number, autor: any): number {
    return index; // Ou use uma propriedade única, se tiver
  }
  
  trackByAssunto(index: number, assunto: any): number {
    return index; // Ou use uma propriedade única, se tiver
  }

  formatarValor(event: any) {
    // Captura o valor digitado
    let valor = event.target.value;

    // Remove todos os caracteres que não são dígitos ou vírgula
    valor = valor.replace(/[^0-9,]/g, '');

    if (!valor) {
        // Se o valor estiver vazio, redefine para 0,00
        this.livro.valor = 0;
        event.target.value = '0,00';
        return;
    }
    
    // Ajusta para os centavos em caso de entrada direta
    // Como o usuário pode digitar "1000,00", precisaremos considerar o valor como temos:
    const valorFormatado = valor.replace(',', '.'); // Troca vírgula por ponto para a conversão
    const valorNumero = parseFloat(valorFormatado); // Converte para número

    // Formata diretamente na moeda BR
    const valorMostrado = valorNumero.toLocaleString('pt-BR', { 
        minimumFractionDigits: 2, 
        maximumFractionDigits: 2 
    });

    // Preenche o campo de entrada com a formatação correta
    event.target.value = valorMostrado;

    // Atualiza o valor no objeto livro como centavos
    this.livro.valor = valorNumero; // Armazena como número decimal para futuras operações
}
}
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { LivroService, LivroUpdateDTO } from '../livro.service'; // Atualizar importações

@Component({
  selector: 'app-editar',
  templateUrl: './editar.component.html',
  styleUrls: ['./editar.component.css']
})
export class EditarComponent implements OnInit {
  livro: LivroUpdateDTO = {
    titulo: '',
    editora: '',
    anoPublicacao: '',
    valor: 0,
    autorCodAus: [],
    assuntoCodAs: [],
};
  
  id!: number;

  constructor(
    private livroService: LivroService,
    private route: ActivatedRoute,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.id = Number(this.route.snapshot.paramMap.get('id'));

    // Obter o livro pelo ID e mapear para LivroUpdateDTO
    this.livroService.getLivroById(this.id).subscribe((response: any) => {
      this.livro = response.data; // o retorno deve ser o DTO diretamente
    });
  }

  adicionarAutor() {
    this.livro.autorCodAus.push({ codAu: 0, nome: '' }); // Inicializa com codAu
  }

  removerAutor(index: number) {
    this.livro.autorCodAus.splice(index, 1);
  }

  adicionarAssunto() {
    this.livro.assuntoCodAs.push({ codAs: 0, descricao: '' }); // Inicializa com codAs
  }

  removerAssunto(index: number) {
    this.livro.assuntoCodAs.splice(index, 1);
  }

  atualizarLivro(): void {
    this.livroService.updateLivro(this.id, this.livro).subscribe(() => {
        this.router.navigate(['/livros']);
    }, error => {
        console.error('Erro ao atualizar o livro:', error);
    });
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
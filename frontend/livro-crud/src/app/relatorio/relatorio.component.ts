import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-relatorio',
  templateUrl: './relatorio.component.html',
})
export class RelatorioComponent {
  constructor(private http: HttpClient) {}

  gerarRelatorio() {
    this.http.get('https://localhost:44393/api/Livros/relatorio', { responseType: 'blob' }) // Espera um blob como resposta
        .subscribe((response: Blob) => {
            const url = window.URL.createObjectURL(response); // Cria um URL para o Blob
            const a = document.createElement('a');
            a.href = url;
            a.download = 'relatorio_livros.pdf'; // Nome do arquivo

            // Dispara o download
            document.body.appendChild(a); // Adiciona o elemento ao DOM
            a.click(); // Clica para baixar
            document.body.removeChild(a); // Remove o elemento após a ação
            window.URL.revokeObjectURL(url); // Libera o objeto URL
        }, error => {
            console.error('Erro ao gerar relatório:', error); // Lida com erros
        });
}

}
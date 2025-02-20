import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface Livro {
  codi: number;
  titulo: string;
  editora: string;
  anoPublicacao: string;
  valor: number;
  autorCodAus: Autor[];
  assuntoCodAs: Assunto[];
}

export interface Autor {
  nome: string;
}

export interface Assunto {
  descricao: string;
}

export interface LivroUpdateDTO {
  titulo: string;
  editora: string;
  anoPublicacao: string;
  valor: number;
  autorCodAus: AutorUpdateDto[];
  assuntoCodAs: AssuntoUpdateDto[];
}

export interface AutorUpdateDto {
  codAu: number;
  nome: string;
}

export interface AssuntoUpdateDto {
  codAs: number;
  descricao: string;
}

@Injectable({
  providedIn: 'root'
})
export class LivroService {

  private apiBaseUrl = 'https://localhost:44393/api/Livros';  // Ajuste o URL conforme necessário

  constructor(private http: HttpClient) { }

  getLivros(): Observable<Livro[]> {
    return this.http.get<Livro[]>(this.apiBaseUrl);
  }

  getLivroById(id: number): Observable<Livro> {
    return this.http.get<Livro>(`${this.apiBaseUrl}/${id}`);
  }

  adicionarLivro(livro: Livro): Observable<any> {
    return this.http.post(`${this.apiBaseUrl}`, livro);
  }

  updateLivro(id: number, livroUpdateDTO: LivroUpdateDTO): Observable<void> {
    return this.http.put<void>(`${this.apiBaseUrl}/${id}`, livroUpdateDTO);
  }

  deleteLivro(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiBaseUrl}/${id}`);
  }
}
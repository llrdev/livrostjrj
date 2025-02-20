using Domain.Core.Result;
using Domain.Dtos.Livro;
using System.Collections.Generic;

namespace Domain.Interfaces.Livro
{
    public interface ILivro
    {
        Result<string> Add(LivroDTO livroDTO);
        Result<List<LivroFlatDTO>> Get();
        Result<LivroFlatDTO> GetById(int idLivro);
        Result<List<RelatorioDTO>> Relatorio();
        Result<string> Update(int idLivro, LivroUpdateDTO livroDTO);
        Result<string> Delete(int idLivro);
        byte[] GerarPDF(List<RelatorioDTO> relatorioDTOs);
    }
}

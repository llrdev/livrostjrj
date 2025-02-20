using Domain.Dtos.Livro;
using Domain.Interfaces.Livro;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using Webapi.Controllers.Core;
using Webapi.Core.Helpers;
using Webapi.Extensions;

namespace Webapi.Controllers
{
    public class LivrosController : BaseApiController
    {
        private readonly ILivro _livro;

        public LivrosController(ILivro livro, IUriHelper uriHelper)
        {
            _livro = livro;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public IActionResult AdicionaLivro([FromBody] LivroDTO input)
        {
            var result = _livro.Add(input);
            return this.FromResult(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet]
        public IActionResult RetornaLivros()
        {
            var result = _livro.Get();
            return this.FromResult(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("{id:int}")]
        public IActionResult RetornaLivro(int id)
        {
            var result = _livro.GetById(id);
            return this.FromResult(result);
        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut("{id:int}")]
        public IActionResult AtualizarLivro(int id, [FromBody] LivroUpdateDTO livroDTO)
        {
            var result = _livro.Update(id, livroDTO);
            return this.FromResult(result);
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{id:int}")]
        public IActionResult ExcluirLivro(int id)
        {
            var result = _livro.Delete(id);
            return this.FromResult(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("relatorio")]
        public IActionResult Relatorio()
        {
            var result = _livro.Relatorio();

            // Gera o PDF a partir dos dados do relatório
            var pdfBytes = _livro.GerarPDF(result.Data);

            return File(pdfBytes, "application/pdf", "relatorio_livros.pdf");
        }
    }
}



using Domain.Core.Resources;
using Domain.Core.Result;
using Domain.Dtos.Livro;
using Domain.Entities.Livros;
using Domain.Interfaces;
using Domain.Interfaces.Infra;
using Domain.Interfaces.Livro;
using Domain.Specifications.Siaf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using PdfSharp.Drawing;
using PdfSharp.Pdf;

namespace Domain.Services.Livro
{
    public class LivroService : ILivro
    {
        private readonly IUnitOfWork _uow;
        private readonly IMyMapper _mapper;

        public LivroService(IUnitOfWork uow, IMyMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public Result<string> Add(LivroDTO livroDTO)
        {
            try
            {

                var livro = new Entities.Livros.Livro
                {
                    Titulo = livroDTO.Titulo,
                    Editora = livroDTO.Editora,
                    AnoPublicacao = livroDTO.AnoPublicacao,
                    Valor = livroDTO.Valor,
                };


                _uow.Repository<Entities.Livros.Livro>().Add(livro);
                _uow.Complete();


                var livroId = livro.Codi;


                foreach (var autorDTO in livroDTO.AutorCodAus)
                {
                    var autor = new Autor { Nome = autorDTO.Nome };
                    _uow.Repository<Autor>().Add(autor);
                    _uow.Complete();


                    var livroAutor = new LivroAutor
                    {
                        LivroCodiAu = livroId,
                        AutorCodAu = autor.CodAu
                    };
                    _uow.Repository<LivroAutor>().Add(livroAutor);
                }


                foreach (var assuntoDTO in livroDTO.AssuntoCodAs)
                {
                    var assunto = new Assunto { Descricao = assuntoDTO.Descricao };
                    _uow.Repository<Assunto>().Add(assunto);
                    _uow.Complete();
                    var livroAssunto = new LivroAssunto
                    {
                        LivroCodiAs = livroId,
                        AssuntoCodAs = assunto.CodAs
                    };
                    _uow.Repository<LivroAssunto>().Add(livroAssunto);
                }

                foreach (var assuntoDTO in livroDTO.AssuntoCodAs)
                {
                    var assunto = new Assunto { Descricao = assuntoDTO.Descricao };
                    _uow.Repository<Assunto>().Add(assunto);
                    _uow.Complete();


                    var livroAssunto = new LivroAssunto
                    {
                        LivroCodiAs = livroId,
                        AssuntoCodAs = assunto.CodAs
                    };
                    _uow.Repository<LivroAssunto>().Add(livroAssunto);
                }


                _uow.Complete();

                return ResultHelper.Success<string>(Mensagens.SucessoDefault);
            }
            catch (Exception ex)
            {
                return ResultHelper.Invalid<string>(ex.Message);
            }
        }

        public Result<string> Delete(int idLivro)
        {
            try
            {
                Entities.Livros.Livro livroExistente = _uow.Repository<Entities.Livros.Livro>().Find(new LivrosSpec(idLivro)).First();
                if (livroExistente == null)
                {
                    return ResultHelper.Invalid<string>("Livro não encontrado.");
                }


                var livroAutor = _uow.Repository<Entities.Livros.LivroAutor>().Find(new LivroAutorSpec(idLivro)).ToList();
                var livroAssunto = _uow.Repository<Entities.Livros.LivroAssunto>().Find(new LivroAssuntoSpec(idLivro)).ToList();

                foreach (var itemLivroAutor in livroAutor)
                {
                    _uow.Repository<Entities.Livros.LivroAutor>().Remove(itemLivroAutor);
                    _uow.Complete();

                    var autor = _uow.Repository<Entities.Livros.Autor>().Find(new AutorSpec(itemLivroAutor.AutorCodAu)).FirstOrDefault();
                    _uow.Repository<Entities.Livros.Autor>().Remove(autor);
                    _uow.Complete();
                }

                foreach (var itemLivroAssunto in livroAssunto)
                {
                    _uow.Repository<Entities.Livros.LivroAssunto>().Remove(itemLivroAssunto);
                    _uow.Complete();

                    var assunto = _uow.Repository<Entities.Livros.Assunto>().Find(new AssuntoSpec(itemLivroAssunto.AssuntoCodAs)).FirstOrDefault();
                    _uow.Repository<Entities.Livros.Assunto>().Remove(assunto);
                    _uow.Complete();
                }

                _uow.Repository<Entities.Livros.Livro>().Remove(livroExistente);
                _uow.Complete(); // Salva as mudanças

                return ResultHelper.Success<string>("Livro excluído com sucesso.");
            }
            catch (Exception ex)
            {
                return ResultHelper.Invalid<string>(ex.Message);
            }
        }

        public Result<List<LivroFlatDTO>> Get()
        {
            try
            {
                // Obter todos os livros sem usar as tabelas de junção
                var livros = _uow.Repository<Entities.Livros.Livro>()
                    .Find(new LivrosSpec())
                    .ToList();

                // Criar a lista de DTOs
                var livroFlatDTOs = new List<LivroFlatDTO>();

                foreach (var livro in livros)
                {
                    // Para cada livro, carregue seus autores e assuntos
                    var autores = _uow.Repository<LivroAutor>()
                        .Find(l => l.LivroCodiAu == livro.Codi).ToList(); // Obtém os autores diretamente

                    var assuntos = _uow.Repository<LivroAssunto>()
                        .Find(l => l.LivroCodiAs == livro.Codi).ToList(); // Obtém os assuntos diretamente

                    // Criar o DTO do livro
                    var livroFlatDTO = new LivroFlatDTO
                    {
                        Codi = livro.Codi,
                        Titulo = livro.Titulo,
                        Editora = livro.Editora,
                        AnoPublicacao = livro.AnoPublicacao,
                        Valor = livro.Valor.Value, // Aqui assumimos que Valor não será nulo. Caso contrário, deve-se abordar a possibilidade de nulo
                        AutorCodAus = autores.Select(la => new AutorFlatDto
                        {
                            CodAu = la.AutorCodAu,
                            Nome = _uow.Repository<Autor>().FindById(la.AutorCodAu).Nome // Aqui, usa o ID do Autor para obter o Nome
                        }).ToList(),
                        AssuntoCodAs = assuntos.Select(la => new AssuntoFlatDto
                        {
                            CodAs = la.AssuntoCodAs,
                            Descricao = _uow.Repository<Assunto>().FindById(la.AssuntoCodAs).Descricao // Aqui, usa o ID do Assunto para obter a Descrição
                        }).ToList()
                    };

                    // Adiciona o DTO à lista
                    livroFlatDTOs.Add(livroFlatDTO);
                }

                return ResultHelper.Success(livroFlatDTOs, Mensagens.SucessoDefault);
            }
            catch (Exception ex)
            {
                return ResultHelper.Invalid<List<LivroFlatDTO>>(ex.Message);
            }
        }

        public Result<LivroFlatDTO> GetById(int idLivro)
        {
            try
            {


                // Obter todos os livros sem usar as tabelas de junção
                var livros = _uow.Repository<Entities.Livros.Livro>()
                    .Find(new LivrosSpec(idLivro))
                    .ToList();

                // Criar a lista de DTOs
                var livroFlatDTOs = new List<LivroFlatDTO>();

                foreach (var livro in livros)
                {
                    // Para cada livro, carregue seus autores e assuntos
                    var autores = _uow.Repository<LivroAutor>()
                        .Find(l => l.LivroCodiAu == livro.Codi).ToList(); // Obtém os autores diretamente

                    var assuntos = _uow.Repository<LivroAssunto>()
                        .Find(l => l.LivroCodiAs == livro.Codi).ToList(); // Obtém os assuntos diretamente

                    // Criar o DTO do livro
                    var livroFlatDTO = new LivroFlatDTO
                    {
                        Codi = livro.Codi,
                        Titulo = livro.Titulo,
                        Editora = livro.Editora,
                        AnoPublicacao = livro.AnoPublicacao,
                        Valor = livro.Valor.Value, // Aqui assumimos que Valor não será nulo. Caso contrário, deve-se abordar a possibilidade de nulo
                        AutorCodAus = autores.Select(la => new AutorFlatDto
                        {
                            CodAu = la.AutorCodAu,
                            Nome = _uow.Repository<Autor>().FindById(la.AutorCodAu).Nome // Aqui, usa o ID do Autor para obter o Nome
                        }).ToList(),
                        AssuntoCodAs = assuntos.Select(la => new AssuntoFlatDto
                        {
                            CodAs = la.AssuntoCodAs,
                            Descricao = _uow.Repository<Assunto>().FindById(la.AssuntoCodAs).Descricao // Aqui, usa o ID do Assunto para obter a Descrição
                        }).ToList()
                    };

                    // Adiciona o DTO à lista
                    livroFlatDTOs.Add(livroFlatDTO);
                }

                return ResultHelper.Success(livroFlatDTOs.First(), Mensagens.SucessoDefault);
            }
            catch (Exception ex)
            {
                return ResultHelper.Invalid<LivroFlatDTO>(ex.Message);
            }

        }

        public Result<string> Update(int idLivro, LivroUpdateDTO livroDTO)
        {
            try
            {
                var livroExistente = _uow.Repository<Entities.Livros.Livro>().FindById(idLivro);
                if (livroExistente == null)
                {
                    return ResultHelper.Invalid<string>("Livro não encontrado.");
                }

                // Atualiza as propriedades do livro
                livroExistente.Titulo = livroDTO.Titulo;
                livroExistente.Editora = livroDTO.Editora;
                livroExistente.AnoPublicacao = livroDTO.AnoPublicacao;
                livroExistente.Valor = livroDTO.Valor;

                // Atualiza autores
                foreach (var autorDTO in livroDTO.AutorCodAus)
                {
                    // Consulta e atualiza ou adiciona o autor
                    var autorExistente = _uow.Repository<Autor>().FindById(autorDTO.CodAu);
                    if (autorExistente != null)
                    {
                        // Atualiza o autor existente
                        autorExistente.Nome = autorDTO.Nome;
                        _uow.Repository<Autor>().Update(autorExistente);
                        _uow.Complete();
                    }
                    else
                    {
                        // Se não existir, cria um novo autor
                        var autor = new Autor { Nome = autorDTO.Nome };
                        _uow.Repository<Autor>().Add(autor);
                        _uow.Complete();

                        var livroAutor = new LivroAutor { AutorCodAu = autor.CodAu, LivroCodiAu = livroExistente.Codi };
                        _uow.Repository<LivroAutor>().Add(livroAutor); // Adiciona novo autor


                    }
                }

                // Remover os autores que não estão no DTO
                var autoresAssociados = _uow.Repository<LivroAutor>()
                    .Find(l => l.LivroCodiAu == idLivro)
                    .ToList(); // Pega todos os autores associados ao livro

                // Identifica os autores para remover
                var autoresParaRemover = autoresAssociados
                    .Where(a => !livroDTO.AutorCodAus.Any(dto => dto.CodAu == a.AutorCodAu))
                    .ToList();

                foreach (var autor in autoresParaRemover)
                {
                    _uow.Repository<LivroAutor>().Remove(autor); // Remove associação

                }

                // Atualiza assuntos
                foreach (var assuntoDTO in livroDTO.AssuntoCodAs)
                {
                    // Consulta e atualiza ou adiciona o assunto
                    var assuntoExistente = _uow.Repository<Assunto>().FindById(assuntoDTO.CodAs);
                    if (assuntoExistente != null)
                    {
                        // Atualiza a descrição do assunto existente
                        assuntoExistente.Descricao = assuntoDTO.Descricao;
                        _uow.Repository<Entities.Livros.Assunto>().Update(assuntoExistente);
                        _uow.Complete();


                    }
                    else
                    {
                        // Se não existir, cria um novo autor
                        var assunto = new Assunto { Descricao = assuntoDTO.Descricao };
                        _uow.Repository<Assunto>().Add(assunto);
                        _uow.Complete();

                        var livroAssunto = new LivroAssunto { AssuntoCodAs = assunto.CodAs, LivroCodiAs = livroExistente.Codi };
                        _uow.Repository<LivroAssunto>().Add(livroAssunto);
                    }
                }

                // Remover os assuntos que não estão no DTO
                var assuntosAssociados = _uow.Repository<LivroAssunto>()
                    .Find(l => l.LivroCodiAs == idLivro)
                    .ToList(); // Pega todos os assuntos associados ao livro

                // Identifica os assuntos para remover
                var assuntosParaRemover = assuntosAssociados
                    .Where(a => !livroDTO.AssuntoCodAs.Any(dto => dto.CodAs == a.AssuntoCodAs))
                    .ToList();

                foreach (var assunto in assuntosParaRemover)
                {
                    _uow.Repository<LivroAssunto>().Remove(assunto); // Remove associação
                }

                // Atualiza a entidade de livro no repositório
                _uow.Repository<Entities.Livros.Livro>().Update(livroExistente);

                // Persiste todas as alterações
                _uow.Complete();

                return ResultHelper.Success<string>("Livro, autores e assuntos atualizados com sucesso.");
            }
            catch (Exception ex)
            {
                return ResultHelper.Invalid<string>(ex.Message);
            }
        }

        public Result<List<RelatorioDTO>> Relatorio()
        {
            var relatorio = _uow.Repository<RelatorioLivro>()
            .Find(new RelatorioSpec())
            .ToList();

            var maped = _mapper.Map<List<RelatorioDTO>>(relatorio);

            return ResultHelper.Success(maped);
        }

        public byte[] GerarPDF(List<RelatorioDTO> relatorioDTOs)
        {

            using (var memoryStream = new MemoryStream())
            {
                // Criar um novo documento PDF
                PdfDocument document = new PdfDocument();
                document.Info.Title = "Relatório de Livros";

                // Cria uma nova página
                PdfPage page = document.AddPage();
                page.Size = PdfSharp.PageSize.A4;

                // Cria um objeto de gráficos para desenhar na página
                XGraphics gfx = XGraphics.FromPdfPage(page);
                XFont font = new XFont("Verdana", 10, XFontStyleEx.Regular);

                // Adiciona título ao PDF
                gfx.DrawString("Relatório de Livros", font, XBrushes.Black, new XRect(0, 0, page.Width, page.Height), XStringFormats.TopCenter);

                // Define uma posição inicial para o próximo item
                double yPoint = 40;

                // Adiciona cabeçalhos
                gfx.DrawString("Livro ID", font, XBrushes.Black, new XRect(20, yPoint, page.Width, page.Height), XStringFormats.TopLeft);
                gfx.DrawString("Título", font, XBrushes.Black, new XRect(120, yPoint, page.Width, page.Height), XStringFormats.TopLeft);
                gfx.DrawString("Autor", font, XBrushes.Black, new XRect(220, yPoint, page.Width, page.Height), XStringFormats.TopLeft);
                gfx.DrawString("Assuntos", font, XBrushes.Black, new XRect(320, yPoint, page.Width, page.Height), XStringFormats.TopLeft);

                yPoint += 20; // Ajuste a altura para os itens da tabela

                // Adiciona os dados do relatório ao PDF
                foreach (var item in relatorioDTOs)
                {
                    gfx.DrawString(item.LivroId.ToString(), font, XBrushes.Black, new XRect(20, yPoint, page.Width, page.Height), XStringFormats.TopLeft);
                    gfx.DrawString(item.Titulo, font, XBrushes.Black, new XRect(120, yPoint, page.Width, page.Height), XStringFormats.TopLeft);
                    gfx.DrawString(item.Autor, font, XBrushes.Black, new XRect(220, yPoint, page.Width, page.Height), XStringFormats.TopLeft);
                    gfx.DrawString(item.Assuntos, font, XBrushes.Black, new XRect(320, yPoint, page.Width, page.Height), XStringFormats.TopLeft);

                    yPoint += 20; // Ajuste para a próxima linha
                }

                // Salva o documento no Stream
                document.Save(memoryStream, false);
                return memoryStream.ToArray(); // Retorna os bytes do PDF
            }
        }
    }
}

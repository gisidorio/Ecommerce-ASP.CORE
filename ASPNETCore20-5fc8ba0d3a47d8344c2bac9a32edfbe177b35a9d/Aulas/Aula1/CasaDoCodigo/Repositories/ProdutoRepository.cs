using CasaDoCodigo.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace CasaDoCodigo.Repositories
{
    public class ProdutoRepository : BaseRepository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(ApplicationContext context) : base(context)
        {
        }

        public IList<Produto> GetProdutos()
        {
            // NÃO ESQUECER DE INSERIR A DIRETIVA using System.Linq acima, caso o contrário dará erro
            // ToList() depende dessa diretiva
            //return context.Set<Produto>().ToList();
            return dbSet.ToList();
        }

        public void SaveProdutos(List<Livro> livros)
        {
            foreach (var livro in livros)
            {
                // SE O LIVRO QUE PRETENDEMOS SALVAR POSSUI O MESMO CÓDIGO QUE ALGUM NA TABELA
                // NÃO IRÁ SALVAR. NÃO IRÁ DUPLICAR O VALOR
                if (!dbSet.Where(p => p.Codigo == livro.Codigo).Any())
                {
                    // SET ADICIONA INFORMAÇÕES MEMÓRIA

                    dbSet.Add(new Produto(livro.Codigo, livro.Nome, livro.Preco));
                }                                
            }

            // QUANDO TIVERMOS OS PRODUTOS CARREGADOS EM MEMÓRIA E PRONTOS PARA SEREM GRAVADOS NO BANCO CHAMAMOS O SAVE CHANGES
            context.SaveChanges();

            /* O método SaveChanges grava no banco de dados todas as alterações pendentes do contexto do Entity Framework Core. */
        }
    }

    public class Livro
    {
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }
    }
}

using CasaDoCodigo.Models;
using Microsoft.EntityFrameworkCore;

namespace CasaDoCodigo
{
    // DbContext é uma classe do Entity Framework Core
    // Ela possui atributos e métodos do ORM EF
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions options) : base(options)
        {

        }

        // MÉTODO QUE FARÁ O MAPEAMENTO. QUANDO TIVER CRIANDO O MODELO COMPILADOR VAI ACESSAR ESSE MÉTODO PARA FAZER O MAPEAMENTO
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // VAI REGISTRAR UMA CLASSE DO NOSSO MODELO, VAI MAPEAR O PRODUTO, CASO CONTRÁRIO NÃO SABERÁ QUAL CLASSE MAPEAR
            // logo possui o haskey para indicar que possui chave primária seguido de uma expressão lambda indicando que é o atributo Id
            modelBuilder.Entity<Produto>().HasKey(t => t.Id);

            modelBuilder.Entity<Pedido>().HasKey(t => t.Id);

            /* REPRESENTA RELACIONAMENTO DE PEDIDO QUE POSSUI VÁRIOS ITENS
             * UM ITEM PODE ESTAR ASSOCIADO A APENAS UM PEDIDO (1 PARA MUITOS) */
            modelBuilder.Entity<Pedido>().HasMany(t => t.Itens).WithOne(t => t.Pedido);

            /* um pedido pode estar associado a apenas um cadastro e um cadastro pode estar associado 
             * a apenas um pedido */
            modelBuilder.Entity<Pedido>().HasOne(t => t.Cadastro).WithOne(t => t.Pedido).IsRequired();            

            modelBuilder.Entity<ItemPedido>().HasKey(t => t.Id);
            modelBuilder.Entity<ItemPedido>().HasOne(t => t.Pedido);
            modelBuilder.Entity<ItemPedido>().HasOne(t => t.Produto);

            modelBuilder.Entity<Cadastro>().HasKey(t => t.Id);
            modelBuilder.Entity<Cadastro>().HasOne(t => t.Pedido);
        }
    }
}

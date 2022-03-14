using CasaDoCodigo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace CasaDoCodigo.Repositories
{
    public class PedidoRepository : BaseRepository<Pedido>, IPedidoRepository
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public PedidoRepository(ApplicationContext context,
            IHttpContextAccessor httpContextAccessor) : base(context)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public void AddItem(string codigo)
        {
            var produto = context.Set<Produto>().Where(p => p.Codigo == codigo).SingleOrDefault();

            if (produto == null)
            {
                throw new ArgumentException("Produto não encontrado");
            }

            var pedido = GetPedido();

            var itemPedido = context.Set<ItemPedido>()
                                .Where(i => i.Produto.Codigo == codigo
                                && i.Pedido.Id == pedido.Id)
                                .SingleOrDefault();

            if (itemPedido == null)
            {
                itemPedido = new ItemPedido(pedido, produto, 1, produto.Preco);
                context.Set<ItemPedido>().Add(itemPedido);
                context.SaveChanges();
            }
        }

        public Pedido GetPedido()
        {
            var pedidoId = GetPedidoId();

            var pedido = dbSet
                .Include(p => p.Itens)
                .ThenInclude(i => i.Produto)
                .Where(p => p.Id == pedidoId)
                .SingleOrDefault();

            // se não houver um pedido do dbset é adicionado um novo
            if (pedido == null)
            {
                pedido = new Pedido();
                dbSet.Add(pedido);
                context.SaveChanges();
                SetPedidoId(pedido.Id);
            }

            return pedido;  
        }

        private int? GetPedidoId()
        {
            return httpContextAccessor.HttpContext.Session.GetInt32("pedidoId");
        }

        private void SetPedidoId(int pedidoId)
        {
            httpContextAccessor.HttpContext.Session.SetInt32("pedidoId", pedidoId);
        }
    }
}

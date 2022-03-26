using CasaDoCodigo.Models;
using System.Linq;

namespace CasaDoCodigo.Repositories
{
    public class ItemPedidoRepository : BaseRepository<ItemPedido>, IItemPedidoRepository
    {
        public ItemPedidoRepository(ApplicationContext context) : base(context)
        {
        }

        public ItemPedido GetItemPedido(int itemPedidoId)
        {
            return
            dbSet
                .Where(ip => ip.Id == itemPedidoId)
                .SingleOrDefault(); // retorna um elemento, se não encontrar retorna null
        }

        public void RemoverItemPedido(int itemPedidoId)
        {
            dbSet.Remove(GetItemPedido(itemPedidoId));
        }
    }
}

using CasaDoCodigo.Models.ViewModels;

namespace CasaDoCodigo.Models
{
    public class UpdateQuantidadeResponse
    {
        public ItemPedido itemPedido { get; }
        public CarrinhoViewModel carrinhoViewModel { get; }

        public UpdateQuantidadeResponse(ItemPedido itemPedido, CarrinhoViewModel carrinhoViewModel)
        {
            this.itemPedido = itemPedido;
            this.carrinhoViewModel = carrinhoViewModel;
        }
    }
}

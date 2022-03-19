using CasaDoCodigo.Models;
using CasaDoCodigo.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CasaDoCodigo.Controllers
{
    public class Pedido : Controller
    {
        private readonly IProdutoRepository produtoRepository;
        private readonly IPedidoRepository pedidoRepository;
        private readonly IItemPedidoRepository itemPedidoRepository;

        public Pedido(
            IProdutoRepository produtoRepository, 
            IPedidoRepository pedidoRepository, 
            IItemPedidoRepository itemPedidoRepository
        )
        {
            this.produtoRepository = produtoRepository;
            this.pedidoRepository = pedidoRepository;
            this.itemPedidoRepository = itemPedidoRepository; 
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Carrossel()
        {            
            return View(produtoRepository.GetProdutos());
        }

        public IActionResult Cadastro()
        {
            return View();
        }

        public IActionResult Carrinho(string codigo)
        {
            if (!string.IsNullOrEmpty(codigo))
            {
                pedidoRepository.AddItem(codigo);
            }

            var pedido = pedidoRepository.GetPedido();
            return View(pedido.Itens);
        }

        public IActionResult Resumo()
        {
            var pedido = pedidoRepository.GetPedido();

            return View(pedido);
        }

        // MÉTODO POST EXIGE QUE OS PARÂMETROS SEJAM PASSADOS PELO CORPO DA REQUISIÇÃO
        // ela é ideal para alterar alguma informação no seu sistema
        // MÉTODO GET PODEMOS UTILIZAR QUERY STRING PARA PASSAR VALOR PARA OS PARÂMETROS
        // USAREMOS MÉTODO GET PARA OBTER DADOS APENAS
        [HttpPost]
        public void UpdateQuantidade([FromBody]ItemPedido itemPedido)
        {
            //FromBody indica que o parâmetro virá do corpo da requisição

            itemPedidoRepository.UpdateQuantidade(itemPedido);

        }
    }
}

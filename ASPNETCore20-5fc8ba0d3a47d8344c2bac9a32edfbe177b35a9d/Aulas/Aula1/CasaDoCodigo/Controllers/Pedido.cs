using CasaDoCodigo.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CasaDoCodigo.Controllers
{
    public class Pedido : Controller
    {
        private readonly IProdutoRepository produtoRepository;
        private readonly IPedidoRepository pedidoRepository;


        public Pedido(IProdutoRepository produtoRepository, 
            IPedidoRepository pedidoRepository)
        {
            this.produtoRepository = produtoRepository;
            this.pedidoRepository = pedidoRepository;
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
    }
}

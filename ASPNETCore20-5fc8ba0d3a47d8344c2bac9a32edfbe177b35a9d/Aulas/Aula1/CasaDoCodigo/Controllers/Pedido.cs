using CasaDoCodigo.Models;
using CasaDoCodigo.Models.ViewModels;
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
            var pedido = pedidoRepository.GetPedido();

            if (pedido == null)
            {
                return RedirectToAction("Carrossel");
            }

            return View(pedido.Cadastro);
        }

        public IActionResult Carrinho(string codigo)
        {
            if (!string.IsNullOrEmpty(codigo))
            {
                pedidoRepository.AddItem(codigo);
            }

            var itens = pedidoRepository.GetPedido().Itens;
            var carrinhoViewModel = new CarrinhoViewModel(itens);
            return View(carrinhoViewModel);
        }

        /* ATRIBUTO HTTP POST VAI IMPEDIR A CHAMADA/REQUISIÇÃO DIRETO DO BROWSER 
         * AFINAL QUEREMOS PRIMEIRO FAZER O ENVIO DE UM FORMULÁRIO ANTES DE ACESSAR O RESUMO */
        [HttpPost]
        [ValidateAntiForgeryToken]
        /* Previne acessar ao recurso /pedido/resumo diretamente fazendo ataques externos
         * Podemos fazer uma requisição via postman sem essa annotation e com para ver a diferença */
        public IActionResult Resumo(Cadastro cadastro)
        {
            /* PODE SER QUE NÃO PREENCHEMOS OS DADOS CORRETOS LÁ NA VIEW
             * É SEMPRE IMPORTANTE VALIDAR NO CLIENTE E NO SERVIDOR 
             * IREMOS VERIFICAR O ESTADO DO NOSSO MODELO 
                Em determinado momento, você necessita 
                proteger uma action do controller contra ataques 
                CSRF (Cross-site Request Forgery, 
                ou Falsificação de solicitação entre sites).
             */
            if (ModelState.IsValid)
            {
                var pedido = pedidoRepository.UpdateCadastro(cadastro);

                return View(pedido);
            }

            return RedirectToAction("Cadastro");
        }

        // MÉTODO POST EXIGE QUE OS PARÂMETROS SEJAM PASSADOS PELO CORPO DA REQUISIÇÃO
        // ela é ideal para alterar alguma informação no seu sistema
        // MÉTODO GET PODEMOS UTILIZAR QUERY STRING PARA PASSAR VALOR PARA OS PARÂMETROS
        // USAREMOS MÉTODO GET PARA OBTER DADOS APENAS
        [HttpPost]
        /* o antigorgerytoken eliminará o token, sendo assim 
         * criaremos um form na carrinho.js para gerar um token e na carrinho js iremos utiliza-lo */
        [ValidateAntiForgeryToken] 
        public UpdateQuantidadeResponse UpdateQuantidade([FromBody]ItemPedido itemPedido)
        {
            //FromBody indica que o parâmetro virá do corpo da requisição

            return pedidoRepository.UpdateQuantidade(itemPedido);

            
        }
    }
}

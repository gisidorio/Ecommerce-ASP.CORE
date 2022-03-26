class Carrinho {
    ClickIncremento(button) {
        let data = this.GetData(button);
        data.Quantidade++;
        this.PostQuantidade(data);
    }

    ClickDecremento(button) {
        let data = this.GetData(button);
        data.Quantidade--;
        this.PostQuantidade(data);
    }

    UpdateQuantidade(input) {
        let data = this.GetData(input);
        this.PostQuantidade(data);
    }

    GetData(elemento) {
        let linha_item = $(elemento).parents('[item-id]'); // pega os pais do elemento
        let item_id = (linha_item).attr('item-id');
        let novaQuantidade = $(linha_item).find('input').val();

        let data = {
            Id: item_id,
            Quantidade: novaQuantidade
        };

        return data;
    }

    PostQuantidade(data) {

        let token = $('[name=__RequestVerificationToken]').val();

        let headers = {};
        headers['RequestVerificationToken'] = token;


        //Asynchronous JAvavascript Xml
        // Assíncrono signífica que o seu código não vai aguardar a resposta do servidor pra continuar rodando
        /* PARA FAZERMOS UMA REQUISIÇÃO AJAX COM JQUERY PRECISAREMOS DE 4 PARÂMETROS
           1° url: o endereço do nosso método no controller (pedido/updatequantidade)
           2° tipo: o tipo de requisição HTTP que escolhemos (POST nesse caso)
           3° contentType: o formato dos dados (nesse caso é JSON)
           4° data: o objeto que vai levar os dados do cliente para o servidor
        */
        $.ajax({
            url: '/Pedido/UpdateQuantidade',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(data), // JSON.STRINGFY TRANSFORMA OS DADOS EM STRING
            headers: headers
        }).done(function (response) {
            // função done representa o término da requisição

            // location.reload irá recarregar a página famoso (F5)
            //location.reload();
            // PORÉM RECARREGAR TODA A PÁGINA SÓ PARA MODIFICAR A QUANTIDADE NÃO É O IDEAL
            // POIS AUMENTA O CONSUMO DE BANDA, PIORA PERFORMANCE, IMAGINA AO MÚLTIPLICAR ESSE PROCESSO
            // POR CADA USUÁRIO QUE ACESSA ESSA PÁGINA
            // por isso foi criada uma model chamada UpdateQuantidadeReponse

            let item_pedido = response.itemPedido; // recebe o itemPedido vindo da action
            let linha_item = $('[item-id=' + item_pedido.id + ']'); // recebe o elemento com o respectivo item
            linha_item.find('input').val(item_pedido.quantidade); // muda a quantidade             
            //console.log(linha_item.find('[subtotal]'));
            linha_item.find('[subtotal]').html((item_pedido.subtotal).duasCasas());

            let carrinhoViewModel = response.carrinhoViewModel;
            $('[numero-itens]').html('Total: ' + carrinhoViewModel.itens.length + ' itens');

            if (item_pedido.quantidade == 0) {
                linha_item.remove();
            }

            $('[total]').html((carrinhoViewModel.total).duasCasas());

        });
    }
}

let carrinho = new Carrinho();

Number.prototype.duasCasas = function () {
    return this.toFixed(2).replace('.', ',');
}
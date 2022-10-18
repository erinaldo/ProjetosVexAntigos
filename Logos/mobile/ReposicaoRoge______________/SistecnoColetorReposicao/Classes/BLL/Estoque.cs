using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace SistecnoColetor.Classes.BLL
{
    public class Estoque
    {

        public void EntrarComUA(string IdUa, string IdProdutoCliente, DateTime? Validade, string Quantidade, string Referencia, string IdDepositoplantaLocalizacao, string IdProdutoEmbalagem)
        {
            new DAL.Estoque().EntrarComUA(IdUa, IdProdutoCliente, Validade, Quantidade, Referencia, IdDepositoplantaLocalizacao, IdProdutoEmbalagem);
        }
        public void Sair(string IdUa, string IdProdutoCliente, string Quantidade, string IdDepositoplantaLocalizacao)
        {
            new DAL.Estoque().Sair(IdUa, IdProdutoCliente, Quantidade, IdDepositoplantaLocalizacao);
        }

        public void SairPorPicking(string IdUa, string IdProdutoCliente, string Quantidade, string IdDepositoplantaLocalizacaoOrigem, string idUaDestino, string IdProdutoEmbalagem)
        {
            new DAL.Estoque().SairPorPicking(IdUa, IdProdutoCliente, Quantidade, IdDepositoplantaLocalizacaoOrigem, idUaDestino, IdProdutoEmbalagem);
        }

        public void SairPorPicking(string IdUa, string IdProdutoCliente, string Quantidade, string IdDepositoplantaLocalizacaoOrigem, string idUaDestino, string IdProdutoEmbalagem, string IdMovimentacaoItem, string IdRomaneio)
        {
            new DAL.Estoque().SairPorPicking(IdUa, IdProdutoCliente, Quantidade, IdDepositoplantaLocalizacaoOrigem, idUaDestino, IdProdutoEmbalagem, IdMovimentacaoItem, IdRomaneio);

        }

        public void SairUAInteira(string IdUa, string IdProdutoCliente, string Quantidade, string IdDepositoplantaLocalizacaoOrigem, string IdDepositoplantaLocalizacaoDestino, string IdProdutoEmbalagem)
        {
            new DAL.Estoque().SairUAInteira(IdUa, IdProdutoCliente, Quantidade, IdDepositoplantaLocalizacaoOrigem, IdDepositoplantaLocalizacaoDestino, IdProdutoEmbalagem);
        }

        public void SairUAInteira(string IdUa, string IdProdutoCliente, string Quantidade, string IdDepositoplantaLocalizacaoOrigem, string IdDepositoplantaLocalizacaoDestino, string IdProdutoEmbalagem, string IdMovimentacaoItem, string IdRomaneio)
        {
            new DAL.Estoque().SairUAInteira(IdUa, IdProdutoCliente, Quantidade, IdDepositoplantaLocalizacaoOrigem, IdDepositoplantaLocalizacaoDestino, IdProdutoEmbalagem, IdMovimentacaoItem, IdRomaneio);

        }
    }
}
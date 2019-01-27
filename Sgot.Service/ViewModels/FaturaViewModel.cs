using System;
using System.Collections.Generic;
using System.Text;

namespace Sgot.Service.ViewModels
{
    public class FaturaViewModel
    {
        public string Id { get; set; }
        public string Valor { get; set; }
        public string Total { get; set; }
        public string Sinal { get; set; }
        public string IsPaga { get; set; }
        public string DataPagamento { get; set; }
        public string NumeroParcelas { get; set; }
        public string FormaPagamento { get; set; }
        public string PedidoId { get; set; }
        public string ApplicationUserId { get; set; }
        public string ClienteId { get; set; }
        public PedidoViewModel Pedido { get; set; }
        public ClienteViewModel Cliente { get; set; }
        public IList<ParcelaViewModel> Parcelas { get; private set; }
    }
}

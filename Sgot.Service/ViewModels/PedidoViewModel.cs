using Sgot.Domain.Entities;
using System.Collections.Generic;

namespace Sgot.Service.ViewModels
{
    public class PedidoViewModel
    {
        public string Id { get; set; }
        public string ApplicationUserId { get; set; }
        public string ClienteId { get; set; }
        public string OculosId { get; set; }
        public string FaturaId { get; set; }
        public string Servico { get; set; }
        public string Obs { get; set; }
        public string Medico { get; set; }
        public string DataEntrega { get; set; }
        public string DataSolicitacao { get; set; }
        public ClienteViewModel Cliente { get; set; }
        public IList<OculosViewModel> Oculos { get; set; }
        public FaturaViewModel Fatura { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public string FormaPagamento { get; set; }
        public string Preco { get; set; }
    }
}

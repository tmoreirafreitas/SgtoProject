using System;
using System.Collections.Generic;
using System.Text;

namespace Sgot.Service.ViewModels
{
    public class ParcelaViewModel
    {
        public string Id { get; set; }
        public string Numero { get; set; }
        public string Valor { get; set; }
        public string DataVencimento { get; set; }
        public string DataPagamento { get; set; }
        public string Recebido { get; set; }
        public string FaturaId { get; set; }        
    }
}

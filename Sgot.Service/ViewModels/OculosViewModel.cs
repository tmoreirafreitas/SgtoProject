using System;
using System.Collections.Generic;
using System.Text;

namespace Sgot.Service.ViewModels
{
    public class OculosViewModel
    {
        public string Id { get; set; }
        public IList<LenteViewModel> Lentes { get; private set; }
        public string Cor { get; set; }
        public string DP { get; set; }
        public string ALT { get; set; }
        public string PedidoId { get; set; }        

        public OculosViewModel()
        {
            Lentes = new List<LenteViewModel>();
        }
    }
}

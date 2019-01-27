using System;
using System.Collections.Generic;
using System.Text;

namespace Sgot.Service.ViewModels
{
    public class ClienteViewModel
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public string Rg { get; set; }
        public string Cpf { get; set; }
        public string Nascimento { get; set; }
        public string Filiacao { get; set; }
        public string EnderecoId { get; set; }
        public bool IsSPC { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string Sexo { get; set; }
        public EnderecoViewModel Endereco { get; set; }
    }
}

using Sgot.Domain.Validators;
using System;
using System.Collections.Generic;

namespace Sgot.Domain.Entities
{
    public class Cliente : Entity
    {
        public string Nome { get; private set; }
        public string Rg { get; private set; }
        public string Cpf { get; private set; }
        public DateTime Nascimento { get; private set; }
        public string Filiacao { get; private set; }        
        //public long EnderecoId { get; private set; }
        public bool IsSPC { get; private set; }
        public string Telefone { get; private set; }
        public string Email { get; private set; }
        public SexoType Sexo { get; private set; }
        public string Logradouro { get; private set; }
        public string Bairro { get; private set; }
        public string Cidade { get; private set; }
        public string Estado { get; private set; }
        public int? Numero { get; private set; }
        public string Complemento { get; private set; }
        public string Cep { get; private set; }

        //public virtual Endereco Endereco { get; set; }
        public virtual IList<Pedido> Pedidos { get; private set; }
        public virtual IList<Fatura> Faturas { get; private set; }

        public Cliente(string nome, string rg, string cpf,
            DateTime nascimento, string filiacao,
            string telefone, string email, SexoType sexo,
            string logradouro, string bairro, string cidade,
            string estado, int? numero, string complemento, string cep,
            bool isSPC = false)
        {
            Nome = nome;
            Rg = rg;
            Cpf = cpf;
            Nascimento = nascimento;
            Filiacao = filiacao;            
            IsSPC = isSPC;
            Telefone = telefone;
            Email = email;
            Sexo = sexo;
            Logradouro = logradouro;
            Bairro = bairro;
            Cidade = cidade;
            Estado = estado;
            Numero = numero;
            Complemento = complemento;
            Cep = cep;

            Pedidos = new List<Pedido>();
            Faturas = new List<Fatura>();
            Validate(this, new ClienteValidator());
        }
    }
}
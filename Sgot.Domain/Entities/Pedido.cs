using Sgot.Domain.Validators;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Sgot.Domain.Entities
{
    public class Pedido : Entity
    {
        public string ApplicationUserId { get { if (ApplicationUser != null) return ApplicationUser.Id; return string.Empty; } private set { } }
        public long ClienteId { get { if (Cliente != null) return Cliente.Id; return 0; } private set { } }
        public long FaturaId { get { if (Fatura != null) return Fatura.Id; return 0; } private set { } }
        public string Servico { get; private set; }
        public string Obs { get; private set; }
        public string Medico { get; private set; }
        public DateTime DataEntrega { get; private set; }
        public DateTime DataSolicitacao { get; private set; }
        public virtual Cliente Cliente { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual IList<Oculos> Oculos { get; private set; }
        public virtual Fatura Fatura { get; set; }
        public FormaPagamento FormaPagamento { get; private set; }
        public decimal Preco { get; private set; }

        public Pedido(string servico, string obs, string medico,
            DateTime dataEntrega, DateTime dataSolicitacao,
            FormaPagamento formaPagamento, decimal preco)
        {
            //ApplicationUserId = applicationUserId;
            //ClienteId = clienteId;
            //OculosId = oculosId;
            //FaturaId = faturaId;
            //Cliente = cliente;
            //ApplicationUser = applicationUser;
            //Fatura = fatura;

            Servico = servico;
            Obs = obs;
            Medico = medico;
            DataEntrega = dataEntrega;
            DataSolicitacao = dataSolicitacao;
            Oculos = new List<Oculos>();
            FormaPagamento = formaPagamento;
            Preco = preco;
            Validate(this, new PedidoValidator());
        }
    }
}
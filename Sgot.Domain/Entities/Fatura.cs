using Sgot.Domain.Validators;
using System;
using System.Collections.Generic;

namespace Sgot.Domain.Entities
{
    public class Fatura : Entity
    {
        public decimal Valor { get; private set; }
        public decimal Total { get; private set; }
        public decimal Sinal { get; private set; }
        public bool IsPaga { get; private set; }
        public DateTime DataPagamento { get; private set; }
        public int NumeroParcelas { get; private set; }
        public FormaPagamento FormaPagamento { get; private set; }
        public virtual Pedido Pedido { get; set; }
        public virtual Cliente Cliente { get; set; }
        public virtual IList<Parcela> Parcelas { get; private set; }
        public long PedidoId
        {
            get
            {
                if (Pedido != null)
                {
                    return Pedido.Id;
                }
                return 0;
            }
            private set { }
        }
        public long ClienteId
        {
            get
            {
                if (Cliente != null)
                    return Cliente.Id;
                return 0;
            }
            private set { }
        }
        public Fatura(decimal valor, decimal total, decimal sinal, bool isPaga,
            DateTime dataPagamento, int numeroParcelas, FormaPagamento formaPagamento)
        {
            Valor = valor;
            Total = total;
            Sinal = sinal;
            IsPaga = isPaga;
            DataPagamento = dataPagamento;
            NumeroParcelas = numeroParcelas;
            FormaPagamento = formaPagamento;
            Parcelas = new List<Parcela>();

            Validate(this, new FaturaValidator());
        }
    }
}
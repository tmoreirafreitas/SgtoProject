using Sgot.Domain.Validators;
using System;

namespace Sgot.Domain.Entities
{
    public class Parcela : Entity
    {
        public int Numero { get; private set; }
        public decimal Valor { get; private set; }
        public DateTime DataVencimento { get; private set; }
        public DateTime DataPagamento { get; private set; }
        public bool Recebido { get; private set; }
        public virtual Fatura Fatura { get; set; }
        public long FaturaId { get { if (Fatura != null) return Fatura.Id; return 0; } private set { } }
        public Parcela(int numero, decimal valor, DateTime dataVencimento, DateTime dataPagamento, bool recebido)
        {
            Numero = numero;
            Valor = valor;
            DataVencimento = dataVencimento;
            DataPagamento = dataPagamento;
            Recebido = recebido;
            Validate(this, new ParcelaValidator());
        }
    }
}
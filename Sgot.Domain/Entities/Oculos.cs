using Sgot.Domain.Validators;
using System;
using System.Collections.Generic;

namespace Sgot.Domain.Entities
{
    public class Oculos : Entity
    {
        public virtual IList<Lente> Lentes { get; private set; }
        public string Cor { get; private set; }
        public float DP { get; private set; }
        public float ALT { get; private set; }        
        public virtual Pedido Pedido { get; set; }
        public long PedidoId {
            get
            {
                if (Pedido != null)
                    return Pedido.Id;
                return 0;
            }
            private set { }
        }
        public float Adicao
        {
            get
            {
                float adicao = 0;

                if (Lentes != null && Lentes.Count == 2)
                {
                    adicao = Lentes[0].Grau + Lentes[1].Grau;
                    return adicao;
                }
                else
                {
                    throw new ArgumentException("Erro no cálculo do grau do óculos, objeto OD ou OE são nulos!");
                }
            }
            private set
            {

            }
        }

        public Oculos(string cor, float dP, float aLT, long pedidoId)
        {
            Cor = cor;
            DP = dP;
            ALT = aLT;                        
            Lentes = new List<Lente>(2);
            Validate(this, new OculosValidator());
        }
    }
}
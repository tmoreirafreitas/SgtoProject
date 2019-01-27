using Sgot.Domain.Validators;

namespace Sgot.Domain.Entities
{
    public class Lente : Entity
    {
        public float Grau { get; private set; }
        public float Cyl { get; private set; }
        public byte Eixo { get; private set; }
        public long OculosId { get; private set; }
        public LenteType LenteType { get; private set; }
        public virtual Oculos Oculos { get; private set; }

        public Lente(float grau, float cyl, byte eixo, LenteType lenteType)
        {
            Grau = grau;
            Cyl = cyl;
            Eixo = eixo;
            LenteType = lenteType;

            Validate(this, new LenteValidator());
        }
    }
}
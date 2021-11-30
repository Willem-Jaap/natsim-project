using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NatSimII
{
    public abstract class Vleeseter : Dier
    {
        public Vleeseter(int verhoudingTicksJaren,
           string latijnseNaam,
           int levensduur,
           double gewichtMaximaal)
           : base(verhoudingTicksJaren, latijnseNaam, levensduur, gewichtMaximaal) { }
        public override void Eet(Leven leven)
        {
            if (leven.IsDier)
            {
                if (leven.Voedingswaarde < this.Voedingswaarde)
                {
                    leven.Sterf();
                }
            }
            // Als het leven geen plant is keert de planteneter om. 
            else
            {
                SnelheidObject = SnelheidObject.Keerom();
            }

        }
    }
}

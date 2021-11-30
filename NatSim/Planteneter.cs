using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NatSimII
{
    public abstract class Planteneter : Dier
    {
        public Planteneter(int verhoudingTicksJaren,
        string latijnseNaam,
        int levensduur,
        double gewichtMaximaal)
        : base(verhoudingTicksJaren, latijnseNaam, levensduur, gewichtMaximaal) { }
        public override void Eet(Leven leven)
        {
            if (leven.IsPlant)
            {
                // Wat doet een planteneter met giftige planten? 
                if (WordtVergiftigdDoor.Contains(leven.NederlandseNaam))
                {
                    // Normaal eet de planteneter geen giftige planten. Behalve als de 
                    // planteneter honger heeft. Alleen gaat de planteneter dan wel dood. 
                    // De planteneter eet een giftige plant en wordt verwijderd. 
                    if (Honger())
                    {
                        this.Sterf();
                    }
                    else
                    {                         // Als de plant niet eetbaar is, keer dan om. 
                        SnelheidObject = SnelheidObject.Keerom();
                    }
                }
                // Alles wat niet giftig is wordt gewoon opgegeten als er nog plek is. 
                else if (MaagGevuld < 100)
                {
                    MaagGevuld = MaagGevuld + leven.Voedingswaarde;
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

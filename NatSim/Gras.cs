using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace NatSimII
{
    public class Gras : Plant
    {
        public Gras() : base(1, "Gramineae", 4, Bloeiwijze.aar)
        {
            initClass(new Point(0, 0));
        }
        public Gras(Point locatie) : base(1, "Gramineae", 4, Bloeiwijze.aar)
        {
            initClass(locatie);
        }

        private void initClass(Point locatie)
        {
            Locatie = locatie;
            Tekengebied.Afmetingen = new Size(2, 10);
            Kleur = Color.LawnGreen;
            Voedingswaarde = 1;
        }
    }
    public enum Bloeiwijze
    {
        none,
        hoofdje,
        bloemkoek,
        bloemkluwen,
        aar,
        aartje,
        katje,
        bloeikolf,
        tros,
        schermvormigeTros,
        bundel,
        scherm,
        schijnkras,
        eentakkigBijscherm,
        schroef,
        sikkel,
        schicht,
        waaier,
        samengesteldScherm,
        samengesteldGevorktScherm,
        samengesteldeAar,
        pluim,
        dichasialePluim,
        tuil,
        dichasialeTuil,
        thyrsus,
        kegel
    }
}

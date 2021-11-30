using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace NatSimII
{
    public class Lynx : Vleeseter
    {
        // 
        // Initialisatie 
        // 
        public Lynx()
: base(_verhoudingTicksJaren, _latijnseNaam, _leeftijd, _gewichtMaximaal)
        {
            initClass(new Point(0, 0), "", Color.Gray);
        }
        public Lynx(Point locatie)
        : base(_verhoudingTicksJaren, _latijnseNaam, _leeftijd, _gewichtMaximaal)
        {
            initClass(locatie, "", Color.Gray);
        }
        public Lynx(Point locatie, string naam, Color kleur)
        : base(_verhoudingTicksJaren, _latijnseNaam, _leeftijd, _gewichtMaximaal)
        {
            initClass(locatie, naam, kleur);
        }
        // 
        // Kenmerken van de jaguar 
        // 
        private void initClass(Point locatie, string naam, Color kleur)
        {
            Locatie = locatie;
            Naam = naam;
            Kleur = kleur;
            Tekengebied.Afmetingen = new Size(7, 4);
            Gewicht = 24;
        }

        // 
        // ReadOnly-variabelen 
        // 
        private const string _latijnseNaam = "Lynx";
        private const int _leeftijd = 24;
        private const int _verhoudingTicksJaren = 1;
        private const int _gewichtMaximaal = 36;

        // 
        // Eigenschappen 
        // 
        public string Naam { get; set; }
        public double Gewicht { get; set; }
        public DateTime Geboortedatum { get; set; }
        public DateTime Sterfdatum { get; set; }
    }
}
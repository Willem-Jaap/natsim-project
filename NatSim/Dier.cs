using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace NatSimII
{
    public abstract class Dier : Leven, IBewegendObject
    {
        public Dier(int verhoudingTicksJaren,  string latijnseNaam, int levensduur, double gewichtMaximaal): base(verhoudingTicksJaren, latijnseNaam, levensduur)
        {
            initDier(gewichtMaximaal);
        }
        private void initDier(double gewichtMaximaal)
        {
            _gewichtMaximaal = gewichtMaximaal;
            WordtVergiftigdDoor = new List<string>();
        }
        // 
        // Private variabelen 
        // 
        // Maximaal gewicht van een dier in kg: 
        private double _gewichtMaximaal = 0;
        // 
        // Eigenschappen voor alle dieren 
        // 
        // Van 0 tot 100% (honger tot 75%): 
        public int MaagGevuld { get; set; }
        // Aantal ticks dat het duurt voordat een maaltijd verteerd is: 
        public int SpijsverteringsDuur { get; set; }
        // Maximaal gewicht van een dier in kg: 
        public double GewichtMaximaal
        { get { return _gewichtMaximaal; } }
        public List<string> WordtVergiftigdDoor { get; set; }
        public Snelheid SnelheidObject { get; set; }
        public Timer klok { get; set; }

        // 
        // Methoden 
        // 
        // Een kenmerk van dieren is dat ze eten. Op dit niveau weten we nog niet wat. 
        // Een konijn eet geen lynx, andersom wel, en een lynx eet geen gras. 
        public abstract void Eet(Leven leven);
        // Dieren zullen wel, net als in NatSim, alleen eten als ze honger hebben: 
        public bool Honger()
        {
            return (MaagGevuld < 25);
        }

        public Point Stap()
        {
            return Stap(this.SnelheidObject);
        }
        public Point Stap(Snelheid SnelheidObject)
        {
            this.SnelheidObject = SnelheidObject;

            int berekendeX = Locatie.X + SnelheidObject.X;
            int berekendeY = Locatie.Y + SnelheidObject.Y;

            Rechthoek nieuwTekengeboed = new Rechthoek(new Point(berekendeX, berekendeY), Tekengebied.Afmetingen);

            Vlak vlak = Rechthoek.GrensBereik(nieuwTekengeboed, GraphicsVenster);

            SnelheidObject = SnelheidObject.Stuiter(vlak);

            berekendeX = Locatie.X + SnelheidObject.X;
            berekendeY = Locatie.Y + SnelheidObject.Y;

            this.SnelheidObject = SnelheidObject;

            return new Point(berekendeX, berekendeY);
        }

        public void Beweeg()
        {
            Verwijder();
            Locatie = Stap();
            Teken();
        }

        public override string ToString()
        {
            Type type = this.GetType();
            string diertypen = "";
            if (type.IsSubclassOf(typeof(Vleeseter)))
            {
                diertypen = "Vleeseter";
            }
            else if (type.IsSubclassOf(typeof(Planteneter)))
            {
                diertypen = "Planteneter";
            }
            else if (type.IsSubclassOf(typeof(Alleseter)))
            {
                diertypen = "Alleseter";
            }

            return "Latijnse naam: " + LatijnseNaam + Environment.NewLine
                + Environment.NewLine + "Nederlandse naam:" + NederlandseNaam
                + Environment.NewLine + Environment.NewLine + "Levensduur: " + Levensduur
                + Environment.NewLine + Environment.NewLine + "DierSoort: " + diertypen
                + Environment.NewLine + Environment.NewLine + "Locatie: " + Locatie.ToString();
        }

        public bool IsBotsing(Leven leven)
        {
            if (this.Tekengebied.Overlap(leven.Tekengebied))
            {
                Dier dier = leven.ToDier();
                if (dier != null)
                {
                    this.SnelheidObject = this.SnelheidObject.Keerom();
                    dier.SnelheidObject = dier.SnelheidObject.Keerom();
                }
                Eet(leven);
                return true;
            }
            return false;
        }
    }
}

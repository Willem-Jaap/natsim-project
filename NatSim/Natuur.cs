using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace NatSimII
{
    public class Natuur : List<Leven>
    {
        Timer _levensklok = new Timer();
        public new void Add(Leven leven)
        {
            leven.Einde += Leven_Einde;
            leven.OpObject += Leven_OpObject;
            if (leven.IsDier)
            {
                Random random = new Random();
                Snelheid snelheid = new Snelheid(random.Next(-4, 4), random.Next(-4, 4));
                ((Dier)leven).SnelheidObject = snelheid;
            }

            base.Add(leven);

            if (NieuwLeven != null)
            {
                NieuwLeven(this, new NieuwLevenArgs(leven));
            }
        }

        private void Leven_OpObject(object sender, EventArgs e)
        {
            if (Getroffen != null)
            {
                Getroffen(this, new GetroffenEventAgs((Leven)sender));
            }
        }

        private void Leven_Einde(object sender, EventArgs e)
        {
            this.Remove((Leven)sender);
        }

        public Natuur() : base()
        {
            _levensklok.Interval = 10;
            _levensklok.Start();
            _levensklok.Tick += _levensklok_Tick;
        }

        private void _levensklok_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < this.Count; i++)
            {
                Dier dier = this[i].ToDier();
                if (dier != null)
                {
                    dier.Beweeg();
                    CollisionDetection(dier);
                }
            }
        }

        public void CollisionDetection(Dier dier)
        {
            for (int i = 0; i < this.Count; i++)
            {
                if (dier.ID != this[i].ID)
                {
                    dier.IsBotsing(this[i]);
                }
            }
        }

        public event EventHandler<NieuwLevenArgs> NieuwLeven;
        public event EventHandler<GetroffenEventAgs> Getroffen;

        public void LevenGeraakt(Point locatie)
        {
            foreach (Leven leven in this)
            {
                leven.IsOpObject(locatie);
            }
        }

        public void Zaaien(Point locatie, Graphics papier, Plant plant)
        {
            Zaaien(locatie, papier, 150, 46, 15, plant);
        }
        public void Zaaien(Point locatie, Graphics papier, int lengte, int breedte, int zaaiAfstand, Plant plant)
        {
            int puntX = locatie.X - lengte / 2;
            int puntY = locatie.Y - breedte / 2;

            Point oorspronkelijkeLocatie = locatie;
            int startpuntY = puntY;

            lengte = puntX + lengte;
            breedte = puntY + breedte;

            while (puntX < lengte)
            {
                while (puntY < breedte)
                {
                    locatie = new Point(puntX, puntY);
                    if (plant.ToGras() != null)
                    {
                        Gras gras = new Gras(locatie);
                        Add(gras);
                    }
                    puntY = puntY + zaaiAfstand;
                }
                puntY = startpuntY;
                puntX = puntX + zaaiAfstand;
            }
        }
    }

    public enum Soortleven
    {
        Gras,
        Vingerhoedskruid,
        Venijnboom,
        Koe,
        Konijn,
        Beer,
        Lynx
    }

    public class NieuwLevenArgs : EventArgs
    {
        public NieuwLevenArgs(Leven leven)
        {
            NieuwLeven = leven;
        }

        public Leven NieuwLeven { get; set; }
    }

    public class GetroffenEventAgs : EventArgs
    {
        public GetroffenEventAgs(Leven leven)
        {
            Getroffen = leven;
            GeraaktOp = DateTime.Now;
        }

        public Leven Getroffen { get; set; }
        public DateTime GeraaktOp { get; set; }
    }
}

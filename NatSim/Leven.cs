using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NatSimII
{
    public abstract class Leven : GrafischObject
    {
        public Venijnboom ToVenijnboom()
        {
            if (this.GetType() == typeof(Venijnboom))
            {
                return (Venijnboom)this;
            }
            else
            {
                return null;
            }
        }

        public Vingerhoedskruid ToVingerhoedskruid()
        {
            if (this.GetType() == typeof(Vingerhoedskruid))
            {
                return (Vingerhoedskruid)this;
            }
            else
            {
                return null;
            }
        }
        public Gras ToGras()
        {
            if (this.GetType() == typeof(Gras))
            {
                return (Gras)this;
            }
            else
            {
                return null;
            }
        }
        public Plant ToPlant()
        {
            if (IsPlant) return (Plant)this;
            else return null;
        }
        public Dier ToDier()
        {
            if (IsDier) return (Dier)this;
            else return null;
        }
        public Soortleven ToSoort()
        {
            switch (NederlandseNaam.ToLower())
            {
                case "beer": return Soortleven.Beer;
                case "gras": return Soortleven.Gras;
                case "koe": return Soortleven.Koe;
                case "konijn": return Soortleven.Konijn;
                case "lynx": return Soortleven.Lynx;
                case "venijnboom": return Soortleven.Venijnboom;
                case "vingerhoedskruid": return Soortleven.Vingerhoedskruid;
                default: return Soortleven.Gras;
            }
        }
        public Leven()
        {
            initClass(1, "", int.MaxValue);
        }

        public Leven(int verhoudingTicksJaren, string latijnseNaam, int levensduur)
        {
            initClass(verhoudingTicksJaren, latijnseNaam, levensduur);
        }

        private const int _aantalTicksPerSeconde = 1000;
        private string _latijnseNaam;
        private double _levensduur;
        private Timer _verouder;

        public int Leeftijd { get; set; }
        public int VerhoudingTicksJaren { get; set; }
        public int Voedingswaarde { get; set; }

        public string LatijnseNaam { get { return _latijnseNaam; } }
        public double Levensduur { get { return _levensduur; } }
        public string NederlandseNaam { get { return base.ToString().Split('.').Last(); } }
        public Timer Verouder { get { return _verouder; } }

        public void Sterf()
        {
            Verwijder();
            OnEinde();
        }

        private void initClass(int verhoudingTicksJaren, string latijnseNaam, int levensduur)
        {
            VerhoudingTicksJaren = verhoudingTicksJaren;
            _latijnseNaam = latijnseNaam;
            _levensduur = levensduur;
            _verouder = new Timer();
            _verouder.Interval = _aantalTicksPerSeconde * VerhoudingTicksJaren;
            _verouder.Start();
            _verouder.Tick += _verouder_Tick;
        }

        private void _verouder_Tick(object sender, EventArgs e)
        {
            if (Leeftijd < Levensduur)
            {
                Leeftijd++;
            }
            else
            {
                _verouder.Stop();
                Sterf();
            }
        }

        public bool IsPlant
        {
            get { return this.GetType().IsSubclassOf(typeof(Plant)); }
        }

        public bool IsDier
        {
            get { return this.GetType().IsSubclassOf(typeof(Dier)); }
        }

        public event EventHandler<EventArgs> Einde;

        protected virtual void OnEinde()
        {
            if (Einde != null)
            {
                Einde(this, EventArgs.Empty);
            }
        }
    }
}

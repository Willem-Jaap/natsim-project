using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace NatSimII
{
    public abstract class GrafischObject
    {
        // 
        // Constructor 
        // 
        public GrafischObject()
        {
            initClass();
        }
        // 
        // Eigenschappen 
        // 
        public Color Kleur { get; set; }
        public Color KaderKleur { get; set; }
        public Point Locatie
        {
            // Deze eigenschap is eigenlijk een doorgefluik 
            // naar de bijbehorende instantie van Rechthoek. 
            // Door deze eigenschap hier op te nemen kan de klasse 
            // Konijn makkelijker gebruikt worden. 
            get { return Tekengebied.Locatie; }
            set { Tekengebied.Locatie = value; }
        }
        public Graphics Papier
        {
            get { return _papier; }
            set
            {
                _papier = value;
                // Om te bepalen hoe groot het tekengebied is van de control waarop 
                // getekend wordt, kun je deze van het grafisch object krijgen. 
                // Addertje onder het gras is dat de VisibleClipBounds het gebied 
                // aangeven wat je ziet. Het totale grafische object is veel groter. 
                // Omdat in uitzonderlijke gevallen een null-waarde kan ontstaan 
                // wordt hierop gecheckt om foutmeldingen te voorkomen. 
                if (value != null)
                {
                    int breedteVenster = (int)Papier.VisibleClipBounds.Width;
                    int hoogteVenster = (int)Papier.VisibleClipBounds.Height;
                    _graphicsVenster = new Rechthoek(new Point(0, 0),
                    new Size(breedteVenster, hoogteVenster));
                }
            }
        }
        public Rechthoek Tekengebied { get; set; }
        public Color Wiskleur { get; set; }
        // 48 

        // ReadOnly-eigenschappen 
        // 
        public Rechthoek GraphicsVenster { get { return _graphicsVenster; } }
        public Guid ID { get { return _id; } }
        // 
        // Variabelen 
        // 
        private Guid _id;
        private bool _verwijderd = false;
        private Graphics _papier;
        private Rechthoek _graphicsVenster;
        // 
        // Methoden 
        // 
        // De methode initClass stelt de standaardwaarde in voor enkele 
        // eigenschappen. De methode is private omdat deze methode alleen binnen 
        // deze klasse gebruikt wordt. Deze methode wordt altijd door één of 
        // meerdere constructors gebruikt. 
        private void initClass()
        {
            KaderKleur = Color.Black;
            Kleur = Color.WhiteSmoke;
            Wiskleur = Color.PaleGoldenrod;
            // Grafische instellingen: 
            Tekengebied = new Rechthoek();
            // Instellen van de guid. Een guid of uuid is een acroniem voor 
            // ‘Globally Unique Identifier’ (of ‘Universally Unique Identifier’). 
            // Het is een 128-bits integer die gebruikt wordt om objecten te 
            // identificeren: 
            _id = Guid.NewGuid();
        }
        public void Verwijder()
        {
            _verwijderd = true;
            Wis();
        }
        public void Wis()
        {
            // Door het object opnieuw te tekenen met de Wiskleur, de kleur van 
            // het object waarop getekend wordt, wordt een object onzichtbaar: 
            Color oudeKleur = Kleur;
            Color oudeKaderkleur = KaderKleur;
            Kleur = Wiskleur;
            KaderKleur = Wiskleur;
            Teken();
            KaderKleur = oudeKaderkleur;
            Kleur = oudeKleur;
        }
        public void Teken(Graphics papier)
        {
            // Door het GraphicsObject waarop getekend wordt te bewaren kunnen we een 
            // getekend object ook altijd weer uitvegen: 
            Papier = papier;
            // Het tekenen van een kader op de locatie van het object met de afmetingen 
            // van het object: 
            Pen pen = new Pen(KaderKleur, 2);
            // Het tekenen van de rechthoek: 
            if (Papier != null)
            {
                Papier.DrawRectangle(pen, Tekengebied.ToRectangle());
                // Het direct vrijgeven van het geheugen van een gebruikt object: 
                pen.Dispose();
                // Het vullen van de rechthoek op de locatie van het object met de  
                // afmetingen van het object:           
                SolidBrush kwast = new SolidBrush(Kleur);
                Papier.FillRectangle(kwast, Tekengebied.ToRectangle());
                // Het direct vrijgeven van het geheugen van een gebruikt object: 
                kwast.Dispose();
            }
        }
        public void Teken()
        {
            Teken(Papier);
        }

        public event EventHandler<EventArgs> OpObject;

        protected virtual void OnOpObject(object sender)
        {
            if (OpObject != null)
            {
                OpObject(this, EventArgs.Empty);
            }
        }

        public void IsOpObject(Point punt)
        {
            if (punt.X >= Tekengebied.A.X
                && punt.X <= Tekengebied.B.X
                && punt.Y >= Tekengebied.A.Y
                && punt.Y <= Tekengebied.D.Y)
            {
                OnOpObject(this);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NatSimII
{

    public partial class FrmNatSim : Form
    {
        Graphics papier;
        Soortleven SoortPlant = Soortleven.Gras;
        Soortleven SoortDier = Soortleven.Konijn;
        Natuur natuur = new Natuur();

        public FrmNatSim()
        {
            InitializeComponent();
            ResizePicureBox();
            ResizeLblInformatie();
            papier = pbWereld.CreateGraphics();
            natuur.NieuwLeven += Natuur_NieuwLeven;
            natuur.Getroffen += Natuur_Getroffen;
        }

        private void Natuur_Getroffen(object sender, GetroffenEventAgs e)
        {
            lblInformatie.Text = e.GeraaktOp.ToString("hh:mm:ss tt") +
                Environment.NewLine + Environment.NewLine + e.Getroffen.ToString();
        }

        private void Natuur_NieuwLeven(object sender, NieuwLevenArgs e)
        {
            e.NieuwLeven.Teken(papier);
        }

        private void ResizePicureBox()
        {
            int breedte = 44;
            int hoogte = 60;
            pbWereld.Width = this.Width - grbDieren.Width - lblInformatie.Width - breedte;
            pbWereld.Height = this.Height - hoogte;
            papier = pbWereld.CreateGraphics();
        }

        private void ResizeLblInformatie()
        {
            int hoogte = 88;
            lblInformatie.Height = this.Height - hoogte - pnlKnoppen.Height;
        }

        private void FrmNatSim_Resize(object sender, EventArgs e)
        {
            ResizePicureBox();
            ResizeLblInformatie();
        }

        private void pbWereld_MouseClick(object sender, MouseEventArgs e)
        {
            Soortleven soortLeven = Soortleven.Gras;
            if (e.Button == MouseButtons.Left)
            {
                soortLeven = SoortDier;
            }
            else if (e.Button == MouseButtons.Right)
            {
                soortLeven = SoortPlant;
            }

            switch (soortLeven)
            {
                case Soortleven.Gras:
                    natuur.Add(new Gras(e.Location));
                    break;
                case Soortleven.Vingerhoedskruid:
                    natuur.Add(new Vingerhoedskruid(e.Location));
                    break;
                case Soortleven.Venijnboom:
                    natuur.Add(new Venijnboom(e.Location));
                    break;
                case Soortleven.Koe:
                    natuur.Add(new Koe(e.Location));
                    break;
                case Soortleven.Konijn:
                    natuur.Add(new Konijn(e.Location));
                    break;
                case Soortleven.Beer:
                    natuur.Add(new Beer(e.Location));
                    break;
                case Soortleven.Lynx:
                    natuur.Add(new Lynx(e.Location));
                    break;
                default:
                    break;
            }
        }

        private void TekenDier(Point positie)
        {
            if (rdbKonijn.Checked == true)
            {
                Konijn Konijn01 = new Konijn(positie, "Flappie", Color.Brown);
                Konijn01.Teken(pbWereld.CreateGraphics());
            }
            else if (rdbKoe.Checked == true)
            {
                Koe Koe01 = new Koe(positie, "Flappie", Color.Brown);
                Koe01.Teken(pbWereld.CreateGraphics());
            }
        }

        private void TekenPlant(Point positie)
        {
            if (rdbGras.Checked == true)
            {
                Gras Gras01 = new Gras(positie);
                Gras01.Teken(pbWereld.CreateGraphics());
            }
            else if (rdbVenijnboom.Checked == true)
            {
                Venijnboom Venijnboom01 = new Venijnboom(positie);
                Venijnboom01.Teken(pbWereld.CreateGraphics());
            }
        }

        private void FrmNatSim_Load(object sender, EventArgs e)
        {

        }

        private void rdbKonijn_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbKonijn.Checked) SoortDier = Soortleven.Konijn;
        }

        private void rdbGras_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbGras.Checked) SoortPlant = Soortleven.Gras;
        }

        private void rdbKoe_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbKoe.Checked) SoortDier = Soortleven.Koe;
        }

        private void rdbLynx_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbLynx.Checked) SoortDier = Soortleven.Lynx;
        }

        private void rdbBeer_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbBeer.Checked) SoortDier = Soortleven.Beer;
        }

        private void rdbVenijnboom_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbVenijnboom.Checked) SoortPlant = Soortleven.Venijnboom;
        }

        private void rdbVingerhoedskruid_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbVingerhoedskruid.Checked) SoortPlant = Soortleven.Vingerhoedskruid;
        }

        private void pbWereld_MouseMove(object sender, MouseEventArgs e)
        {
            if (chbZaai.Checked && rdbGras.Checked)
            {
                Gras gras = new Gras(e.Location);
                natuur.Zaaien(e.Location, pbWereld.CreateGraphics(), gras);
            }
            else
            {
                lblInformatie.Text = "";
                natuur.LevenGeraakt(e.Location);
            }
        }
    }
}

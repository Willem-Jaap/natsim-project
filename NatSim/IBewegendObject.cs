using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace NatSimII
{
    public interface IBewegendObject
    {
        Snelheid SnelheidObject { get; set; }
        Timer klok { get; set; }

        Point Stap();
        Point Stap(Snelheid SnelheidObject);
    }
}

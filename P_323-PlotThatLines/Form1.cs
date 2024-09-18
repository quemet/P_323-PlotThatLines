using System.Windows.Forms;
using ScottPlot.WinForms;

namespace P_323_PlotThatLines
{   
    public partial class Form1 : Form
    {
        readonly FormsPlot FormsPlot1 = new FormsPlot() { Dock = DockStyle.Fill };

        public Form1()
        {
            InitializeComponent();

            panel1.Controls.Add(FormsPlot1);

            Api a = new Api(this, FormsPlot1);
        }
    }
}

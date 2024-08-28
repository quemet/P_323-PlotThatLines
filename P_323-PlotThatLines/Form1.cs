using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

            double[] xs = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            double[] ys = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            FormsPlot1.Plot.Add.Scatter(xs, ys);

            FormsPlot1.Refresh();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}

using System.IO;
using System.Windows.Forms;
using ScottPlot.WinForms;
using System.Collections.Generic;

namespace P_FUN_PlotThatLines
{
    public partial class Form1 : Form
    {
		readonly FormsPlot FormsPlot1 = new FormsPlot() { Dock = DockStyle.Fill };
		
        public Form1()
        {
            InitializeComponent();
			
			bool isBitcoin = File.Exists("bitcoin.csv");

            if (isBitcoin)
            {
                MessageBox.Show("IS");
            }

            panel1.Controls.Add(FormsPlot1);

            Api a = new Api(this, FormsPlot1);
        }
    }
}

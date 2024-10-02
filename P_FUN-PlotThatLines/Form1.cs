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

            string type = "";
            string currentPath = Path.GetFullPath(".");

            bool isDotEnv = File.Exists(".env");

            if (!isDotEnv)
            {
                string newPath = currentPath + @"\.env";
                MessageBox.Show("Sorry, we cannot find the file named .env in " + newPath, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            using (StreamReader sr = new StreamReader(".env"))
            {
                string line = sr.ReadLine();
                type = line.Split('=')[1];
            }

            string path = type == "Developement" ? "../../../" : "./";


            bool isBitcoin = File.Exists(path + "bitcoin.csv");
            bool isEthereum = File.Exists(path + "ethereum.csv");
            bool isSolana = File.Exists(path + "solana.csv");

            if (!isBitcoin)
            {
                string newPath = currentPath + @"\bitcoin.csv";
                MessageBox.Show("Sorry, we cannot find the file named bitcoin.csv in " + newPath, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (!isEthereum)
            {
                string newPath = currentPath + @"\ethereum.csv";
                MessageBox.Show("Sorry, we cannot find the file named ethereum.csv in " + newPath, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (!isSolana)
            {
                string newPath = currentPath + @"\solana.csv";
                MessageBox.Show("Sorry, we cannot find the file named solana.csv in " + newPath, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            panel1.Controls.Add(FormsPlot1);

            FormPlotter a = new FormPlotter(this, FormsPlot1);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}

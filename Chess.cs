using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.VisualBasic.PowerPacks;
using Chess.Properties;

namespace Chess
{
	public partial class Chess : Form
    {
        #region Fields
        Bord bord;
		Clock clock;
		Speler speler;
		Computer computer;
        Notation notation;
        bool aanZet;
        private bool Schaak
        {
            get { return computer.IsSchaak(aanZet); }
        }
        private bool Mat
        {
            get { return computer.IsSchaakMat(aanZet); }
        }
        public bool AanZet
		{
			get { return aanZet; }
			set 
            { 
                aanZet = value;
                if (Mat)
                    clock.Show(Schaak ? "M A T" : "P A D"); // V129
                else if (Schaak)
                    clock.Show("S C H A A K");
                else
                    clock.Toon(aanZet);
            }
		}
        #endregion

		public Chess()
		{
			InitializeComponent();
			this.Text += My.Version;
			My.SetStatus(toolStripStatusLabel1);
            InitializeSchaakbord();
            LoadGameviaCmdLine(); //V124
		}

        private void LoadGameviaCmdLine()
        {
            string[] args = Environment.GetCommandLineArgs();
            if (args.Length > 1) LoadGame(args[1]);
        }
        private void InitializeSchaakbord() //V118
        {
            aanZet = Kleur.WIT;
            bord = new Bord();
            clock = new Clock(btnHint);
            notation = new Notation(listView1);
            computer = new Computer(bord, 5);
            if (speler!=null) speler.Dispose(); //V122 
            speler = new Speler(this.shapeContainer1, computer);
            speler.animate = animationToolStripMenuItem.Checked; //V146
            speler.Speler_Zet += new ZetEventHandler(Speler_Zet);
            Display(bord);
        }
		private void Speler_Zet(object sender, Zet zet)
		{
			if (sender is string) 
			{
				if (zet.van == null) bord.VoerUit(zet); // vanuit doos.
				else bord.Herstel(zet);                 // Berg op in doos verwijder stuk van bord.
			}
            else
            {
                //V140 if (zet.van.stuk.kleur == AanZet && !Mat) // Als speler aanzet is en niet mat staat.
                if (zet.van.stuk.kleur == AanZet) // Als speler aanzet is .
                {
                    bord.VoerUit(zet);
                    if (Schaak) //V136
                    {
                        if (zet.van.stuk.type == StukType.Koning)
                        {
                            MessageBox.Show("Je mag jezelf niet schaak zetten!");
                        }
                        else if (Mat) MessageBox.Show("Einde oefening. Nog een keer?","Het is MAT.");
                        else MessageBox.Show(@"1) Probeer het stuk te slaan. 
2) Probeer er een stuk tussen te zetten. 
3) Of zet koning een stapje opzij.", "Je staat schaak!");
                        //MessageBox.Show("Je mag jezelf niet schaak zetten!");
                        bord.Herstel(zet);
                        speler.Herstel(zet);
                        clock.Toon(AanZet);
                    }
                    else
                    {
                        notation.Noteer(zet);//V111
                        AanZet = !AanZet;
                        if (mensTegenComputerToolStripMenuItem.Checked) //V142
                        {
                            ComputerZet();
                        }
                    }
                }
				else
				{
					My.Status("{0} is aan zet.",AanZet?"Wit":"Zwart");
					speler.Herstel(zet);
				}
			}
		}
		private void ComputerZet()
		{
            if (Mat) return;

            speler.enabled = false;
            My.Status("Thinking");
			Application.DoEvents();
			Zet zet = computer.BedenkBesteZet(AanZet);
            if (zet != null) 
            {
                My.Status(computer.ShowZetHistory(zet));
                Application.DoEvents(); // handel speler events af.
                bord.VoerUit(zet);
                speler.VoerUit(zet);
                notation.Noteer(zet); //V111
                AanZet = !AanZet; 
            }
            speler.enabled = true;
			return;
		}
		private void Display(Bord bord)
		{
			Clear(bord);
			for (int x = 0; x < 8; x++) for (int y = 0; y < 8; y++) if (Bord.veld[x, y] != 0)
			{
			    DisplayStuk(Bord.getStuk(x, y), x, y);
				Application.DoEvents();
			}
		}
		private void DisplayStuk(Stuk stuk, int x, int y)
		{
			//Console.WriteLine("DisplayStuk {0} x,y=({1},{2})",stuk,x,y);
			OvalShape shape = speler.doos.HaalOp(stuk);
			if (shape == null) MessageBox.Show(string.Format("stuk {0} niet gevonden in doos", stuk.type));
			else shape.Location = speler.getBordLocation(x, y);
		}
		private void Clear(Bord bord)
		{
            OvalShape shape;
			for (int x = 0; x < 8; x++) for (int y = 0; y < 8; y++) //if (Bord.veld[x, y] != 0)
            {
                shape = speler.getShape(new Veld(x, y));
                if (shape != null)
                    speler.doos.BergOp(shape);
            }
		}
		private void setupGameToolStripMenuItem_Click(object sender, EventArgs e)
		{
			bord.Clear();
			Display(bord);
            notation = new Notation(listView1); //V128+
            AanZet = Kleur.WIT;
        }
		private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
		{
            InitializeSchaakbord();
		}
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close(); //V116
        }
		private void saveGameToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (saveFileDialog1.ShowDialog() == DialogResult.OK)
			{
				My.WriteToFile(saveFileDialog1.FileName, bord.Save()+notation.Save()); // V120
			}
		}
		private void loadGameToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (openFileDialog1.ShowDialog() == DialogResult.OK)
			{
                LoadGame(openFileDialog1.FileName);
			}
		}
        private void wedstrijdModeToolStripMenuItem_Click(object sender, EventArgs e) //V133 
        {
            speler.WedstrijdMode = ((ToolStripMenuItem)sender).Checked = !((ToolStripMenuItem)sender).Checked; // V134
            btnHint.Enabled = btnForward.Enabled = btnBack.Enabled = !speler.WedstrijdMode;
        }
        private void mensTegenComputerToolStripMenuItem_Click(object sender, EventArgs e) //V142
        {
            mensTegenComputerToolStripMenuItem.Checked = !mensTegenComputerToolStripMenuItem.Checked;
        }
        private void LoadGame(string filename)
        {
            string game = My.ReadFromFile(filename);
            bord.Load(game);
            Display(bord);
            notation = new Notation(listView1);
            AanZet = notation.Load(game); //V121
        }
		private void btnBack_Click(object sender, EventArgs e)
		{
            for (int i = 0; i < 1; i++) //V127 //V142+
            {
                Zet zet = notation.Back();
                if (zet != null)
                {
                    bord.Herstel(zet);
                    speler.Herstel(zet);
                    AanZet = !AanZet; //V109
                }
                
            }
        }
        private void btnForward_Click(object sender, EventArgs e) //V111
        {
            Zet zet = notation.Forward();
            if (zet != null)
            {
                bord.VoerUit(zet);
                speler.animate = false;//V145
                speler.VoerUit(zet); 
                speler.animate = animationToolStripMenuItem.Checked; //V145
                AanZet = !AanZet;
            }
        }
        private void btnHint_Click(object sender, EventArgs e)
		{
			ComputerZet();
		}
		private void btnTest_Click(object sender, EventArgs e)
		{
            bord.Show();
        }
        private void Chess_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {

        }
        private void Chess_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 2) bord.Show();//Ctrl+B
        }

        private void chessRulesToolStripMenuItem_Click(object sender, EventArgs e) //V144
        {
            string url = @"http://nl.wikipedia.org/wiki/Chess";
            System.Diagnostics.Process.Start(url);
        }

        private void animationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            animationToolStripMenuItem.Checked = !animationToolStripMenuItem.Checked;
            speler.animate=((ToolStripMenuItem)sender).Checked;//V146
        }
	}
	public static class Kleur
	{
		public const bool WIT = true;
		public const bool ZWART = false;
	}
}


using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Chess
{
	class Notation // V111
	{
		List<Zet> stack;
        int stackPointer;
        int zetNummer;
		ListView lv;
        public int ZetNummer
        {
            get 
            { 
                zetNummer = (int)(((stackPointer-1) / 2)+1);
                return zetNummer;
            }
            set { zetNummer = value; }
        }
        private bool IsWitAanzet()
        {
            return stackPointer % 2 == 0;
        }
        public Notation(ListView lv)
		{
			stack = new List<Zet>();
			lv.Items.Clear();
			this.lv = lv;
			stackPointer = 0;
		}
		public void Noteer(Zet zet)
		{
            while (stack.Count > stackPointer )
            {
                stack.RemoveAt(stackPointer);
            } 
            ++stackPointer;
			stack.Add(zet);
            RemoveFromListview();
			add2ListView(zet);
            ShowZetCursor();
		}
        private void RemoveFromListview()
        {
            while (lv.Items.Count > ZetNummer)
            {
                lv.Items.RemoveAt(ZetNummer);
            }
            //if (stackPointer % 2 == 0) // Zwart aanzet
            if (IsWitAanzet()) // Wit aanzet
            {
                ListViewItem lvi = lv.Items[lv.Items.Count - 1];
                if (lvi != null && lvi.SubItems.Count == 3)
                {
                    lvi.SubItems.RemoveAt(2);
                }
            }
            else if(ZetNummer <= lv.Items.Count) //Wit aanzet dus remove ook huidige ply.
            {
                lv.Items.RemoveAt(ZetNummer-1);
            }
        }
		private void add2ListView(Zet zet)
		{	
			ListViewItem lvi;
			if (zet.van.stuk.kleur == Kleur.WIT)
			{
				lvi = new ListViewItem(ZetNummer.ToString());
				lv.Items.Add(lvi);
			}
			else
			{
				lvi = lv.Items[ZetNummer-1];
			}
			lvi.SubItems.Add(zet.ToString());
            lv.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent); //V113
		}
        private void ShowZetCursor()
        {
            //
            // Reset all background to default color.
            //
            foreach (ListViewItem lvitem in lv.Items)
            {
                for (int i = 0; i < lvitem.SubItems.Count; i++)
                {
                    lvitem.SubItems[i].BackColor = Color.White;
                }
            }
            //
            // Set current cursor to selected color.
            //
            if (stackPointer > 0)
            {
                ListViewItem lvi = lv.Items[ZetNummer - 1];
                lvi.UseItemStyleForSubItems = false;
                //lvi.SubItems[stackPointer % 2 == 0 ? 1 : 2].BackColor = Color.SandyBrown;
                lvi.SubItems[IsWitAanzet() ? 2 : 1].BackColor = Color.SandyBrown;
                lvi.EnsureVisible();
            }

        }
		public Zet Back()
		{
            Zet zet = null; ;
            if (stackPointer > 0)
            {
                stackPointer--;
                zet = stack[stackPointer];
            }
            ShowZetCursor();
            return zet;
		}
		public Zet Forward()
		{
            Zet zet = null; ;
            if (stackPointer < stack.Count)
            {
                stackPointer++;
                zet = stack[stackPointer-1];
            }
            ShowZetCursor();
            return zet;
		}
        public string Save() // V120
        {
            string s="zetten:";
            for (int i = 0; i < stackPointer; i++)
            {
                s += stack[i].ToStringLong() + ";";
            }
            s += "\r\n";
            return s;
        }

        public bool Load(string fileContent)
        {
            string[] zetten = Zetten(fileContent);
            if (zetten != null)
            {
                lv.BeginUpdate();
                for (int i = 0; i < zetten.Length-1; i++)
                {
                    Noteer(new Zet(zetten[i]));
                }
                lv.EndUpdate();
            }
            return IsWitAanzet();
        }

        private string[] Zetten(string fileContent)
        {
            string[] zetten=null;
            int p = fileContent.IndexOf("zetten:");
            if (p>0)
            {
                zetten = fileContent.Substring(p + 7).Split(';');
            }
            return zetten;
        }
    }
}

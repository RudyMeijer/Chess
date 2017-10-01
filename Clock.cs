using System.Drawing;
using System.Windows.Forms;

namespace Chess
{
    class Clock
    {
        Control l;
        public Clock(Control l)
        {
            this.l = l;
            Toon(Kleur.WIT);
        }
        public void Toon(bool aanzet)
        {
            if (aanzet)
            {
                l.Text = "Wit is aan zet";
                l.BackColor = Color.White;
                l.ForeColor = Color.Black;
            }
            else
            {
                l.Text = "Zwart is aan zet";
                l.BackColor = (l.Enabled) ? Color.Black : Color.DarkGray;//V133
                l.ForeColor = Color.White;
            }
        }
        public void Show(string text)
        {
            l.Text = text;
            l.BackColor = Color.Red;
            l.ForeColor = Color.Black;
        }
    }
}

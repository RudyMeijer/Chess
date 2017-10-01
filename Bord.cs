using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;

namespace Chess
{
    public class Bord
    {
        public static int[,] veld; 
        private int evaluatie; //absolute waarde.
        public Bord()
        {
            Load(@"
            8-T-P-L-D-K-L-P-T ;
            7-p-p-p-p-p-p-p-p ;
            6 . . . . . . . . ;
            5 . . . . . . . . ;
            4 . . . . . . . . ;
            3 . . . . . . . . ;
            2 p p p p p p p p ;
            1 T P L D K L P T ;
              a b c d e f g h ");
        }
        public void ResetEvaluatie() //V114
        {
            evaluatie = 0;
        }
        public void Load(string stukken)
        {
            InitBord(stukken);
            evaluatie = 0; 
        }
        private void InitBord(string stukken)
        {
            veld = new int[8,8];
            int x = 0; // links. kolom 1 --> veld[x,y] => veld[k,r]
            int y = 0; // boven. rij h
            int kleur=-1;
            foreach (char stuk in stukken)
            {
				if (stuk == '+') kleur = +1; // WIT
				if (stuk == ' ') kleur = +1; // WIT
                if (stuk == '-') kleur = -1; // ZWART;
                if (stuk == ';') { y++; x = 0; }
                if (stuk == '.') veld[x++,y]  = (int)StukType.geen;
                if (stuk == 'K') veld[x++,y]  = kleur * (int)StukType.Koning;
                if (stuk == 'D') veld[x++, y] = kleur * (int)StukType.Dame;
                if (stuk == 'T') veld[x++, y] = kleur * (int)StukType.Toren;
                if (stuk == 'L') veld[x++, y] = kleur * (int)StukType.Loper;
                if (stuk == 'P') veld[x++, y] = kleur * (int)StukType.Paard;
                if (stuk == 'p') veld[x++, y] = kleur * (int)StukType.pion;
                if (y == 8) break;
            }
        }
        public int VoerUit(Zet zet)
        {
            //Check(zet,false);
            //
            // Check for Mat.
            //
            zet.mat = zet.naar.stuk.type == StukType.Koning;
            //
            // Voer zet uit op het bord.
            //
			if (zet.van == null) //Vanuit doos
			{
				veld[zet.naar.x, zet.naar.y] = setStuk(zet.naar.stuk);
			}
			else
			{
				veld[zet.naar.x, zet.naar.y] = veld[zet.van.x, zet.van.y];
				veld[zet.van.x, zet.van.y] = (int)StukType.geen;
			}
			if (zet.promotie)
            {
                Stuk dame = new Stuk(zet.van.stuk.kleur, StukType.Dame);
                veld[zet.naar.x, zet.naar.y] = setStuk(dame);
                evaluatie += Stuk.Waarde(dame);
                evaluatie -= zet.van.stuk.waarde;
            }
			if (zet.castling) //V105
			{
				bool longCastling = zet.naar.x < 4;
				if (longCastling)
				{
					veld[3, zet.van.y] = veld[0, zet.van.y];
					veld[0, zet.van.y] = 0;
				}
				else
				{
					veld[5, zet.van.y] = veld[7, zet.van.y];
					veld[7, zet.van.y] = 0;
				}
				evaluatie += (zet.van.stuk.kleur) ? 1 : -1;
			}
            //
            // Bepaal evaluatie.
            //
            evaluatie -= zet.naar.stuk.waarde; 
            return evaluatie; // absolute bordwaarde;
        }
        //
        // Herstel zet op het bord.
        //
        public void Herstel(Zet zet)
        {
            //Check(zet, true);
			if (zet.naar == null) // stuk naar doos
			{
				veld[zet.van.x, zet.van.y] = (int) StukType.geen;
				return;
			}
            veld[zet.van.x, zet.van.y] = setStuk(zet.van.stuk);// ivm promotie veld[zet.naar.x, zet.naar.y];
            veld[zet.naar.x, zet.naar.y] = setStuk(zet.naar.stuk);
            
            if (zet.promotie)
            {
                Stuk dame = new Stuk(zet.van.stuk.kleur, StukType.Dame);
                evaluatie -= Stuk.Waarde(dame);
                evaluatie += zet.van.stuk.waarde;
            }
			if (zet.castling) //V105
			{
				bool longCastling = zet.naar.x < 4;
				if (longCastling)
				{
					veld[0, zet.van.y] = veld[3, zet.van.y];
					veld[3, zet.van.y] = 0;
				}
				else
				{
					veld[7, zet.van.y] = veld[5, zet.van.y];
					veld[5, zet.van.y] = 0;
				}
				evaluatie -= (zet.van.stuk.kleur) ? 1 : -1;
			}
			evaluatie += zet.naar.stuk.waarde;
            //Check(zet,false);
        }
        //
        // Check
        //   Herstel: naarveld <> 0, vanveld = 0
        //
        private bool Check(Zet zet,bool herstel)
        {
            int vanStuk = setStuk(zet.van.stuk);
            int naarStuk = setStuk(zet.naar.stuk);
            if (herstel)
            {
                if (veld[zet.van.x, zet.van.y] == 0 &&
                    veld[zet.naar.x, zet.naar.y] == vanStuk * ((zet.promotie)?(int)StukType.Dame:1) &&
                    vanStuk * naarStuk <= 0
                    )
                {
                    return true;
                }
            }
            else
            {
                if (veld[zet.van.x, zet.van.y] == vanStuk &&
                    veld[zet.naar.x, zet.naar.y] == naarStuk &&
                    veld[zet.van.x, zet.van.y]>0 == zet.van.stuk.kleur &&
                    vanStuk * naarStuk <= 0
                    )
                {
                    return true;
                }

            }
                
            throw new ArgumentException();
        }
        private int setStuk(Stuk stuk)
        {
            return (stuk.kleur)?(int)stuk.type:-(int)stuk.type;
        }
		public void PlaatsStuk(StukType type, bool kleur, int x, int y)
		{
			veld[x, y] = (kleur)?(int)type:-(int)type;
		}
        public static Stuk getStuk(int x, int y)
        {
            int stuk = veld[x, y];
            return new Stuk(stuk >= 0, (StukType)((stuk >= 0) ? stuk : -stuk));
        }
        public int Evalueer(bool kleur)
        {
            return (kleur) ? evaluatie : -evaluatie;
        }
        private int EvalueerAbsoluut()
        {
            int evaluatie = 0;
            for (int x = 0; x < 8; x++) for (int y = 0; y < 8; y++)  
            {
                evaluatie += Stuk.Waarde(getStuk(x, y));
            }
            return evaluatie;
        }
        internal void Show()
        {
            Console.WriteLine(Save());
            //for (int y = 0; y < 8; y++)
            //{
            //    Console.Write("{0}:",8-y);
            //    for (int x = 0; x < 8; x++)
            //    {
            //        Console.Write("{0,2}", Bord.getStuk(x,y));//veld[x, y]);
            //    }
            //    Console.WriteLine();
            //}
            //Console.WriteLine("   a b c d e f g h");
        }
		internal string Save()
		{
			StringBuilder result = new StringBuilder();
            for (int y = 0; y < 8; y++)
            {
                result.AppendFormat("{0}:",8-y);
                for (int x = 0; x < 8; x++)
                {
					result.AppendFormat("{0,2}", Bord.getStuk(x, y));//veld[x, y]);
                }
				result.AppendLine(";");
            }
            result.AppendLine("   a b c d e f g h");
			return result.ToString();
		}
		internal void Clear()
		{
			for (int x = 0; x < 8; x++) for (int y = 0; y < 8; y++)
			{
				veld[x, y] = 0;
			}
            evaluatie = 0; //V114
		}
	}
    public class Veld
    {
        public int x;
        public int y;
        public Stuk stuk;
        public Veld(int x, int y)
        {
            this.x = x;
            this.y = y;
            this.stuk = Bord.getStuk(x, y);
        }
        public Veld(int x, int y, bool kleur, StukType stukType)
        {
            this.x = x;
            this.y = y;
            this.stuk = new Stuk(kleur, stukType);
        }
        public Veld(string coor)
        {
            int p = coor.Length - 2; // last 2 chars = coordinate. 
            x = coor[p] - 'a';
            y = 8 - coor[p + 1] + '0';
            if (x < 0 || x > 7 || y < 0 || y > 7) throw new Exception("Veld coordinates out of range");
            //
            // bepaal kleur en stuk
            //
            bool kleur = coor[0] != '-';
            char c = coor[kleur ? 0 : 1];
            this.stuk = new Stuk(kleur,c);

        }
        [Obsolete("Use Veld(string coor)")]
        public Veld(string coor, Stuk stuk)
        {
            int p = coor.Length - 2; // last 2 chars = coordinate. 
            x = coor[p] - 'a';
            y = 8 - coor[p+1] + '0';
            if (x < 0 || x > 7 || y < 0 || y > 7) throw new Exception("Veld coordinates out of range");
            this.stuk = stuk;
        }
        public Veld(Point coor): this(coor.X, coor.Y)
        {
        }
        public override string ToString()
        {
            return String.Format("{0}{1}{2}", stuk, "abcdefgh"[x], 8 - y).Trim('p');
        }
        public bool IsEquals(Veld veld)
        {
            return x == veld.x && y == veld.y && stuk.IsEqual(veld.stuk);
        }

    }
    public class Zet
    {
        public Veld van;
        public Veld naar;
		public bool promotie;
		public bool castling; // V105
        public bool mat;
        public override string ToString()
        {
            string zet;
            if (naar.stuk.type == StukType.geen)
            {
                zet = string.Format("{0}{1}", (van == null) ? "doos " : van.stuk.ToString(), naar);
            }
            else
            {
                zet = string.Format("{0}*{1}", van, naar);
            }
			zet = zet.Replace(".", "").Replace("-", "").Replace("p", "");//.Replace(" ","");stuk.tostring() aangepast.
            return zet;
        }

        public string ToStringLong()
        {
            string zet;
            bool save = naar.stuk.kleur; // V121
            naar.stuk.kleur = true;
            if (naar.stuk.type == StukType.geen)
            {
                zet = string.Format("{0}{1}", van, naar);
            }
            else
            {
                zet = string.Format("{0}*{1}", van, naar);
            }
            naar.stuk.kleur = save;
            return zet;
        } //V120
        public Zet Clone()
        {
            return (Zet)this.MemberwiseClone();
        }
        internal void Show(int count, int ply, int eval)
        {
            Console.WriteLine("{0,4} {1} {2}: {3} eval: {4}",count , ply, (this.van.stuk.kleur)?"Wit":"Zwart", this, eval);
        }
		internal bool IsEquals(Zet legalZet)
		{
			return (van.IsEquals(legalZet.van) && naar.IsEquals(legalZet.naar));
		}
		public Zet(){}
        [Obsolete("use Zet(string)")]
		public Zet(string s, bool kleur) 
		{
			van = new Veld(s,new Stuk(kleur,s[0]));
			naar = new Veld(s.Substring(2),new Stuk(kleur,'.'));
		}
        //
        //  zetten:d2.d4;-Pb8.c6;Pb1.d2;-Ta8.b8;e2.e4;-pa7.a6;f2.f4;-pa6.a5;e4.e5;-Pc6*d4;
        //
        public Zet(string zetLong) //V120
        {
            string[] velden;
            bool kleur = !zetLong.StartsWith("-");
            bool slag  = zetLong.Contains("*");
            if (slag)
            {
                velden = zetLong.Split('*');
            }
            else
            {
                velden = zetLong.Split('.');
            }
            van = new Veld(velden[0],new Stuk(kleur,zetLong[kleur?0:1]));
            naar = new Veld(velden[1], new Stuk(!kleur, zetLong[zetLong.Length - 3]));
            castling = van.stuk.type == StukType.Koning && Math.Abs( van.x - naar.x) == 2;
            promotie = van.stuk.type == StukType.pion && (naar.y == 0 || naar.y == 7);
        }
	}
    public class Stuk
    {
        public bool kleur { get; set; }// Wit = true, Zwart = false
        public StukType type { get; set; }
		public Stuk(bool kleur, StukType stuk)
		{
			this.kleur = kleur;
			this.type = stuk;
		}
		public Stuk(bool kleur, char stuk)
		{
			this.kleur = kleur;
			this.type = GetStukType(stuk);
		}
		public int waarde 
        { 
            get
            {
                return Stuk.Waarde(this);
            } 
        }
        internal static int Waarde(Stuk stuk)
        {
            int waarde;
            switch (stuk.type)
            {
                case StukType.Koning:   waarde = 100; break;
                case StukType.Dame:     waarde = 9; break;
                case StukType.Toren:    waarde = 5; break;
                case StukType.Loper:    waarde = 3; break;
                case StukType.Paard:    waarde = 3; break;
                case StukType.pion:     waarde = 1; break;
                case StukType.geen:     waarde = 0; break;
                default: throw new ArgumentException();
            }
            return (stuk.kleur)?waarde:-waarde;
        }
		private StukType GetStukType(char c)
		{
			int p = ".pPLTDK".IndexOf(c);
			if (p == -1) p = 1;
			return (StukType)p; 
		}
        public override string ToString()
        {
            return (((kleur)?"":"-")+".pPLTDK"[(int)type]).Trim();
        }
		internal bool IsEqual(Stuk stuk)
		{
			return kleur == stuk.kleur && type == stuk.type;
		}
	}
    public enum StukType { geen, pion, Paard, Loper, Toren, Dame, Koning} //pas ook directionTable en image aan!
}

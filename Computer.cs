using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Drawing;
namespace Chess
{
    public class Computer
    {
        Bord bord;
        Zet besteZet;
        public int maxPly;
        public int bestEval;// for test only
        public int count;   // for test only
        Zet[] stack;		// for test only
		bool debug = true;
        public Computer(Bord bord, int maxPly)
        {
            this.bord = bord;
            this.maxPly = maxPly;
        }
        public Zet BedenkBesteZet(bool kleur)
        {
            if (debug) Console.WriteLine("Bedenk beste zet voor {0} maxply: {1}",(kleur)?"Wit":"Zwart",maxPly);
            count = 0;
			stack = new Zet[maxPly+1]; // For test only
            besteZet = null;
            bestEval = 0;
            bord.ResetEvaluatie(); //V114
            long start = DateTime.Now.Ticks;
            BedenkBesteZet(kleur, 1000, 1); // 1001 = Doorzoek alle zetten op ply 1.
            long eind = DateTime.Now.Ticks;
			if (debug) Console.WriteLine("Beste zet: {0} eval: {1}, count: {2}, Elapsed time: {3} ms", besteZet, bestEval, count, (eind - start) / 10000);
            return besteZet;
        }
        private int BedenkBesteZet(bool kleur, int AlphaBetaEval, int ply)
        {
            int eval, bestEval = -1000; // Initieel nog geen beste zet gevonden.
            //
            // Herhaal voor ieder veld op het bord. Origin links boven x,y = (0,0).
            //
            for (int x = 0; x < 8; x++) for (int y = 0; y < 8; y++) if (Bord.veld[x, y] != 0) if (Bord.veld[x, y] > 0 == kleur)
            {
                //
                // Doe een willekeurige zet vanuit huidig veld en bepaal evaluatie.
                //
                Veld huidigveld = new Veld(x, y);
                foreach (Zet zet in DoeEenZet(huidigveld))			
                {                                                                   count++; //if (count == -107772) Console.WriteLine(zet);//For test only.
                    bord.VoerUit(zet);  									        //stack[ply] = zet; //For test only.
                    //bord.Show();
                    if ((ply >= maxPly || zet.mat) )
                    {
                        eval = bord.Evalueer(kleur); if (zet.mat) eval -= ply * 10; //V119     //if (eval >0) { bord.Show(); }
					}
                    else
                    {
                        eval = BedenkBesteZet(!kleur, -bestEval, ply + 1);
                    }
																			        //stack[ply] = null;//For test only.
                    bord.Herstel(zet);                              
                    if (ply == 1 && debug) zet.Show(count,ply,eval);
                   														        //if (ply == 1 || count == -107772)				//|| (ply == 2 && count >= 96733 && count <= 119692)//|| (ply == 3 && count >= 100380 && count <= 107772)//|| (ply == 4 && count >= 106553 && count <= 107772))//{zet.Show(count, ply, eval); if (ply != 1) Console.Write(""); ;}
                    if (eval > bestEval)
                    {
                        if (ply == 1)
                        {
                            this.besteZet = zet.Clone();
                            this.bestEval = eval;
                        }
                        //
                        // Alpha-Beta algoritme.
                        // Als Zwart een zet kan doen waardoor Wit zijn vorige zet niet gedaan zou hebben 
                        // stop dan met zoeken in rest van deze boom (prune tree).
                        //
                        if (eval >= AlphaBetaEval || zet.mat) // breek ook af als geen zet mogelijkheid voor tegenpartij ( eval is 1000 ) dus AlphaBetaEval = 1000 // || (ply >1 && eval > 50)) 
                        {
                            return -eval;
                        }
                        bestEval = eval;
                    }
                }
            }
            //
            // Min-Max algoritme.
            // Een positieve evaluatie voor WIT is negatief voor ZWART.
            //
            return -bestEval; 
        }
        public IEnumerable<Zet> DoeEenZet(Veld huidigveld) 
        {
            Zet zet = new Zet();
            zet.van = huidigveld;
            Stuk stuk = huidigveld.stuk;
            Point[] direction = DirectionTable[(stuk.type == StukType.pion && stuk.kleur==Kleur.ZWART)?0:(int)stuk.type];
            int steps = (stuk.type == StukType.Dame  ||
                         stuk.type == StukType.Toren ||
                         stuk.type == StukType.Loper)? 8 : 1; // 7 is voldoende.
            //
            // Herhaal voor iedere zetrichting tot bezet veld of buiten bord.
            //
            for (int richting = 0; richting < direction.Length; richting++)
            {
                for (int step = 1; step <= steps; step++)
                {
                    int x = zet.van.x + step * direction[richting].X;
                    int y = zet.van.y + step * direction[richting].Y;
                    if (BuitenBord(x, y)) break;
					#region Pion en Koning
					if (stuk.type == StukType.pion)
                    {
                        zet.promotie = (y==0 || y==7);
                        if (zet.van.y != 1 && zet.van.y != 6 && richting == 3) yield break; // Pion alleen 2 vooruit vanaf rij 1 of 6
                    }
					if (stuk.type == StukType.Koning && richting < 2) //V105 Castling.
					{
						if (zet.van.x != 4 || (zet.van.y != ((stuk.kleur)?7:0))) break;
						bool longCastling = x < 4;
						if (longCastling)
						{
							if (Bord.veld[0, y] != ((int)StukType.Toren) * ((stuk.kleur) ? 1 : -1)) break;
							if (Bord.veld[1, y] != 0) break;
							if (Bord.veld[2, y] != 0) break;
							if (Bord.veld[3, y] != 0) break;
						}
						else
						{
							if (Bord.veld[5, y] != 0) break;
							if (Bord.veld[6, y] != 0) break;
							if (Bord.veld[7, y] != ((int)StukType.Toren) * ((stuk.kleur) ? 1 : -1)) break;
						}
                        //
                        // Staat koning schaak? --> Dan geen roccade.
                        //
                        if (IsSchaak(stuk.kleur)) break; //V139
                        zet.naar = new Veld(x, y);
						zet.castling = true;
                        yield return zet;
						zet.castling = false;
						break;
					}
					#endregion
					if (Bord.veld[x, y] == 0)
                    {
                        if (stuk.type == StukType.pion && richting < 2) break;
                        zet.naar = new Veld(x, y);
                        yield return zet;
                    }
                    else // Naar veld bevat een stuk.
                    {
                        if (stuk.type == StukType.pion && richting >= 2) yield break; // V104 Zoek niet verder in andere richtingen.
                        if (huidigveld.stuk.kleur == Bord.veld[x, y] < 0) //SLAG
                        {
                            zet.naar = new Veld(x, y);
                            yield return zet;
                        }
                        break;
                    }
                }
            }
        }
        static private bool BuitenBord(int x, int y)
        {
            return !(x >= 0 && x < 8 && y >= 0 && y < 8);
        }
        #region Direction table
		static Point[] Koning= {new Point(-2, 0),new Point(2, 0),// V105 Castling.
								new Point(-1,-1),new Point(0,-1),new Point(1,-1),
                                new Point(-1, 0),                new Point(1, 0),
                                new Point(-1, 1),new Point(0, 1),new Point(1, 1)};
        static Point[] Dame  = {new Point(-1,-1),new Point(0,-1),new Point(1,-1),
                                new Point(-1, 0),                new Point(1, 0),
                                new Point(-1, 1),new Point(0, 1),new Point(1, 1)};
        static Point[] Toren = {                 new Point(0,-1), 
                                new Point(-1, 0),                new Point(1, 0),
                                                 new Point(0, 1)                };
        static Point[] Loper = {new Point(-1,-1),                new Point(1,-1),

                                new Point(-1, 1),                new Point(1, 1)};
        static Point[] Paard = {new Point(-2,-1),new Point(-1,-2),new Point(1,-2), new Point(2,-1),
                                new Point(-2, 1),new Point(-1, 2),new Point(1, 2), new Point(2, 1)};
        static Point[] Pion  = {new Point(-1,-1), new Point(+1,-1), new Point(0,-1), new Point(0,-2) };
        static Point[] PionZw= {new Point(-1, 1), new Point(+1, 1), new Point(0, 1), new Point(0, 2) };
        static Point[][] DirectionTable = { PionZw, Pion, Paard, Loper, Toren, Dame, Koning }; // pas ook StukType aan!
        #endregion

		public bool IsZetAllowed(ref Zet zet)
		{
			foreach (Zet legalZet in DoeEenZet(zet.van))
			{
				if (zet.IsEquals(legalZet))
				{
					zet = legalZet; // Add promotion and castling.
					return true;
				}
			}
			return false;
		}
        public bool IsSchaak(bool kleur)
        {
            //
            // bepaal veld coordinate van koning..
            //
            //int x = 4;
            //int y = kleur ? 7 : 0;
            
            Point k = GetCoorKoning(kleur);
            int x = k.X;
            int y = k.Y;
            //
            // Vervang koning door Paard en Dame en test of geslagen stuk van eenzelfde soort is.
            //
            Veld paardVeld = new Veld(x, y, kleur, StukType.Paard);
            Veld dameVeld = new Veld(x, y, kleur, StukType.Dame);
            //
            // Herhaal voor Paard en Dame
            //
            foreach (Zet zet in DoeEenZet(paardVeld))
            {
                if (zet.naar.stuk.type == StukType.Paard) return true;
            }
            foreach (Zet zet in DoeEenZet(dameVeld))
            {
                if (zet.naar.stuk.type == StukType.Dame) return true;
                if (zet.naar.stuk.type == StukType.Loper && IsDiagonaal(zet)) return true;
                if (zet.naar.stuk.type == StukType.Toren && !IsDiagonaal(zet)) return true;
                if (zet.naar.stuk.type == StukType.Koning && IsEen(zet)) return true; //V141
                if (zet.naar.stuk.type == StukType.pion && IsDiagonaal1(zet)) return true;
            }
            return false;
        }

        private Point GetCoorKoning(bool kleur)
        {
            for (int x = 0; x < 8; x++) for (int y = 0; y < 8; y++) if (Bord.veld[x, y] != 0)
            {
                if (kleur == (Bord.veld[x, y] > 0) && Math.Abs(Bord.veld[x, y]) == (int)StukType.Koning)
                {
                    return new Point(x,y);
                }
            }
            return new Point();
        }

        private bool IsDiagonaal(Zet zet)
        {
            if (zet.van.x == zet.naar.x || zet.van.y == zet.naar.y) return false; // Horizontal
            return true;
        }
        private bool IsEen(Zet zet) //V141
        {
            if (Math.Abs(zet.van.x - zet.naar.x) == 1 ||
                Math.Abs(zet.van.y - zet.naar.y) == 1) return true; // koning.
            return false;
        }
        private bool IsDiagonaal1(Zet zet)
        {

            if (Math.Abs(zet.van.x - zet.naar.x) == 1 &&
                (zet.van.y - zet.naar.y) == ((zet.naar.stuk.kleur)?-1:1)) return true; // V143
                //Math.Abs(zet.van.y - zet.naar.y) == 1) return true; // Pion
            return false;
        }
        public bool IsSchaakMat(bool kleur)
        {
            //int _bestEval = BedenkBesteZet(kleur, 1000, maxPly-1)-maxPly*10;
            //return _bestEval < -60;
            int savMaxPly = maxPly;
            maxPly = 2;
            debug = false;
            BedenkBesteZet(kleur);
            debug = true;
            maxPly = savMaxPly;
            return (bestEval < -60);
        }
        public string ShowZetHistory(Zet zet)
		{
            if (zet == null) return "geen zet meer";
			int savPly = this.maxPly;
			int savCount = this.count;
			int savBestEval = this.bestEval;
			bool kleur = zet.van.stuk.kleur;
			Zet[] zetten = new Zet[this.maxPly];
			string result = "Zetten: ";
			zetten[0] = zet.Clone();
			debug = false;
			for (int i = 1; i <= savPly; i++)
			{
				bord.VoerUit(zet);
				if (zet.mat || i == savPly) break;
				this.maxPly = savPly - i;
				kleur = !kleur;
				zet = this.BedenkBesteZet(kleur);
                if (zet == null) break;
                zetten[i] = zet.Clone();
			}
			for (int i = 0; i < savPly; i++)
			{
				result += String.Format("{0} ", zetten[i]);
				if (zetten[savPly - i - 1] != null)
					bord.Herstel(zetten[savPly - i - 1]);
			}
            //result += "eval: " + bestEval.ToString();
            result += "eval: " + savBestEval.ToString(); //V125
			this.maxPly = savPly;
			this.count = savCount;
			this.bestEval = savBestEval;
			this.debug = true;
			zet = zetten[0];
			result = result.Trim();
			Console.WriteLine(result);
			return result;
		}
	}
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Windows.Forms;
namespace Chess
{
    class Test
    {
        Zet zet;
        Bord bord;
        Computer computer;
		public Test(Form f)
		{
			TestNotation(f);
		}

		private void TestNotation(Form f)
		{
			ListView lv = (ListView)f.Controls["ListView1"];
			Notation n = new Notation(lv);
			n.Noteer(new Zet("e2.e4"));
            n.Noteer(new Zet("-e7.e5"));
            n.Noteer(new Zet("Pg1.f3"));
		}
        public Test()
        {
            bord = new Bord();
            computer = new Computer(bord, 1);
            bool loop = false;// System.Diagnostics.Debugger.IsAttached;
			do
            {
                //TestDefaultBord();
                //TestPromotion();
                //TestPionzet();
                //TestAlphaBeta();
                //TestDameZet();
                //TestMat();
                //TestMat1();
                //TestMatToren();
                //TestMat3();
                TestMatInZes();
                //TestCastling();
            } while (loop);
        }

		private void TestCastling()
		{
			bord.Load(@"
            8-T . . .-K . .-T ;
            7-p . . . . . .-p ;
            6 . . . . . . . . ;
            5 . . . . . . . . ;
            4 . . . . . . . . ;
            3 . . . . . . . . ;
            2+p . . . . . .+p ;
            1+T . . .+K . .+T ;
              a b c d e f g h ");
			computer.maxPly = 1;
			//while(true)
			zet = computer.BedenkBesteZet(Kleur.WIT);
			Debug.Assert(computer.count == 16, "count <> ");
			Debug.Assert(computer.bestEval == 1, "bestEval <> ");
			//bord.PlaatsStuk(StukType.pion, Kleur.WIT, 2, 7);
			//zet = computer.BedenkBesteZet(Kleur.WIT);
			//Debug.Assert(computer.count == 6, "count <> ");
		}
        private void TestMatInZes()
        {
            bord.Load(@"
            8 .+L . . . .-P . ;
            7 . . . . . . .+P ;
            6+K .-p+P . . . . ;
            5 . . . . .+p . . ;
            4+T .-P . .-K . . ;
            3 . .+p .+T . .+p ;
            2 . . . .-L .+L . ;
            1 . .+D . . . . . ;
              a b c d e f g h ");
            computer.maxPly = 6;
			//cross-check probleem.
			//http://74.125.77.132/search?q=cache:VUBf58WhRAwJ:www.dewilligedame.nl/SCHAAKBOEKEN/Probleemboeken.htm+Mat+In+5+zetten&hl=en&ct=clnk&cd=10
			//V105 Beste zet: Le4 eval: 87, count: 27475336, Elapsed time: 76578 ms
			//V105 Beste zet: Le4 eval: 87, count: 27475336, Elapsed time: 50531 ms zonder Check() function.
            //V138 Beste zet: Le4 eval: 42, count: 27480455, Elapsed time: 117503 ms
            //V139 Beste zet: Le4 eval: 42, count: 27480455, Elapsed time: 119378 ms

            //while(true)
            zet = computer.BedenkBesteZet(Kleur.WIT);
            //string result = ShowZetHistory(zet);
            Debug.Assert(zet.ToString() == "Le4", "zet <> ");
            Debug.Assert(computer.bestEval == 42, "bestEval ");
            Debug.Assert(computer.count == 27480455, "count <> "); // V105
            //Debug.Assert(result == "Zetten: Le4 Pc4*Pd6 Ld3 Kf4*Te3 Dc1*Ke3", "result <> ");
        }
        private void TestMat3()
        {
            bord.Load(@"
            8 . . . . . . . . ;
            7 . . . . . .-p . ;
            6 . . . . . . .-K ;
            5 . . . . . . . . ;
            4 . . . . . . .+D ;
            3 . . . . . . . . ;
            2 . . . . . . . . ;
            1 . . . .+T . . . ;
              a b c d e f g h ");
            computer.maxPly = 3;
            //while(true)
            zet = computer.BedenkBesteZet(Kleur.WIT);
            string result = ShowZetHistory(zet);
            Debug.Assert(zet.ToString() == "Dh4*Kh6", "zet <> ");
            //Debug.Assert(computer.bestEval == 1000, "bestEval ");
            Debug.Assert(computer.bestEval == 90, "bestEval "); // V103 zet.mat
            //Debug.Assert(computer.count == 420, "count <> "); // V102 81852
            Debug.Assert(computer.count == 793, "count <> "); // V103 if zet.mat return -eval
            Debug.Assert(result == "Zetten: Dh4*Kh6", "result <> ");
        }
        private void TestMatToren()
        {
            bord.Load(@"
            8 . . . . . . . . ;
            7 . . . . . . .-K ;
            6 . . . . . . . . ;
            5 . . . . .+K . . ;
            4 . . . . . . . . ;
            3 . . . . . . . . ;
            2 . . . . . . . . ;
            1 . . . . . .+T . ;
              a b c d e f g h ");
            computer.maxPly = 7;
            //while (true) 
            zet = computer.BedenkBesteZet(Kleur.WIT);
            string result = ShowZetHistory(zet);
            Debug.Assert(zet.ToString() == "Kf6", "zet ");
            Debug.Assert(computer.bestEval == 45, "bestEval ");
            Debug.Assert(computer.count == 434360, "count <> ");
            Debug.Assert(result == "Zetten: Kf6 Kh8 Tg7 Kh8*Tg7 Kf6*Kg7", "result <> ");
        }
        private void TestMat1()
        {
            bord.Load(@"
            8 . . . . . . . . ;
            7 . . . . . .-p-K ;
            6 . . . . . . . . ;
            5 . .+D . . . . . ;
            4 . . . . . . . . ;
            3 . . . . . . . . ;
            2 . . . . . . . . ;
            1 . . . .+T . . . ;
              a b c d e f g h ");
            computer.maxPly = 5;
            //while(true)
            zet = computer.BedenkBesteZet(Kleur.WIT);
            string result = ShowZetHistory(zet);
            Debug.Assert(zet.ToString() == "Dh5", "zet ");
            //Debug.Assert(computer.bestEval == 1000, "bestEval ");
            //Debug.Assert(computer.bestEval == 91, "bestEval "); // V103 zet.mat
            //Debug.Assert(computer.count == 35643, "count <> "); // V102 

            //Debug.Assert(computer.bestEval == 86, "bestEval "); // V103 (ply >1 && eval > 50) zet.mat -ply
            //Debug.Assert(computer.count == 81852, "count <> "); // V103 (ply >1 && eval > 50)
            //Debug.Assert(result == "Zetten: Dh5 Kg8 Te8 Kf8 Te8*Kf6", "result <> "); (ply >1 && eval > 50)
            //
            // if (zet.mat) return -eval
            //
            Debug.Assert(computer.bestEval == 50, "bestEval ");
			//Debug.Assert(computer.count == 85803, "count <> "); //V103
			Debug.Assert(computer.count == 85226, "count <> "); //V104
            Debug.Assert(result == "Zetten: Dh5 Kg8 Te8 Kf8 Te8*Kf8", "result <> ");
        }
        private void TestMat()
        {
            bord.Load(@"
            8 . . . . . .-p . ;
            7 . . . . . .-p-K ;
            6 . . . . . . . . ;
            5 . .+D . . . . . ;
            4 . . . . . . . . ;
            3 . . . . . . . . ;
            2 . . . . . . . . ;
            1 . . . .+T . . . ;
              a b c d e f g h ");
            computer.maxPly = 3;
            //while(true) 
            zet = computer.BedenkBesteZet(Kleur.WIT);
            string result = ShowZetHistory(zet);
            Debug.Assert(zet.ToString() == "Dh5", "zet ");
            //Debug.Assert(computer.bestEval == 1000, "bestEval ");
            Debug.Assert(computer.bestEval == 70, "bestEval ");
            //Debug.Assert(computer.count == 1830, "count <> "); // V102
            Debug.Assert(computer.count == 1658, "count <> "); // V103 zet.mat
            //Debug.Assert(computer.count == 753, "count <> "); // V103 return mat
        }
        private void TestDameZet()
        {
            bord.Load(@"
            8 . . . . . . . . ;
            7 . . . . . . . . ;
            6 . . .-p . . . . ;
            5 . . . .+D . . . ;
            4 . . . . . . . . ;
            3 .+p . . . . . . ;
            2 . . . . . . . . ;
            1 . . . . . . . . ;
              a b c d e f g h ");
            computer.maxPly = 2;
            zet = computer.BedenkBesteZet(Kleur.WIT);
            bord.VoerUit(zet);
            //bord.Show();
            bord.Herstel(zet);
            zet.Show(0, 0, computer.bestEval);
            Debug.Assert(computer.count == 4, "count <> 5.");
        }
        private void TestAlphaBeta()
        {
            bord.Load(@"
            8 . . . . . . . . ;
            7 . . . . . . . . ;
            6-p . . . . . . . ;
            5 . . . . . . . . ;
            4 . .+D . . . . . ;
            3+p . . . . . . . ;
            2 . . . . . . . . ;
            1 . . . . . . . . ;
              a b c d e f g h ");
            computer.maxPly = 2;
            zet = computer.BedenkBesteZet(Kleur.WIT);
			//bord.VoerUit(zet);
			//bord.Show();
			//bord.Herstel(zet);
            Console.WriteLine("Zet: {0} eval: {1}", zet, computer.bestEval);
            Debug.Assert(computer.count == 5, "count <> 5");
            //Debug.Assert(computer.count == 51, "count <> 51"); //AlphaBeta = 1001 
        }
        private void TestPionzet()
        {
            bord.Load(@"
            8 . . . . . . . . ;
            7 . . . . .-p . . ;
            6 . . . . . . . . ;
            5 . . . . . . . . ;
            4 . . . . . . . . ;
            3+p-p .-p+p+p . . ;
            2 . .+p .-p+p . . ;
            1 . . . . . . . . ;
              a b c d e f g h ");
            computer.maxPly = 1;
            zet = computer.BedenkBesteZet(Kleur.WIT);
            Console.WriteLine("Zet: {0} eval: {1}", zet, computer.bestEval);
			//Debug.Assert(computer.count == 4, "count <>");
            Debug.Assert(computer.count == 7, "count <>"); // V104
            Debug.Assert(zet.ToString() == "c2*b3","zet <>");
            zet = computer.BedenkBesteZet(Kleur.ZWART);
            Debug.Assert(computer.count == 7, "zwart count <>");
            Debug.Assert(zet.ToString() == "e1","zet <>");
        }
        private void TestPromotion()
        {
            bord.Load(@"
            8 . . . . . . . . ;
            7 . . .+p . . . . ;
            6 . . . . . . . . ;
            5 . . . . . . . . ;
            4 . . . . . . . . ;
            3 . . . . . . . . ;
            2 . . . . . . . . ;
            1 . . . . . . . . ;
              a b c d e f g h ");
            computer.maxPly = 1;
            zet = computer.BedenkBesteZet(Kleur.WIT);
            Console.WriteLine("Zet: {0} eval: {1}",zet,computer.bestEval);
            Debug.Assert(computer.bestEval == 8,"evaluation <> 8");
            computer.maxPly = 2;
            zet = computer.BedenkBesteZet(Kleur.WIT);
            Debug.Assert(computer.bestEval == 1000,"evaluation <> 1000");
        }
        private void TestDefaultBord()
        {
            bord = new Bord();
            computer = new Computer(bord, 5);
			zet = computer.BedenkBesteZet(Kleur.WIT);
			Debug.Assert(zet.ToString() == "b3", "zet "); // V103 Elapsed time: 156 ms
			Debug.Assert(computer.bestEval == 1, "besteval ");
			//Debug.Assert(computer.count == 28329, "count "); // Elapsed time: 1078 ms
			//Debug.Assert(computer.count == 27979, "count "); // V104
			Debug.Assert(computer.count == 27966, "count "); // V105++
            computer.maxPly = 6;
            // V102 Elapsed time: 18734 ms
            // V103 Elapsed time: 1562 ms (Remove EvalueerAbsoluut)
			// V103 Beste zet: a3 eval: -1, count: 546771, Elapsed time: 1781 ms
			// V104 Beste zet: a3 eval: -1, count: 518719, Elapsed time: 1703 ms
			// V105 Beste zet: a3 eval: -1, count: 518719, Elapsed time: 1937 ms
			// V105 Beste zet: a3 eval: -1, count: 518719, Elapsed time: 1359 ms zonder Check() function

            //while(true)
            zet = computer.BedenkBesteZet(Kleur.WIT); 
            Debug.Assert(zet.ToString() == "a3", "zet "); 
            Debug.Assert(computer.bestEval == -1, "besteval <> -1");
            //Debug.Assert(computer.count == 546780, "count <>"); 
            //Debug.Assert(computer.count == 546771, "count <>"); // V103 
			//Debug.Assert(computer.count == 518719, "count <>"); // V104 
			Debug.Assert(computer.count == 483676, "count <>"); // V105++ 
        }
        private string ShowZetHistory(Zet zet)
        {
            int savPly      = computer.maxPly;
            int savCount    = computer.count;
            int savBestEval = computer.bestEval;
            bool kleur = zet.van.stuk.kleur;
            Zet[] zetten = new Zet[computer.maxPly];
            string result = "Zetten: ";
            zetten[0] = zet.Clone();

            for (int i = 1; i <= savPly; i++)
            {
                bord.VoerUit(zet);
                //bord.Show();
                if (zet.mat || i == savPly) break;
                computer.maxPly = savPly - i;
                kleur = !kleur;
                zet = computer.BedenkBesteZet(kleur);
                zetten[i] = zet.Clone();
            }
            for (int i = 0; i < savPly; i++)
            {
                result += String.Format("{0} ",zetten[i]);
                if (zetten[savPly - i - 1] !=null)
                bord.Herstel(zetten[savPly - i - 1]);
            }
            computer.maxPly     = savPly;
            computer.count      = savCount;
            computer.bestEval   = savBestEval;
            zet                 = zetten[0];
            result = result.Trim();
            Console.WriteLine(result);
            return result;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Chess;

namespace Test
{
    class Program
    {
        Zet zet;
        Bord bord;
        Computer computer;
        public Program()
        {
        }
        static void Main(string[] args)
        {
            bord = new Bord();
            computer = new Computer(bord, 1);

            TestPromotion();
        }

        private static void TestPromotion()
        {
            bord.Load(Kleur.WIT, @"
            8-. . . . . . . . ;
            7 . . .+p . . . . ;
            6 . . . . . . . . ;
            5 . . . . . . . . ;
            4 . . . . . . . . ;
            3 . . . . . . . . ;
            2 . . . . . . . . ;
            1 . . . . . . . . ;
              a b c d e f g h ");
            computer = new Computer(bord, 1);
            zet = computer.BedenkBesteZet();
            //Console.WriteLine("Zet: {0} eval:{1}",zet, computer.bestEval);
            Assert.IsTrue(false, "Zet: {0} eval:{1}", zet, computer.bestEval);
            throw new NotImplementedException();
        }
    }
}

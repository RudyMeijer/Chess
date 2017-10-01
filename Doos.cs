using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Microsoft.VisualBasic.PowerPacks;
using System.Drawing;

namespace Chess
{
    class Doos
    {
        #region Fields
        public event MouseEventHandler ShapeMouseDown;
        string pieces = "pnbrqk";
        string nrofPieces = "822221";
        List<OvalShape> inDoos;
        ShapeContainer shapeContainer;
        RectangleShape chessBord;
        #endregion

        public Doos(ShapeContainer f)
        {
            this.shapeContainer = f;
            chessBord = getChessBord();
            shapeContainer.Shapes.Clear(); //V118
            shapeContainer.Shapes.Add(chessBord);
            inDoos = new List<OvalShape>();
            initDoos(Kleur.WIT);
            initDoos(Kleur.ZWART);
        }
        
        public RectangleShape getChessBord()
        {
            int p = this.shapeContainer.Shapes.IndexOfKey("rectangleShape1");
            return (RectangleShape)this.shapeContainer.Shapes.get_Item(p);
        }
        private void initDoos(bool kleur)
        {
            for (int i = 0; i < pieces.Length; i++)
            {
                int nrofPiece = int.Parse(nrofPieces[i].ToString());
                string imageName = string.Format("Chess_{0}{1}", pieces[i], kleur ? "l" : "d");
                for (int j = 1; j <= nrofPiece; j++)
                {
                    CreatePiece(imageName, j);
                }
            }
        }
        private void CreatePiece(string imageName, int suffix)
        {
            OvalShape shape = new OvalShape(shapeContainer);
            shape.BackgroundImage = (Image)Properties.Resources.ResourceManager.GetObject(imageName);
            shape.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            shape.BorderColor = System.Drawing.Color.Transparent;
            shape.Name = imageName + suffix.ToString();
            shape.Location = DoosLocation(shape.Name);
            shape.Size = new System.Drawing.Size(26, 26);
            shape.BringToFront();
            shape.MouseDown += new MouseEventHandler(shape_MouseDown);
            shape.SelectionColor = System.Drawing.Color.Transparent;

            inDoos.Add(shape);
        }
        void shape_MouseDown(object sender, MouseEventArgs e)
        {
            ShapeMouseDown(sender, e);
        }
        private Point DoosLocation(string shapeName)
        {
            int x = 0;
            int y = 0;
            string piece = shapeName.Substring(6);
            bool kleur = shapeName[7] == 'l';
            piece = piece.Remove(1, 1); // remove color.
            string[] loc = new string[] {"p1p2p3p4"
										,"p5p6p7p8"
										,"k1q1r1n1"
										,"r2b1b2n2"};
            if (piece == "q2") return new Point(10, 1);
            for (int rij = 0; rij < loc.Length; rij++)
            {
                int col = loc[rij].IndexOf(piece);
                if (col >= 0)
                {
                    x = col * ((rij < 2) ? 10 : 10);
                    y = 30 + rij * 25;
                    break;
                }
            }
            x += (kleur) ? 0 : 0;// V111+ 415;
            y += (kleur) ? 200 : 0; //V109

            return new Point(x, y);
        }
        /// <summary>
        /// Haal stuk uit doos.
        /// </summary>
        /// <param name="stuk"></param>
        /// <returns></returns>
        public OvalShape HaalOp(Stuk stuk)
        {
            OvalShape shape = null;
            string key = string.Format("Chess_{0}{1}", pieces[(int)stuk.type - 1], stuk.kleur ? "l" : "d");
            foreach (OvalShape item in inDoos) if (item.Name.Contains(key))
                {
                    shape = item;
                    break;
                }
            Console.WriteLine("HaalStukUitDoos {0}", shape == null ? "nothing" : shape.Name);
            inDoos.Remove(shape);
            return shape;
        }
        /// <summary>
        /// Berg stuk op in doos.
        /// </summary>
        /// <param name="shape"></param>
        public void BergOp(OvalShape shape)
        {
            if (shape != null)
            {
                Console.WriteLine("BergOpInDoos {0}", shape.Name);
                shape.Location = DoosLocation(shape.Name);
                inDoos.Add(shape);
            }
        }
        public Stuk HaalOp(OvalShape shape)
        {
            inDoos.Remove(shape);
            return getStuk(shape);
        }
        private Stuk getStuk(OvalShape selectedShape)
        {
            string name = selectedShape.Name;
            char type = name[6];
            char kleur = name[7];
            Stuk stuk = new Stuk((kleur == 'l'), (StukType)pieces.IndexOf(type) + 1);
            return stuk;
        }
    }
}

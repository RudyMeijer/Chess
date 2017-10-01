using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualBasic.PowerPacks;
using System.Windows.Forms;
using System.Drawing;

namespace Chess
{
	public delegate void ZetEventHandler(object sender, Zet zet);
    class Speler
    {
		Point BUITENBORD = new Point(-1, -1);
		public event ZetEventHandler Speler_Zet;
        public Doos doos;
		bool dragMode;
        RectangleShape chessBord;
        ShapeContainer shapeContainer;
		OvalShape selectedShape;
		Zet zet;
		public bool enabled; 
        public bool WedstrijdMode;
        static int spelerNummer=0;
        Computer computer; //V139
        public bool animate;//V145
        public Speler(ShapeContainer f, Computer comp)
        {
            this.computer = comp;
            this.shapeContainer = f;
            shapeContainer.MouseDown += new MouseEventHandler(shapeContainer_MouseDown);
            shapeContainer.MouseMove += new MouseEventHandler(shapeContainer_MouseMove);
            shapeContainer.MouseUp += new MouseEventHandler(shapeContainer_MouseUp);
            doos = new Doos(f);
            doos.ShapeMouseDown +=new MouseEventHandler(shape_MouseDown);
            chessBord = doos.getChessBord();
			enabled = true;
            spelerNummer++;
            Console.WriteLine("new speler {0}",spelerNummer);
        }
        public void Dispose() //V122
        {
			shapeContainer.MouseDown -= new MouseEventHandler(shapeContainer_MouseDown);
			shapeContainer.MouseMove -= new MouseEventHandler(shapeContainer_MouseMove);
			shapeContainer.MouseUp -= new MouseEventHandler(shapeContainer_MouseUp);
            doos.ShapeMouseDown -= new MouseEventHandler(shape_MouseDown);
        }

		void shape_MouseDown(object sender, MouseEventArgs e)
		{
			selectedShape = null;
			if (!enabled) return; 
			selectedShape = sender as OvalShape;
			Console.WriteLine("{2} shape_MouseDown {0} selectedShape {1}",selectedShape.Location,selectedShape.Name,spelerNummer);
			Point coor = getBordCoor(selectedShape.Location);
			zet = new Zet();
			if (coor == BUITENBORD)
				zet.van = null;
			else
			{
				zet.van = new Veld(coor);
                ShowDots(true);
			}
			dragMode = true;
		}

		void shapeContainer_MouseDown(object sender, MouseEventArgs e)
		{
			//Console.WriteLine("shapeContainer_MouseDown");
            if (!dragMode) selectedShape = null; //V126
		}
		void shapeContainer_MouseMove(object sender, MouseEventArgs e)
		{
			if (dragMode)
			{
				//Console.WriteLine("shapeContainer_MouseMove {0}", e.Location);
				selectedShape.Location = new Point(e.Location.X-13,e.Location.Y-13);
			}
		}
		void shapeContainer_MouseUp(object sender, MouseEventArgs e)
		{
            ShowDots(false);
			if (!enabled) return; 
			Console.WriteLine("{3} shapeContainer_MouseUp {0} dragmode={1}, selected shape={2}", e.Location, dragMode, (selectedShape == null) ? "none" : selectedShape.Name,spelerNummer);
			Point coor = getBordCoor(e.Location);
			if (dragMode)
			{
				dragMode = false;
				if (coor != BUITENBORD)
				{
					zet.naar = new Veld(coor);
					Console.WriteLine("Zet {0}",zet);
					selectedShape.Location = getBordLocation(zet.naar.x, zet.naar.y);
					selectedShape.Left += 10; //V108 zo wordt het stuk niet meer in doos opgeborgen bij voeruit().
					//
					// plaats stuk vanuit doos op schaakbord.
					//
					if (zet.van == null) 
					{
                        if (WedstrijdMode) //V134
                        {
                            MessageBox.Show("In Wedstrijd mode mogen geen stukken op het bord geplaats worden.");
                            doos.BergOp(selectedShape);
                            return;
                        }
                        zet.naar.stuk = doos.HaalOp(selectedShape); 
						VoerUit(zet);
						Speler_Zet("doos", zet);
					}
					else if (computer.IsZetAllowed(ref zet))
					{
						VoerUit(zet); 
						Speler_Zet(this, zet);
					}
					else
					{
                        if (!zet.van.IsEquals(zet.naar))
                        {
                            My.Status("Illegal move {0}", zet);
                        }
						selectedShape.Location = getBordLocation(zet.van.x, zet.van.y);
					}
				}
                else if (zet.van != null) // Berg stuk op in doos.
				{
					Console.WriteLine("buitenbord");
                    if (WedstrijdMode) //V134
                    {
                        MessageBox.Show("In Wedstrijd mode mogen geen stukken van het bord gehaald worden.");
                        selectedShape.Location = getBordLocation(zet.van.x, zet.van.y);
                        return;
                    }
                    doos.BergOp(selectedShape);
					Speler_Zet("doos", zet);
				}
            }
        }

        private void ShowDots(bool show) //V137
        {
            if (WedstrijdMode) return;
            if (show)
                foreach (Zet legalZet in computer.DoeEenZet(zet.van))
                {
                    ShowDot(legalZet);
                }
            else
            {
                ShapeCollection sc = shapeContainer.Shapes;
                for (int i = sc.Count-1; i >=0 ; i--)
                {
                    if (((Shape)sc.get_Item(i)).Name == "DOT") sc.RemoveAt(i);
                }
            }
        }

        private void ShowDot(Zet zet)
        {
            Point van = getBordLocation(zet.van.x, zet.van.y);
            Point naar = getBordLocation(zet.naar.x, zet.naar.y);
            OvalShape circle = new OvalShape(shapeContainer);
            bool isPiece = zet.naar.stuk.type != StukType.geen; //V138
            int offset = isPiece ? 0 : 12;
            int r      = isPiece ? 26 : 2;
            circle.Location = new Point(naar.X+offset,naar.Y+offset);
            circle.Name = "DOT";
            circle.BorderColor = Color.Red;
            circle.Size = new Size(r,r);
            circle.BringToFront();
            circle.Show();
        }

        /// <summary>
        /// Voer grafisch de zet uit op schaakbord.
        /// </summary>
        /// <param name="zet"></param>
        public void VoerUit(Zet zet)
        {

            OvalShape shape = null;
            if (zet.van != null)
            {
                shape = getShape(zet.van); // shape != null when pressing notation arrows.
            }
            if (shape == null) shape = selectedShape;// Shape is null when dragged.
            if (animate)
            Animate(shape, getBordLocation(zet.naar.x, zet.naar.y)); //V145
            //
            // Berg geslagen stuk op in doos.
            //
            doos.BergOp(getShape(zet.naar)); //V129
            if (zet.promotie)
            {
                doos.BergOp(shape); // Pion
                Stuk dame = new Stuk(zet.van.stuk.kleur, StukType.Dame);
                shape = doos.HaalOp(dame);
            }
            shape.Location = getBordLocation(zet.naar.x, zet.naar.y);
            if (zet.castling)
            {
                Zet toren = new Zet();
                bool longCastling = zet.naar.x < 4;
                toren.van = new Veld((longCastling) ? 0 : 7, zet.van.y);
                toren.naar = new Veld((longCastling) ? 3 : 5, zet.van.y);//V105++
                VoerUit(toren);
            }
        }

        private void Animate(OvalShape shape, Point to) //V145
        {
            const int MAX_STEPS = 100;
            PointF from = shape.Location;
            float dx = (to.X - from.X) / MAX_STEPS;
            float dy = (to.Y - from.Y) / MAX_STEPS;
            for (int i = 0; i < MAX_STEPS*0.9; i++)
			{
                shape.Location = AddPoint(from, dx, dy, i);
                Application.DoEvents();
			}
        }

        private Point AddPoint(PointF from, float dx, float dy, int i)
        {
            Point p = new Point();
            p.X = (int)(from.X + i * dx);
            p.Y = (int)(from.Y + i * dy);
            return p;
        }
        /// <summary>
        /// Return shape at veld location
        /// </summary>
        public OvalShape getShape(Veld veld)
        {
            Point location = getBordLocation(veld.x, veld.y);
            //Console.Write();
            foreach (SimpleShape shape in shapeContainer.Shapes)
            {
                //Console.WriteLine("shapecontainer: {0} loc={1}",shape.Name,shape.Location );
                if (shape.Location == location)
                {
                    //Console.WriteLine("getshape {0} at {1} found",shape.Name, location);
                    return shape as OvalShape;
                }
            }
            //Console.WriteLine("not found");
            return null;
        }
        public void Herstel(Zet zet)
        {

            VerwijderShapeOpVanVeld(zet.van); //V135
            OvalShape shape = getShape(zet.naar);
            if (shape == null) return;
            // Herstel promotie.
            if (zet.promotie)
            {
                doos.BergOp(shape); // berg dame op
                shape = doos.HaalOp(zet.van.stuk);
            }
            shape.Location = getBordLocation(zet.van.x, zet.van.y); //npe bij terug in doos
            // Zet geslagen stuk terug.
            if (zet.naar.stuk.type != StukType.geen)
            {
                shape = doos.HaalOp(zet.naar.stuk);
                if (shape == null) //stuk staat al ergens op het bord.
                {

                }
                else shape.Location = getBordLocation(zet.naar.x, zet.naar.y);
            }
            //Herstel castling.
            if (zet.castling)
            {
                Zet toren = new Zet();
                bool longCastling = zet.naar.x < 4;
                toren.van = new Veld((longCastling) ? 0 : 7, zet.van.y);
                toren.naar = new Veld((longCastling) ? 3 : 5, zet.van.y);//V105++
                Herstel(toren);
            }
        }

        private void VerwijderShapeOpVanVeld(Veld vanveld) //V135
        {
            OvalShape shape = getShape(vanveld);
            if (shape != null)
            {
                shape.Left -= 10;
                shape.Top -= 10;
                shape.Select();
                MessageBox.Show("Mag dit stuk weer in de doos?");
                doos.BergOp(shape);
            }
        }
        /// <summary>
		/// Return veld location at coor. x,y
		/// </summary>
		/// <param name="x"> 0-7</param>
		/// <param name="y"> 0-7</param>
		/// <returns></returns>
		public Point getBordLocation(int x, int y)
		{
			Point newlocation = chessBord.Location;
			newlocation.Offset((x + 1) * 32 + 2, (y + 1) * 32 + 2);
			return newlocation;
		}
		public Point getBordCoor(Point mouseLocation)
		{
			Point offset = chessBord.Location;
			int x = (mouseLocation.X - offset.X) / 32 - 1;
			int y = (mouseLocation.Y - offset.Y) / 32 - 1;
			if (x < 0 || x > 7 || y < 0 || y > 7) return BUITENBORD;
			//Console.WriteLine("veld {0}{1}", "abcdefgh"[x], 8 - y);
			return new Point(x, y);
		}
	}
}

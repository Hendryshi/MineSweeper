using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MineSweeper.Model;

namespace MineSweeper
{
	public partial class mainForm : Form
	{
		private Game game;

		public mainForm()
		{
			InitializeComponent();
		}

		private void mainForm_Paint(object sender, PaintEventArgs e)
		{
			//game.Draw(this.CreateGraphics(), pnlInfo.CreateGraphics(), pnlMine.CreateGraphics());
			Size a = ClientRectangle.Size;
			this.CreateGraphics().DrawImage(game.GameFrame.DrawMainFrame(), game.GameFrame.RctGameField.Location);


			Graphics g = this.pnlMine.CreateGraphics();
			Bitmap newImage = new Bitmap("..\\..\\Img\\mine.bmp");
			int width = newImage.Width;
			int height = newImage.Height;
			// Create coordinates for upper-left corner of image.
			float x = 0;
			float y = 0;


			// Create rectangle for source image.
			RectangleF srcRect = new RectangleF(0, 0, 20, 20);
			GraphicsUnit units = GraphicsUnit.Pixel;
			// Draw image to screen.
			g.DrawImage(newImage, new Rectangle(0, 0, 20, 20), srcRect, units);
			g.DrawImage(newImage, new Rectangle(20, 0, 20, 20), srcRect, units);
			g.DrawImage(newImage, new Rectangle(0, 20, 20, 20), srcRect, units);
			g.DrawImage(newImage, new Rectangle(20, 20, 20, 20), srcRect, units);

			Graphics xd = this.pnlMine.CreateGraphics();

			xd.DrawImage(newImage, new Rectangle(0, 0, 20, 20), srcRect, units);
		}

		private void mainForm_Load(object sender, EventArgs e)
		{
			Point gameOffsetPosition = new Point(0, this.mainMenuStrip.Height);
			game = new Game(gameOffsetPosition);

			pnlInfo.Location = game.GameFrame.RctPnlInfo.Location;
			pnlInfo.Size = game.GameFrame.RctPnlInfo.Size;

			pnlMine.Location = game.GameFrame.RctPnlMine.Location;
			pnlMine.Size = game.GameFrame.RctPnlMine.Size;

			ClientSize = new Size(game.GameFrame.RctGameField.Width + gameOffsetPosition.X, game.GameFrame.RctGameField.Height + gameOffsetPosition.Y);
			
		}
	}
}


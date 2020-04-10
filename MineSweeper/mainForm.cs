using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MineSweeper
{
	public partial class mainForm : Form
	{
		public mainForm()
		{
			InitializeComponent();
		}

		private void mainForm_Paint(object sender, PaintEventArgs e)
		{
			string currentDir = Environment.CurrentDirectory;
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

			int borderWidth = (Width - ClientSize.Width) / 2;
			int borderHeight = (Height - ClientSize.Height) / 2;

			this.pnlInfo.Width = 20 * 9;
			this.pnlInfo.Height = 50;
			
			this.pnlInfo.Location = new Point(5, this.mainMenuStrip.Height + 5);

			this.pnlMine.Width = 20 * 9;
			this.pnlMine.Height = 20 * 9;

			this.pnlMine.Location = new Point(5, this.pnlInfo.Location.Y + pnlInfo.Height + 5);

			ClientSize = new Size(10 + pnlMine.Width, pnlMine.Location.Y + pnlMine.Height + 5);

			//Size = new Size(1000, 500);
		
		}
	}
}


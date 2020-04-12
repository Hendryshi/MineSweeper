using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Configuration;

namespace MineSweeper.Model
{
	class Frame
	{
		//private readonly int border_width;

		private readonly Rectangle rctPnlInfo;

		private readonly Rectangle rctPnlMine;

		private readonly Rectangle rctGameField;

		private Bitmap bufferMainFrame;
		private Bitmap bufferInfoFrame;
		private Bitmap bufferMineFrame;

		private readonly int squareWidth = 20;

		private readonly int boardWidth = Int16.Parse(ConfigurationManager.AppSettings["boardWidth"]);

		
		private Color GRAY = Color.FromArgb(192, 192, 192);
		private Color DARK_GRAY = Color.FromArgb(128, 128, 128);
		private Color WHITE = Color.FromArgb(255, 255, 255);


		public Frame(Point gameOffsetPosition, Size mineCountSize)
		{
			int pnlWidth = mineCountSize.Width * squareWidth;
			int pnlHeight = mineCountSize.Height * squareWidth;
			rctPnlInfo = new Rectangle(new Point(gameOffsetPosition.X + boardWidth, gameOffsetPosition.Y + boardWidth), new Size(pnlWidth, 40));
			rctPnlMine = new Rectangle(new Point(rctPnlInfo.X, rctPnlInfo.Y + rctPnlInfo.Height + boardWidth), new Size(pnlWidth, pnlHeight));

			rctGameField = new Rectangle(gameOffsetPosition, new Size(pnlWidth + boardWidth * 2, rctPnlInfo.Height + RctPnlMine.Height + boardWidth * 3));

			bufferMainFrame = new Bitmap(rctGameField.Width, rctGameField.Height);
			bufferInfoFrame = new Bitmap(rctPnlInfo.Width, rctPnlInfo.Height);
			bufferMineFrame = new Bitmap(rctPnlMine.Width, rctPnlMine.Height);
		}


		public Rectangle RctPnlInfo { get => rctPnlInfo; }
		public Rectangle RctPnlMine { get => rctPnlMine; }
		public Rectangle RctGameField { get => rctGameField; }


		public Bitmap DrawMainFrame()
		{
			Graphics g = Graphics.FromImage(bufferMainFrame);
			Pen gray = new Pen(GRAY, boardWidth);
			Pen black = new Pen(DARK_GRAY, 2);
			Pen light = new Pen(WHITE, 2);

			g.DrawLine(gray, new Point(boardWidth / 2, 0), new Point(boardWidth / 2, rctGameField.Height));
			g.DrawLine(gray, new Point(rctGameField.Width - boardWidth / 2, 0), new Point(rctGameField.Width - boardWidth / 2, rctGameField.Height));
			g.DrawLine(gray, new Point(0, boardWidth / 2), new Point(rctGameField.Width, boardWidth / 2));
			g.DrawLine(gray, new Point(0, boardWidth + rctPnlInfo.Height + boardWidth / 2), new Point(rctGameField.Width, boardWidth + rctPnlInfo.Height + boardWidth / 2));
			g.DrawLine(gray, new Point(0, rctGameField.Height - boardWidth / 2), new Point(rctGameField.Width, rctGameField.Height - boardWidth / 2));

			g.DrawLine(black, new Point(boardWidth - 1, boardWidth - 1), new Point(boardWidth - 1, boardWidth + rctPnlInfo.Height));
			g.DrawLine(black, new Point(boardWidth - 1, boardWidth - 1), new Point(boardWidth + rctPnlInfo.Width + 1, boardWidth - 1));
			g.DrawLine(light, new Point(boardWidth + rctPnlInfo.Width + 1, boardWidth), new Point(boardWidth + rctPnlInfo.Width + 1, boardWidth + rctPnlInfo.Height + 1));
			g.DrawLine(light, new Point(boardWidth - 1, boardWidth + rctPnlInfo.Height + 1), new Point(boardWidth + rctPnlInfo.Width + 1, boardWidth + rctPnlInfo.Height + 1));

			g.DrawLine(black, new Point(boardWidth - 1, rctGameField.Height - boardWidth - rctPnlMine.Height - 1), new Point(boardWidth - 1, rctGameField.Height - boardWidth));
			g.DrawLine(black, new Point(boardWidth - 1, rctGameField.Height - boardWidth - rctPnlMine.Height - 1), new Point(boardWidth + rctPnlMine.Width + 1, rctGameField.Height - boardWidth - rctPnlMine.Height - 1));
			g.DrawLine(light, new Point(boardWidth + rctPnlMine.Width + 1, rctGameField.Height - boardWidth - rctPnlMine.Height), new Point(boardWidth + rctPnlMine.Width + 1, rctGameField.Height - boardWidth + 1));
			g.DrawLine(light, new Point(boardWidth - 1, rctGameField.Height - boardWidth + 1), new Point(boardWidth + rctPnlMine.Width + 1, rctGameField.Height - boardWidth + 1));

			return bufferMainFrame;
		}

		//string currentDir = Environment.CurrentDirectory;
		//Graphics g = this.pnlMine.CreateGraphics();
		//Bitmap newImage = new Bitmap("..\\..\\Img\\mine.bmp");
		//int width = newImage.Width;
		//int height = newImage.Height;
		//// Create coordinates for upper-left corner of image.
		//float x = 0;
		//float y = 0;


		//// Create rectangle for source image.
		//RectangleF srcRect = new RectangleF(0, 0, 20, 20);
		//GraphicsUnit units = GraphicsUnit.Pixel;
		//// Draw image to screen.
		//g.DrawImage(newImage, new Rectangle(0, 0, 20, 20), srcRect, units);
		//	g.DrawImage(newImage, new Rectangle(20, 0, 20, 20), srcRect, units);
		//	g.DrawImage(newImage, new Rectangle(0, 20, 20, 20), srcRect, units);
		//	g.DrawImage(newImage, new Rectangle(20, 20, 20, 20), srcRect, units);

		//	Graphics xd = this.pnlMine.CreateGraphics();

		//xd.DrawImage(newImage, new Rectangle(0, 0, 20, 20), srcRect, units);
	}
}

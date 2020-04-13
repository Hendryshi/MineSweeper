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
		
		private readonly Rectangle rctPnlInfo;

		private readonly Rectangle rctPnlMine;

		private readonly Rectangle rctGameField;

		private Bitmap bufferMainFrame;
		private Bitmap bufferInfoFrame;
		private Bitmap bufferMineFrame;


		private readonly int boardWidth = Int16.Parse(ConfigurationManager.AppSettings["boardWidth"]);
		private readonly int squareSize = Int16.Parse(ConfigurationManager.AppSettings["squareSize"]);
		private readonly int pnlInfoHeight = Int16.Parse(ConfigurationManager.AppSettings["pnlInfoHeight"]);
		private readonly Bitmap imgMine = new Bitmap(@"..\..\Img\mine.bmp");


		private Color GRAY = Color.FromArgb(192, 192, 192);
		private Color DARK_GRAY = Color.FromArgb(128, 128, 128);
		private Color WHITE = Color.FromArgb(255, 255, 255);

		public const int ImgUnitWidth = 20;

		public Frame(Point gameOffsetPosition, Size mineCountSize)
		{
			int pnlWidth = mineCountSize.Width * squareSize;
			int pnlHeight = mineCountSize.Height * squareSize;
			rctPnlInfo = new Rectangle(new Point(gameOffsetPosition.X + boardWidth, gameOffsetPosition.Y + boardWidth), new Size(pnlWidth, pnlInfoHeight));
			rctPnlMine = new Rectangle(new Point(rctPnlInfo.X, rctPnlInfo.Y + rctPnlInfo.Height + boardWidth), new Size(pnlWidth, pnlHeight));

			rctGameField = new Rectangle(gameOffsetPosition, new Size(pnlWidth + boardWidth * 2, rctPnlInfo.Height + RctPnlMine.Height + boardWidth * 3));

			bufferMainFrame = new Bitmap(rctGameField.Width, rctGameField.Height);
			bufferInfoFrame = new Bitmap(rctPnlInfo.Width, rctPnlInfo.Height);
			bufferMineFrame = new Bitmap(rctPnlMine.Width, rctPnlMine.Height);
		}


		public Rectangle RctPnlInfo { get => rctPnlInfo; }
		public Rectangle RctPnlMine { get => rctPnlMine; }
		public Rectangle RctGameField { get => rctGameField; }
		public Bitmap MineFrame { get => bufferMineFrame; }


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

		public Bitmap DrawSquare(Square square)
		{
			Graphics g = Graphics.FromImage(bufferMineFrame);
			int srcY = (int)square.Status + (square.Status == MineStatus.OpenedNumber ? (Square.MaxSquareNum - square.Value) * ImgUnitWidth : 0);
			Rectangle mineRect = new Rectangle(square.Location, new Size(squareSize, squareSize));
			Rectangle srcRect = new Rectangle(new Point(0, srcY), new Size(ImgUnitWidth, ImgUnitWidth));

			GraphicsUnit units = GraphicsUnit.Pixel;
			g.DrawImage(imgMine, mineRect, srcRect, units);

			return bufferMineFrame;
		}

	}
}

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
		private readonly Rectangle rctPnlTimer;
		private readonly Rectangle rctPnlMine;
		private readonly Rectangle rctGameField;

		private Bitmap bufferMainFrame;
		private Bitmap bufferInfoFrame;
		private Bitmap bufferTimerFrame;
		private Bitmap bufferMineFrame;


		private readonly int boardWidth = Int16.Parse(ConfigurationManager.AppSettings["boardWidth"]);
		private readonly int squareSize = Int16.Parse(ConfigurationManager.AppSettings["squareSize"]);
		private readonly int pnlInfoHeight = Int16.Parse(ConfigurationManager.AppSettings["pnlInfoHeight"]);
		private readonly Bitmap imgMine = new Bitmap(@"Img\mine.bmp");
		private readonly Bitmap imgFace = new Bitmap(@"Img\face.bmp");
		private readonly Bitmap imgNbr = new Bitmap(@"Img\number.bmp");


		private Color GRAY = Color.FromArgb(192, 192, 192);
		private Color DARK_GRAY = Color.FromArgb(128, 128, 128);
		private Color WHITE = Color.FromArgb(255, 255, 255);

		public const int ImgMineUnitWidth = 20;
		public const int ImgFaceUnitWidth = 24;
		public const int ImgNbrUnitWidth = 14;
		public const int ImgNbrUnitHeight = 23;

		public Frame(Point gameOffsetPosition, Size mineCountSize, int mineCount)
		{
			int pnlWidth = mineCountSize.Width * squareSize;
			int pnlHeight = mineCountSize.Height * squareSize;

			rctPnlInfo = new Rectangle(new Point(gameOffsetPosition.X + boardWidth, gameOffsetPosition.Y + boardWidth), new Size(pnlWidth, pnlInfoHeight));
			rctPnlTimer = new Rectangle(new Point(rctPnlInfo.Width - 5 - 3 * ImgNbrUnitWidth, 0), new Size(3 * ImgNbrUnitWidth, pnlInfoHeight));
			rctPnlMine = new Rectangle(new Point(rctPnlInfo.X, rctPnlInfo.Y + rctPnlInfo.Height + boardWidth), new Size(pnlWidth, pnlHeight));
			rctGameField = new Rectangle(gameOffsetPosition, new Size(pnlWidth + boardWidth * 2, rctPnlInfo.Height + RctPnlMine.Height + boardWidth * 3));

			bufferMainFrame = new Bitmap(rctGameField.Width, rctGameField.Height);
			bufferInfoFrame = new Bitmap(rctPnlInfo.Width, rctPnlInfo.Height);
			bufferTimerFrame = new Bitmap(rctPnlTimer.Width, rctPnlTimer.Height);
			bufferMineFrame = new Bitmap(rctPnlMine.Width, rctPnlMine.Height);

			Graphics.FromImage(bufferMainFrame).Clear(GRAY);
			Graphics.FromImage(bufferInfoFrame).Clear(GRAY);
			Graphics.FromImage(bufferTimerFrame).Clear(GRAY);
			Graphics.FromImage(bufferMineFrame).Clear(GRAY);

			DrawMainFrame();
			DrawFace();
			DrawFlagNbr(mineCount);
			DrawTimeNbr();
		}


		public Rectangle RctPnlInfo { get => rctPnlInfo; }
		public Rectangle RctPnlTimer { get => rctPnlTimer; }
		public Rectangle RctPnlMine { get => rctPnlMine; }
		public Rectangle RctGameField { get => rctGameField; }
		public Bitmap MainFrame { get => bufferMainFrame; }
		public Bitmap MineFrame { get => bufferMineFrame; }
		public Bitmap InfoFrame { get => bufferInfoFrame; }
		public Bitmap TimerFrame { get => bufferTimerFrame; }


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
			int srcY = (int)square.Status + (square.Status == MineStatus.OpenedNumber ? (Square.MaxSquareNum - square.Value) * ImgMineUnitWidth : 0);
			Rectangle mineRect = new Rectangle(square.Location, new Size(squareSize, squareSize));
			Rectangle srcRect = new Rectangle(new Point(0, srcY), new Size(ImgMineUnitWidth, ImgMineUnitWidth));

			GraphicsUnit units = GraphicsUnit.Pixel;
			g.DrawImage(imgMine, mineRect, srcRect, units);

			return bufferMineFrame;
		}

		public Bitmap DrawFace(GameFace gameFace = GameFace.SmileUp)
		{
			Graphics g = Graphics.FromImage(bufferInfoFrame);
			
			GraphicsUnit units = GraphicsUnit.Pixel;
			int faceSize = rctPnlInfo.Height - 12;

			Rectangle faceRect = new Rectangle(new Point(rctPnlInfo.Width / 2 - faceSize / 2, rctPnlInfo.Height / 2 - faceSize / 2), new Size(faceSize, faceSize));
			Rectangle srcRect = new Rectangle(new Point(0, (int)gameFace), new Size(ImgFaceUnitWidth, ImgFaceUnitWidth));
			g.DrawImage(imgFace, faceRect, srcRect, units);

			return bufferMineFrame;
		}

		public Bitmap DrawFlagNbr(int flagNbr)
		{
			Graphics g = Graphics.FromImage(bufferInfoFrame);
			GraphicsUnit units = GraphicsUnit.Pixel;
			int nbrSize = rctPnlInfo.Height - 17;

			if(flagNbr < -99) flagNbr = -99;
			int fstNbr = flagNbr >= 0 ? Math.Abs(flagNbr / 100) : 11;
			int scdNbr = Math.Abs(flagNbr / 10 % 10);
			int trdNbr = Math.Abs(flagNbr % 10);

			Rectangle fstRect = new Rectangle(new Point(5, rctPnlInfo.Height / 2 - nbrSize / 2), new Size(ImgNbrUnitWidth, nbrSize));
			Rectangle fstSrcRect = new Rectangle(new Point(0, (11 - fstNbr) * ImgNbrUnitHeight), new Size(ImgNbrUnitWidth, ImgNbrUnitHeight));
			g.DrawImage(imgNbr, fstRect, fstSrcRect, units);

			Rectangle scdRect = new Rectangle(new Point(fstRect.X + ImgNbrUnitWidth, rctPnlInfo.Height / 2 - nbrSize / 2), new Size(ImgNbrUnitWidth, nbrSize));
			Rectangle scdSrcRect = new Rectangle(new Point(0, (11 - scdNbr) * ImgNbrUnitHeight), new Size(ImgNbrUnitWidth, ImgNbrUnitHeight));
			g.DrawImage(imgNbr, scdRect, scdSrcRect, units);

			Rectangle trdRect = new Rectangle(new Point(scdRect.X + ImgNbrUnitWidth, rctPnlInfo.Height / 2 - nbrSize / 2), new Size(ImgNbrUnitWidth, nbrSize));
			Rectangle trdSrcRect = new Rectangle(new Point(0, (11 - trdNbr) * ImgNbrUnitHeight), new Size(ImgNbrUnitWidth, ImgNbrUnitHeight));
			g.DrawImage(imgNbr, trdRect, trdSrcRect, units);

			return bufferMineFrame;

		}

		public Bitmap DrawTimeNbr(int timeNbr = 0)
		{
			Graphics g = Graphics.FromImage(bufferTimerFrame);
			GraphicsUnit units = GraphicsUnit.Pixel;
			int nbrSize = rctPnlInfo.Height - 17;

			if(timeNbr > 999) timeNbr = 999;
			int fstNbr = timeNbr / 100;
			int scdNbr = timeNbr / 10 % 10;
			int trdNbr = timeNbr % 10;

			Rectangle fstRect = new Rectangle(new Point(0, rctPnlTimer.Height / 2 - nbrSize / 2), new Size(ImgNbrUnitWidth, nbrSize));
			Rectangle fstSrcRect = new Rectangle(new Point(0, (11 - fstNbr) * ImgNbrUnitHeight), new Size(ImgNbrUnitWidth, ImgNbrUnitHeight));
			g.DrawImage(imgNbr, fstRect, fstSrcRect, units);

			Rectangle scdRect = new Rectangle(new Point(fstRect.X + ImgNbrUnitWidth, rctPnlTimer.Height / 2 - nbrSize / 2), new Size(ImgNbrUnitWidth, nbrSize));
			Rectangle scdSrcRect = new Rectangle(new Point(0, (11 - scdNbr) * ImgNbrUnitHeight), new Size(ImgNbrUnitWidth, ImgNbrUnitHeight));
			g.DrawImage(imgNbr, scdRect, scdSrcRect, units);

			Rectangle trdRect = new Rectangle(new Point(scdRect.X + ImgNbrUnitWidth, rctPnlTimer.Height / 2 - nbrSize / 2), new Size(ImgNbrUnitWidth, nbrSize));
			Rectangle trdSrcRect = new Rectangle(new Point(0, (11 - trdNbr) * ImgNbrUnitHeight), new Size(ImgNbrUnitWidth, ImgNbrUnitHeight));
			g.DrawImage(imgNbr, trdRect, trdSrcRect, units);

			return bufferMineFrame;

		}

	}
}

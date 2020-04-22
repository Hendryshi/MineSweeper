using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Configuration;

namespace MineSweeper.Model
{
	class Game
	{
		private Square[,] squares;
		private Frame gameFrame;
		private bool isStart;
		private bool? result;
		
		private readonly GameLevel level;
		private int mineCount;
		private int timeNbr;
		private readonly int squareSize = Int16.Parse(ConfigurationManager.AppSettings["squareSize"]);

		#region test
		public override string ToString()
		{
			string str = string.Empty;
			foreach(Square sq in squares)
			{
				if(sq.IsMine())
					str += string.Format("location[{0}, {1}], value[{2}] ", sq.Location.X, sq.Location.Y, sq.Value);
			}
			return str;
		}

		public int getMineCount()
		{
			var query = from Square sq in squares
						where sq.IsMine()
						select sq;
			return query.Count();
		}
		#endregion

		public Game(Point gameOffsetPosition, GameLevel level = GameLevel.Intermediate)
		{
			switch(level)
			{
				case GameLevel.Beginner:
					squares = new Square[9, 9];
					mineCount = 99;
					break;
				case GameLevel.Intermediate:
					squares = new Square[16, 16];
					mineCount = 40;
					break;
				case GameLevel.Expert:
					squares = new Square[30, 16];
					mineCount = 99;
					break;
			}

			isStart = false;
			result = null;
			this.level = level;
			timeNbr = 0;
			gameFrame = new Frame(gameOffsetPosition, new Size(squares.GetLength(0), squares.GetLength(1)), mineCount);
			CreateSquares();
		}

		public Frame GameFrame { get => gameFrame; }
		public bool IsStart { get => isStart; set { isStart = value; } }
		public bool? Result { get => result; }

		public bool InGameSize(Point location)
		{
			return location.X > 0 && location.Y > 0 && location.X < gameFrame.RctPnlMine.Width && location.Y < gameFrame.RctPnlMine.Height;
		}

		public void StartGame(Point point)
		{
			KnuthShuffleMine(point);
			IsStart = true;
		}

		private void CreateSquares()
		{
			for(int y = 0; y < squares.GetLength(1); y++)
			{
				for(int x = 0; x < squares.GetLength(0); x++)
				{
					int squareValue = y * squares.GetLength(0) + x < mineCount ? -1 : 0;
					Square sq = new Square(new Point(x * squareSize, y * squareSize), squareValue);

					gameFrame.DrawSquare(sq);
					squares[x, y] = sq;
				}
			}
		}

		public void OpenSingleSquare(Point point)
		{
			Square sq = squares[point.X / squareSize, point.Y / squareSize];
			if(sq.IsClosed())
			{
				bool noError = sq.OpenSquare();
				gameFrame.DrawSquare(sq);

				if(sq.Value == 0)
					ExpandSquares(sq);

				CheckWinOrLose(noError);
			}
		}

		public void OpenAroundSquares(Point point)
		{
			Square sq = squares[point.X / squareSize, point.Y / squareSize];

			if(!sq.IsClosed() && sq.Value - sq.GetAroundFlagCount(squares) == 0)
			{
				bool noError = ExpandSquares(sq);
				CheckWinOrLose(noError);
			}
			else
				SetAllSquaresUp();
		}

		public void AddRemoveFlag(Point point)
		{
			Square sq = squares[point.X / squareSize, point.Y / squareSize];
			if(sq.IsClosed())
			{
				mineCount += sq.AddRemoveFlag();
				gameFrame.DrawSquare(sq);
			}
			gameFrame.DrawFlagNbr(mineCount);
		}

		public bool ExpandSquares(Square sq)
		{
			bool noError = true;
			List<Square> lstAroundSquare = sq.GetAroundSquare(squares, false, true);

			foreach(Square sqAround in lstAroundSquare)
			{
				noError &= sqAround.OpenSquare();
				gameFrame.DrawSquare(sqAround);

				if(sqAround.Value == 0)
					noError &= ExpandSquares(sqAround);
			}
			return noError;
		}

		public void OpenAllSquares(bool hitMine = true)
		{
			foreach(Square sq in squares)
			{
				sq.OpenSquare(hitMine);
				gameFrame.DrawSquare(sq);
			}
		}

		private void OpenAllMines(bool hasWin = false)
		{
			var query = from Square sq in squares
						where sq.IsClosed()
						select sq;

			foreach(Square sq in query)
			{
				if(sq.IsMine() || sq.Status == MineStatus.Flagged)
				{
					sq.OpenSquare(false, hasWin);
					gameFrame.DrawSquare(sq);
				}
			}
		}

		private void CheckWinOrLose(bool checkWin = true)
		{
			if(checkWin)
				foreach(Square sq in squares)
				{
					if(!sq.IsMine() && sq.Status != MineStatus.OpenedNumber)
						return;
				}

			// has result
			OpenAllMines(checkWin);
			result = checkWin;
			ChangeFace(GameFace.SunGlasses);

			if(checkWin)
			{
				mineCount = 0;
				gameFrame.DrawFlagNbr(mineCount);
			}
		}


		public void SetSquaresDown(Point point, bool getAround = false)
		{
			SetAllSquaresUp();
			Square sq = squares[point.X / squareSize, point.Y / squareSize];
			sq.SetSquareDown();
			gameFrame.DrawSquare(sq);

			if(getAround)
			{
				foreach(Square sqAround in sq.GetAroundSquare(squares, false, true))
				{
					sqAround.SetSquareDown();
					gameFrame.DrawSquare(sqAround);
				}
			}
		}

		public void SetAllSquaresUp()
		{
			var query = from Square sq in squares
						where sq.Status == MineStatus.MouseDown
						select sq;
			foreach(Square SqDown in query)
			{
				SqDown.Status = MineStatus.Closed;
				gameFrame.DrawSquare(SqDown);
			}
		}

		public void ChangeFace(GameFace gf)
		{
			if(result.HasValue)
				gameFrame.DrawFace(result == true ? GameFace.SunGlasses : GameFace.Crying);
			else
				gameFrame.DrawFace(gf);
		}

		public void ChangeTime()
		{
			gameFrame.DrawTimeNbr(timeNbr);
			timeNbr++;
		}


		private void KnuthShuffleMine(Point point)
		{
			Random ran = new Random();
			int indexX = point.X / squareSize;
			int indexY = point.Y / squareSize;
			Square sqPoint = squares[indexX, indexY];

			int indexOffset = sqPoint.GetAroundMineCount(squares) + (sqPoint.IsMine() ? 1 : 0);

			for(int y = 0; y <= squares.GetLength(1) - 1; y++)
			{
				for(int x = 0; x <= squares.GetLength(0) - 1; x++)
				{
					// this area cannot be mine
					if(x >= indexX - 1 && x <= indexX + 1 && y >= indexY - 1 && y <= indexY + 1)
						squares[x, y].Value = 0;
					else
					{
						Point ranP = GetRandomPoint(ran, x, y, indexX, indexY);
						int ranSquareValue = ranP.Y * squares.GetLength(0) + ranP.X < mineCount + indexOffset ? -1 : squares[ranP.X, ranP.Y].Value;

						int value = y * squares.GetLength(0) + x < mineCount + indexOffset ? -1 : squares[x, y].Value;
						squares[x, y].Value = ranSquareValue;
						squares[ranP.X, ranP.Y].Value = value;
						gameFrame.DrawSquare(squares[x, y]);
					}
				}
			}

			var queryMine = from Square sq in squares
							where sq.IsMine()
							select sq;

			foreach(Square sq in queryMine)
			{
				List<Square> lstAroundSquare = sq.GetAroundSquare(squares, true);
				foreach(Square sqAround in lstAroundSquare)
				{
					sqAround.Value += 1;
					gameFrame.DrawSquare(sqAround);
				}
			}
		}

		private Point GetRandomPoint(Random random, int x, int y, int indexX, int indexY)
		{
			int ranX;
			int ranY;
			int ranT;
			do
			{
				ranT = random.Next(y * squares.GetLength(0) + x, squares.GetLength(0) * squares.GetLength(1) - 1);
				ranX = ranT % squares.GetLength(0);
				ranY = ranT / squares.GetLength(0);
			} while(ranX >= indexX - 1 && ranX <= indexX + 1 && ranY >= indexY - 1 && ranY <= indexY + 1);

			return new Point(ranX, ranY);
		}

	}

	public enum GameLevel
	{
		Beginner = 0,
		Intermediate,
		Expert,
		Custome
	}

	public enum GameFace
	{
		SmileDown = 0,
		SunGlasses = 24,
		Crying = 48,
		MouthOpen = 72,
		SmileUp = 96
	}
}

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

		private readonly GameLevel level;
		private readonly int mineCount;
		private readonly Size gameOffsetSize;
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

		public Game(Point gameOffsetPosition)
		{
			//this.gameOffsetSize = gameOffsetPosition;
			squares = new Square[30, 16];
			isStart = false;
			level = GameLevel.Beginner;
			mineCount = 99;
			gameFrame = new Frame(gameOffsetPosition, new Size(squares.GetLength(0), squares.GetLength(1)));
			CreateSquares();
		}

		public Frame GameFrame { get => gameFrame; }
		public bool IsStart { get => isStart; set { isStart = value; } }

		public bool InGameSize(Point location)
		{
			return location.X > 0 && location.Y > 0 && location.X < gameFrame.RctPnlMine.Width && location.Y < gameFrame.RctPnlMine.Height;
		}

		public void CreateSquares()
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
				bool isCorrect = sq.OpenSquare();
				gameFrame.DrawSquare(sq);

				if(isCorrect && sq.Value == 0)
					ExpandSquares(sq);
				else if(!isCorrect)
				{
					OpenAllMines();
					ChangeFace(GameFace.Crying);
				}
			}

			IsWin();
		}

		public void AddRemoveFlag(Point point)
		{
			Square sq = squares[point.X / squareSize, point.Y / squareSize];
			if(sq.IsClosed())
			{
				sq.AddRemoveFlag();
				gameFrame.DrawSquare(sq);
			}
		}

		public bool ExpandSquares(Square sq)
		{
			List<Square> lstAroundSquare = sq.GetAroundSquare(squares, false, true);
			bool isCorrect = true;
			foreach(Square sqAround in lstAroundSquare)
			{
				isCorrect &= sqAround.OpenSquare();
				gameFrame.DrawSquare(sqAround);

				if(sqAround.Value == 0)
					isCorrect &= ExpandSquares(sqAround);
			}
			return isCorrect;
		}

		public void OpenAroundSquares(Point point)
		{
			Square sq = squares[point.X / squareSize, point.Y / squareSize];

			if(!sq.IsClosed() && sq.Value - sq.GetAroundFlagCount(squares) == 0)
			{
				if(!ExpandSquares(sq))
					OpenAllMines();
			}
			else
				SetAllSquaresUp();

			IsWin();
		}

		public void OpenAllSquares(bool hitMine = true)
		{
			foreach(Square sq in squares)
			{
				sq.OpenSquare(hitMine);
				gameFrame.DrawSquare(sq);
			}
		}

		public void OpenAllMines(bool hasWin = false)
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

		public bool IsWin()
		{

			foreach(Square sq in squares)
			{
				if(!sq.IsMine() && sq.Status != MineStatus.OpenedNumber)
					return false;
			}

			OpenAllMines(true);
			return true;
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
			gameFrame.DrawFace(gf);
		}


		public void KnuthShuffleMine(Point point)
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
				//lstAroundSquare.ForEach(item => { item.Value = item.Value + 1; gameFrame.DrawSquare(item); });
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

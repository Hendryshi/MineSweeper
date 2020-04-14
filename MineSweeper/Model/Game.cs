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

		public Game(Point gameOffsetPosition)
		{
			//this.gameOffsetSize = gameOffsetPosition;
			squares = new Square[9, 9];
			isStart = true;
			level = GameLevel.Beginner;
			mineCount = 10;
			gameFrame = new Frame(gameOffsetPosition, new Size(9,9));
			CreateSquares();
		}

		public Frame GameFrame { get => gameFrame; }
		public bool IsStart { get => isStart; set { isStart = value; } }

		public void CreateSquares()
		{
			for(int y = 0; y < squares.GetLength(1); y++)
			{
				for(int x = 0; x < squares.GetLength(0); x++)
				{
					int squareValue = y * squares.GetLength(1) + x < mineCount ? -1 : 0;
					Square sq = new Square(new Point(x * squareSize, y * squareSize), squareValue);

					gameFrame.DrawSquare(sq);
					squares[x, y] = sq;
				}
			}
		}

		public void OpenSquare(Point point)
		{
			Square sq = squares[point.X / squareSize, point.Y / squareSize];
			if(sq.IsClosed())
			{
				sq.OpenSquare();
				
				if(sq.Value == 0)
					ExpandSquares(sq);
				
				gameFrame.DrawSquare(sq);
			}
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

		public void ExpandSquares(Square sq)
		{
			List<Square> lstAroundSquare = sq.GetAroundSquare(squares, false, true);
			foreach(Square sqAround in lstAroundSquare)
			{
				sqAround.OpenSquare();
				gameFrame.DrawSquare(sqAround);
				if(sqAround.Value == 0)
					ExpandSquares(sqAround);
			}
		}

		public void OpenAroundSquare(Point point)
		{
			if(point.X > 0 && point.Y > 0)
			{
				Square sq = squares[point.X / squareSize, point.Y / squareSize];
				
				if(sq.Value - sq.GetAroundFlagCount(squares) == 0)
					ExpandSquares(sq);
			}
		}
		
		//TODO: the first should always be 0
		public void KnuthShuffleMine()
		{
			Random ran = new Random();
			
			for(int y = squares.GetLength(1) - 1; y >= 0; y--)
			{
				for(int x = squares.GetLength(0)- 1; x >= 0; x--)
				{
					int ranX = ran.Next(x);
					int ranY = ran.Next(y);
					int value = squares[x, y].Value;
					squares[x, y].Value = squares[ranX, ranY].Value;
					squares[ranX, ranY].Value = value;
					gameFrame.DrawSquare(squares[x, y]);
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


	}

	public enum GameLevel
	{
		Beginner = 0,
		Intermediate,
		Expert,
		Custome
	}
}

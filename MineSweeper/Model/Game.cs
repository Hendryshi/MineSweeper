using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace MineSweeper.Model
{
	class Game
	{
		private Square[,] squares;
		private Frame gameFrame;


		private readonly GameLevel level;
		private readonly int mineCount;
		private readonly Size gameOffsetSize;

		public Game(Point gameOffsetPosition)
		{
			//this.gameOffsetSize = gameOffsetPosition;
			squares = new Square[9, 9];
			level = GameLevel.Beginner;
			mineCount = 10;
			gameFrame = new Frame(gameOffsetPosition, new Size(9, 9));
		}

		public Frame GameFrame { get => gameFrame; }
	}

	public enum GameLevel
	{
		Beginner = 0,
		Intermediate,
		Expert,
		Customize
	}
}

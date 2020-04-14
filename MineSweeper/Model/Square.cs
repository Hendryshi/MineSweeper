using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Configuration;

namespace MineSweeper.Model
{
	

	class Square
	{
		private int value;	//mine = -1
		private Point location;
		private MineStatus status;

		private readonly int squareSize = Int16.Parse(ConfigurationManager.AppSettings["squareSize"]);

		public const int MaxSquareNum = 8;
		
		public Square(Point location)
		{
			this.location = location;
			this.value = 0;
			this.status = MineStatus.Closed;
		}

		public Square(Point location, int value)
		{
			this.location = location;
			this.value = value;
		}

		public Square(Square sq)
		{
			this.location = sq.Location;
			this.value = sq.Value;
			this.status = sq.Status;
		}

		public bool IsMine()
		{
			return this.value == -1;
		}

		public bool IsClosed()
		{
			return this.status == MineStatus.Closed || this.status == MineStatus.Flagged;
		}

		public void OpenSquare()
		{
			if(IsMine())
			{
				if(this.status == MineStatus.Closed)
					this.status = MineStatus.HitMine;
			}
			else if(this.status == MineStatus.Flagged)
				this.status = MineStatus.ErrorMine;
			else
				this.status = MineStatus.OpenedNumber;
		}

		public void AddRemoveFlag()
		{
			if(this.status == MineStatus.Flagged)
				this.status = MineStatus.Closed;
			else if(this.status == MineStatus.Closed)
				this.status = MineStatus.Flagged;
		}


		public List<Square> GetAroundSquare(Square[,] squares, bool excludeMine = false, bool excludeOpen = false)
		{
			var query = from Square sq in squares 
						where sq.location.X >= this.location.X - squareSize && sq.location.X <= this.location.X + squareSize
						&& sq.location.Y >= this.location.Y - squareSize && sq.location.Y <= this.location.Y + squareSize
						&& sq.location != this.location
						select sq;
			if(excludeMine)
				query = query.ToList().Where(sq => !sq.IsMine());

			if(excludeOpen)
				query = query.ToList().Where(sq => sq.IsClosed());

			return query.ToList();
		}

		public int GetAroundFlagCount(Square[,] squares)
		{
			var query = from Square sq in squares
						where sq.location.X >= this.location.X - squareSize && sq.location.X <= this.location.X + squareSize
						&& sq.location.Y >= this.location.Y - squareSize && sq.location.Y <= this.location.Y + squareSize
						&& sq.location != this.location && sq.status == MineStatus.Flagged
						select sq;

			return query.Count();
		}

		public Point Location { get => location; }
		public int Value { get => value; set { this.value = value; } }
		public MineStatus Status { get => status; set { status = value; } }
	}

	public enum MineStatus
	{
		Closed = 0,
		Flagged = 20,
		HitMine = 60,
		ErrorMine = 80,
		OpenedMine = 100,
		OpenedNumber = 140,
	}
}

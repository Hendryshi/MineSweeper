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
		private bool leftDown = false;
		private bool rightDown = false;
		int test = 0;

		public mainForm()
		{
			InitializeComponent();
		}

		private void mainForm_Paint(object sender, PaintEventArgs e)
		{
			//game.Draw(this.CreateGraphics(), pnlInfo.CreateGraphics(), pnlMine.CreateGraphics());
			Size a = ClientRectangle.Size;
			this.CreateGraphics().DrawImage(game.GameFrame.DrawMainFrame(), game.GameFrame.RctGameField.Location);
			pnlMine.CreateGraphics().DrawImage(game.GameFrame.MineFrame, ClientRectangle.Location);
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

		private void pnlMine_MouseClick(object sender, MouseEventArgs e)
		{
			if(e.Button == MouseButtons.Left && (!leftDown || !rightDown) && game.InGameSize(e.Location))
			{
				if(!game.IsStart)
				{
					game.KnuthShuffleMine(e.Location);
					game.IsStart = true;
					//game.OpenAllSquares();
					//MessageBox.Show(game.getMineCount().ToString());
				}
				game.OpenSquare(e.Location);
				pnlMine.CreateGraphics().DrawImage(game.GameFrame.MineFrame, ClientRectangle.Location);
			}
		}

		private void pnlMine_MouseDown(object sender, MouseEventArgs e)
		{
			switch(e.Button)
			{
				case MouseButtons.Left:
					leftDown = true;
					break;
				case MouseButtons.Right:
					rightDown = true;
					if(!leftDown)
						game.AddRemoveFlag(e.Location);
					break;			
			}

			if(leftDown && rightDown && game.InGameSize(e.Location))
				game.SetSquaresDown(e.Location, true);

			pnlMine.CreateGraphics().DrawImage(game.GameFrame.MineFrame, ClientRectangle.Location);
		}

		private void pnlMine_MouseUp(object sender, MouseEventArgs e)
		{
			if(leftDown && rightDown && game.InGameSize(e.Location))
				game.OpenAroundSquares(e.Location);

			switch(e.Button)
			{
				case MouseButtons.Left:
					leftDown = false;
					break;
				case MouseButtons.Right:
					rightDown = false;
					//game.AddRemoveFlag(e.Location);
					break;
			}
			game.SetAllSquaresUp();
			pnlMine.CreateGraphics().DrawImage(game.GameFrame.MineFrame, ClientRectangle.Location);
		}

		private void pnlMine_MouseMove(object sender, MouseEventArgs e)
		{
			if(game.InGameSize(e.Location))
			{
				if(leftDown)
				{
					game.SetSquaresDown(e.Location, rightDown);
					pnlMine.CreateGraphics().DrawImage(game.GameFrame.MineFrame, ClientRectangle.Location);
				}
			}
		}

		private void pnlMine_MouseEnter(object sender, EventArgs e)
		{

		}

		private void pnlMine_MouseLeave(object sender, EventArgs e)
		{

		}
	}
}


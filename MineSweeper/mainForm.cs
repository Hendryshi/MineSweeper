﻿using System;
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
			if(game.IsStart)
			{
				game.KnuthShuffleMine();
				game.IsStart = false;
			}

			switch(e.Button)
			{
				case MouseButtons.Left:
					game.OpenSquare(e.Location);
					break;
				//case MouseButtons.Right:
				//	game.AddRemoveFlag(e.Location);
				//	break;
			}
			pnlMine.CreateGraphics().DrawImage(game.GameFrame.MineFrame, ClientRectangle.Location);
		}

		private void pnlMine_MouseDown(object sender, MouseEventArgs e)
		{
			switch(e.Button)
			{
				case MouseButtons.Right:
					game.AddRemoveFlag(e.Location);
					break;
			}
			pnlMine.CreateGraphics().DrawImage(game.GameFrame.MineFrame, ClientRectangle.Location);
		}
	}
}


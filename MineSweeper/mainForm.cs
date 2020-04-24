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
using System.Threading;


namespace MineSweeper
{
	public partial class mainForm : Form
	{
		private Game game;
		private System.Threading.Timer threadTimer;
		private bool leftDown = false;
		private bool rightDown = false;
		private GameLevel level = (GameLevel)Properties.Settings.Default["Level"];

		public mainForm()
		{
			InitializeComponent();
		}

		public delegate void MyInvoke();

		private void mainForm_Paint(object sender, PaintEventArgs e)
		{
			this.CreateGraphics().DrawImage(game.GameFrame.MainFrame, game.GameFrame.RctGameField.Location);
			pnlMine.CreateGraphics().DrawImage(game.GameFrame.MineFrame, ClientRectangle.Location);
			pnlInfo.CreateGraphics().DrawImage(game.GameFrame.InfoFrame, ClientRectangle.Location);
			pnlTimer.CreateGraphics().DrawImage(game.GameFrame.TimerFrame, ClientRectangle.Location);
		}

		private void mainForm_Load(object sender, EventArgs e)
		{
			newGame(level);
		}

		private void newGame(GameLevel level)
		{
			Point gameOffsetPosition = new Point(0, this.mainMenuStrip.Height);
			this.level = level;
			game = new Game(gameOffsetPosition, level);

			if(threadTimer != null)
				threadTimer.Dispose();

			threadTimer = new System.Threading.Timer(new TimerCallback(ChangeTime), null, Timeout.Infinite, 1000);

			leftDown = false;
			rightDown = false;

			pnlInfo.Location = game.GameFrame.RctPnlInfo.Location;
			pnlInfo.Size = game.GameFrame.RctPnlInfo.Size;

			pnlTimer.Location = game.GameFrame.RctPnlTimer.Location;
			pnlTimer.Size = game.GameFrame.RctPnlTimer.Size;

			pnlMine.Location = game.GameFrame.RctPnlMine.Location;
			pnlMine.Size = game.GameFrame.RctPnlMine.Size;

			ClientSize = new Size(game.GameFrame.RctGameField.Width + gameOffsetPosition.X, game.GameFrame.RctGameField.Height + gameOffsetPosition.Y);
			this.FormBorderStyle = FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.CenterToScreen();


			this.CreateGraphics().DrawImage(game.GameFrame.MainFrame, game.GameFrame.RctGameField.Location);
			pnlMine.CreateGraphics().DrawImage(game.GameFrame.MineFrame, ClientRectangle.Location);
			pnlInfo.CreateGraphics().DrawImage(game.GameFrame.InfoFrame, ClientRectangle.Location);
			pnlTimer.CreateGraphics().DrawImage(game.GameFrame.TimerFrame, ClientRectangle.Location);
		}

		//TODO
		private void ChangeTime(object value)
		{
			if(!game.Result.HasValue)
			{
				game.ChangeTime();
				pnlTimer.CreateGraphics().DrawImage(game.GameFrame.TimerFrame, ClientRectangle.Location);
			}
			else
			{
				threadTimer.Dispose();
				if(game.Result == true && game.CheckBreakRecord())
				{
					MyInvoke mi = new MyInvoke(SetNewRecords);
					BeginInvoke(mi);
				}	
			}
		}

		private void SetNewRecords()
		{
			if(level == GameLevel.Beginner)
			{
				this.rankBegItem.Text = "Beginner: " + game.TimeRecord;
				Properties.Settings.Default["BegRecord"] = game.TimeRecord;
			}
			else if(level == GameLevel.Intermediate)
			{
				this.rankInterItem.Text = "Intermediate: " + game.TimeRecord;
				Properties.Settings.Default["InterRecord"] = game.TimeRecord;
			}
			else if(level == GameLevel.Expert)
			{
				this.rankExpertItem.Text = "Expert: " + game.TimeRecord;
				Properties.Settings.Default["ExpertRecord"] = game.TimeRecord;
			}
			Properties.Settings.Default.Save();
			MessageBox.Show("You have break a new records: " + game.TimeRecord);
		}

		private void RefreshFrame()
		{
			pnlMine.CreateGraphics().DrawImage(game.GameFrame.MineFrame, ClientRectangle.Location);
			pnlInfo.CreateGraphics().DrawImage(game.GameFrame.InfoFrame, ClientRectangle.Location);
		}

		private void pnlMine_MouseClick(object sender, MouseEventArgs e)
		{
			if(e.Button == MouseButtons.Left && (!leftDown || !rightDown) && game.InGameSize(e.Location) && !game.Result.HasValue)
			{
				if(!game.IsStart)
				{
					game.StartGame(e.Location);
					threadTimer.Change(0, 1000);
				}

				game.OpenSingleSquare(e.Location);
				RefreshFrame();
			}
		}

		private void pnlMine_MouseDown(object sender, MouseEventArgs e)
		{
			if(!game.Result.HasValue)
			{
				switch(e.Button)
				{
					case MouseButtons.Left:
						leftDown = true;
						game.SetSquaresDown(e.Location, rightDown);
						game.ChangeFace(GameFace.MouthOpen);
						break;
					case MouseButtons.Right:
						rightDown = true;
						if(!leftDown)
							game.AddRemoveFlag(e.Location);
						break;
				}

				if(leftDown && rightDown && game.InGameSize(e.Location))
					game.SetSquaresDown(e.Location, true);

				RefreshFrame();
			}
		}

		private void pnlMine_MouseUp(object sender, MouseEventArgs e)
		{
			if(!game.Result.HasValue)
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
						break;
				}

				game.SetAllSquaresUp();
				game.ChangeFace(GameFace.SmileUp);
				RefreshFrame();
			}
		}

		private void pnlMine_MouseMove(object sender, MouseEventArgs e)
		{
			if(game.InGameSize(e.Location) && !game.Result.HasValue)
			{
				if(leftDown)
				{
					game.SetSquaresDown(e.Location, rightDown);
					RefreshFrame();
				}
			}
		}

		private void pnlInfo_MouseClick(object sender, MouseEventArgs e)
		{
			newGame(level);
		}

		private void newGameNToolStripMenuItem_Click(object sender, EventArgs e)
		{
			newGame(level);
		}

		private void beginnerToolStripMenuItem_Click(object sender, EventArgs e)
		{
			newGame(GameLevel.Beginner);
			Properties.Settings.Default["Level"] = (int)GameLevel.Beginner;
			Properties.Settings.Default.Save();
		}

		private void begToolStripMenuItem_Click(object sender, EventArgs e)
		{
			newGame(GameLevel.Intermediate);
			Properties.Settings.Default["Level"] = (int)GameLevel.Intermediate;
			Properties.Settings.Default.Save();
		}

		private void expertEToolStripMenuItem_Click(object sender, EventArgs e)
		{
			newGame(GameLevel.Expert);
			Properties.Settings.Default["Level"] = (int)GameLevel.Expert;
			Properties.Settings.Default.Save();
		}
	}
}


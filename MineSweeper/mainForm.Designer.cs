using System.Drawing;

namespace MineSweeper
{
    partial class mainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if(disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			this.mainMenuStrip = new System.Windows.Forms.MenuStrip();
			this.gameGToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.newGameNToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
			this.beginnerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.begToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.expertEToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.pnlInfo = new System.Windows.Forms.Panel();
			this.pnlTimer = new System.Windows.Forms.Panel();
			this.pnlMine = new System.Windows.Forms.Panel();
			this.rankRToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.rankBegItem = new System.Windows.Forms.ToolStripMenuItem();
			this.rankInterItem = new System.Windows.Forms.ToolStripMenuItem();
			this.rankExpertItem = new System.Windows.Forms.ToolStripMenuItem();
			this.mainMenuStrip.SuspendLayout();
			this.pnlInfo.SuspendLayout();
			this.SuspendLayout();
			// 
			// mainMenuStrip
			// 
			this.mainMenuStrip.BackColor = System.Drawing.SystemColors.InactiveCaption;
			this.mainMenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gameGToolStripMenuItem,
            this.rankRToolStripMenuItem});
			this.mainMenuStrip.Location = new System.Drawing.Point(0, 0);
			this.mainMenuStrip.Name = "mainMenuStrip";
			this.mainMenuStrip.Size = new System.Drawing.Size(1182, 28);
			this.mainMenuStrip.TabIndex = 0;
			this.mainMenuStrip.Text = "menuStrip1";
			// 
			// gameGToolStripMenuItem
			// 
			this.gameGToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newGameNToolStripMenuItem,
            this.toolStripMenuItem1,
            this.beginnerToolStripMenuItem,
            this.begToolStripMenuItem,
            this.expertEToolStripMenuItem});
			this.gameGToolStripMenuItem.Name = "gameGToolStripMenuItem";
			this.gameGToolStripMenuItem.Size = new System.Drawing.Size(86, 24);
			this.gameGToolStripMenuItem.Text = "Game(&G)";
			// 
			// newGameNToolStripMenuItem
			// 
			this.newGameNToolStripMenuItem.Name = "newGameNToolStripMenuItem";
			this.newGameNToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F2;
			this.newGameNToolStripMenuItem.ShowShortcutKeys = false;
			this.newGameNToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
			this.newGameNToolStripMenuItem.Text = "New Game(&N)";
			this.newGameNToolStripMenuItem.Click += new System.EventHandler(this.newGameNToolStripMenuItem_Click);
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(221, 6);
			// 
			// beginnerToolStripMenuItem
			// 
			this.beginnerToolStripMenuItem.Name = "beginnerToolStripMenuItem";
			this.beginnerToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
			this.beginnerToolStripMenuItem.Text = "Beginner(&B)";
			this.beginnerToolStripMenuItem.Click += new System.EventHandler(this.beginnerToolStripMenuItem_Click);
			// 
			// begToolStripMenuItem
			// 
			this.begToolStripMenuItem.Name = "begToolStripMenuItem";
			this.begToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
			this.begToolStripMenuItem.Text = "Intermediate(&I)";
			this.begToolStripMenuItem.Click += new System.EventHandler(this.begToolStripMenuItem_Click);
			// 
			// expertEToolStripMenuItem
			// 
			this.expertEToolStripMenuItem.Name = "expertEToolStripMenuItem";
			this.expertEToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
			this.expertEToolStripMenuItem.Text = "Expert(E)";
			this.expertEToolStripMenuItem.Click += new System.EventHandler(this.expertEToolStripMenuItem_Click);
			// 
			// pnlInfo
			// 
			this.pnlInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
			this.pnlInfo.Controls.Add(this.pnlTimer);
			this.pnlInfo.Location = new System.Drawing.Point(12, 41);
			this.pnlInfo.Name = "pnlInfo";
			this.pnlInfo.Size = new System.Drawing.Size(1158, 52);
			this.pnlInfo.TabIndex = 1;
			this.pnlInfo.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pnlInfo_MouseClick);
			// 
			// pnlTimer
			// 
			this.pnlTimer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
			this.pnlTimer.Location = new System.Drawing.Point(1071, 15);
			this.pnlTimer.Name = "pnlTimer";
			this.pnlTimer.Size = new System.Drawing.Size(71, 34);
			this.pnlTimer.TabIndex = 3;
			// 
			// pnlMine
			// 
			this.pnlMine.BackColor = System.Drawing.SystemColors.Info;
			this.pnlMine.Location = new System.Drawing.Point(12, 99);
			this.pnlMine.Name = "pnlMine";
			this.pnlMine.Size = new System.Drawing.Size(1158, 591);
			this.pnlMine.TabIndex = 2;
			this.pnlMine.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pnlMine_MouseClick);
			this.pnlMine.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnlMine_MouseDown);
			this.pnlMine.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnlMine_MouseMove);
			this.pnlMine.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pnlMine_MouseUp);
			// 
			// rankRToolStripMenuItem
			// 
			this.rankRToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.rankBegItem,
            this.rankInterItem,
            this.rankExpertItem});
			this.rankRToolStripMenuItem.Name = "rankRToolStripMenuItem";
			this.rankRToolStripMenuItem.Size = new System.Drawing.Size(78, 24);
			this.rankRToolStripMenuItem.Text = "Rank(&R)";
			// 
			// beginnerToolStripMenuItem1
			// 
			this.rankBegItem.Name = "rankBegItem";
			this.rankBegItem.Size = new System.Drawing.Size(224, 26);
			this.rankBegItem.Text = "Beginner: " + Properties.Settings.Default["BegRecord"];
			// 
			// intermediateToolStripMenuItem
			// 
			this.rankInterItem.Name = "rankInterItem";
			this.rankInterItem.Size = new System.Drawing.Size(224, 26);
			this.rankInterItem.Text = "Intermediate: " + Properties.Settings.Default["InterRecord"];
			// 
			// expertToolStripMenuItem
			// 
			this.rankExpertItem.Name = "rankExpertItem";
			this.rankExpertItem.Size = new System.Drawing.Size(224, 26);
			this.rankExpertItem.Text = "Expert: " + Properties.Settings.Default["ExpertRecord"];
			// 
			// mainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1182, 702);
			this.Controls.Add(this.pnlMine);
			this.Controls.Add(this.pnlInfo);
			this.Controls.Add(this.mainMenuStrip);
			this.MainMenuStrip = this.mainMenuStrip;
			this.Name = "mainForm";
			this.Text = "MineSweeper";
			this.Load += new System.EventHandler(this.mainForm_Load);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.mainForm_Paint);
			this.mainMenuStrip.ResumeLayout(false);
			this.mainMenuStrip.PerformLayout();
			this.pnlInfo.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem gameGToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newGameNToolStripMenuItem;
        private System.Windows.Forms.Panel pnlInfo;
        private System.Windows.Forms.Panel pnlMine;
		private System.Windows.Forms.Panel pnlTimer;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem beginnerToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem begToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem expertEToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem rankRToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem rankBegItem;
		private System.Windows.Forms.ToolStripMenuItem rankInterItem;
		private System.Windows.Forms.ToolStripMenuItem rankExpertItem;
	}
}


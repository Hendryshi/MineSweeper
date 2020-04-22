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
			this.pnlInfo = new System.Windows.Forms.Panel();
			this.pnlMine = new System.Windows.Forms.Panel();
			this.pnlTimer = new System.Windows.Forms.Panel();
			this.mainMenuStrip.SuspendLayout();
			this.pnlInfo.SuspendLayout();
			this.SuspendLayout();
			// 
			// mainMenuStrip
			// 
			this.mainMenuStrip.BackColor = System.Drawing.SystemColors.InactiveCaption;
			this.mainMenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gameGToolStripMenuItem});
			this.mainMenuStrip.Location = new System.Drawing.Point(0, 0);
			this.mainMenuStrip.Name = "mainMenuStrip";
			this.mainMenuStrip.Size = new System.Drawing.Size(1182, 28);
			this.mainMenuStrip.TabIndex = 0;
			this.mainMenuStrip.Text = "menuStrip1";
			// 
			// gameGToolStripMenuItem
			// 
			this.gameGToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newGameNToolStripMenuItem});
			this.gameGToolStripMenuItem.Name = "gameGToolStripMenuItem";
			this.gameGToolStripMenuItem.Size = new System.Drawing.Size(86, 24);
			this.gameGToolStripMenuItem.Text = "Game(&G)";
			// 
			// newGameNToolStripMenuItem
			// 
			this.newGameNToolStripMenuItem.Name = "newGameNToolStripMenuItem";
			this.newGameNToolStripMenuItem.Size = new System.Drawing.Size(193, 26);
			this.newGameNToolStripMenuItem.Text = "New Game(&N)";
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
			// pnlMine
			// 
			this.pnlMine.BackColor = System.Drawing.SystemColors.Info;
			this.pnlMine.Location = new System.Drawing.Point(12, 99);
			this.pnlMine.Name = "pnlMine";
			this.pnlMine.Size = new System.Drawing.Size(1158, 591);
			this.pnlMine.TabIndex = 2;
			this.pnlMine.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pnlMine_MouseClick);
			this.pnlMine.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnlMine_MouseDown);
			this.pnlMine.MouseEnter += new System.EventHandler(this.pnlMine_MouseEnter);
			this.pnlMine.MouseLeave += new System.EventHandler(this.pnlMine_MouseLeave);
			this.pnlMine.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnlMine_MouseMove);
			this.pnlMine.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pnlMine_MouseUp);
			// 
			// pnlTimer
			// 
			this.pnlTimer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
			this.pnlTimer.Location = new System.Drawing.Point(1071, 15);
			this.pnlTimer.Name = "pnlTimer";
			this.pnlTimer.Size = new System.Drawing.Size(71, 34);
			this.pnlTimer.TabIndex = 3;
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
	}
}


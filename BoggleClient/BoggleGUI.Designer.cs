namespace BoggleClient
{
    partial class BoggleGUI
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
            if (disposing && (components != null))
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
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.boggleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.joinNewGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cancelGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.howToPlayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.gamePanel = new System.Windows.Forms.Panel();
            this.cubeLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.box16 = new System.Windows.Forms.Button();
            this.box15 = new System.Windows.Forms.Button();
            this.box14 = new System.Windows.Forms.Button();
            this.box13 = new System.Windows.Forms.Button();
            this.box12 = new System.Windows.Forms.Button();
            this.box11 = new System.Windows.Forms.Button();
            this.box10 = new System.Windows.Forms.Button();
            this.box9 = new System.Windows.Forms.Button();
            this.box8 = new System.Windows.Forms.Button();
            this.box7 = new System.Windows.Forms.Button();
            this.box6 = new System.Windows.Forms.Button();
            this.box5 = new System.Windows.Forms.Button();
            this.box4 = new System.Windows.Forms.Button();
            this.box3 = new System.Windows.Forms.Button();
            this.box2 = new System.Windows.Forms.Button();
            this.box1 = new System.Windows.Forms.Button();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.startPanel = new System.Windows.Forms.Panel();
            this.menuStrip1.SuspendLayout();
            this.gamePanel.SuspendLayout();
            this.cubeLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.boggleToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(584, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // boggleToolStripMenuItem
            // 
            this.boggleToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.joinNewGameToolStripMenuItem,
            this.cancelGameToolStripMenuItem,
            this.quitToolStripMenuItem});
            this.boggleToolStripMenuItem.Name = "boggleToolStripMenuItem";
            this.boggleToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
            this.boggleToolStripMenuItem.Text = "Boggle";
            // 
            // joinNewGameToolStripMenuItem
            // 
            this.joinNewGameToolStripMenuItem.Name = "joinNewGameToolStripMenuItem";
            this.joinNewGameToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.joinNewGameToolStripMenuItem.Text = "Join new game";
            // 
            // cancelGameToolStripMenuItem
            // 
            this.cancelGameToolStripMenuItem.Name = "cancelGameToolStripMenuItem";
            this.cancelGameToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.cancelGameToolStripMenuItem.Text = "Cancel game";
            // 
            // quitToolStripMenuItem
            // 
            this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            this.quitToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.quitToolStripMenuItem.Text = "Quit";
            this.quitToolStripMenuItem.Click += new System.EventHandler(this.quitToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.howToPlayToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // howToPlayToolStripMenuItem
            // 
            this.howToPlayToolStripMenuItem.Name = "howToPlayToolStripMenuItem";
            this.howToPlayToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.howToPlayToolStripMenuItem.Text = "How to play...";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.aboutToolStripMenuItem.Text = "About";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 440);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(584, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // gamePanel
            // 
            this.gamePanel.Controls.Add(this.cubeLayoutPanel);
            this.gamePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gamePanel.Location = new System.Drawing.Point(0, 0);
            this.gamePanel.Name = "gamePanel";
            this.gamePanel.Size = new System.Drawing.Size(584, 462);
            this.gamePanel.TabIndex = 2;
            // 
            // cubeLayoutPanel
            // 
            this.cubeLayoutPanel.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Outset;
            this.cubeLayoutPanel.ColumnCount = 4;
            this.cubeLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.cubeLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.cubeLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.cubeLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.cubeLayoutPanel.Controls.Add(this.box16, 3, 3);
            this.cubeLayoutPanel.Controls.Add(this.box15, 2, 3);
            this.cubeLayoutPanel.Controls.Add(this.box14, 1, 3);
            this.cubeLayoutPanel.Controls.Add(this.box13, 0, 3);
            this.cubeLayoutPanel.Controls.Add(this.box12, 3, 2);
            this.cubeLayoutPanel.Controls.Add(this.box11, 2, 2);
            this.cubeLayoutPanel.Controls.Add(this.box10, 1, 2);
            this.cubeLayoutPanel.Controls.Add(this.box9, 0, 2);
            this.cubeLayoutPanel.Controls.Add(this.box8, 3, 1);
            this.cubeLayoutPanel.Controls.Add(this.box7, 2, 1);
            this.cubeLayoutPanel.Controls.Add(this.box6, 1, 1);
            this.cubeLayoutPanel.Controls.Add(this.box5, 0, 1);
            this.cubeLayoutPanel.Controls.Add(this.box4, 3, 0);
            this.cubeLayoutPanel.Controls.Add(this.box3, 2, 0);
            this.cubeLayoutPanel.Controls.Add(this.box2, 1, 0);
            this.cubeLayoutPanel.Controls.Add(this.box1, 0, 0);
            this.cubeLayoutPanel.Cursor = System.Windows.Forms.Cursors.Default;
            this.cubeLayoutPanel.Location = new System.Drawing.Point(3, 27);
            this.cubeLayoutPanel.Name = "cubeLayoutPanel";
            this.cubeLayoutPanel.RowCount = 4;
            this.cubeLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.cubeLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.cubeLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.cubeLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.cubeLayoutPanel.Size = new System.Drawing.Size(410, 410);
            this.cubeLayoutPanel.TabIndex = 0;
            // 
            // box16
            // 
            this.box16.Cursor = System.Windows.Forms.Cursors.Hand;
            this.box16.Location = new System.Drawing.Point(311, 311);
            this.box16.Name = "box16";
            this.box16.Size = new System.Drawing.Size(94, 94);
            this.box16.TabIndex = 19;
            this.box16.Text = "box16";
            this.box16.UseVisualStyleBackColor = true;
            // 
            // box15
            // 
            this.box15.Cursor = System.Windows.Forms.Cursors.Hand;
            this.box15.Location = new System.Drawing.Point(209, 311);
            this.box15.Name = "box15";
            this.box15.Size = new System.Drawing.Size(94, 94);
            this.box15.TabIndex = 18;
            this.box15.Text = "box15";
            this.box15.UseVisualStyleBackColor = true;
            // 
            // box14
            // 
            this.box14.Cursor = System.Windows.Forms.Cursors.Hand;
            this.box14.Location = new System.Drawing.Point(107, 311);
            this.box14.Name = "box14";
            this.box14.Size = new System.Drawing.Size(94, 94);
            this.box14.TabIndex = 17;
            this.box14.Text = "box14";
            this.box14.UseVisualStyleBackColor = true;
            // 
            // box13
            // 
            this.box13.Cursor = System.Windows.Forms.Cursors.Hand;
            this.box13.Location = new System.Drawing.Point(5, 311);
            this.box13.Name = "box13";
            this.box13.Size = new System.Drawing.Size(94, 94);
            this.box13.TabIndex = 16;
            this.box13.Text = "box13";
            this.box13.UseVisualStyleBackColor = true;
            // 
            // box12
            // 
            this.box12.Cursor = System.Windows.Forms.Cursors.Hand;
            this.box12.Location = new System.Drawing.Point(311, 209);
            this.box12.Name = "box12";
            this.box12.Size = new System.Drawing.Size(94, 94);
            this.box12.TabIndex = 15;
            this.box12.Text = "box12";
            this.box12.UseVisualStyleBackColor = true;
            // 
            // box11
            // 
            this.box11.Cursor = System.Windows.Forms.Cursors.Hand;
            this.box11.Location = new System.Drawing.Point(209, 209);
            this.box11.Name = "box11";
            this.box11.Size = new System.Drawing.Size(94, 94);
            this.box11.TabIndex = 14;
            this.box11.Text = "box11";
            this.box11.UseVisualStyleBackColor = true;
            // 
            // box10
            // 
            this.box10.Cursor = System.Windows.Forms.Cursors.Hand;
            this.box10.Location = new System.Drawing.Point(107, 209);
            this.box10.Name = "box10";
            this.box10.Size = new System.Drawing.Size(94, 94);
            this.box10.TabIndex = 13;
            this.box10.Text = "box10";
            this.box10.UseVisualStyleBackColor = true;
            // 
            // box9
            // 
            this.box9.Cursor = System.Windows.Forms.Cursors.Hand;
            this.box9.Location = new System.Drawing.Point(5, 209);
            this.box9.Name = "box9";
            this.box9.Size = new System.Drawing.Size(94, 94);
            this.box9.TabIndex = 12;
            this.box9.Text = "box9";
            this.box9.UseVisualStyleBackColor = true;
            // 
            // box8
            // 
            this.box8.Cursor = System.Windows.Forms.Cursors.Hand;
            this.box8.Location = new System.Drawing.Point(311, 107);
            this.box8.Name = "box8";
            this.box8.Size = new System.Drawing.Size(94, 94);
            this.box8.TabIndex = 11;
            this.box8.Text = "box8";
            this.box8.UseVisualStyleBackColor = true;
            // 
            // box7
            // 
            this.box7.Cursor = System.Windows.Forms.Cursors.Hand;
            this.box7.Location = new System.Drawing.Point(209, 107);
            this.box7.Name = "box7";
            this.box7.Size = new System.Drawing.Size(94, 94);
            this.box7.TabIndex = 10;
            this.box7.Text = "box7";
            this.box7.UseVisualStyleBackColor = true;
            // 
            // box6
            // 
            this.box6.Cursor = System.Windows.Forms.Cursors.Hand;
            this.box6.Location = new System.Drawing.Point(107, 107);
            this.box6.Name = "box6";
            this.box6.Size = new System.Drawing.Size(94, 94);
            this.box6.TabIndex = 9;
            this.box6.Text = "box6";
            this.box6.UseVisualStyleBackColor = true;
            // 
            // box5
            // 
            this.box5.Cursor = System.Windows.Forms.Cursors.Hand;
            this.box5.Location = new System.Drawing.Point(5, 107);
            this.box5.Name = "box5";
            this.box5.Size = new System.Drawing.Size(94, 94);
            this.box5.TabIndex = 8;
            this.box5.Text = "box5";
            this.box5.UseVisualStyleBackColor = true;
            // 
            // box4
            // 
            this.box4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.box4.Location = new System.Drawing.Point(311, 5);
            this.box4.Name = "box4";
            this.box4.Size = new System.Drawing.Size(94, 94);
            this.box4.TabIndex = 7;
            this.box4.Text = "box4";
            this.box4.UseVisualStyleBackColor = true;
            // 
            // box3
            // 
            this.box3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.box3.Location = new System.Drawing.Point(209, 5);
            this.box3.Name = "box3";
            this.box3.Size = new System.Drawing.Size(94, 94);
            this.box3.TabIndex = 6;
            this.box3.Text = "box3";
            this.box3.UseVisualStyleBackColor = true;
            // 
            // box2
            // 
            this.box2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.box2.Location = new System.Drawing.Point(107, 5);
            this.box2.Name = "box2";
            this.box2.Size = new System.Drawing.Size(94, 94);
            this.box2.TabIndex = 5;
            this.box2.Text = "box2";
            this.box2.UseVisualStyleBackColor = true;
            // 
            // box1
            // 
            this.box1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.box1.Location = new System.Drawing.Point(5, 5);
            this.box1.Name = "box1";
            this.box1.Size = new System.Drawing.Size(94, 94);
            this.box1.TabIndex = 4;
            this.box1.Text = "box1";
            this.box1.UseVisualStyleBackColor = true;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // startPanel
            // 
            this.startPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.startPanel.Location = new System.Drawing.Point(0, 0);
            this.startPanel.Name = "startPanel";
            this.startPanel.Size = new System.Drawing.Size(584, 462);
            this.startPanel.TabIndex = 1;
            this.startPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.startPanel_Paint);
            // 
            // BoggleGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 462);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.startPanel);
            this.Controls.Add(this.gamePanel);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "BoggleGUI";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.BoggleGUI_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.gamePanel.ResumeLayout(false);
            this.cubeLayoutPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripMenuItem boggleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem howToPlayToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem joinNewGameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cancelGameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quitToolStripMenuItem;
        private System.Windows.Forms.Panel gamePanel;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.TableLayoutPanel cubeLayoutPanel;
        private System.Windows.Forms.Button box16;
        private System.Windows.Forms.Button box15;
        private System.Windows.Forms.Button box14;
        private System.Windows.Forms.Button box13;
        private System.Windows.Forms.Button box12;
        private System.Windows.Forms.Button box11;
        private System.Windows.Forms.Button box10;
        private System.Windows.Forms.Button box9;
        private System.Windows.Forms.Button box8;
        private System.Windows.Forms.Button box7;
        private System.Windows.Forms.Button box6;
        private System.Windows.Forms.Button box5;
        private System.Windows.Forms.Button box4;
        private System.Windows.Forms.Button box3;
        private System.Windows.Forms.Button box2;
        private System.Windows.Forms.Button box1;
        private System.Windows.Forms.Panel startPanel;
    }
}


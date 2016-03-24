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
            this.domainToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setNicknameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.howToPlayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.gamePanel = new System.Windows.Forms.Panel();
            this.wordEntry = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.wordBox = new System.Windows.Forms.RichTextBox();
            this.wordCountBox = new System.Windows.Forms.TextBox();
            this.ScoreCountBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
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
            this.player1NameLabel = new System.Windows.Forms.Label();
            this.player1ScoreLabel = new System.Windows.Forms.Label();
            this.player2ScoreLabel = new System.Windows.Forms.Label();
            this.player2NameLabel = new System.Windows.Forms.Label();
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
            this.quitToolStripMenuItem,
            this.domainToolStripMenuItem,
            this.setNicknameToolStripMenuItem});
            this.boggleToolStripMenuItem.Name = "boggleToolStripMenuItem";
            this.boggleToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
            this.boggleToolStripMenuItem.Text = "Boggle";
            // 
            // joinNewGameToolStripMenuItem
            // 
            this.joinNewGameToolStripMenuItem.Name = "joinNewGameToolStripMenuItem";
            this.joinNewGameToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.joinNewGameToolStripMenuItem.Text = "Join new game";
            this.joinNewGameToolStripMenuItem.Click += new System.EventHandler(this.joinNewGameToolStripMenuItem_Click);
            // 
            // cancelGameToolStripMenuItem
            // 
            this.cancelGameToolStripMenuItem.Name = "cancelGameToolStripMenuItem";
            this.cancelGameToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.cancelGameToolStripMenuItem.Text = "Cancel game";
            this.cancelGameToolStripMenuItem.Click += new System.EventHandler(this.cancelGameToolStripMenuItem_Click);
            // 
            // quitToolStripMenuItem
            // 
            this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            this.quitToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.quitToolStripMenuItem.Text = "Quit";
            this.quitToolStripMenuItem.Click += new System.EventHandler(this.quitToolStripMenuItem_Click);
            // 
            // domainToolStripMenuItem
            // 
            this.domainToolStripMenuItem.Name = "domainToolStripMenuItem";
            this.domainToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.domainToolStripMenuItem.Text = "Domain";
            this.domainToolStripMenuItem.Click += new System.EventHandler(this.domainToolStripMenuItem_Click);
            // 
            // setNicknameToolStripMenuItem
            // 
            this.setNicknameToolStripMenuItem.Name = "setNicknameToolStripMenuItem";
            this.setNicknameToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.setNicknameToolStripMenuItem.Text = "Set Nickname";
            this.setNicknameToolStripMenuItem.Click += new System.EventHandler(this.setNicknameToolStripMenuItem_Click);
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
            this.howToPlayToolStripMenuItem.Click += new System.EventHandler(this.howToPlayToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.aboutToolStripMenuItem.Text = "About";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 480);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(584, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // gamePanel
            // 
            this.gamePanel.Controls.Add(this.player2ScoreLabel);
            this.gamePanel.Controls.Add(this.player2NameLabel);
            this.gamePanel.Controls.Add(this.player1ScoreLabel);
            this.gamePanel.Controls.Add(this.player1NameLabel);
            this.gamePanel.Controls.Add(this.wordEntry);
            this.gamePanel.Controls.Add(this.textBox3);
            this.gamePanel.Controls.Add(this.label4);
            this.gamePanel.Controls.Add(this.wordBox);
            this.gamePanel.Controls.Add(this.wordCountBox);
            this.gamePanel.Controls.Add(this.ScoreCountBox);
            this.gamePanel.Controls.Add(this.label3);
            this.gamePanel.Controls.Add(this.label2);
            this.gamePanel.Controls.Add(this.label1);
            this.gamePanel.Controls.Add(this.cubeLayoutPanel);
            this.gamePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gamePanel.Location = new System.Drawing.Point(0, 0);
            this.gamePanel.Name = "gamePanel";
            this.gamePanel.Size = new System.Drawing.Size(584, 502);
            this.gamePanel.TabIndex = 2;
            // 
            // wordEntry
            // 
            this.wordEntry.Location = new System.Drawing.Point(17, 398);
            this.wordEntry.Name = "wordEntry";
            this.wordEntry.Size = new System.Drawing.Size(245, 20);
            this.wordEntry.TabIndex = 9;
            this.wordEntry.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.wordEntry_KeyPress);
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(424, 354);
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new System.Drawing.Size(107, 20);
            this.textBox3.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(424, 338);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(107, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Time remaining (sec):";
            // 
            // wordBox
            // 
            this.wordBox.Cursor = System.Windows.Forms.Cursors.No;
            this.wordBox.DetectUrls = false;
            this.wordBox.Location = new System.Drawing.Point(424, 103);
            this.wordBox.Name = "wordBox";
            this.wordBox.ReadOnly = true;
            this.wordBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.wordBox.Size = new System.Drawing.Size(139, 227);
            this.wordBox.TabIndex = 6;
            this.wordBox.Text = "";
            // 
            // wordCountBox
            // 
            this.wordCountBox.Location = new System.Drawing.Point(462, 77);
            this.wordCountBox.Name = "wordCountBox";
            this.wordCountBox.ReadOnly = true;
            this.wordCountBox.Size = new System.Drawing.Size(100, 20);
            this.wordCountBox.TabIndex = 5;
            // 
            // ScoreCountBox
            // 
            this.ScoreCountBox.Location = new System.Drawing.Point(462, 50);
            this.ScoreCountBox.Name = "ScoreCountBox";
            this.ScoreCountBox.ReadOnly = true;
            this.ScoreCountBox.Size = new System.Drawing.Size(100, 20);
            this.ScoreCountBox.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(420, 80);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Words:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(421, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Score:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.label1.Location = new System.Drawing.Point(420, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "My Words";
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
            this.cubeLayoutPanel.Location = new System.Drawing.Point(12, 142);
            this.cubeLayoutPanel.Name = "cubeLayoutPanel";
            this.cubeLayoutPanel.RowCount = 4;
            this.cubeLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.cubeLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.cubeLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.cubeLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.cubeLayoutPanel.Size = new System.Drawing.Size(250, 250);
            this.cubeLayoutPanel.TabIndex = 0;
            // 
            // box16
            // 
            this.box16.Cursor = System.Windows.Forms.Cursors.Hand;
            this.box16.Location = new System.Drawing.Point(191, 191);
            this.box16.Name = "box16";
            this.box16.Size = new System.Drawing.Size(54, 54);
            this.box16.TabIndex = 19;
            this.box16.Text = "box16";
            this.box16.UseVisualStyleBackColor = true;
            // 
            // box15
            // 
            this.box15.Cursor = System.Windows.Forms.Cursors.Hand;
            this.box15.Location = new System.Drawing.Point(129, 191);
            this.box15.Name = "box15";
            this.box15.Size = new System.Drawing.Size(54, 54);
            this.box15.TabIndex = 18;
            this.box15.Text = "box15";
            this.box15.UseVisualStyleBackColor = true;
            // 
            // box14
            // 
            this.box14.Cursor = System.Windows.Forms.Cursors.Hand;
            this.box14.Location = new System.Drawing.Point(67, 191);
            this.box14.Name = "box14";
            this.box14.Size = new System.Drawing.Size(54, 54);
            this.box14.TabIndex = 17;
            this.box14.Text = "box14";
            this.box14.UseVisualStyleBackColor = true;
            // 
            // box13
            // 
            this.box13.Cursor = System.Windows.Forms.Cursors.Hand;
            this.box13.Location = new System.Drawing.Point(5, 191);
            this.box13.Name = "box13";
            this.box13.Size = new System.Drawing.Size(54, 54);
            this.box13.TabIndex = 16;
            this.box13.Text = "box13";
            this.box13.UseVisualStyleBackColor = true;
            // 
            // box12
            // 
            this.box12.Cursor = System.Windows.Forms.Cursors.Hand;
            this.box12.Location = new System.Drawing.Point(191, 129);
            this.box12.Name = "box12";
            this.box12.Size = new System.Drawing.Size(54, 54);
            this.box12.TabIndex = 15;
            this.box12.Text = "box12";
            this.box12.UseVisualStyleBackColor = true;
            // 
            // box11
            // 
            this.box11.Cursor = System.Windows.Forms.Cursors.Hand;
            this.box11.Location = new System.Drawing.Point(129, 129);
            this.box11.Name = "box11";
            this.box11.Size = new System.Drawing.Size(54, 54);
            this.box11.TabIndex = 14;
            this.box11.Text = "box11";
            this.box11.UseVisualStyleBackColor = true;
            // 
            // box10
            // 
            this.box10.Cursor = System.Windows.Forms.Cursors.Hand;
            this.box10.Location = new System.Drawing.Point(67, 129);
            this.box10.Name = "box10";
            this.box10.Size = new System.Drawing.Size(54, 54);
            this.box10.TabIndex = 13;
            this.box10.Text = "box10";
            this.box10.UseVisualStyleBackColor = true;
            // 
            // box9
            // 
            this.box9.Cursor = System.Windows.Forms.Cursors.Hand;
            this.box9.Location = new System.Drawing.Point(5, 129);
            this.box9.Name = "box9";
            this.box9.Size = new System.Drawing.Size(54, 54);
            this.box9.TabIndex = 12;
            this.box9.Text = "box9";
            this.box9.UseVisualStyleBackColor = true;
            // 
            // box8
            // 
            this.box8.Cursor = System.Windows.Forms.Cursors.Hand;
            this.box8.Location = new System.Drawing.Point(191, 67);
            this.box8.Name = "box8";
            this.box8.Size = new System.Drawing.Size(54, 54);
            this.box8.TabIndex = 11;
            this.box8.Text = "box8";
            this.box8.UseVisualStyleBackColor = true;
            // 
            // box7
            // 
            this.box7.Cursor = System.Windows.Forms.Cursors.Hand;
            this.box7.Location = new System.Drawing.Point(129, 67);
            this.box7.Name = "box7";
            this.box7.Size = new System.Drawing.Size(54, 54);
            this.box7.TabIndex = 10;
            this.box7.Text = "box7";
            this.box7.UseVisualStyleBackColor = true;
            // 
            // box6
            // 
            this.box6.Cursor = System.Windows.Forms.Cursors.Hand;
            this.box6.Location = new System.Drawing.Point(67, 67);
            this.box6.Name = "box6";
            this.box6.Size = new System.Drawing.Size(54, 54);
            this.box6.TabIndex = 9;
            this.box6.Text = "box6";
            this.box6.UseVisualStyleBackColor = true;
            // 
            // box5
            // 
            this.box5.Cursor = System.Windows.Forms.Cursors.Hand;
            this.box5.Location = new System.Drawing.Point(5, 67);
            this.box5.Name = "box5";
            this.box5.Size = new System.Drawing.Size(54, 54);
            this.box5.TabIndex = 8;
            this.box5.Text = "box5";
            this.box5.UseVisualStyleBackColor = true;
            // 
            // box4
            // 
            this.box4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.box4.Location = new System.Drawing.Point(191, 5);
            this.box4.Name = "box4";
            this.box4.Size = new System.Drawing.Size(54, 54);
            this.box4.TabIndex = 7;
            this.box4.Text = "box4";
            this.box4.UseVisualStyleBackColor = true;
            // 
            // box3
            // 
            this.box3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.box3.Location = new System.Drawing.Point(129, 5);
            this.box3.Name = "box3";
            this.box3.Size = new System.Drawing.Size(54, 54);
            this.box3.TabIndex = 6;
            this.box3.Text = "box3";
            this.box3.UseVisualStyleBackColor = true;
            // 
            // box2
            // 
            this.box2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.box2.Location = new System.Drawing.Point(67, 5);
            this.box2.Name = "box2";
            this.box2.Size = new System.Drawing.Size(54, 54);
            this.box2.TabIndex = 5;
            this.box2.Text = "box2";
            this.box2.UseVisualStyleBackColor = true;
            // 
            // box1
            // 
            this.box1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.box1.Location = new System.Drawing.Point(5, 5);
            this.box1.Name = "box1";
            this.box1.Size = new System.Drawing.Size(54, 54);
            this.box1.TabIndex = 4;
            this.box1.Text = "box1";
            this.box1.UseVisualStyleBackColor = true;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // player1NameLabel
            // 
            this.player1NameLabel.AutoSize = true;
            this.player1NameLabel.Location = new System.Drawing.Point(14, 57);
            this.player1NameLabel.Name = "player1NameLabel";
            this.player1NameLabel.Size = new System.Drawing.Size(76, 13);
            this.player1NameLabel.TabIndex = 10;
            this.player1NameLabel.Text = "Player 1 Name";
            // 
            // player1ScoreLabel
            // 
            this.player1ScoreLabel.AutoSize = true;
            this.player1ScoreLabel.Location = new System.Drawing.Point(17, 74);
            this.player1ScoreLabel.Name = "player1ScoreLabel";
            this.player1ScoreLabel.Size = new System.Drawing.Size(13, 13);
            this.player1ScoreLabel.TabIndex = 11;
            this.player1ScoreLabel.Text = "0";
            // 
            // player2ScoreLabel
            // 
            this.player2ScoreLabel.AutoSize = true;
            this.player2ScoreLabel.Location = new System.Drawing.Point(155, 74);
            this.player2ScoreLabel.Name = "player2ScoreLabel";
            this.player2ScoreLabel.Size = new System.Drawing.Size(13, 13);
            this.player2ScoreLabel.TabIndex = 13;
            this.player2ScoreLabel.Text = "0";
            // 
            // player2NameLabel
            // 
            this.player2NameLabel.AutoSize = true;
            this.player2NameLabel.Location = new System.Drawing.Point(152, 57);
            this.player2NameLabel.Name = "player2NameLabel";
            this.player2NameLabel.Size = new System.Drawing.Size(76, 13);
            this.player2NameLabel.TabIndex = 12;
            this.player2NameLabel.Text = "Player 2 Name";
            // 
            // BoggleGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 502);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.gamePanel);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "BoggleGUI";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.BoggleGUI_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.gamePanel.ResumeLayout(false);
            this.gamePanel.PerformLayout();
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
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RichTextBox wordBox;
        private System.Windows.Forms.TextBox wordCountBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox ScoreCountBox;
        private System.Windows.Forms.ToolStripMenuItem setNicknameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem domainToolStripMenuItem;
        private System.Windows.Forms.TextBox wordEntry;
        private System.Windows.Forms.Label player2ScoreLabel;
        private System.Windows.Forms.Label player2NameLabel;
        private System.Windows.Forms.Label player1ScoreLabel;
        private System.Windows.Forms.Label player1NameLabel;
    }
}


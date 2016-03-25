namespace BoggleClient
{
    partial class EndForm
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
            this.Player1Label = new System.Windows.Forms.Label();
            this.Player2Label = new System.Windows.Forms.Label();
            this.Name1 = new System.Windows.Forms.Label();
            this.Name2 = new System.Windows.Forms.Label();
            this.Player1WordsPlayed = new System.Windows.Forms.Label();
            this.Player2WordsPlayed = new System.Windows.Forms.Label();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.Done = new System.Windows.Forms.Button();
            this.Score1Label = new System.Windows.Forms.Label();
            this.Score2Label = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // Player1Label
            // 
            this.Player1Label.AutoSize = true;
            this.Player1Label.Location = new System.Drawing.Point(12, 9);
            this.Player1Label.Name = "Player1Label";
            this.Player1Label.Size = new System.Drawing.Size(48, 13);
            this.Player1Label.TabIndex = 0;
            this.Player1Label.Text = "Player 1:";
            // 
            // Player2Label
            // 
            this.Player2Label.AutoSize = true;
            this.Player2Label.Location = new System.Drawing.Point(220, 9);
            this.Player2Label.Name = "Player2Label";
            this.Player2Label.Size = new System.Drawing.Size(48, 13);
            this.Player2Label.TabIndex = 1;
            this.Player2Label.Text = "Player 2:";
            // 
            // Name1
            // 
            this.Name1.AutoSize = true;
            this.Name1.Location = new System.Drawing.Point(66, 9);
            this.Name1.Name = "Name1";
            this.Name1.Size = new System.Drawing.Size(41, 13);
            this.Name1.TabIndex = 2;
            this.Name1.Text = "Name1";
            // 
            // Name2
            // 
            this.Name2.AutoSize = true;
            this.Name2.Location = new System.Drawing.Point(275, 9);
            this.Name2.Name = "Name2";
            this.Name2.Size = new System.Drawing.Size(41, 13);
            this.Name2.TabIndex = 3;
            this.Name2.Text = "Name2";
            // 
            // Player1WordsPlayed
            // 
            this.Player1WordsPlayed.AutoSize = true;
            this.Player1WordsPlayed.Location = new System.Drawing.Point(12, 43);
            this.Player1WordsPlayed.Name = "Player1WordsPlayed";
            this.Player1WordsPlayed.Size = new System.Drawing.Size(75, 13);
            this.Player1WordsPlayed.TabIndex = 4;
            this.Player1WordsPlayed.Text = "Words played:";
            // 
            // Player2WordsPlayed
            // 
            this.Player2WordsPlayed.AutoSize = true;
            this.Player2WordsPlayed.Location = new System.Drawing.Point(223, 42);
            this.Player2WordsPlayed.Name = "Player2WordsPlayed";
            this.Player2WordsPlayed.Size = new System.Drawing.Size(75, 13);
            this.Player2WordsPlayed.TabIndex = 5;
            this.Player2WordsPlayed.Text = "Words played:";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(15, 59);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(172, 257);
            this.richTextBox1.TabIndex = 6;
            this.richTextBox1.Text = "";
            // 
            // richTextBox2
            // 
            this.richTextBox2.Location = new System.Drawing.Point(226, 59);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.ReadOnly = true;
            this.richTextBox2.Size = new System.Drawing.Size(168, 257);
            this.richTextBox2.TabIndex = 7;
            this.richTextBox2.Text = "";
            // 
            // Done
            // 
            this.Done.Location = new System.Drawing.Point(167, 322);
            this.Done.Name = "Done";
            this.Done.Size = new System.Drawing.Size(75, 23);
            this.Done.TabIndex = 8;
            this.Done.Text = "doneButton";
            this.Done.UseVisualStyleBackColor = true;
            this.Done.Click += new System.EventHandler(this.Done_Click);
            // 
            // Score1Label
            // 
            this.Score1Label.AutoSize = true;
            this.Score1Label.Location = new System.Drawing.Point(12, 332);
            this.Score1Label.Name = "Score1Label";
            this.Score1Label.Size = new System.Drawing.Size(41, 13);
            this.Score1Label.TabIndex = 9;
            this.Score1Label.Text = "Score: ";
            // 
            // Score2Label
            // 
            this.Score2Label.AutoSize = true;
            this.Score2Label.Location = new System.Drawing.Point(278, 330);
            this.Score2Label.Name = "Score2Label";
            this.Score2Label.Size = new System.Drawing.Size(41, 13);
            this.Score2Label.TabIndex = 10;
            this.Score2Label.Text = "Score2";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(60, 324);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 11;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(326, 322);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(68, 20);
            this.textBox2.TabIndex = 12;
            // 
            // EndForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(406, 356);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.Score2Label);
            this.Controls.Add(this.Score1Label);
            this.Controls.Add(this.Done);
            this.Controls.Add(this.richTextBox2);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.Player2WordsPlayed);
            this.Controls.Add(this.Player1WordsPlayed);
            this.Controls.Add(this.Name2);
            this.Controls.Add(this.Name1);
            this.Controls.Add(this.Player2Label);
            this.Controls.Add(this.Player1Label);
            this.Name = "EndForm";
            this.Text = "EndForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Player1Label;
        private System.Windows.Forms.Label Player2Label;
        private System.Windows.Forms.Label Name1;
        private System.Windows.Forms.Label Name2;
        private System.Windows.Forms.Label Player1WordsPlayed;
        private System.Windows.Forms.Label Player2WordsPlayed;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.RichTextBox richTextBox2;
        private System.Windows.Forms.Button Done;
        private System.Windows.Forms.Label Score1Label;
        private System.Windows.Forms.Label Score2Label;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
    }
}
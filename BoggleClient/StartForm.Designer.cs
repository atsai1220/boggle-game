namespace BoggleClient
{
    partial class StartForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.startButton = new System.Windows.Forms.Button();
            this.nicknameLabel = new System.Windows.Forms.Label();
            this.nicknameBox = new System.Windows.Forms.TextBox();
            this.durationLabel = new System.Windows.Forms.Label();
            this.durationBox = new System.Windows.Forms.TextBox();
            this.domainBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Domain:";
            // 
            // startButton
            // 
            this.startButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.startButton.Location = new System.Drawing.Point(7, 91);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(354, 88);
            this.startButton.TabIndex = 2;
            this.startButton.Text = "Start Game";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // nicknameLabel
            // 
            this.nicknameLabel.AutoSize = true;
            this.nicknameLabel.Location = new System.Drawing.Point(4, 42);
            this.nicknameLabel.Name = "nicknameLabel";
            this.nicknameLabel.Size = new System.Drawing.Size(58, 13);
            this.nicknameLabel.TabIndex = 4;
            this.nicknameLabel.Text = "Nickname:";
            // 
            // nicknameBox
            // 
            this.nicknameBox.Location = new System.Drawing.Point(68, 39);
            this.nicknameBox.Name = "nicknameBox";
            this.nicknameBox.Size = new System.Drawing.Size(293, 20);
            this.nicknameBox.TabIndex = 5;
            this.nicknameBox.Text = "Player";
            // 
            // durationLabel
            // 
            this.durationLabel.AutoSize = true;
            this.durationLabel.Location = new System.Drawing.Point(7, 68);
            this.durationLabel.Name = "durationLabel";
            this.durationLabel.Size = new System.Drawing.Size(50, 13);
            this.durationLabel.TabIndex = 6;
            this.durationLabel.Text = "Duration:";
            // 
            // durationBox
            // 
            this.durationBox.Location = new System.Drawing.Point(68, 65);
            this.durationBox.Name = "durationBox";
            this.durationBox.Size = new System.Drawing.Size(293, 20);
            this.durationBox.TabIndex = 7;
            this.durationBox.Text = "60";
            // 
            // domainBox
            // 
            this.domainBox.Location = new System.Drawing.Point(68, 12);
            this.domainBox.Name = "domainBox";
            this.domainBox.Size = new System.Drawing.Size(293, 20);
            this.domainBox.TabIndex = 8;
            this.domainBox.Text = " http://bogglecs3500s16.azurewebsites.net";
            // 
            // StartForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(373, 191);
            this.Controls.Add(this.domainBox);
            this.Controls.Add(this.durationBox);
            this.Controls.Add(this.durationLabel);
            this.Controls.Add(this.nicknameBox);
            this.Controls.Add(this.nicknameLabel);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "StartForm";
            this.Text = "Boggle Client";
            this.Load += new System.EventHandler(this.StartForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Label nicknameLabel;
        private System.Windows.Forms.TextBox nicknameBox;
        private System.Windows.Forms.Label durationLabel;
        private System.Windows.Forms.TextBox durationBox;
        private System.Windows.Forms.TextBox domainBox;
    }
}
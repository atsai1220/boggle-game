namespace BoggleClient
{
    partial class domainForm
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
            this.domainComboBox = new System.Windows.Forms.ComboBox();
            this.domainAcceptButton = new System.Windows.Forms.Button();
            this.domainCancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Domain:";
            // 
            // domainComboBox
            // 
            this.domainComboBox.FormattingEnabled = true;
            this.domainComboBox.Items.AddRange(new object[] {
            "http://bogglecs3500s16.azurewebsites.net/"});
            this.domainComboBox.Location = new System.Drawing.Point(56, 10);
            this.domainComboBox.Name = "domainComboBox";
            this.domainComboBox.Size = new System.Drawing.Size(568, 21);
            this.domainComboBox.TabIndex = 1;
            this.domainComboBox.SelectedIndex = 0;
            // 
            // domainAcceptButton
            // 
            this.domainAcceptButton.Location = new System.Drawing.Point(549, 38);
            this.domainAcceptButton.Name = "domainAcceptButton";
            this.domainAcceptButton.Size = new System.Drawing.Size(75, 23);
            this.domainAcceptButton.TabIndex = 2;
            this.domainAcceptButton.Text = "Accept";
            this.domainAcceptButton.UseVisualStyleBackColor = true;
            this.domainAcceptButton.Click += new System.EventHandler(this.domainAcceptButton_Click);
            // 
            // domainCancelButton
            // 
            this.domainCancelButton.Location = new System.Drawing.Point(468, 38);
            this.domainCancelButton.Name = "domainCancelButton";
            this.domainCancelButton.Size = new System.Drawing.Size(75, 23);
            this.domainCancelButton.TabIndex = 3;
            this.domainCancelButton.Text = "Cancel";
            this.domainCancelButton.UseVisualStyleBackColor = true;
            this.domainCancelButton.Click += new System.EventHandler(this.domainCancelButton_Click);
            // 
            // domainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(636, 73);
            this.Controls.Add(this.domainCancelButton);
            this.Controls.Add(this.domainAcceptButton);
            this.Controls.Add(this.domainComboBox);
            this.Controls.Add(this.label1);
            this.Name = "domainForm";
            this.Text = "domainFom";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox domainComboBox;
        private System.Windows.Forms.Button domainAcceptButton;
        private System.Windows.Forms.Button domainCancelButton;
    }
}
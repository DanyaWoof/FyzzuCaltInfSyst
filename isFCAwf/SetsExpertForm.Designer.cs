namespace isFCAwf
{
    partial class SetsExpertForm
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
            this.BackToTheAuthorizeBTN = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // BackToTheAuthorizeBTN
            // 
            this.BackToTheAuthorizeBTN.Location = new System.Drawing.Point(723, 12);
            this.BackToTheAuthorizeBTN.Name = "BackToTheAuthorizeBTN";
            this.BackToTheAuthorizeBTN.Size = new System.Drawing.Size(75, 23);
            this.BackToTheAuthorizeBTN.TabIndex = 0;
            this.BackToTheAuthorizeBTN.Text = "Выход";
            this.BackToTheAuthorizeBTN.UseVisualStyleBackColor = true;
            this.BackToTheAuthorizeBTN.Click += new System.EventHandler(this.BackToTheAuthorizeBTN_Click);
            // 
            // SetsExpertForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.BackToTheAuthorizeBTN);
            this.Name = "SetsExpertForm";
            this.Text = "SetsExpertForm";
            this.Load += new System.EventHandler(this.SetsExpertForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button BackToTheAuthorizeBTN;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}
namespace isFCAwf
{
    partial class FreeSets
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
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.dgvnmA = new System.Windows.Forms.DataGridView();
            this.bindingSourceNMA = new System.Windows.Forms.BindingSource(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgvU = new System.Windows.Forms.DataGridView();
            this.bindingSourceU = new System.Windows.Forms.BindingSource(this.components);
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.dgvX = new System.Windows.Forms.DataGridView();
            this.bindingSourceX = new System.Windows.Forms.BindingSource(this.components);
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.dgvnmLPX = new System.Windows.Forms.DataGridView();
            this.bindingSourceNML_PX = new System.Windows.Forms.BindingSource(this.components);
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvnmA)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceNMA)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvU)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceU)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceX)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvnmLPX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceNML_PX)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.checkBox2);
            this.groupBox3.Controls.Add(this.dgvnmA);
            this.groupBox3.Location = new System.Drawing.Point(2, 260);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(643, 262);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Множества nmA";
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(94, 0);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(247, 17);
            this.checkBox2.TabIndex = 1;
            this.checkBox2.Text = "Показывать множества nmA без связи с U";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // dgvnmA
            // 
            this.dgvnmA.AllowUserToAddRows = false;
            this.dgvnmA.AllowUserToDeleteRows = false;
            this.dgvnmA.AutoGenerateColumns = false;
            this.dgvnmA.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvnmA.DataSource = this.bindingSourceNMA;
            this.dgvnmA.Location = new System.Drawing.Point(1, 20);
            this.dgvnmA.Name = "dgvnmA";
            this.dgvnmA.ReadOnly = true;
            this.dgvnmA.Size = new System.Drawing.Size(642, 239);
            this.dgvnmA.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dgvU);
            this.groupBox1.Location = new System.Drawing.Point(2, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(643, 251);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Свободные множества U";
            // 
            // dgvU
            // 
            this.dgvU.AllowUserToAddRows = false;
            this.dgvU.AllowUserToDeleteRows = false;
            this.dgvU.AutoGenerateColumns = false;
            this.dgvU.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvU.DataSource = this.bindingSourceU;
            this.dgvU.Location = new System.Drawing.Point(0, 23);
            this.dgvU.Name = "dgvU";
            this.dgvU.ReadOnly = true;
            this.dgvU.Size = new System.Drawing.Size(643, 228);
            this.dgvU.TabIndex = 0;
            this.dgvU.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvU_CellClick);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.checkBox1);
            this.groupBox2.Controls.Add(this.dgvX);
            this.groupBox2.Location = new System.Drawing.Point(652, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(643, 251);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Свободные наборы X";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(122, 0);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(258, 17);
            this.checkBox1.TabIndex = 1;
            this.checkBox1.Text = "Показывать также не связанные с U наборы";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // dgvX
            // 
            this.dgvX.AllowUserToAddRows = false;
            this.dgvX.AllowUserToDeleteRows = false;
            this.dgvX.AutoGenerateColumns = false;
            this.dgvX.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvX.DataSource = this.bindingSourceX;
            this.dgvX.Location = new System.Drawing.Point(0, 23);
            this.dgvX.Name = "dgvX";
            this.dgvX.ReadOnly = true;
            this.dgvX.Size = new System.Drawing.Size(643, 228);
            this.dgvX.TabIndex = 0;
            this.dgvX.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvX_CellClick);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.checkBox3);
            this.groupBox4.Controls.Add(this.dgvnmLPX);
            this.groupBox4.Location = new System.Drawing.Point(652, 260);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(643, 262);
            this.groupBox4.TabIndex = 2;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Множества nmLP(X)";
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Location = new System.Drawing.Point(112, 0);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(252, 17);
            this.checkBox3.TabIndex = 1;
            this.checkBox3.Text = "Показывать множества nmLP без связи с X";
            this.checkBox3.UseVisualStyleBackColor = true;
            this.checkBox3.CheckedChanged += new System.EventHandler(this.checkBox3_CheckedChanged);
            // 
            // dgvnmLPX
            // 
            this.dgvnmLPX.AllowUserToAddRows = false;
            this.dgvnmLPX.AllowUserToDeleteRows = false;
            this.dgvnmLPX.AutoGenerateColumns = false;
            this.dgvnmLPX.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvnmLPX.DataSource = this.bindingSourceNML_PX;
            this.dgvnmLPX.Location = new System.Drawing.Point(0, 19);
            this.dgvnmLPX.Name = "dgvnmLPX";
            this.dgvnmLPX.ReadOnly = true;
            this.dgvnmLPX.Size = new System.Drawing.Size(643, 240);
            this.dgvnmLPX.TabIndex = 0;
            // 
            // FreeSets
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1298, 522);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "FreeSets";
            this.Text = "FreeSets";
            this.Load += new System.EventHandler(this.FreeSets_Load);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvnmA)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceNMA)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvU)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceU)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceX)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvnmLPX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceNML_PX)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridView dgvnmA;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgvU;
        private System.Windows.Forms.BindingSource bindingSourceNMA;
        private System.Windows.Forms.BindingSource bindingSourceU;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgvX;
        private System.Windows.Forms.BindingSource bindingSourceX;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.DataGridView dgvnmLPX;
        private System.Windows.Forms.BindingSource bindingSourceNML_PX;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBox3;
    }
}
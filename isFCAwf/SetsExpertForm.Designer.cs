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
            this.components = new System.ComponentModel.Container();
            this.BackToTheAuthorizeBTN = new System.Windows.Forms.Button();
            this.gbFreeOrders = new System.Windows.Forms.GroupBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dgvOrders = new System.Windows.Forms.DataGridView();
            this.bsOrders = new System.Windows.Forms.BindingSource(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.radioButton5 = new System.Windows.Forms.RadioButton();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.UnFreeOrderBtn = new System.Windows.Forms.Button();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.dgwFrOrUpdaterBtn = new System.Windows.Forms.Button();
            this.radioButton4 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.radioButton6 = new System.Windows.Forms.RadioButton();
            this.radioButton8 = new System.Windows.Forms.RadioButton();
            this.radioButton7 = new System.Windows.Forms.RadioButton();
            this.dgvSelectedOrderData = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.radioButton9 = new System.Windows.Forms.RadioButton();
            this.radioButton10 = new System.Windows.Forms.RadioButton();
            this.dgvSetsData = new System.Windows.Forms.DataGridView();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.bsSelectedOrderData = new System.Windows.Forms.BindingSource(this.components);
            this.bsSetsData = new System.Windows.Forms.BindingSource(this.components);
            this.gbFreeOrders.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrders)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsOrders)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSelectedOrderData)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSetsData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsSelectedOrderData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsSetsData)).BeginInit();
            this.SuspendLayout();
            // 
            // BackToTheAuthorizeBTN
            // 
            this.BackToTheAuthorizeBTN.Location = new System.Drawing.Point(792, 16);
            this.BackToTheAuthorizeBTN.Name = "BackToTheAuthorizeBTN";
            this.BackToTheAuthorizeBTN.Size = new System.Drawing.Size(75, 23);
            this.BackToTheAuthorizeBTN.TabIndex = 0;
            this.BackToTheAuthorizeBTN.Text = "Выход";
            this.BackToTheAuthorizeBTN.UseVisualStyleBackColor = true;
            this.BackToTheAuthorizeBTN.Click += new System.EventHandler(this.BackToTheAuthorizeBTN_Click_1);
            // 
            // gbFreeOrders
            // 
            this.gbFreeOrders.Controls.Add(this.checkBox1);
            this.gbFreeOrders.Controls.Add(this.label2);
            this.gbFreeOrders.Controls.Add(this.dgvOrders);
            this.gbFreeOrders.Controls.Add(this.label1);
            this.gbFreeOrders.Controls.Add(this.radioButton5);
            this.gbFreeOrders.Controls.Add(this.dateTimePicker2);
            this.gbFreeOrders.Controls.Add(this.UnFreeOrderBtn);
            this.gbFreeOrders.Controls.Add(this.dateTimePicker1);
            this.gbFreeOrders.Controls.Add(this.dgwFrOrUpdaterBtn);
            this.gbFreeOrders.Controls.Add(this.BackToTheAuthorizeBTN);
            this.gbFreeOrders.Controls.Add(this.radioButton4);
            this.gbFreeOrders.Controls.Add(this.radioButton1);
            this.gbFreeOrders.Controls.Add(this.radioButton2);
            this.gbFreeOrders.Location = new System.Drawing.Point(1, 2);
            this.gbFreeOrders.Name = "gbFreeOrders";
            this.gbFreeOrders.Size = new System.Drawing.Size(883, 218);
            this.gbFreeOrders.TabIndex = 1;
            this.gbFreeOrders.TabStop = false;
            this.gbFreeOrders.Text = "Заявки";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(653, 22);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(133, 17);
            this.checkBox1.TabIndex = 3;
            this.checkBox1.Text = "фильтровать по дате";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged_1);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(495, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(22, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "по:";
            // 
            // dgvOrders
            // 
            this.dgvOrders.AutoGenerateColumns = false;
            this.dgvOrders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOrders.DataSource = this.bsOrders;
            this.dgvOrders.Location = new System.Drawing.Point(0, 51);
            this.dgvOrders.Name = "dgvOrders";
            this.dgvOrders.Size = new System.Drawing.Size(776, 150);
            this.dgvOrders.TabIndex = 0;
            this.dgvOrders.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvOrders_CellClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(349, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(16, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "c:";
            // 
            // radioButton5
            // 
            this.radioButton5.AutoSize = true;
            this.radioButton5.Location = new System.Drawing.Point(303, 19);
            this.radioButton5.Name = "radioButton5";
            this.radioButton5.Size = new System.Drawing.Size(44, 17);
            this.radioButton5.TabIndex = 1;
            this.radioButton5.Text = "Все";
            this.radioButton5.UseVisualStyleBackColor = true;
            this.radioButton5.CheckedChanged += new System.EventHandler(this.radioButton5_CheckedChanged);
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Location = new System.Drawing.Point(523, 20);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(121, 20);
            this.dateTimePicker2.TabIndex = 1;
            // 
            // UnFreeOrderBtn
            // 
            this.UnFreeOrderBtn.Location = new System.Drawing.Point(792, 123);
            this.UnFreeOrderBtn.Name = "UnFreeOrderBtn";
            this.UnFreeOrderBtn.Size = new System.Drawing.Size(75, 60);
            this.UnFreeOrderBtn.TabIndex = 0;
            this.UnFreeOrderBtn.Text = "Принять/ выбрать заявку";
            this.UnFreeOrderBtn.UseVisualStyleBackColor = true;
            this.UnFreeOrderBtn.Click += new System.EventHandler(this.UnFreeOrderBtn_Click);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(371, 19);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(121, 20);
            this.dateTimePicker1.TabIndex = 1;
            // 
            // dgwFrOrUpdaterBtn
            // 
            this.dgwFrOrUpdaterBtn.Location = new System.Drawing.Point(792, 74);
            this.dgwFrOrUpdaterBtn.Name = "dgwFrOrUpdaterBtn";
            this.dgwFrOrUpdaterBtn.Size = new System.Drawing.Size(75, 23);
            this.dgwFrOrUpdaterBtn.TabIndex = 0;
            this.dgwFrOrUpdaterBtn.Text = "Обновить";
            this.dgwFrOrUpdaterBtn.UseVisualStyleBackColor = true;
            this.dgwFrOrUpdaterBtn.Click += new System.EventHandler(this.dgwFrOrUpdaterBtn_Click);
            // 
            // radioButton4
            // 
            this.radioButton4.AutoSize = true;
            this.radioButton4.Location = new System.Drawing.Point(197, 19);
            this.radioButton4.Name = "radioButton4";
            this.radioButton4.Size = new System.Drawing.Size(100, 17);
            this.radioButton4.TabIndex = 1;
            this.radioButton4.Text = "Мои закрытые";
            this.radioButton4.UseVisualStyleBackColor = true;
            this.radioButton4.CheckedChanged += new System.EventHandler(this.radioButton4_CheckedChanged);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(11, 19);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(82, 17);
            this.radioButton1.TabIndex = 1;
            this.radioButton1.Text = "Свободные";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Checked = true;
            this.radioButton2.Location = new System.Drawing.Point(99, 19);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(92, 17);
            this.radioButton2.TabIndex = 1;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "Мои текущие";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.dgvSelectedOrderData);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.button4);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.button3);
            this.groupBox1.Location = new System.Drawing.Point(1, 209);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(883, 231);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Заявка №";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.radioButton6);
            this.groupBox2.Controls.Add(this.radioButton8);
            this.groupBox2.Controls.Add(this.radioButton7);
            this.groupBox2.Location = new System.Drawing.Point(11, 19);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(458, 36);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Показать данные:";
            // 
            // radioButton6
            // 
            this.radioButton6.AutoSize = true;
            this.radioButton6.Location = new System.Drawing.Point(6, 13);
            this.radioButton6.Name = "radioButton6";
            this.radioButton6.Size = new System.Drawing.Size(107, 17);
            this.radioButton6.TabIndex = 1;
            this.radioButton6.Text = "по предприятию";
            this.radioButton6.UseVisualStyleBackColor = true;
            this.radioButton6.CheckedChanged += new System.EventHandler(this.radioButton6_CheckedChanged);
            // 
            // radioButton8
            // 
            this.radioButton8.AutoSize = true;
            this.radioButton8.Checked = true;
            this.radioButton8.Location = new System.Drawing.Point(214, 13);
            this.radioButton8.Name = "radioButton8";
            this.radioButton8.Size = new System.Drawing.Size(181, 17);
            this.radioButton8.TabIndex = 1;
            this.radioButton8.TabStop = true;
            this.radioButton8.Text = "По набору множеств U заявки";
            this.radioButton8.UseVisualStyleBackColor = true;
            this.radioButton8.CheckedChanged += new System.EventHandler(this.radioButton8_CheckedChanged);
            // 
            // radioButton7
            // 
            this.radioButton7.AutoSize = true;
            this.radioButton7.Location = new System.Drawing.Point(119, 13);
            this.radioButton7.Name = "radioButton7";
            this.radioButton7.Size = new System.Drawing.Size(89, 17);
            this.radioButton7.TabIndex = 1;
            this.radioButton7.Text = "по клиентам";
            this.radioButton7.UseVisualStyleBackColor = true;
            this.radioButton7.CheckedChanged += new System.EventHandler(this.radioButton7_CheckedChanged);
            // 
            // dgvSelectedOrderData
            // 
            this.dgvSelectedOrderData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSelectedOrderData.Location = new System.Drawing.Point(0, 59);
            this.dgvSelectedOrderData.Name = "dgvSelectedOrderData";
            this.dgvSelectedOrderData.Size = new System.Drawing.Size(776, 150);
            this.dgvSelectedOrderData.TabIndex = 0;
            this.dgvSelectedOrderData.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSelectedOrderData_CellClick);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(792, 144);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(85, 48);
            this.button1.TabIndex = 0;
            this.button1.Text = "Работать с множеством";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(792, 77);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 35);
            this.button4.TabIndex = 0;
            this.button4.Text = "Данные по заявке";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(792, 48);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 0;
            this.button2.Text = "Сбросить";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(792, 19);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 0;
            this.button3.Text = "Обновить";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.radioButton9);
            this.groupBox3.Controls.Add(this.radioButton10);
            this.groupBox3.Controls.Add(this.dgvSetsData);
            this.groupBox3.Controls.Add(this.button6);
            this.groupBox3.Controls.Add(this.button7);
            this.groupBox3.Controls.Add(this.button8);
            this.groupBox3.Controls.Add(this.button9);
            this.groupBox3.Location = new System.Drawing.Point(1, 446);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(883, 231);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Множества U";
            // 
            // radioButton9
            // 
            this.radioButton9.AutoSize = true;
            this.radioButton9.Checked = true;
            this.radioButton9.Location = new System.Drawing.Point(7, 19);
            this.radioButton9.Name = "radioButton9";
            this.radioButton9.Size = new System.Drawing.Size(161, 17);
            this.radioButton9.TabIndex = 1;
            this.radioButton9.TabStop = true;
            this.radioButton9.Text = "Нечеткие множества A<u>";
            this.radioButton9.UseVisualStyleBackColor = true;
            this.radioButton9.CheckedChanged += new System.EventHandler(this.radioButton9_CheckedChanged);
            // 
            // radioButton10
            // 
            this.radioButton10.AutoSize = true;
            this.radioButton10.Location = new System.Drawing.Point(174, 19);
            this.radioButton10.Name = "radioButton10";
            this.radioButton10.Size = new System.Drawing.Size(191, 17);
            this.radioButton10.TabIndex = 1;
            this.radioButton10.TabStop = true;
            this.radioButton10.Text = "Лингвистические переменные X";
            this.radioButton10.UseVisualStyleBackColor = true;
            this.radioButton10.CheckedChanged += new System.EventHandler(this.radioButton10_CheckedChanged);
            // 
            // dgvSetsData
            // 
            this.dgvSetsData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSetsData.Location = new System.Drawing.Point(0, 42);
            this.dgvSetsData.Name = "dgvSetsData";
            this.dgvSetsData.Size = new System.Drawing.Size(776, 150);
            this.dgvSetsData.TabIndex = 0;
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(792, 131);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(75, 38);
            this.button6.TabIndex = 0;
            this.button6.Text = "Работать";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.BackToTheAuthorizeBTN_Click);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(792, 77);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(75, 23);
            this.button7.TabIndex = 0;
            this.button7.Text = "Данные по заявке";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.BackToTheAuthorizeBTN_Click);
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(792, 48);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(75, 23);
            this.button8.TabIndex = 0;
            this.button8.Text = "Сбросить";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.BackToTheAuthorizeBTN_Click);
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(792, 19);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(75, 23);
            this.button9.TabIndex = 0;
            this.button9.Text = "Обновить";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.BackToTheAuthorizeBTN_Click);
            // 
            // SetsExpertForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1355, 699);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gbFreeOrders);
            this.Name = "SetsExpertForm";
            this.Text = "SetsExpertForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SetsExpertForm_FormClosed);
            this.Load += new System.EventHandler(this.SetsExpertForm_Load);
            this.gbFreeOrders.ResumeLayout(false);
            this.gbFreeOrders.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrders)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsOrders)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSelectedOrderData)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSetsData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsSelectedOrderData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsSetsData)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button BackToTheAuthorizeBTN;
        private System.Windows.Forms.GroupBox gbFreeOrders;
        private System.Windows.Forms.DataGridView dgvOrders;
        private System.Windows.Forms.Button UnFreeOrderBtn;
        private System.Windows.Forms.Button dgwFrOrUpdaterBtn;
        private System.Windows.Forms.RadioButton radioButton5;
        private System.Windows.Forms.RadioButton radioButton4;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgvSelectedOrderData;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton radioButton6;
        private System.Windows.Forms.RadioButton radioButton8;
        private System.Windows.Forms.RadioButton radioButton7;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton radioButton9;
        private System.Windows.Forms.RadioButton radioButton10;
        private System.Windows.Forms.DataGridView dgvSetsData;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.BindingSource bsOrders;
        private System.Windows.Forms.BindingSource bsSelectedOrderData;
        private System.Windows.Forms.BindingSource bsSetsData;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}
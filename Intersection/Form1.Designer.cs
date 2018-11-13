namespace affine_transform
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.intersectionBtn = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.poly2Chk = new System.Windows.Forms.CheckBox();
            this.polyChk = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // intersectionBtn
            // 
            this.intersectionBtn.Location = new System.Drawing.Point(708, 19);
            this.intersectionBtn.Name = "intersectionBtn";
            this.intersectionBtn.Size = new System.Drawing.Size(117, 44);
            this.intersectionBtn.TabIndex = 27;
            this.intersectionBtn.Text = "Найти пересечение";
            this.intersectionBtn.UseVisualStyleBackColor = true;
            this.intersectionBtn.Click += new System.EventHandler(this.applyBtn_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(708, 69);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(117, 30);
            this.button1.TabIndex = 2;
            this.button1.Text = "Очистить";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(831, 468);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Controls.Add(this.poly2Chk);
            this.groupBox2.Controls.Add(this.intersectionBtn);
            this.groupBox2.Controls.Add(this.polyChk);
            this.groupBox2.Location = new System.Drawing.Point(12, 486);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(831, 105);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            // 
            // poly2Chk
            // 
            this.poly2Chk.AutoSize = true;
            this.poly2Chk.ForeColor = System.Drawing.Color.MediumVioletRed;
            this.poly2Chk.Location = new System.Drawing.Point(19, 69);
            this.poly2Chk.Name = "poly2Chk";
            this.poly2Chk.Size = new System.Drawing.Size(78, 17);
            this.poly2Chk.TabIndex = 29;
            this.poly2Chk.Text = "Полигон 2";
            this.poly2Chk.UseVisualStyleBackColor = true;
            this.poly2Chk.CheckedChanged += new System.EventHandler(this.poly2Chk_CheckedChanged);
            // 
            // polyChk
            // 
            this.polyChk.AutoSize = true;
            this.polyChk.Checked = true;
            this.polyChk.CheckState = System.Windows.Forms.CheckState.Checked;
            this.polyChk.ForeColor = System.Drawing.Color.BlueViolet;
            this.polyChk.Location = new System.Drawing.Point(19, 34);
            this.polyChk.Name = "polyChk";
            this.polyChk.Size = new System.Drawing.Size(78, 17);
            this.polyChk.TabIndex = 28;
            this.polyChk.Text = "Полигон 1";
            this.polyChk.UseVisualStyleBackColor = true;
            this.polyChk.CheckedChanged += new System.EventHandler(this.polyChk_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(855, 603);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.pictureBox1);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(870, 640);
            this.Name = "Form1";
            this.Text = "Аффинные преобразования";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button intersectionBtn;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox poly2Chk;
        private System.Windows.Forms.CheckBox polyChk;
    }
}


namespace WindowsFormsApp1
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.keyWordTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.blogRadioButton = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.kinRadioButton = new System.Windows.Forms.RadioButton();
            this.cafeRadioButton = new System.Windows.Forms.RadioButton();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.searchButton = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.sixHour_RadioButton = new System.Windows.Forms.RadioButton();
            this.threehour_RadioButton = new System.Windows.Forms.RadioButton();
            this.oneHour_Radiobutton = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.stopSearchButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.uPwTextBox = new System.Windows.Forms.TextBox();
            this.uIdTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.autoReplyCheckBox = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // keyWordTextBox
            // 
            this.keyWordTextBox.Location = new System.Drawing.Point(84, 33);
            this.keyWordTextBox.Name = "keyWordTextBox";
            this.keyWordTextBox.Size = new System.Drawing.Size(112, 21);
            this.keyWordTextBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "키워드 :";
            // 
            // blogRadioButton
            // 
            this.blogRadioButton.AutoSize = true;
            this.blogRadioButton.Checked = true;
            this.blogRadioButton.Location = new System.Drawing.Point(11, 17);
            this.blogRadioButton.Name = "blogRadioButton";
            this.blogRadioButton.Size = new System.Drawing.Size(59, 16);
            this.blogRadioButton.TabIndex = 3;
            this.blogRadioButton.TabStop = true;
            this.blogRadioButton.Text = "블로그";
            this.blogRadioButton.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.kinRadioButton);
            this.panel1.Controls.Add(this.cafeRadioButton);
            this.panel1.Controls.Add(this.blogRadioButton);
            this.panel1.Location = new System.Drawing.Point(218, 92);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(196, 49);
            this.panel1.TabIndex = 4;
            // 
            // kinRadioButton
            // 
            this.kinRadioButton.AutoSize = true;
            this.kinRadioButton.Location = new System.Drawing.Point(132, 17);
            this.kinRadioButton.Name = "kinRadioButton";
            this.kinRadioButton.Size = new System.Drawing.Size(59, 16);
            this.kinRadioButton.TabIndex = 5;
            this.kinRadioButton.TabStop = true;
            this.kinRadioButton.Text = "지식인";
            this.kinRadioButton.UseVisualStyleBackColor = true;
            // 
            // cafeRadioButton
            // 
            this.cafeRadioButton.AutoSize = true;
            this.cafeRadioButton.Location = new System.Drawing.Point(73, 17);
            this.cafeRadioButton.Name = "cafeRadioButton";
            this.cafeRadioButton.Size = new System.Drawing.Size(47, 16);
            this.cafeRadioButton.TabIndex = 4;
            this.cafeRadioButton.TabStop = true;
            this.cafeRadioButton.Text = "카페";
            this.cafeRadioButton.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridView1.Location = new System.Drawing.Point(6, 147);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(698, 422);
            this.dataGridView1.TabIndex = 5;
            // 
            // searchButton
            // 
            this.searchButton.Location = new System.Drawing.Point(629, 21);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(75, 49);
            this.searchButton.TabIndex = 6;
            this.searchButton.Text = "검색하기";
            this.searchButton.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.sixHour_RadioButton);
            this.panel2.Controls.Add(this.threehour_RadioButton);
            this.panel2.Controls.Add(this.oneHour_Radiobutton);
            this.panel2.Location = new System.Drawing.Point(218, 21);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(196, 49);
            this.panel2.TabIndex = 7;
            // 
            // sixHour_RadioButton
            // 
            this.sixHour_RadioButton.AutoSize = true;
            this.sixHour_RadioButton.Location = new System.Drawing.Point(132, 17);
            this.sixHour_RadioButton.Name = "sixHour_RadioButton";
            this.sixHour_RadioButton.Size = new System.Drawing.Size(53, 16);
            this.sixHour_RadioButton.TabIndex = 10;
            this.sixHour_RadioButton.Text = "6시간";
            this.sixHour_RadioButton.UseVisualStyleBackColor = true;
            // 
            // threehour_RadioButton
            // 
            this.threehour_RadioButton.AutoSize = true;
            this.threehour_RadioButton.Location = new System.Drawing.Point(73, 17);
            this.threehour_RadioButton.Name = "threehour_RadioButton";
            this.threehour_RadioButton.Size = new System.Drawing.Size(53, 16);
            this.threehour_RadioButton.TabIndex = 9;
            this.threehour_RadioButton.Text = "3시간";
            this.threehour_RadioButton.UseVisualStyleBackColor = true;
            // 
            // oneHour_Radiobutton
            // 
            this.oneHour_Radiobutton.AutoSize = true;
            this.oneHour_Radiobutton.Checked = true;
            this.oneHour_Radiobutton.Location = new System.Drawing.Point(14, 17);
            this.oneHour_Radiobutton.Name = "oneHour_Radiobutton";
            this.oneHour_Radiobutton.Size = new System.Drawing.Size(53, 16);
            this.oneHour_Radiobutton.TabIndex = 8;
            this.oneHour_Radiobutton.TabStop = true;
            this.oneHour_Radiobutton.Text = "1시간";
            this.oneHour_Radiobutton.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(221, 77);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 9;
            this.label3.Text = "카테고리";
            // 
            // stopSearchButton
            // 
            this.stopSearchButton.Location = new System.Drawing.Point(629, 92);
            this.stopSearchButton.Name = "stopSearchButton";
            this.stopSearchButton.Size = new System.Drawing.Size(75, 49);
            this.stopSearchButton.TabIndex = 10;
            this.stopSearchButton.Text = "검색중단";
            this.stopSearchButton.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(221, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 11;
            this.label2.Text = "검색간격";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.uPwTextBox);
            this.panel3.Controls.Add(this.uIdTextBox);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Location = new System.Drawing.Point(420, 25);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(203, 64);
            this.panel3.TabIndex = 12;
            // 
            // uPwTextBox
            // 
            this.uPwTextBox.Location = new System.Drawing.Point(78, 36);
            this.uPwTextBox.Name = "uPwTextBox";
            this.uPwTextBox.PasswordChar = '*';
            this.uPwTextBox.ReadOnly = true;
            this.uPwTextBox.Size = new System.Drawing.Size(112, 21);
            this.uPwTextBox.TabIndex = 16;
            // 
            // uIdTextBox
            // 
            this.uIdTextBox.Location = new System.Drawing.Point(78, 8);
            this.uIdTextBox.Name = "uIdTextBox";
            this.uIdTextBox.ReadOnly = true;
            this.uIdTextBox.Size = new System.Drawing.Size(112, 21);
            this.uIdTextBox.TabIndex = 15;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(11, 41);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 12);
            this.label5.TabIndex = 14;
            this.label5.Text = "비밀번호 :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 13);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 12);
            this.label4.TabIndex = 13;
            this.label4.Text = "아이디 :";
            // 
            // autoReplyCheckBox
            // 
            this.autoReplyCheckBox.AutoSize = true;
            this.autoReplyCheckBox.Location = new System.Drawing.Point(420, 6);
            this.autoReplyCheckBox.Name = "autoReplyCheckBox";
            this.autoReplyCheckBox.Size = new System.Drawing.Size(72, 16);
            this.autoReplyCheckBox.TabIndex = 0;
            this.autoReplyCheckBox.Text = "자동댓글";
            this.autoReplyCheckBox.UseVisualStyleBackColor = true;
            this.autoReplyCheckBox.CheckedChanged += new System.EventHandler(this.autoReplyCheckBox_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(711, 577);
            this.Controls.Add(this.autoReplyCheckBox);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.stopSearchButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.searchButton);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.keyWordTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "키워드검색";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox keyWordTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton blogRadioButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton kinRadioButton;
        private System.Windows.Forms.RadioButton cafeRadioButton;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button searchButton;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton sixHour_RadioButton;
        private System.Windows.Forms.RadioButton threehour_RadioButton;
        private System.Windows.Forms.RadioButton oneHour_Radiobutton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button stopSearchButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox uPwTextBox;
        private System.Windows.Forms.TextBox uIdTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox autoReplyCheckBox;
    }
}


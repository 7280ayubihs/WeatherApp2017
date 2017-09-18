namespace WeatherApp2017
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.buttonTest = new System.Windows.Forms.Button();
            this.dateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.comboBoxTimeSelect = new System.Windows.Forms.ComboBox();
            this.pictureBoxExpect = new System.Windows.Forms.PictureBox();
            this.textBoxPointA = new System.Windows.Forms.TextBox();
            this.textBoxPointB = new System.Windows.Forms.TextBox();
            this.pictureBoxActual = new System.Windows.Forms.PictureBox();
            this.comboBoxPointSelect = new System.Windows.Forms.ComboBox();
            this.comboBoxExpectedDateSelect = new System.Windows.Forms.ComboBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Temperature = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Humidity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Pressure = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxExpect)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxActual)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonTest
            // 
            this.buttonTest.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.buttonTest.Location = new System.Drawing.Point(13, 125);
            this.buttonTest.Name = "buttonTest";
            this.buttonTest.Size = new System.Drawing.Size(406, 50);
            this.buttonTest.TabIndex = 12;
            this.buttonTest.Text = "推定";
            this.buttonTest.UseVisualStyleBackColor = true;
            this.buttonTest.Click += new System.EventHandler(this.buttonTest_Click);
            // 
            // dateTimePicker
            // 
            this.dateTimePicker.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.dateTimePicker.Location = new System.Drawing.Point(13, 12);
            this.dateTimePicker.Name = "dateTimePicker";
            this.dateTimePicker.Size = new System.Drawing.Size(200, 31);
            this.dateTimePicker.TabIndex = 14;
            // 
            // comboBoxTimeSelect
            // 
            this.comboBoxTimeSelect.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.comboBoxTimeSelect.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.comboBoxTimeSelect.FormattingEnabled = true;
            this.comboBoxTimeSelect.Location = new System.Drawing.Point(13, 49);
            this.comboBoxTimeSelect.Name = "comboBoxTimeSelect";
            this.comboBoxTimeSelect.Size = new System.Drawing.Size(200, 32);
            this.comboBoxTimeSelect.TabIndex = 15;
            // 
            // pictureBoxExpect
            // 
            this.pictureBoxExpect.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxExpect.Location = new System.Drawing.Point(13, 223);
            this.pictureBoxExpect.Name = "pictureBoxExpect";
            this.pictureBoxExpect.Size = new System.Drawing.Size(200, 200);
            this.pictureBoxExpect.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxExpect.TabIndex = 17;
            this.pictureBoxExpect.TabStop = false;
            // 
            // textBoxPointA
            // 
            this.textBoxPointA.BackColor = System.Drawing.SystemColors.Control;
            this.textBoxPointA.Font = new System.Drawing.Font("メイリオ", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBoxPointA.Location = new System.Drawing.Point(13, 181);
            this.textBoxPointA.Name = "textBoxPointA";
            this.textBoxPointA.ReadOnly = true;
            this.textBoxPointA.Size = new System.Drawing.Size(200, 43);
            this.textBoxPointA.TabIndex = 18;
            this.textBoxPointA.Text = "推定";
            this.textBoxPointA.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBoxPointB
            // 
            this.textBoxPointB.BackColor = System.Drawing.SystemColors.Control;
            this.textBoxPointB.Font = new System.Drawing.Font("メイリオ", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBoxPointB.Location = new System.Drawing.Point(219, 181);
            this.textBoxPointB.Name = "textBoxPointB";
            this.textBoxPointB.ReadOnly = true;
            this.textBoxPointB.Size = new System.Drawing.Size(200, 43);
            this.textBoxPointB.TabIndex = 20;
            this.textBoxPointB.Text = "Weather Hacks";
            this.textBoxPointB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // pictureBoxActual
            // 
            this.pictureBoxActual.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxActual.Location = new System.Drawing.Point(219, 223);
            this.pictureBoxActual.Name = "pictureBoxActual";
            this.pictureBoxActual.Size = new System.Drawing.Size(200, 200);
            this.pictureBoxActual.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxActual.TabIndex = 19;
            this.pictureBoxActual.TabStop = false;
            // 
            // comboBoxPointSelect
            // 
            this.comboBoxPointSelect.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.comboBoxPointSelect.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.comboBoxPointSelect.FormattingEnabled = true;
            this.comboBoxPointSelect.Location = new System.Drawing.Point(13, 87);
            this.comboBoxPointSelect.Name = "comboBoxPointSelect";
            this.comboBoxPointSelect.Size = new System.Drawing.Size(200, 32);
            this.comboBoxPointSelect.TabIndex = 38;
            // 
            // comboBoxExpectedDateSelect
            // 
            this.comboBoxExpectedDateSelect.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.comboBoxExpectedDateSelect.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.comboBoxExpectedDateSelect.FormattingEnabled = true;
            this.comboBoxExpectedDateSelect.Location = new System.Drawing.Point(219, 12);
            this.comboBoxExpectedDateSelect.Name = "comboBoxExpectedDateSelect";
            this.comboBoxExpectedDateSelect.Size = new System.Drawing.Size(200, 32);
            this.comboBoxExpectedDateSelect.TabIndex = 39;
            // 
            // comboBox1
            // 
            this.comboBox1.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.comboBox1.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(219, 49);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(200, 32);
            this.comboBox1.TabIndex = 61;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Date,
            this.Time,
            this.Temperature,
            this.Humidity,
            this.Pressure});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("メイリオ", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.Location = new System.Drawing.Point(425, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 21;
            this.dataGridView1.Size = new System.Drawing.Size(527, 411);
            this.dataGridView1.TabIndex = 62;
            // 
            // Date
            // 
            this.Date.HeaderText = "日付";
            this.Date.Name = "Date";
            this.Date.ReadOnly = true;
            // 
            // Time
            // 
            this.Time.HeaderText = "時間";
            this.Time.Name = "Time";
            this.Time.ReadOnly = true;
            // 
            // Temperature
            // 
            this.Temperature.HeaderText = "気温（℃）";
            this.Temperature.Name = "Temperature";
            this.Temperature.ReadOnly = true;
            // 
            // Humidity
            // 
            this.Humidity.HeaderText = "湿度（%）";
            this.Humidity.Name = "Humidity";
            this.Humidity.ReadOnly = true;
            // 
            // Pressure
            // 
            this.Pressure.HeaderText = "気圧（hPa）";
            this.Pressure.Name = "Pressure";
            this.Pressure.ReadOnly = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(964, 436);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.comboBoxExpectedDateSelect);
            this.Controls.Add(this.comboBoxPointSelect);
            this.Controls.Add(this.textBoxPointB);
            this.Controls.Add(this.pictureBoxActual);
            this.Controls.Add(this.textBoxPointA);
            this.Controls.Add(this.pictureBoxExpect);
            this.Controls.Add(this.comboBoxTimeSelect);
            this.Controls.Add(this.dateTimePicker);
            this.Controls.Add(this.buttonTest);
            this.Name = "Form1";
            this.Text = "JEX Weather Forecast 2017";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxExpect)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxActual)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button buttonTest;
        private System.Windows.Forms.DateTimePicker dateTimePicker;
        private System.Windows.Forms.ComboBox comboBoxTimeSelect;
        private System.Windows.Forms.PictureBox pictureBoxExpect;
        private System.Windows.Forms.TextBox textBoxPointA;
        private System.Windows.Forms.TextBox textBoxPointB;
        private System.Windows.Forms.PictureBox pictureBoxActual;
        private System.Windows.Forms.ComboBox comboBoxPointSelect;
        private System.Windows.Forms.ComboBox comboBoxExpectedDateSelect;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Date;
        private System.Windows.Forms.DataGridViewTextBoxColumn Time;
        private System.Windows.Forms.DataGridViewTextBoxColumn Temperature;
        private System.Windows.Forms.DataGridViewTextBoxColumn Humidity;
        private System.Windows.Forms.DataGridViewTextBoxColumn Pressure;
    }
}


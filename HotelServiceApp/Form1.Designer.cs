namespace HotelServiceApp
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            textBox1 = new TextBox();
            textBoxName = new TextBox();
            comboBoxRooms = new ComboBox();
            textBox3 = new TextBox();
            textBox4 = new TextBox();
            textBox5 = new TextBox();
            textBoxDescription = new TextBox();
            textBoxPrice = new TextBox();
            AddService = new Button();
            button1 = new Button();
            SuspendLayout();
            // 
            // textBox1
            // 
            textBox1.BorderStyle = BorderStyle.None;
            textBox1.Location = new Point(32, 71);
            textBox1.Margin = new Padding(4);
            textBox1.Name = "textBox1";
            textBox1.ReadOnly = true;
            textBox1.Size = new Size(138, 22);
            textBox1.TabIndex = 0;
            textBox1.Text = "Oda Numarası:";
            // 
            // textBoxName
            // 
            textBoxName.Location = new Point(31, 265);
            textBoxName.Name = "textBoxName";
            textBoxName.Size = new Size(298, 29);
            textBoxName.TabIndex = 1;
            // 
            // comboBoxRooms
            // 
            comboBoxRooms.DisplayMember = "Id";
            comboBoxRooms.FormattingEnabled = true;
            comboBoxRooms.Location = new Point(31, 125);
            comboBoxRooms.Name = "comboBoxRooms";
            comboBoxRooms.Size = new Size(214, 29);
            comboBoxRooms.TabIndex = 2;
            comboBoxRooms.ValueMember = "Id";
            comboBoxRooms.SelectedIndexChanged += comboBoxRooms_SelectedIndexChanged;
            // 
            // textBox3
            // 
            textBox3.Location = new Point(31, 190);
            textBox3.Name = "textBox3";
            textBox3.ReadOnly = true;
            textBox3.Size = new Size(100, 29);
            textBox3.TabIndex = 3;
            textBox3.Text = "Hizmet giriniz:";
            // 
            // textBox4
            // 
            textBox4.Location = new Point(32, 341);
            textBox4.Name = "textBox4";
            textBox4.ReadOnly = true;
            textBox4.Size = new Size(213, 29);
            textBox4.TabIndex = 4;
            textBox4.Text = "Hİzmet Açıklaması Giriniz:";
            // 
            // textBox5
            // 
            textBox5.Location = new Point(32, 488);
            textBox5.Name = "textBox5";
            textBox5.ReadOnly = true;
            textBox5.Size = new Size(100, 29);
            textBox5.TabIndex = 5;
            textBox5.Text = "Fiyat Giriniz:";
            // 
            // textBoxDescription
            // 
            textBoxDescription.Location = new Point(31, 404);
            textBoxDescription.Name = "textBoxDescription";
            textBoxDescription.Size = new Size(298, 29);
            textBoxDescription.TabIndex = 6;
            // 
            // textBoxPrice
            // 
            textBoxPrice.Location = new Point(31, 553);
            textBoxPrice.Name = "textBoxPrice";
            textBoxPrice.Size = new Size(298, 29);
            textBoxPrice.TabIndex = 7;
            // 
            // AddService
            // 
            AddService.Location = new Point(726, 224);
            AddService.Name = "AddService";
            AddService.Size = new Size(235, 191);
            AddService.TabIndex = 8;
            AddService.Text = "AddService";
            AddService.UseVisualStyleBackColor = true;
            AddService.Click += AddService_Click;
            // 
            // button1
            // 
            button1.Location = new Point(736, 524);
            button1.Name = "button1";
            button1.Size = new Size(225, 41);
            button1.TabIndex = 9;
            button1.Text = "ShowServices";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(9F, 21F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1029, 630);
            Controls.Add(button1);
            Controls.Add(AddService);
            Controls.Add(textBoxPrice);
            Controls.Add(textBoxDescription);
            Controls.Add(textBox5);
            Controls.Add(textBox4);
            Controls.Add(textBox3);
            Controls.Add(comboBoxRooms);
            Controls.Add(textBoxName);
            Controls.Add(textBox1);
            Font = new Font("Segoe UI", 12F);
            Margin = new Padding(4);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBox1;
        private TextBox textBoxName;
        private ComboBox comboBoxRooms;
        private TextBox textBox3;
        private TextBox textBox4;
        private TextBox textBox5;
        private TextBox textBoxDescription;
        private TextBox textBoxPrice;
        private Button AddService;
        private Button button1;
    }
}

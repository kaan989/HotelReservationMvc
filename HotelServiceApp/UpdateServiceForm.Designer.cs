namespace HotelServiceApp
{
    partial class UpdateServiceForm
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
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            textBoxName = new TextBox();
            textBox4 = new TextBox();
            textBoxPrice = new TextBox();
            textBoxDescription = new TextBox();
            btnSaveUpdate = new Button();
            SuspendLayout();
            // 
            // textBox1
            // 
            textBox1.Location = new Point(30, 40);
            textBox1.Name = "textBox1";
            textBox1.ReadOnly = true;
            textBox1.Size = new Size(267, 23);
            textBox1.TabIndex = 0;
            textBox1.Text = "Name:";
            // 
            // textBox2
            // 
            textBox2.Location = new Point(30, 171);
            textBox2.Name = "textBox2";
            textBox2.ReadOnly = true;
            textBox2.Size = new Size(267, 23);
            textBox2.TabIndex = 1;
            textBox2.Text = "Description:";
            // 
            // textBoxName
            // 
            textBoxName.Location = new Point(30, 90);
            textBoxName.Name = "textBoxName";
            textBoxName.Size = new Size(267, 23);
            textBoxName.TabIndex = 2;
            // 
            // textBox4
            // 
            textBox4.Location = new Point(30, 298);
            textBox4.Name = "textBox4";
            textBox4.ReadOnly = true;
            textBox4.Size = new Size(267, 23);
            textBox4.TabIndex = 3;
            textBox4.Text = "Price:";
            // 
            // textBoxPrice
            // 
            textBoxPrice.Location = new Point(30, 355);
            textBoxPrice.Name = "textBoxPrice";
            textBoxPrice.Size = new Size(267, 23);
            textBoxPrice.TabIndex = 4;
            // 
            // textBoxDescription
            // 
            textBoxDescription.Location = new Point(30, 227);
            textBoxDescription.Name = "textBoxDescription";
            textBoxDescription.Size = new Size(267, 23);
            textBoxDescription.TabIndex = 5;
            // 
            // btnSaveUpdate
            // 
            btnSaveUpdate.Location = new Point(537, 127);
            btnSaveUpdate.Name = "btnSaveUpdate";
            btnSaveUpdate.Size = new Size(144, 109);
            btnSaveUpdate.TabIndex = 6;
            btnSaveUpdate.Text = "Update";
            btnSaveUpdate.UseVisualStyleBackColor = true;
            btnSaveUpdate.Click += btnSaveUpdate_Click;
            // 
            // UpdateServiceForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnSaveUpdate);
            Controls.Add(textBoxDescription);
            Controls.Add(textBoxPrice);
            Controls.Add(textBox4);
            Controls.Add(textBoxName);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Name = "UpdateServiceForm";
            Text = "UpdateServiceForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBox1;
        private TextBox textBox2;
        private TextBox textBoxName;
        private TextBox textBox4;
        private TextBox textBoxPrice;
        private TextBox textBoxDescription;
        private Button btnSaveUpdate;
    }
}
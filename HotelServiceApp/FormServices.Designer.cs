namespace HotelServiceApp
{
    partial class FormServices
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
            listBox1 = new ListBox();
            listbutton = new Button();
            DeleteService = new Button();
            UpdateServiceButton = new Button();
            SuspendLayout();
            // 
            // listBox1
            // 
            listBox1.FormattingEnabled = true;
            listBox1.ItemHeight = 15;
            listBox1.Location = new Point(217, 80);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(501, 289);
            listBox1.TabIndex = 0;
            // 
            // listbutton
            // 
            listbutton.Location = new Point(36, 67);
            listbutton.Name = "listbutton";
            listbutton.Size = new Size(130, 23);
            listbutton.TabIndex = 1;
            listbutton.Text = "List All Services";
            listbutton.UseVisualStyleBackColor = true;
            listbutton.Click += listbutton_Click;
            // 
            // DeleteService
            // 
            DeleteService.Location = new Point(37, 108);
            DeleteService.Name = "DeleteService";
            DeleteService.Size = new Size(129, 23);
            DeleteService.TabIndex = 2;
            DeleteService.Text = "Delete";
            DeleteService.UseVisualStyleBackColor = true;
            DeleteService.Click += DeleteService_Click;
            // 
            // UpdateServiceButton
            // 
            UpdateServiceButton.Location = new Point(38, 152);
            UpdateServiceButton.Name = "UpdateServiceButton";
            UpdateServiceButton.Size = new Size(128, 23);
            UpdateServiceButton.TabIndex = 3;
            UpdateServiceButton.Text = "Update Service";
            UpdateServiceButton.UseVisualStyleBackColor = true;
            UpdateServiceButton.Click += UpdateServiceButton_Click;
            // 
            // FormServices
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(UpdateServiceButton);
            Controls.Add(DeleteService);
            Controls.Add(listbutton);
            Controls.Add(listBox1);
            Name = "FormServices";
            Text = "FormServices";
            Load += FormServices_Load;
            ResumeLayout(false);
        }

        #endregion

        private ListBox listBox1;
        private Button listbutton;
        private Button DeleteService;
        private Button UpdateServiceButton;
    }
}
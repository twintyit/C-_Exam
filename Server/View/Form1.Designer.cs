namespace Server
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
            button1 = new Button();
            textBox1 = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            textBox2 = new TextBox();
            button2 = new Button();
            comboBox1 = new ComboBox();
            button3 = new Button();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(12, 71);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 0;
            button1.Text = "Start server";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(40, 42);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(227, 23);
            textBox1.TabIndex = 1;
            textBox1.Text = "700";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(6, 45);
            label1.Name = "label1";
            label1.Size = new Size(32, 15);
            label1.TabIndex = 2;
            label1.Text = "Port:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(60, 179);
            label2.Name = "label2";
            label2.Size = new Size(16, 15);
            label2.TabIndex = 4;
            label2.Text = "...";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 179);
            label3.Name = "label3";
            label3.Size = new Size(42, 15);
            label3.TabIndex = 5;
            label3.Text = "Status:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(6, 15);
            label4.Name = "label4";
            label4.Size = new Size(20, 15);
            label4.TabIndex = 7;
            label4.Text = "Ip:";
            // 
            // textBox2
            // 
            textBox2.Location = new Point(40, 12);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(227, 23);
            textBox2.TabIndex = 6;
            textBox2.Text = "127.0.0.1";
            // 
            // button2
            // 
            button2.Location = new Point(187, 71);
            button2.Name = "button2";
            button2.Size = new Size(80, 23);
            button2.TabIndex = 8;
            button2.Text = "Stop server";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(12, 110);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(255, 23);
            comboBox1.TabIndex = 9;
            // 
            // button3
            // 
            button3.Location = new Point(12, 139);
            button3.Name = "button3";
            button3.Size = new Size(255, 23);
            button3.TabIndex = 10;
            button3.Text = "Disconnect client";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(279, 203);
            Controls.Add(button3);
            Controls.Add(comboBox1);
            Controls.Add(button2);
            Controls.Add(label4);
            Controls.Add(textBox2);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(textBox1);
            Controls.Add(button1);
            Name = "Form1";
            Text = "Server";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private TextBox textBox1;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private TextBox textBox2;
        private Button button2;
        private ComboBox comboBox1;
        private Button button3;
    }
}

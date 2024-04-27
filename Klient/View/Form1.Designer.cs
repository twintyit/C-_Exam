namespace Klient
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
            listBox1 = new ListBox();
            button1 = new Button();
            label4 = new Label();
            textBox2 = new TextBox();
            label1 = new Label();
            textBox1 = new TextBox();
            button2 = new Button();
            pictureBox1 = new PictureBox();
            button3 = new Button();
            label2 = new Label();
            button4 = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // listBox1
            // 
            listBox1.FormattingEnabled = true;
            listBox1.ItemHeight = 15;
            listBox1.Location = new Point(12, 100);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(371, 184);
            listBox1.TabIndex = 0;
            // 
            // button1
            // 
            button1.Location = new Point(11, 299);
            button1.Name = "button1";
            button1.Size = new Size(109, 23);
            button1.TabIndex = 1;
            button1.Text = "Get  window list";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(11, 15);
            label4.Name = "label4";
            label4.Size = new Size(20, 15);
            label4.TabIndex = 12;
            label4.Text = "Ip:";
            // 
            // textBox2
            // 
            textBox2.Location = new Point(45, 12);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(100, 23);
            textBox2.TabIndex = 11;
            textBox2.Text = "127.0.0.1";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(11, 45);
            label1.Name = "label1";
            label1.Size = new Size(32, 15);
            label1.TabIndex = 10;
            label1.Text = "Port:";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(45, 42);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(100, 23);
            textBox1.TabIndex = 9;
            textBox1.Text = "700";
            // 
            // button2
            // 
            button2.Location = new Point(11, 71);
            button2.Name = "button2";
            button2.Size = new Size(134, 23);
            button2.TabIndex = 8;
            button2.Text = "Connect to server";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(438, 10);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(624, 399);
            pictureBox1.TabIndex = 13;
            pictureBox1.TabStop = false;
            // 
            // button3
            // 
            button3.Location = new Point(438, 422);
            button3.Name = "button3";
            button3.Size = new Size(128, 23);
            button3.TabIndex = 14;
            button3.Text = "Start screen";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 426);
            label2.Name = "label2";
            label2.Size = new Size(38, 15);
            label2.TabIndex = 15;
            label2.Text = "label2";
            // 
            // button4
            // 
            button4.Location = new Point(912, 418);
            button4.Name = "button4";
            button4.Size = new Size(150, 23);
            button4.TabIndex = 16;
            button4.Text = " Disconnect";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1074, 451);
            Controls.Add(button4);
            Controls.Add(label2);
            Controls.Add(button3);
            Controls.Add(pictureBox1);
            Controls.Add(label4);
            Controls.Add(textBox2);
            Controls.Add(label1);
            Controls.Add(textBox1);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(listBox1);
            Name = "Form1";
            Text = "Klient";
            FormClosing += Form1_FormClosing;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox listBox1;
        private Button button1;
        private Label label4;
        private TextBox textBox2;
        private Label label1;
        private TextBox textBox1;
        private Button button2;
        private PictureBox pictureBox1;
        private Button button3;
        private Label label2;
        private Button button4;
    }
}

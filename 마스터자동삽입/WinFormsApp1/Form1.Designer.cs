namespace WinFormsApp1
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
            InputBox = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            InputBox.AutoSize = true;
            InputBox.Location = new Point(60, 132);
            InputBox.Name = "label1";
            InputBox.Size = new Size(39, 15);
            InputBox.TabIndex = 0;
            InputBox.Text = "label1";
            InputBox.Click += label1_Input;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(InputBox);
            Name = "Form1";
            Text = "DB INSERT";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label InputBox;
        
    }
}

namespace ChessGUI
{
    partial class Chess
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
            this.AFileLabel = new System.Windows.Forms.Label();
            this.BFileLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // AFileLabel
            // 
            this.AFileLabel.AutoSize = true;
            this.AFileLabel.Font = new System.Drawing.Font("YoonA YoonChe Ultra Light", 35F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.AFileLabel.Location = new System.Drawing.Point(96, 588);
            this.AFileLabel.Name = "AFileLabel";
            this.AFileLabel.Size = new System.Drawing.Size(63, 56);
            this.AFileLabel.TabIndex = 0;
            this.AFileLabel.Text = "A";
            // 
            // BFileLabel
            // 
            this.BFileLabel.AutoSize = true;
            this.BFileLabel.Font = new System.Drawing.Font("YoonA YoonChe Ultra Light", 35F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.BFileLabel.Location = new System.Drawing.Point(165, 588);
            this.BFileLabel.Name = "BFileLabel";
            this.BFileLabel.Size = new System.Drawing.Size(52, 56);
            this.BFileLabel.TabIndex = 0;
            this.BFileLabel.Text = "B";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("YoonA YoonChe Ultra Light", 35F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(214, 588);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 56);
            this.label1.TabIndex = 0;
            this.label1.Text = "C";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("YoonA YoonChe Ultra Light", 35F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(281, 588);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 56);
            this.label2.TabIndex = 0;
            this.label2.Text = "D";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("YoonA YoonChe Ultra Light", 35F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label3.Location = new System.Drawing.Point(344, 588);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 56);
            this.label3.TabIndex = 0;
            this.label3.Text = "E";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("YoonA YoonChe Ultra Light", 35F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label4.Location = new System.Drawing.Point(401, 588);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 56);
            this.label4.TabIndex = 0;
            this.label4.Text = "F";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("YoonA YoonChe Ultra Light", 35F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label5.Location = new System.Drawing.Point(456, 588);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 56);
            this.label5.TabIndex = 0;
            this.label5.Text = "G";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("YoonA YoonChe Ultra Light", 35F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label6.Location = new System.Drawing.Point(527, 588);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(55, 56);
            this.label6.TabIndex = 0;
            this.label6.Text = "H";
            // 
            // Chess
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 661);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BFileLabel);
            this.Controls.Add(this.AFileLabel);
            this.Name = "Chess";
            this.Text = "Chess";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Panel panel1;
        private Label AFileLabel;
        private Label BFileLabel;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
    }
}
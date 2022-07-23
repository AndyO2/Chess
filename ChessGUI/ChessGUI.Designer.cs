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
            this.GameMessagesTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // GameMessagesTextBox
            // 
            this.GameMessagesTextBox.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.GameMessagesTextBox.ForeColor = System.Drawing.Color.Red;
            this.GameMessagesTextBox.Location = new System.Drawing.Point(450, 53);
            this.GameMessagesTextBox.Margin = new System.Windows.Forms.Padding(6);
            this.GameMessagesTextBox.Name = "GameMessagesTextBox";
            this.GameMessagesTextBox.Size = new System.Drawing.Size(394, 61);
            this.GameMessagesTextBox.TabIndex = 1;
            // 
            // Chess
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(192F, 192F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.BackColor = System.Drawing.SystemColors.Desktop;
            this.ClientSize = new System.Drawing.Size(1274, 1229);
            this.Controls.Add(this.GameMessagesTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(6);
            this.MaximizeBox = false;
            this.Name = "Chess";
            this.Text = "Chess";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Panel panel1;
        private TextBox GameMessagesTextBox;
    }
}
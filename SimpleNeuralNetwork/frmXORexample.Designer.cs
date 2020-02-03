namespace SimpleNeuralNetwork
{
    partial class frmXORexample
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
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(492, 312);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(215, 42);
            this.button1.TabIndex = 0;
            this.button1.Text = "Next 1,000 Train!";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(573, 236);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 48);
            this.label1.TabIndex = 1;
            this.label1.Text = "0 xor 0 = 0\r\n0 xor 1 = 1\r\n1 xor 0 = 1\r\n1 xor 1 = 0";
            // 
            // frmXORexample
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(729, 379);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Name = "frmXORexample";
            this.Text = "frmXORexample";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
    }
}
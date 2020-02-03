namespace SimpleNeuralNetwork
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnXORexample = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnXORexample
            // 
            this.btnXORexample.Location = new System.Drawing.Point(278, 12);
            this.btnXORexample.Name = "btnXORexample";
            this.btnXORexample.Size = new System.Drawing.Size(255, 47);
            this.btnXORexample.TabIndex = 0;
            this.btnXORexample.Text = "Show XOR Example";
            this.btnXORexample.UseVisualStyleBackColor = true;
            this.btnXORexample.Click += new System.EventHandler(this.btnXORexample_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(500, 188);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(193, 135);
            this.button1.TabIndex = 1;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnXORexample);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnXORexample;
        private System.Windows.Forms.Button button1;
    }
}


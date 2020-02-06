namespace SimpleNeuralNetwork
{
    partial class frmGuessWhat
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
            this.picCanvas = new System.Windows.Forms.PictureBox();
            this.btnTrain = new System.Windows.Forms.Button();
            this.btnPredict = new System.Windows.Forms.Button();
            this.btnTest = new System.Windows.Forms.Button();
            this.txtTraingDataCount = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnManualTrain = new System.Windows.Forms.Button();
            this.txtNumber = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picCanvas)).BeginInit();
            this.SuspendLayout();
            // 
            // picCanvas
            // 
            this.picCanvas.BackColor = System.Drawing.Color.White;
            this.picCanvas.Location = new System.Drawing.Point(53, 21);
            this.picCanvas.Name = "picCanvas";
            this.picCanvas.Size = new System.Drawing.Size(300, 300);
            this.picCanvas.TabIndex = 0;
            this.picCanvas.TabStop = false;
            this.picCanvas.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picCanvas_MouseDown);
            this.picCanvas.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picCanvas_MouseMove);
            this.picCanvas.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picCanvas_MouseUp);
            // 
            // btnTrain
            // 
            this.btnTrain.Location = new System.Drawing.Point(466, 83);
            this.btnTrain.Name = "btnTrain";
            this.btnTrain.Size = new System.Drawing.Size(200, 40);
            this.btnTrain.TabIndex = 1;
            this.btnTrain.Text = "Dataset으로 학습하기";
            this.btnTrain.UseVisualStyleBackColor = true;
            this.btnTrain.Click += new System.EventHandler(this.btnTrain_Click);
            // 
            // btnPredict
            // 
            this.btnPredict.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnPredict.Location = new System.Drawing.Point(466, 294);
            this.btnPredict.Name = "btnPredict";
            this.btnPredict.Size = new System.Drawing.Size(200, 40);
            this.btnPredict.TabIndex = 2;
            this.btnPredict.Text = "Guess!!";
            this.btnPredict.UseVisualStyleBackColor = true;
            this.btnPredict.Click += new System.EventHandler(this.btnPredict_Click);
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(466, 165);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(200, 40);
            this.btnTest.TabIndex = 3;
            this.btnTest.Text = "Test Set으로 정확도 보기";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // txtTraingDataCount
            // 
            this.txtTraingDataCount.Location = new System.Drawing.Point(544, 32);
            this.txtTraingDataCount.Name = "txtTraingDataCount";
            this.txtTraingDataCount.Size = new System.Drawing.Size(122, 21);
            this.txtTraingDataCount.TabIndex = 4;
            this.txtTraingDataCount.Text = "5000";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(419, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(247, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "트레이닝 갯수 (갯수에 따라 수분 걸립니다.):";
            // 
            // btnManualTrain
            // 
            this.btnManualTrain.Location = new System.Drawing.Point(267, 344);
            this.btnManualTrain.Name = "btnManualTrain";
            this.btnManualTrain.Size = new System.Drawing.Size(119, 40);
            this.btnManualTrain.TabIndex = 6;
            this.btnManualTrain.Text = "학습하기";
            this.btnManualTrain.UseVisualStyleBackColor = true;
            this.btnManualTrain.Click += new System.EventHandler(this.btnManualTrain_Click);
            // 
            // txtNumber
            // 
            this.txtNumber.Location = new System.Drawing.Point(200, 341);
            this.txtNumber.Name = "txtNumber";
            this.txtNumber.Size = new System.Drawing.Size(61, 21);
            this.txtNumber.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(51, 425);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(278, 12);
            this.label2.TabIndex = 8;
            this.label2.Text = "데이터 셋 :  http://yann.lecun.com/exdb/mnist/";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(35, 344);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(159, 12);
            this.label3.TabIndex = 9;
            this.label3.Text = "맞는 숫자 (직접 학습 지도) :";
            // 
            // frmGuessWhat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(697, 460);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtNumber);
            this.Controls.Add(this.btnManualTrain);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtTraingDataCount);
            this.Controls.Add(this.btnTest);
            this.Controls.Add(this.btnPredict);
            this.Controls.Add(this.btnTrain);
            this.Controls.Add(this.picCanvas);
            this.Name = "frmGuessWhat";
            this.Text = "숫자 맞추기 (0~9 까지 그림을 그려주세요)";
            ((System.ComponentModel.ISupportInitialize)(this.picCanvas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picCanvas;
        private System.Windows.Forms.Button btnTrain;
        private System.Windows.Forms.Button btnPredict;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.TextBox txtTraingDataCount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnManualTrain;
        private System.Windows.Forms.TextBox txtNumber;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}
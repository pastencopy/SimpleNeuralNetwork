namespace Snake
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
            this.components = new System.ComponentModel.Container();
            this.picCanvas = new System.Windows.Forms.PictureBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.tmrGame = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.btnStartGenetic = new System.Windows.Forms.Button();
            this.tmrGenetic = new System.Windows.Forms.Timer(this.components);
            this.trackSpeed = new System.Windows.Forms.TrackBar();
            this.trackSkip = new System.Windows.Forms.TrackBar();
            this.chkBestSnake = new System.Windows.Forms.CheckBox();
            this.btnApplyBestSnake = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picCanvas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackSpeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackSkip)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // picCanvas
            // 
            this.picCanvas.Location = new System.Drawing.Point(37, 43);
            this.picCanvas.Name = "picCanvas";
            this.picCanvas.Size = new System.Drawing.Size(440, 440);
            this.picCanvas.TabIndex = 0;
            this.picCanvas.TabStop = false;
            this.picCanvas.Paint += new System.Windows.Forms.PaintEventHandler(this.picCanvas_Paint);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(187, 20);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(121, 35);
            this.btnStart.TabIndex = 1;
            this.btnStart.Text = "New Game";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // tmrGame
            // 
            this.tmrGame.Tick += new System.EventHandler(this.tmrGame_Tick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(168, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "화살표 이동키 / Num(8,4,5,6)";
            // 
            // btnStartGenetic
            // 
            this.btnStartGenetic.Location = new System.Drawing.Point(187, 20);
            this.btnStartGenetic.Name = "btnStartGenetic";
            this.btnStartGenetic.Size = new System.Drawing.Size(121, 37);
            this.btnStartGenetic.TabIndex = 3;
            this.btnStartGenetic.Text = "진화 시작";
            this.btnStartGenetic.UseVisualStyleBackColor = true;
            this.btnStartGenetic.Click += new System.EventHandler(this.btnStartGenetic_Click);
            // 
            // tmrGenetic
            // 
            this.tmrGenetic.Interval = 80;
            this.tmrGenetic.Tick += new System.EventHandler(this.tmrGenetic_Tick);
            // 
            // trackSpeed
            // 
            this.trackSpeed.Location = new System.Drawing.Point(98, 279);
            this.trackSpeed.Maximum = 80;
            this.trackSpeed.Minimum = 1;
            this.trackSpeed.Name = "trackSpeed";
            this.trackSpeed.Size = new System.Drawing.Size(198, 42);
            this.trackSpeed.TabIndex = 4;
            this.trackSpeed.Value = 80;
            this.trackSpeed.Scroll += new System.EventHandler(this.trackSpeed_Scroll);
            // 
            // trackSkip
            // 
            this.trackSkip.Location = new System.Drawing.Point(98, 231);
            this.trackSkip.Maximum = 1000;
            this.trackSkip.Minimum = 1;
            this.trackSkip.Name = "trackSkip";
            this.trackSkip.Size = new System.Drawing.Size(198, 42);
            this.trackSkip.TabIndex = 5;
            this.trackSkip.Value = 1;
            // 
            // chkBestSnake
            // 
            this.chkBestSnake.AutoSize = true;
            this.chkBestSnake.Location = new System.Drawing.Point(153, 345);
            this.chkBestSnake.Name = "chkBestSnake";
            this.chkBestSnake.Size = new System.Drawing.Size(155, 16);
            this.chkBestSnake.TabIndex = 6;
            this.chkBestSnake.Text = "가장 좋은 Snake만 보기";
            this.chkBestSnake.UseVisualStyleBackColor = true;
            // 
            // btnApplyBestSnake
            // 
            this.btnApplyBestSnake.Location = new System.Drawing.Point(230, 367);
            this.btnApplyBestSnake.Name = "btnApplyBestSnake";
            this.btnApplyBestSnake.Size = new System.Drawing.Size(78, 25);
            this.btnApplyBestSnake.TabIndex = 7;
            this.btnApplyBestSnake.Text = "바로 적용";
            this.btnApplyBestSnake.UseVisualStyleBackColor = true;
            this.btnApplyBestSnake.Click += new System.EventHandler(this.btnApplyBestSnake_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnStart);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(532, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(324, 76);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "사용자 키보드로 움직임";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.btnStartGenetic);
            this.groupBox2.Controls.Add(this.chkBestSnake);
            this.groupBox2.Controls.Add(this.trackSpeed);
            this.groupBox2.Controls.Add(this.trackSkip);
            this.groupBox2.Controls.Add(this.btnApplyBestSnake);
            this.groupBox2.Location = new System.Drawing.Point(532, 103);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(323, 409);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "유전 알고리즘 + 신경망";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 285);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 12);
            this.label2.TabIndex = 8;
            this.label2.Text = "진행속도:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(32, 242);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 12);
            this.label3.TabIndex = 9;
            this.label3.Text = "처리량:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 72);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(242, 132);
            this.label4.TabIndex = 10;
            this.label4.Text = "세대별 100 개체\r\n\r\n생명 200\r\n\r\n음식습득시 +200\r\n\r\n변이율 10%\r\n\r\nFitness = 꼬리길이 + 살아남은 시간 에 비례\r\n\r" +
    "\n자식세대 = 최고부모(50%) + 다른부모(50%)";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(868, 542);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.picCanvas);
            this.KeyPreview = true;
            this.Name = "Form1";
            this.Text = "Snake Game";
            ((System.ComponentModel.ISupportInitialize)(this.picCanvas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackSpeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackSkip)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox picCanvas;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Timer tmrGame;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnStartGenetic;
        private System.Windows.Forms.Timer tmrGenetic;
        private System.Windows.Forms.TrackBar trackSpeed;
        private System.Windows.Forms.TrackBar trackSkip;
        private System.Windows.Forms.CheckBox chkBestSnake;
        private System.Windows.Forms.Button btnApplyBestSnake;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
    }
}


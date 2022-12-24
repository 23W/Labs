namespace LB3
{
    partial class MainForm
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
            this.m_reportView = new System.Windows.Forms.ListView();
            this.label1 = new System.Windows.Forms.Label();
            this.m_fingersTextBox = new System.Windows.Forms.TextBox();
            this.m_bestStrategyBox = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.m_numbersBTextBox = new System.Windows.Forms.TextBox();
            this.m_numbersATextBox = new System.Windows.Forms.TextBox();
            this.m_scoresBTextBox = new System.Windows.Forms.TextBox();
            this.m_fingersBTextBox = new System.Windows.Forms.TextBox();
            this.m_scoresATextBox = new System.Windows.Forms.TextBox();
            this.m_fingersATextBox = new System.Windows.Forms.TextBox();
            this.m_stepButton = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_reportView
            // 
            this.m_reportView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_reportView.FullRowSelect = true;
            this.m_reportView.GridLines = true;
            this.m_reportView.Location = new System.Drawing.Point(293, 12);
            this.m_reportView.MultiSelect = false;
            this.m_reportView.Name = "m_reportView";
            this.m_reportView.Size = new System.Drawing.Size(495, 426);
            this.m_reportView.TabIndex = 17;
            this.m_reportView.UseCompatibleStateImageBehavior = false;
            this.m_reportView.View = System.Windows.Forms.View.Details;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(128, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Кількість пальців";
            // 
            // m_fingersTextBox
            // 
            this.m_fingersTextBox.AcceptsReturn = true;
            this.m_fingersTextBox.Location = new System.Drawing.Point(146, 9);
            this.m_fingersTextBox.Name = "m_fingersTextBox";
            this.m_fingersTextBox.Size = new System.Drawing.Size(141, 27);
            this.m_fingersTextBox.TabIndex = 2;
            this.m_fingersTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OnFingersKeyPressed);
            this.m_fingersTextBox.Validated += new System.EventHandler(this.OnFingersChanged);
            // 
            // m_bestStrategyBox
            // 
            this.m_bestStrategyBox.AutoSize = true;
            this.m_bestStrategyBox.Location = new System.Drawing.Point(126, 60);
            this.m_bestStrategyBox.Name = "m_bestStrategyBox";
            this.m_bestStrategyBox.Size = new System.Drawing.Size(161, 24);
            this.m_bestStrategyBox.TabIndex = 3;
            this.m_bestStrategyBox.Text = "Найкращі стратегії";
            this.m_bestStrategyBox.UseVisualStyleBackColor = true;
            this.m_bestStrategyBox.CheckedChanged += new System.EventHandler(this.OnShowStrategy);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.m_numbersBTextBox);
            this.groupBox1.Controls.Add(this.m_numbersATextBox);
            this.groupBox1.Controls.Add(this.m_scoresBTextBox);
            this.groupBox1.Controls.Add(this.m_fingersBTextBox);
            this.groupBox1.Controls.Add(this.m_scoresATextBox);
            this.groupBox1.Controls.Add(this.m_fingersATextBox);
            this.groupBox1.Controls.Add(this.m_stepButton);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(12, 118);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(275, 320);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Гра";
            // 
            // m_numbersBTextBox
            // 
            this.m_numbersBTextBox.Location = new System.Drawing.Point(134, 159);
            this.m_numbersBTextBox.Name = "m_numbersBTextBox";
            this.m_numbersBTextBox.ReadOnly = true;
            this.m_numbersBTextBox.Size = new System.Drawing.Size(135, 27);
            this.m_numbersBTextBox.TabIndex = 11;
            // 
            // m_numbersATextBox
            // 
            this.m_numbersATextBox.Location = new System.Drawing.Point(134, 75);
            this.m_numbersATextBox.Name = "m_numbersATextBox";
            this.m_numbersATextBox.Size = new System.Drawing.Size(135, 27);
            this.m_numbersATextBox.TabIndex = 7;
            this.m_numbersATextBox.Validating += new System.ComponentModel.CancelEventHandler(this.OnFingersAValidating);
            // 
            // m_scoresBTextBox
            // 
            this.m_scoresBTextBox.Location = new System.Drawing.Point(134, 281);
            this.m_scoresBTextBox.Name = "m_scoresBTextBox";
            this.m_scoresBTextBox.ReadOnly = true;
            this.m_scoresBTextBox.Size = new System.Drawing.Size(135, 27);
            this.m_scoresBTextBox.TabIndex = 16;
            // 
            // m_fingersBTextBox
            // 
            this.m_fingersBTextBox.Location = new System.Drawing.Point(134, 119);
            this.m_fingersBTextBox.Name = "m_fingersBTextBox";
            this.m_fingersBTextBox.ReadOnly = true;
            this.m_fingersBTextBox.Size = new System.Drawing.Size(135, 27);
            this.m_fingersBTextBox.TabIndex = 9;
            // 
            // m_scoresATextBox
            // 
            this.m_scoresATextBox.Location = new System.Drawing.Point(134, 248);
            this.m_scoresATextBox.Name = "m_scoresATextBox";
            this.m_scoresATextBox.ReadOnly = true;
            this.m_scoresATextBox.Size = new System.Drawing.Size(135, 27);
            this.m_scoresATextBox.TabIndex = 14;
            // 
            // m_fingersATextBox
            // 
            this.m_fingersATextBox.Location = new System.Drawing.Point(134, 35);
            this.m_fingersATextBox.Name = "m_fingersATextBox";
            this.m_fingersATextBox.Size = new System.Drawing.Size(135, 27);
            this.m_fingersATextBox.TabIndex = 5;
            this.m_fingersATextBox.Validating += new System.ComponentModel.CancelEventHandler(this.OnFingersAValidating);
            // 
            // m_stepButton
            // 
            this.m_stepButton.Location = new System.Drawing.Point(6, 199);
            this.m_stepButton.Name = "m_stepButton";
            this.m_stepButton.Size = new System.Drawing.Size(263, 29);
            this.m_stepButton.TabIndex = 12;
            this.m_stepButton.Text = "Крок";
            this.m_stepButton.UseVisualStyleBackColor = true;
            this.m_stepButton.Click += new System.EventHandler(this.OnStep);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 162);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(120, 20);
            this.label7.TabIndex = 10;
            this.label7.Text = "B припустив у A";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 284);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 20);
            this.label5.TabIndex = 15;
            this.label5.Text = "B балів";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 78);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(120, 20);
            this.label3.TabIndex = 6;
            this.label3.Text = "A припустив у B";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 122);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(75, 20);
            this.label6.TabIndex = 8;
            this.label6.Text = "B загадав";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 251);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 20);
            this.label4.TabIndex = 13;
            this.label4.Text = "A балів";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "A загадав";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.m_bestStrategyBox);
            this.Controls.Add(this.m_fingersTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.m_reportView);
            this.Name = "MainForm";
            this.Text = "Гра у пальці";
            this.Load += new System.EventHandler(this.OnLoad);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ListView m_reportView;
        private Label label1;
        private TextBox m_fingersTextBox;
        private CheckBox m_bestStrategyBox;
        private GroupBox groupBox1;
        private TextBox m_numbersATextBox;
        private TextBox m_fingersATextBox;
        private Button m_stepButton;
        private Label label3;
        private Label label2;
        private TextBox m_scoresBTextBox;
        private TextBox m_scoresATextBox;
        private Label label5;
        private Label label4;
        private TextBox m_numbersBTextBox;
        private TextBox m_fingersBTextBox;
        private Label label7;
        private Label label6;
    }
}
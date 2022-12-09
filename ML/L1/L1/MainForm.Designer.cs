namespace L1
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
            this.m_trainButton = new System.Windows.Forms.Button();
            this.m_trainPlotView = new OxyPlot.WindowsForms.PlotView();
            this.m_errorPlotView = new OxyPlot.WindowsForms.PlotView();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.m_randomRadioButton = new System.Windows.Forms.RadioButton();
            this.m_loadRadioButton = new System.Windows.Forms.RadioButton();
            this.m_iterationsLabel = new System.Windows.Forms.Label();
            this.m_iterationsTextBox = new System.Windows.Forms.TextBox();
            this.m_learningRateLabel = new System.Windows.Forms.Label();
            this.m_learningRateTextBox = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_trainButton
            // 
            this.m_trainButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_trainButton.Location = new System.Drawing.Point(12, 408);
            this.m_trainButton.Name = "m_trainButton";
            this.m_trainButton.Size = new System.Drawing.Size(138, 29);
            this.m_trainButton.TabIndex = 0;
            this.m_trainButton.Text = "Train && Test";
            this.m_trainButton.UseVisualStyleBackColor = true;
            this.m_trainButton.Click += new System.EventHandler(this.OnTrain);
            // 
            // m_trainPlotView
            // 
            this.m_trainPlotView.BackColor = System.Drawing.Color.White;
            this.m_trainPlotView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_trainPlotView.Location = new System.Drawing.Point(3, 3);
            this.m_trainPlotView.Name = "m_trainPlotView";
            this.m_trainPlotView.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.m_trainPlotView.Size = new System.Drawing.Size(444, 384);
            this.m_trainPlotView.TabIndex = 2;
            this.m_trainPlotView.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.m_trainPlotView.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.m_trainPlotView.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // m_errorPlotView
            // 
            this.m_errorPlotView.BackColor = System.Drawing.Color.White;
            this.m_errorPlotView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_errorPlotView.Location = new System.Drawing.Point(453, 3);
            this.m_errorPlotView.Name = "m_errorPlotView";
            this.m_errorPlotView.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.m_errorPlotView.Size = new System.Drawing.Size(445, 384);
            this.m_errorPlotView.TabIndex = 3;
            this.m_errorPlotView.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.m_errorPlotView.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.m_errorPlotView.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.m_trainPlotView, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.m_errorPlotView, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(901, 390);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // m_randomRadioButton
            // 
            this.m_randomRadioButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_randomRadioButton.AutoSize = true;
            this.m_randomRadioButton.Checked = true;
            this.m_randomRadioButton.Location = new System.Drawing.Point(683, 413);
            this.m_randomRadioButton.Name = "m_randomRadioButton";
            this.m_randomRadioButton.Size = new System.Drawing.Size(122, 24);
            this.m_randomRadioButton.TabIndex = 5;
            this.m_randomRadioButton.TabStop = true;
            this.m_randomRadioButton.Text = "Random Data";
            this.m_randomRadioButton.UseVisualStyleBackColor = true;
            // 
            // m_loadRadioButton
            // 
            this.m_loadRadioButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_loadRadioButton.AutoSize = true;
            this.m_loadRadioButton.Location = new System.Drawing.Point(811, 413);
            this.m_loadRadioButton.Name = "m_loadRadioButton";
            this.m_loadRadioButton.Size = new System.Drawing.Size(99, 24);
            this.m_loadRadioButton.TabIndex = 5;
            this.m_loadRadioButton.Text = "Load Data";
            this.m_loadRadioButton.UseVisualStyleBackColor = true;
            // 
            // m_iterationsLabel
            // 
            this.m_iterationsLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_iterationsLabel.AutoSize = true;
            this.m_iterationsLabel.Location = new System.Drawing.Point(168, 412);
            this.m_iterationsLabel.Name = "m_iterationsLabel";
            this.m_iterationsLabel.Size = new System.Drawing.Size(71, 20);
            this.m_iterationsLabel.TabIndex = 6;
            this.m_iterationsLabel.Text = "Iterations";
            // 
            // m_iterationsTextBox
            // 
            this.m_iterationsTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_iterationsTextBox.Location = new System.Drawing.Point(245, 410);
            this.m_iterationsTextBox.Name = "m_iterationsTextBox";
            this.m_iterationsTextBox.Size = new System.Drawing.Size(125, 27);
            this.m_iterationsTextBox.TabIndex = 7;
            this.m_iterationsTextBox.Tag = "";
            // 
            // m_learningRateLabel
            // 
            this.m_learningRateLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_learningRateLabel.AutoSize = true;
            this.m_learningRateLabel.Location = new System.Drawing.Point(389, 413);
            this.m_learningRateLabel.Name = "m_learningRateLabel";
            this.m_learningRateLabel.Size = new System.Drawing.Size(100, 20);
            this.m_learningRateLabel.TabIndex = 8;
            this.m_learningRateLabel.Text = "Learning Rate";
            // 
            // m_learningRateTextBox
            // 
            this.m_learningRateTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_learningRateTextBox.Location = new System.Drawing.Point(495, 410);
            this.m_learningRateTextBox.Name = "m_learningRateTextBox";
            this.m_learningRateTextBox.Size = new System.Drawing.Size(125, 27);
            this.m_learningRateTextBox.TabIndex = 9;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(925, 449);
            this.Controls.Add(this.m_learningRateTextBox);
            this.Controls.Add(this.m_learningRateLabel);
            this.Controls.Add(this.m_iterationsTextBox);
            this.Controls.Add(this.m_iterationsLabel);
            this.Controls.Add(this.m_loadRadioButton);
            this.Controls.Add(this.m_randomRadioButton);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.m_trainButton);
            this.Name = "MainForm";
            this.Text = "Perceptron Neural Network";
            this.Load += new System.EventHandler(this.OnLoad);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button m_trainButton;
        private OxyPlot.WindowsForms.PlotView m_trainPlotView;
        private OxyPlot.WindowsForms.PlotView m_errorPlotView;
        private TableLayoutPanel tableLayoutPanel1;
        private RadioButton m_randomRadioButton;
        private RadioButton m_loadRadioButton;
        private Label m_iterationsLabel;
        private TextBox m_iterationsTextBox;
        private Label m_learningRateLabel;
        private TextBox m_learningRateTextBox;
    }
}
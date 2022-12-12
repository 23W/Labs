namespace L2
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
            this.m_tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.m_uMapPlotView = new OxyPlot.WindowsForms.PlotView();
            this.m_learningRateTextBox = new System.Windows.Forms.TextBox();
            this.m_learningRateLabel = new System.Windows.Forms.Label();
            this.m_iterationsTextBox = new System.Windows.Forms.TextBox();
            this.m_iterationsLabel = new System.Windows.Forms.Label();
            this.m_gridSizelabel = new System.Windows.Forms.Label();
            this.m_gridWidthTextBox = new System.Windows.Forms.TextBox();
            this.m_gridHeightTextBox = new System.Windows.Forms.TextBox();
            this.m_xLabel = new System.Windows.Forms.Label();
            this.m_uMapCheckBox = new System.Windows.Forms.CheckBox();
            this.m_viewPanel = new System.Windows.Forms.Panel();
            this.m_viewPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_trainButton
            // 
            this.m_trainButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_trainButton.Location = new System.Drawing.Point(12, 409);
            this.m_trainButton.Name = "m_trainButton";
            this.m_trainButton.Size = new System.Drawing.Size(116, 29);
            this.m_trainButton.TabIndex = 0;
            this.m_trainButton.Text = "Train && Test";
            this.m_trainButton.UseVisualStyleBackColor = true;
            this.m_trainButton.Click += new System.EventHandler(this.OnTrain);
            // 
            // m_tableLayoutPanel
            // 
            this.m_tableLayoutPanel.ColumnCount = 1;
            this.m_tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.m_tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.m_tableLayoutPanel.MinimumSize = new System.Drawing.Size(50, 50);
            this.m_tableLayoutPanel.Name = "m_tableLayoutPanel";
            this.m_tableLayoutPanel.RowCount = 1;
            this.m_tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.m_tableLayoutPanel.Size = new System.Drawing.Size(776, 391);
            this.m_tableLayoutPanel.TabIndex = 1;
            // 
            // m_uMapPlotView
            // 
            this.m_uMapPlotView.BackColor = System.Drawing.Color.White;
            this.m_uMapPlotView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_uMapPlotView.Location = new System.Drawing.Point(0, 0);
            this.m_uMapPlotView.MinimumSize = new System.Drawing.Size(50, 50);
            this.m_uMapPlotView.Name = "m_uMapPlotView";
            this.m_uMapPlotView.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.m_uMapPlotView.Size = new System.Drawing.Size(776, 391);
            this.m_uMapPlotView.TabIndex = 9;
            this.m_uMapPlotView.Visible = false;
            this.m_uMapPlotView.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.m_uMapPlotView.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.m_uMapPlotView.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // m_learningRateTextBox
            // 
            this.m_learningRateTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_learningRateTextBox.Location = new System.Drawing.Point(446, 411);
            this.m_learningRateTextBox.Name = "m_learningRateTextBox";
            this.m_learningRateTextBox.Size = new System.Drawing.Size(125, 27);
            this.m_learningRateTextBox.TabIndex = 4;
            // 
            // m_learningRateLabel
            // 
            this.m_learningRateLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_learningRateLabel.AutoSize = true;
            this.m_learningRateLabel.Location = new System.Drawing.Point(340, 414);
            this.m_learningRateLabel.Name = "m_learningRateLabel";
            this.m_learningRateLabel.Size = new System.Drawing.Size(100, 20);
            this.m_learningRateLabel.TabIndex = 3;
            this.m_learningRateLabel.Text = "Learning Rate";
            // 
            // m_iterationsTextBox
            // 
            this.m_iterationsTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_iterationsTextBox.Location = new System.Drawing.Point(209, 411);
            this.m_iterationsTextBox.Name = "m_iterationsTextBox";
            this.m_iterationsTextBox.Size = new System.Drawing.Size(125, 27);
            this.m_iterationsTextBox.TabIndex = 2;
            this.m_iterationsTextBox.Tag = "";
            // 
            // m_iterationsLabel
            // 
            this.m_iterationsLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_iterationsLabel.AutoSize = true;
            this.m_iterationsLabel.Location = new System.Drawing.Point(132, 413);
            this.m_iterationsLabel.Name = "m_iterationsLabel";
            this.m_iterationsLabel.Size = new System.Drawing.Size(71, 20);
            this.m_iterationsLabel.TabIndex = 1;
            this.m_iterationsLabel.Text = "Iterations";
            // 
            // m_gridSizelabel
            // 
            this.m_gridSizelabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_gridSizelabel.AutoSize = true;
            this.m_gridSizelabel.Location = new System.Drawing.Point(577, 414);
            this.m_gridSizelabel.Name = "m_gridSizelabel";
            this.m_gridSizelabel.Size = new System.Drawing.Size(40, 20);
            this.m_gridSizelabel.TabIndex = 5;
            this.m_gridSizelabel.Text = "Grid:";
            // 
            // m_gridWidthTextBox
            // 
            this.m_gridWidthTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_gridWidthTextBox.Location = new System.Drawing.Point(623, 411);
            this.m_gridWidthTextBox.Name = "m_gridWidthTextBox";
            this.m_gridWidthTextBox.Size = new System.Drawing.Size(50, 27);
            this.m_gridWidthTextBox.TabIndex = 6;
            // 
            // m_gridHeightTextBox
            // 
            this.m_gridHeightTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_gridHeightTextBox.Location = new System.Drawing.Point(693, 411);
            this.m_gridHeightTextBox.Name = "m_gridHeightTextBox";
            this.m_gridHeightTextBox.Size = new System.Drawing.Size(50, 27);
            this.m_gridHeightTextBox.TabIndex = 7;
            // 
            // m_xLabel
            // 
            this.m_xLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_xLabel.AutoSize = true;
            this.m_xLabel.Location = new System.Drawing.Point(675, 414);
            this.m_xLabel.Name = "m_xLabel";
            this.m_xLabel.Size = new System.Drawing.Size(16, 20);
            this.m_xLabel.TabIndex = 8;
            this.m_xLabel.Text = "x";
            // 
            // m_uMapCheckBox
            // 
            this.m_uMapCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_uMapCheckBox.AutoSize = true;
            this.m_uMapCheckBox.Location = new System.Drawing.Point(749, 414);
            this.m_uMapCheckBox.Name = "m_uMapCheckBox";
            this.m_uMapCheckBox.Size = new System.Drawing.Size(41, 24);
            this.m_uMapCheckBox.TabIndex = 9;
            this.m_uMapCheckBox.Text = "U";
            this.m_uMapCheckBox.UseVisualStyleBackColor = true;
            this.m_uMapCheckBox.CheckedChanged += new System.EventHandler(this.OnViewStyleChanged);
            // 
            // m_viewPanel
            // 
            this.m_viewPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_viewPanel.Controls.Add(this.m_uMapPlotView);
            this.m_viewPanel.Controls.Add(this.m_tableLayoutPanel);
            this.m_viewPanel.Location = new System.Drawing.Point(12, 12);
            this.m_viewPanel.Name = "m_viewPanel";
            this.m_viewPanel.Size = new System.Drawing.Size(776, 391);
            this.m_viewPanel.TabIndex = 11;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.m_viewPanel);
            this.Controls.Add(this.m_uMapCheckBox);
            this.Controls.Add(this.m_gridHeightTextBox);
            this.Controls.Add(this.m_gridWidthTextBox);
            this.Controls.Add(this.m_learningRateTextBox);
            this.Controls.Add(this.m_xLabel);
            this.Controls.Add(this.m_gridSizelabel);
            this.Controls.Add(this.m_learningRateLabel);
            this.Controls.Add(this.m_iterationsTextBox);
            this.Controls.Add(this.m_iterationsLabel);
            this.Controls.Add(this.m_trainButton);
            this.Name = "MainForm";
            this.Text = "Kohonen SOM (cars type classification)";
            this.Load += new System.EventHandler(this.OnLoad);
            this.m_viewPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button m_trainButton;
        private TableLayoutPanel m_tableLayoutPanel;
        private TextBox m_learningRateTextBox;
        private Label m_learningRateLabel;
        private TextBox m_iterationsTextBox;
        private Label m_iterationsLabel;
        private Label m_gridSizelabel;
        private TextBox m_gridWidthTextBox;
        private TextBox m_gridHeightTextBox;
        private Label m_xLabel;
        private OxyPlot.WindowsForms.PlotView m_uMapPlotView;
        private CheckBox m_uMapCheckBox;
        private Panel m_viewPanel;
    }
}
namespace IDZ
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
            this.m_runButton = new System.Windows.Forms.Button();
            this.m_plotView = new OxyPlot.WindowsForms.PlotView();
            this.m_tableListView = new System.Windows.Forms.ListView();
            this.m_tableXColumnHeader = new System.Windows.Forms.ColumnHeader();
            this.m_tableYColumnHeader = new System.Windows.Forms.ColumnHeader();
            this.m_tableLabel = new System.Windows.Forms.Label();
            this.m_polyListView = new System.Windows.Forms.ListView();
            this.m_polyDataColumnHeader = new System.Windows.Forms.ColumnHeader();
            this.m_polyValueColumnHeader = new System.Windows.Forms.ColumnHeader();
            this.m_ployLabel = new System.Windows.Forms.Label();
            this.m_optimizaLabel = new System.Windows.Forms.Label();
            this.m_optimizationListView = new System.Windows.Forms.ListView();
            this.m_optimizationDataColumnHeader = new System.Windows.Forms.ColumnHeader();
            this.m_optimizationValueColumnHeader = new System.Windows.Forms.ColumnHeader();
            this.SuspendLayout();
            // 
            // m_runButton
            // 
            this.m_runButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_runButton.Location = new System.Drawing.Point(1048, 745);
            this.m_runButton.Name = "m_runButton";
            this.m_runButton.Size = new System.Drawing.Size(94, 29);
            this.m_runButton.TabIndex = 0;
            this.m_runButton.Text = "Run";
            this.m_runButton.UseVisualStyleBackColor = true;
            this.m_runButton.Click += new System.EventHandler(this.OnRun);
            // 
            // m_plotView
            // 
            this.m_plotView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_plotView.BackColor = System.Drawing.Color.White;
            this.m_plotView.Location = new System.Drawing.Point(169, 12);
            this.m_plotView.Name = "m_plotView";
            this.m_plotView.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.m_plotView.Size = new System.Drawing.Size(973, 727);
            this.m_plotView.TabIndex = 1;
            this.m_plotView.Text = "plotView";
            this.m_plotView.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.m_plotView.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.m_plotView.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // m_tableListView
            // 
            this.m_tableListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.m_tableXColumnHeader,
            this.m_tableYColumnHeader});
            this.m_tableListView.FullRowSelect = true;
            this.m_tableListView.GridLines = true;
            this.m_tableListView.HideSelection = false;
            this.m_tableListView.Location = new System.Drawing.Point(12, 35);
            this.m_tableListView.MultiSelect = false;
            this.m_tableListView.Name = "m_tableListView";
            this.m_tableListView.Size = new System.Drawing.Size(151, 149);
            this.m_tableListView.TabIndex = 2;
            this.m_tableListView.UseCompatibleStateImageBehavior = false;
            this.m_tableListView.View = System.Windows.Forms.View.Details;
            // 
            // m_tableXColumnHeader
            // 
            this.m_tableXColumnHeader.Text = "X";
            // 
            // m_tableYColumnHeader
            // 
            this.m_tableYColumnHeader.Text = "Y";
            // 
            // m_tableLabel
            // 
            this.m_tableLabel.AutoSize = true;
            this.m_tableLabel.Location = new System.Drawing.Point(12, 12);
            this.m_tableLabel.Name = "m_tableLabel";
            this.m_tableLabel.Size = new System.Drawing.Size(44, 20);
            this.m_tableLabel.TabIndex = 3;
            this.m_tableLabel.Text = "Table";
            // 
            // m_polyListView
            // 
            this.m_polyListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.m_polyDataColumnHeader,
            this.m_polyValueColumnHeader});
            this.m_polyListView.FullRowSelect = true;
            this.m_polyListView.GridLines = true;
            this.m_polyListView.HideSelection = false;
            this.m_polyListView.Location = new System.Drawing.Point(12, 226);
            this.m_polyListView.Name = "m_polyListView";
            this.m_polyListView.Size = new System.Drawing.Size(151, 182);
            this.m_polyListView.TabIndex = 5;
            this.m_polyListView.UseCompatibleStateImageBehavior = false;
            this.m_polyListView.View = System.Windows.Forms.View.Details;
            // 
            // m_polyDataColumnHeader
            // 
            this.m_polyDataColumnHeader.Text = "Data";
            // 
            // m_polyValueColumnHeader
            // 
            this.m_polyValueColumnHeader.Text = "Value";
            // 
            // m_ployLabel
            // 
            this.m_ployLabel.AutoSize = true;
            this.m_ployLabel.Location = new System.Drawing.Point(12, 203);
            this.m_ployLabel.Name = "m_ployLabel";
            this.m_ployLabel.Size = new System.Drawing.Size(116, 20);
            this.m_ployLabel.TabIndex = 3;
            this.m_ployLabel.Text = "Polynomial data";
            // 
            // m_optimizaLabel
            // 
            this.m_optimizaLabel.AutoSize = true;
            this.m_optimizaLabel.Location = new System.Drawing.Point(12, 434);
            this.m_optimizaLabel.Name = "m_optimizaLabel";
            this.m_optimizaLabel.Size = new System.Drawing.Size(96, 20);
            this.m_optimizaLabel.TabIndex = 3;
            this.m_optimizaLabel.Text = "Optimization";
            // 
            // m_optimizationListView
            // 
            this.m_optimizationListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.m_optimizationDataColumnHeader,
            this.m_optimizationValueColumnHeader});
            this.m_optimizationListView.FullRowSelect = true;
            this.m_optimizationListView.GridLines = true;
            this.m_optimizationListView.HideSelection = false;
            this.m_optimizationListView.Location = new System.Drawing.Point(12, 457);
            this.m_optimizationListView.Name = "m_optimizationListView";
            this.m_optimizationListView.Size = new System.Drawing.Size(151, 147);
            this.m_optimizationListView.TabIndex = 6;
            this.m_optimizationListView.UseCompatibleStateImageBehavior = false;
            this.m_optimizationListView.View = System.Windows.Forms.View.Details;
            // 
            // m_optimizationDataColumnHeader
            // 
            this.m_optimizationDataColumnHeader.Text = "Data";
            // 
            // m_optimizationValueColumnHeader
            // 
            this.m_optimizationValueColumnHeader.Text = "Value";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1154, 786);
            this.Controls.Add(this.m_optimizationListView);
            this.Controls.Add(this.m_polyListView);
            this.Controls.Add(this.m_optimizaLabel);
            this.Controls.Add(this.m_ployLabel);
            this.Controls.Add(this.m_tableLabel);
            this.Controls.Add(this.m_tableListView);
            this.Controls.Add(this.m_plotView);
            this.Controls.Add(this.m_runButton);
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "MainForm";
            this.Text = "IDZ";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button m_runButton;
        private OxyPlot.WindowsForms.PlotView m_plotView;
        private System.Windows.Forms.ListView m_tableListView;
        private System.Windows.Forms.Label m_tableLabel;
        private System.Windows.Forms.ColumnHeader m_tableXColumnHeader;
        private System.Windows.Forms.ColumnHeader m_tableYColumnHeader;
        private System.Windows.Forms.ListView m_polyListView;
        private System.Windows.Forms.ColumnHeader m_polyDataColumnHeader;
        private System.Windows.Forms.ColumnHeader m_polyValueColumnHeader;
        private System.Windows.Forms.Label m_ployLabel;
        private System.Windows.Forms.Label m_optimizaLabel;
        private System.Windows.Forms.ListView m_optimizationListView;
        private System.Windows.Forms.ColumnHeader m_optimizationDataColumnHeader;
        private System.Windows.Forms.ColumnHeader m_optimizationValueColumnHeader;
    }
}

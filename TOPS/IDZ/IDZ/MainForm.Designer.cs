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
            this.m_tasksTabControl = new System.Windows.Forms.TabControl();
            this.m_polyTabPage = new System.Windows.Forms.TabPage();
            this.m_lpTaskTabPage = new System.Windows.Forms.TabPage();
            this.m_lpTasksTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.m_manufactureLabel = new System.Windows.Forms.Label();
            this.m_manufactureListView = new System.Windows.Forms.ListView();
            this.m_manufactureDataColumnHeader = new System.Windows.Forms.ColumnHeader();
            this.m_manufactureValueColumnHeader = new System.Windows.Forms.ColumnHeader();
            this.m_transportLabel = new System.Windows.Forms.Label();
            this.m_transportPlanListView = new System.Windows.Forms.ListView();
            this.m_transportDetailsListView = new System.Windows.Forms.ListView();
            this.m_transportDetailsDataColumnHeader = new System.Windows.Forms.ColumnHeader();
            this.m_transportDetailsValueColumnHeader = new System.Windows.Forms.ColumnHeader();
            this.m_tasksTabControl.SuspendLayout();
            this.m_polyTabPage.SuspendLayout();
            this.m_lpTaskTabPage.SuspendLayout();
            this.m_lpTasksTableLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_runButton
            // 
            this.m_runButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_runButton.Location = new System.Drawing.Point(991, 651);
            this.m_runButton.Name = "m_runButton";
            this.m_runButton.Size = new System.Drawing.Size(151, 29);
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
            this.m_plotView.Location = new System.Drawing.Point(163, 6);
            this.m_plotView.Name = "m_plotView";
            this.m_plotView.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.m_plotView.Size = new System.Drawing.Size(953, 580);
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
            this.m_tableListView.Location = new System.Drawing.Point(6, 27);
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
            this.m_tableLabel.Location = new System.Drawing.Point(6, 4);
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
            this.m_polyListView.Location = new System.Drawing.Point(6, 209);
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
            this.m_ployLabel.Location = new System.Drawing.Point(6, 186);
            this.m_ployLabel.Name = "m_ployLabel";
            this.m_ployLabel.Size = new System.Drawing.Size(116, 20);
            this.m_ployLabel.TabIndex = 3;
            this.m_ployLabel.Text = "Polynomial data";
            // 
            // m_optimizaLabel
            // 
            this.m_optimizaLabel.AutoSize = true;
            this.m_optimizaLabel.Location = new System.Drawing.Point(6, 405);
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
            this.m_optimizationListView.Location = new System.Drawing.Point(6, 428);
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
            // m_tasksTabControl
            // 
            this.m_tasksTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_tasksTabControl.Controls.Add(this.m_polyTabPage);
            this.m_tasksTabControl.Controls.Add(this.m_lpTaskTabPage);
            this.m_tasksTabControl.Location = new System.Drawing.Point(12, 12);
            this.m_tasksTabControl.Name = "m_tasksTabControl";
            this.m_tasksTabControl.SelectedIndex = 0;
            this.m_tasksTabControl.Size = new System.Drawing.Size(1130, 625);
            this.m_tasksTabControl.TabIndex = 7;
            // 
            // m_polyTabPage
            // 
            this.m_polyTabPage.Controls.Add(this.m_plotView);
            this.m_polyTabPage.Controls.Add(this.m_optimizationListView);
            this.m_polyTabPage.Controls.Add(this.m_tableListView);
            this.m_polyTabPage.Controls.Add(this.m_polyListView);
            this.m_polyTabPage.Controls.Add(this.m_tableLabel);
            this.m_polyTabPage.Controls.Add(this.m_optimizaLabel);
            this.m_polyTabPage.Controls.Add(this.m_ployLabel);
            this.m_polyTabPage.Location = new System.Drawing.Point(4, 29);
            this.m_polyTabPage.Name = "m_polyTabPage";
            this.m_polyTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.m_polyTabPage.Size = new System.Drawing.Size(1122, 592);
            this.m_polyTabPage.TabIndex = 0;
            this.m_polyTabPage.Text = "Interpolation Task";
            this.m_polyTabPage.UseVisualStyleBackColor = true;
            // 
            // m_lpTaskTabPage
            // 
            this.m_lpTaskTabPage.Controls.Add(this.m_lpTasksTableLayoutPanel);
            this.m_lpTaskTabPage.Location = new System.Drawing.Point(4, 29);
            this.m_lpTaskTabPage.Name = "m_lpTaskTabPage";
            this.m_lpTaskTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.m_lpTaskTabPage.Size = new System.Drawing.Size(1122, 592);
            this.m_lpTaskTabPage.TabIndex = 1;
            this.m_lpTaskTabPage.Text = "Linear Programming Tasks";
            this.m_lpTaskTabPage.UseVisualStyleBackColor = true;
            // 
            // m_lpTasksTableLayoutPanel
            // 
            this.m_lpTasksTableLayoutPanel.ColumnCount = 2;
            this.m_lpTasksTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.m_lpTasksTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.m_lpTasksTableLayoutPanel.Controls.Add(this.m_manufactureLabel, 0, 0);
            this.m_lpTasksTableLayoutPanel.Controls.Add(this.m_manufactureListView, 0, 1);
            this.m_lpTasksTableLayoutPanel.Controls.Add(this.m_transportLabel, 1, 0);
            this.m_lpTasksTableLayoutPanel.Controls.Add(this.m_transportPlanListView, 1, 1);
            this.m_lpTasksTableLayoutPanel.Controls.Add(this.m_transportDetailsListView, 1, 2);
            this.m_lpTasksTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_lpTasksTableLayoutPanel.Location = new System.Drawing.Point(3, 3);
            this.m_lpTasksTableLayoutPanel.Name = "m_lpTasksTableLayoutPanel";
            this.m_lpTasksTableLayoutPanel.RowCount = 3;
            this.m_lpTasksTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.m_lpTasksTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.m_lpTasksTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.m_lpTasksTableLayoutPanel.Size = new System.Drawing.Size(1116, 586);
            this.m_lpTasksTableLayoutPanel.TabIndex = 2;
            // 
            // m_manufactureLabel
            // 
            this.m_manufactureLabel.AutoSize = true;
            this.m_manufactureLabel.Location = new System.Drawing.Point(3, 0);
            this.m_manufactureLabel.Name = "m_manufactureLabel";
            this.m_manufactureLabel.Size = new System.Drawing.Size(123, 20);
            this.m_manufactureLabel.TabIndex = 0;
            this.m_manufactureLabel.Text = "Manufacture Task";
            // 
            // m_manufactureListView
            // 
            this.m_manufactureListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.m_manufactureDataColumnHeader,
            this.m_manufactureValueColumnHeader});
            this.m_manufactureListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_manufactureListView.FullRowSelect = true;
            this.m_manufactureListView.GridLines = true;
            this.m_manufactureListView.HideSelection = false;
            this.m_manufactureListView.Location = new System.Drawing.Point(3, 23);
            this.m_manufactureListView.Name = "m_manufactureListView";
            this.m_lpTasksTableLayoutPanel.SetRowSpan(this.m_manufactureListView, 2);
            this.m_manufactureListView.Size = new System.Drawing.Size(552, 560);
            this.m_manufactureListView.TabIndex = 1;
            this.m_manufactureListView.UseCompatibleStateImageBehavior = false;
            this.m_manufactureListView.View = System.Windows.Forms.View.Details;
            // 
            // m_manufactureDataColumnHeader
            // 
            this.m_manufactureDataColumnHeader.Text = "Data";
            // 
            // m_manufactureValueColumnHeader
            // 
            this.m_manufactureValueColumnHeader.Text = "Value";
            // 
            // m_transportLabel
            // 
            this.m_transportLabel.AutoSize = true;
            this.m_transportLabel.Location = new System.Drawing.Point(561, 0);
            this.m_transportLabel.Name = "m_transportLabel";
            this.m_transportLabel.Size = new System.Drawing.Size(102, 20);
            this.m_transportLabel.TabIndex = 0;
            this.m_transportLabel.Text = "Transport Task";
            // 
            // m_transportPlanListView
            // 
            this.m_transportPlanListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_transportPlanListView.FullRowSelect = true;
            this.m_transportPlanListView.GridLines = true;
            this.m_transportPlanListView.HideSelection = false;
            this.m_transportPlanListView.Location = new System.Drawing.Point(561, 23);
            this.m_transportPlanListView.Name = "m_transportPlanListView";
            this.m_transportPlanListView.Size = new System.Drawing.Size(552, 333);
            this.m_transportPlanListView.TabIndex = 2;
            this.m_transportPlanListView.UseCompatibleStateImageBehavior = false;
            this.m_transportPlanListView.View = System.Windows.Forms.View.Details;
            // 
            // m_transportDetailsListView
            // 
            this.m_transportDetailsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.m_transportDetailsDataColumnHeader,
            this.m_transportDetailsValueColumnHeader});
            this.m_transportDetailsListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_transportDetailsListView.FullRowSelect = true;
            this.m_transportDetailsListView.GridLines = true;
            this.m_transportDetailsListView.HideSelection = false;
            this.m_transportDetailsListView.Location = new System.Drawing.Point(561, 362);
            this.m_transportDetailsListView.Name = "m_transportDetailsListView";
            this.m_transportDetailsListView.Size = new System.Drawing.Size(552, 221);
            this.m_transportDetailsListView.TabIndex = 3;
            this.m_transportDetailsListView.UseCompatibleStateImageBehavior = false;
            this.m_transportDetailsListView.View = System.Windows.Forms.View.Details;
            // 
            // m_transportDetailsDataColumnHeader
            // 
            this.m_transportDetailsDataColumnHeader.Text = "Data";
            // 
            // m_transportDetailsValueColumnHeader
            // 
            this.m_transportDetailsValueColumnHeader.Text = "Value";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1154, 692);
            this.Controls.Add(this.m_tasksTabControl);
            this.Controls.Add(this.m_runButton);
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "MainForm";
            this.Text = "IDZ";
            this.m_tasksTabControl.ResumeLayout(false);
            this.m_polyTabPage.ResumeLayout(false);
            this.m_polyTabPage.PerformLayout();
            this.m_lpTaskTabPage.ResumeLayout(false);
            this.m_lpTasksTableLayoutPanel.ResumeLayout(false);
            this.m_lpTasksTableLayoutPanel.PerformLayout();
            this.ResumeLayout(false);

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
        private System.Windows.Forms.TabControl m_tasksTabControl;
        private System.Windows.Forms.TabPage m_polyTabPage;
        private System.Windows.Forms.TabPage m_lpTaskTabPage;
        private System.Windows.Forms.TableLayoutPanel m_lpTasksTableLayoutPanel;
        private System.Windows.Forms.Label m_manufactureLabel;
        private System.Windows.Forms.ListView m_manufactureListView;
        private System.Windows.Forms.Label m_transportLabel;
        private System.Windows.Forms.ListView m_transportPlanListView;
        private System.Windows.Forms.ColumnHeader m_manufactureDataColumnHeader;
        private System.Windows.Forms.ColumnHeader m_manufactureValueColumnHeader;
        private System.Windows.Forms.ListView m_transportDetailsListView;
        private System.Windows.Forms.ColumnHeader m_transportDetailsDataColumnHeader;
        private System.Windows.Forms.ColumnHeader m_transportDetailsValueColumnHeader;
    }
}

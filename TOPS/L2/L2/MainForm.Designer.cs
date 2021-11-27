
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
            this.m_runButton = new System.Windows.Forms.Button();
            this.m_listView = new System.Windows.Forms.ListView();
            this.m_methodColumn = new System.Windows.Forms.ColumnHeader();
            this.m_attemptsColumn = new System.Windows.Forms.ColumnHeader();
            this.m_avgTimeColumn = new System.Windows.Forms.ColumnHeader();
            this.m_stepsColumnt = new System.Windows.Forms.ColumnHeader();
            this.m_minFColumn = new System.Windows.Forms.ColumnHeader();
            this.m_minXColumn = new System.Windows.Forms.ColumnHeader();
            this.m_timePlotView = new OxyPlot.WindowsForms.PlotView();
            this.m_plotsTableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.m_stespPlotView = new OxyPlot.WindowsForms.PlotView();
            this.m_plotsTableLayout.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_runButton
            // 
            this.m_runButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_runButton.Location = new System.Drawing.Point(754, 489);
            this.m_runButton.Name = "m_runButton";
            this.m_runButton.Size = new System.Drawing.Size(94, 29);
            this.m_runButton.TabIndex = 0;
            this.m_runButton.Text = "Run";
            this.m_runButton.UseVisualStyleBackColor = true;
            this.m_runButton.Click += new System.EventHandler(this.OnRun);
            // 
            // m_listView
            // 
            this.m_listView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.m_methodColumn,
            this.m_attemptsColumn,
            this.m_avgTimeColumn,
            this.m_stepsColumnt,
            this.m_minFColumn,
            this.m_minXColumn});
            this.m_listView.FullRowSelect = true;
            this.m_listView.GridLines = true;
            this.m_listView.HideSelection = false;
            this.m_listView.Location = new System.Drawing.Point(12, 322);
            this.m_listView.Name = "m_listView";
            this.m_listView.Size = new System.Drawing.Size(836, 161);
            this.m_listView.TabIndex = 1;
            this.m_listView.UseCompatibleStateImageBehavior = false;
            this.m_listView.View = System.Windows.Forms.View.Details;
            // 
            // m_methodColumn
            // 
            this.m_methodColumn.Text = "Method";
            // 
            // m_attemptsColumn
            // 
            this.m_attemptsColumn.Text = "Attempts";
            // 
            // m_avgTimeColumn
            // 
            this.m_avgTimeColumn.Text = "Time.avg (ms)";
            // 
            // m_stepsColumnt
            // 
            this.m_stepsColumnt.Text = "Steps";
            // 
            // m_minFColumn
            // 
            this.m_minFColumn.Text = "F(min(x))";
            // 
            // m_minXColumn
            // 
            this.m_minXColumn.Text = "min(X)";
            // 
            // m_timePlotView
            // 
            this.m_timePlotView.BackColor = System.Drawing.Color.White;
            this.m_timePlotView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_timePlotView.Location = new System.Drawing.Point(3, 3);
            this.m_timePlotView.Name = "m_timePlotView";
            this.m_timePlotView.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.m_timePlotView.Size = new System.Drawing.Size(412, 298);
            this.m_timePlotView.TabIndex = 2;
            this.m_timePlotView.Text = "plotView";
            this.m_timePlotView.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.m_timePlotView.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.m_timePlotView.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // m_plotsTableLayout
            // 
            this.m_plotsTableLayout.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_plotsTableLayout.ColumnCount = 2;
            this.m_plotsTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.m_plotsTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.m_plotsTableLayout.Controls.Add(this.m_timePlotView, 0, 0);
            this.m_plotsTableLayout.Controls.Add(this.m_stespPlotView, 1, 0);
            this.m_plotsTableLayout.Location = new System.Drawing.Point(12, 12);
            this.m_plotsTableLayout.Name = "m_plotsTableLayout";
            this.m_plotsTableLayout.RowCount = 1;
            this.m_plotsTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.m_plotsTableLayout.Size = new System.Drawing.Size(836, 304);
            this.m_plotsTableLayout.TabIndex = 3;
            // 
            // m_stespPlotView
            // 
            this.m_stespPlotView.BackColor = System.Drawing.Color.White;
            this.m_stespPlotView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_stespPlotView.Location = new System.Drawing.Point(421, 3);
            this.m_stespPlotView.Name = "m_stespPlotView";
            this.m_stespPlotView.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.m_stespPlotView.Size = new System.Drawing.Size(412, 298);
            this.m_stespPlotView.TabIndex = 3;
            this.m_stespPlotView.Text = "plotView1";
            this.m_stespPlotView.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.m_stespPlotView.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.m_stespPlotView.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(860, 530);
            this.Controls.Add(this.m_plotsTableLayout);
            this.Controls.Add(this.m_listView);
            this.Controls.Add(this.m_runButton);
            this.MinimumSize = new System.Drawing.Size(800, 500);
            this.Name = "MainForm";
            this.Text = "Minimizers comparision";
            this.Load += new System.EventHandler(this.OnLoad);
            this.m_plotsTableLayout.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button m_runButton;
        private System.Windows.Forms.ListView m_listView;
        private System.Windows.Forms.ColumnHeader m_methodColumn;
        private System.Windows.Forms.ColumnHeader m_attemptsColumn;
        private System.Windows.Forms.ColumnHeader m_avgTimeColumn;
        private System.Windows.Forms.ColumnHeader m_stepsColumnt;
        private System.Windows.Forms.ColumnHeader m_minFColumn;
        private System.Windows.Forms.ColumnHeader m_minXColumn;
        private OxyPlot.WindowsForms.PlotView m_timePlotView;
        private System.Windows.Forms.TableLayoutPanel m_plotsTableLayout;
        private OxyPlot.WindowsForms.PlotView m_stespPlotView;
    }
}


namespace L3
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
            this.m_run = new System.Windows.Forms.Button();
            this.m_pricePlotView = new OxyPlot.WindowsForms.PlotView();
            this.m_listView = new System.Windows.Forms.ListView();
            this.m_dataColumnHeader = new System.Windows.Forms.ColumnHeader();
            this.m_valueColumnHeader = new System.Windows.Forms.ColumnHeader();
            this.SuspendLayout();
            // 
            // m_run
            // 
            this.m_run.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_run.Location = new System.Drawing.Point(1007, 596);
            this.m_run.Name = "m_run";
            this.m_run.Size = new System.Drawing.Size(94, 29);
            this.m_run.TabIndex = 0;
            this.m_run.Text = "Run";
            this.m_run.UseVisualStyleBackColor = true;
            this.m_run.Click += new System.EventHandler(this.OnRun);
            // 
            // m_pricePlotView
            // 
            this.m_pricePlotView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_pricePlotView.Location = new System.Drawing.Point(257, 12);
            this.m_pricePlotView.Name = "m_pricePlotView";
            this.m_pricePlotView.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.m_pricePlotView.Size = new System.Drawing.Size(840, 578);
            this.m_pricePlotView.TabIndex = 1;
            this.m_pricePlotView.Text = "plotView1";
            this.m_pricePlotView.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.m_pricePlotView.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.m_pricePlotView.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // m_listView
            // 
            this.m_listView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.m_listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.m_dataColumnHeader,
            this.m_valueColumnHeader});
            this.m_listView.FullRowSelect = true;
            this.m_listView.GridLines = true;
            this.m_listView.Location = new System.Drawing.Point(12, 12);
            this.m_listView.Name = "m_listView";
            this.m_listView.Size = new System.Drawing.Size(224, 578);
            this.m_listView.TabIndex = 2;
            this.m_listView.UseCompatibleStateImageBehavior = false;
            this.m_listView.View = System.Windows.Forms.View.Details;
            // 
            // m_dataColumnHeader
            // 
            this.m_dataColumnHeader.Text = "Param";
            // 
            // m_valueColumnHeader
            // 
            this.m_valueColumnHeader.Text = "Value";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1113, 637);
            this.Controls.Add(this.m_listView);
            this.Controls.Add(this.m_pricePlotView);
            this.Controls.Add(this.m_run);
            this.MinimumSize = new System.Drawing.Size(1000, 600);
            this.Name = "MainForm";
            this.Text = "Linear Programming Problem";
            this.ResumeLayout(false);

        }

        #endregion

        private Button m_run;
        private OxyPlot.WindowsForms.PlotView m_pricePlotView;
        private ListView m_listView;
        private ColumnHeader m_dataColumnHeader;
        private ColumnHeader m_valueColumnHeader;
    }
}
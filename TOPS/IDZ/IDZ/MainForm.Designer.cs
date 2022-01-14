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
            this.m_plotView.Location = new System.Drawing.Point(172, 12);
            this.m_plotView.Name = "m_plotView";
            this.m_plotView.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.m_plotView.Size = new System.Drawing.Size(970, 727);
            this.m_plotView.TabIndex = 1;
            this.m_plotView.Text = "plotView";
            this.m_plotView.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.m_plotView.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.m_plotView.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1154, 786);
            this.Controls.Add(this.m_plotView);
            this.Controls.Add(this.m_runButton);
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "MainForm";
            this.Text = "IDZ";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button m_runButton;
        private OxyPlot.WindowsForms.PlotView m_plotView;
    }
}

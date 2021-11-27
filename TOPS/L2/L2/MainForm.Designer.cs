
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
            this.SuspendLayout();
            // 
            // m_runButton
            // 
            this.m_runButton.Location = new System.Drawing.Point(694, 409);
            this.m_runButton.Name = "m_runButton";
            this.m_runButton.Size = new System.Drawing.Size(94, 29);
            this.m_runButton.TabIndex = 0;
            this.m_runButton.Text = "Run";
            this.m_runButton.UseVisualStyleBackColor = true;
            this.m_runButton.Click += new System.EventHandler(this.OnRun);
            // 
            // m_listView
            // 
            this.m_listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.m_methodColumn,
            this.m_attemptsColumn,
            this.m_avgTimeColumn,
            this.m_stepsColumnt,
            this.m_minFColumn,
            this.m_minXColumn});
            this.m_listView.HideSelection = false;
            this.m_listView.Location = new System.Drawing.Point(12, 208);
            this.m_listView.Name = "m_listView";
            this.m_listView.Size = new System.Drawing.Size(776, 195);
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
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.m_listView);
            this.Controls.Add(this.m_runButton);
            this.Name = "MainForm";
            this.Text = "Minimizers comparision";
            this.Load += new System.EventHandler(this.OnLoad);
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
    }
}


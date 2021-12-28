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
            this.SuspendLayout();
            // 
            // m_run
            // 
            this.m_run.Location = new System.Drawing.Point(694, 409);
            this.m_run.Name = "m_run";
            this.m_run.Size = new System.Drawing.Size(94, 29);
            this.m_run.TabIndex = 0;
            this.m_run.Text = "Run";
            this.m_run.UseVisualStyleBackColor = true;
            this.m_run.Click += new System.EventHandler(this.OnRun);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.m_run);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private Button m_run;
    }
}
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
            this.m_tableLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_tableLayoutPanel.ColumnCount = 1;
            this.m_tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.m_tableLayoutPanel.Location = new System.Drawing.Point(12, 12);
            this.m_tableLayoutPanel.Name = "m_tableLayoutPanel";
            this.m_tableLayoutPanel.RowCount = 1;
            this.m_tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.m_tableLayoutPanel.Size = new System.Drawing.Size(776, 391);
            this.m_tableLayoutPanel.TabIndex = 1;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.m_tableLayoutPanel);
            this.Controls.Add(this.m_trainButton);
            this.Name = "MainForm";
            this.Text = "Kohonen SOM (cars type classification)";
            this.ResumeLayout(false);

        }

        #endregion

        private Button m_trainButton;
        private TableLayoutPanel m_tableLayoutPanel;
    }
}
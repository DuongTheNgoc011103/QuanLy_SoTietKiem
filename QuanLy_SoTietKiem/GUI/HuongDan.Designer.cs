namespace QuanLy_SoTietKiem.GUI
{
    partial class HuongDan
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlTop_Title = new System.Windows.Forms.Panel();
            this.pnlFillMain = new System.Windows.Forms.Panel();
            this.panel3.SuspendLayout();
            this.panel.SuspendLayout();
            this.pnlTop_Title.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.pnlFillMain);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1924, 807);
            this.panel3.TabIndex = 1;
            // 
            // panel
            // 
            this.panel.Controls.Add(this.panel3);
            this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel.Location = new System.Drawing.Point(0, 66);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(1924, 807);
            this.panel.TabIndex = 4;
            // 
            // lblTitle
            // 
            this.lblTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Tahoma", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(798, 13);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(387, 40);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "TÀI LIỆU HƯỚNG DẪN";
            // 
            // pnlTop_Title
            // 
            this.pnlTop_Title.Controls.Add(this.lblTitle);
            this.pnlTop_Title.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop_Title.Location = new System.Drawing.Point(0, 0);
            this.pnlTop_Title.Name = "pnlTop_Title";
            this.pnlTop_Title.Size = new System.Drawing.Size(1924, 66);
            this.pnlTop_Title.TabIndex = 3;
            // 
            // pnlFillMain
            // 
            this.pnlFillMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlFillMain.Location = new System.Drawing.Point(0, 0);
            this.pnlFillMain.Name = "pnlFillMain";
            this.pnlFillMain.Size = new System.Drawing.Size(1924, 807);
            this.pnlFillMain.TabIndex = 0;
            // 
            // HuongDan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1924, 873);
            this.Controls.Add(this.panel);
            this.Controls.Add(this.pnlTop_Title);
            this.Name = "HuongDan";
            this.Text = "TÀI LIỆU HƯỚNG DẪN";
            this.panel3.ResumeLayout(false);
            this.panel.ResumeLayout(false);
            this.pnlTop_Title.ResumeLayout(false);
            this.pnlTop_Title.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlTop_Title;
        private System.Windows.Forms.Panel pnlFillMain;
    }
}
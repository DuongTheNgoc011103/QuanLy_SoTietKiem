namespace QuanLy_SoTietKiem.GUI
{
    partial class ThongKe_GiaoDich_TheoNgay
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ThongKe_GiaoDich_TheoNgay));
            this.pnlTop_Title = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnl_Filter = new System.Windows.Forms.Panel();
            this.pnl_Content = new System.Windows.Forms.Panel();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.grp_Content = new System.Windows.Forms.GroupBox();
            this.grp_Filter = new System.Windows.Forms.GroupBox();
            this.dtp_StartDate = new System.Windows.Forms.DateTimePicker();
            this.dtp_EndDate = new System.Windows.Forms.DateTimePicker();
            this.btnFilter = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pnlTop_Title.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pnl_Filter.SuspendLayout();
            this.pnl_Content.SuspendLayout();
            this.grp_Content.SuspendLayout();
            this.grp_Filter.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTop_Title
            // 
            this.pnlTop_Title.Controls.Add(this.lblTitle);
            this.pnlTop_Title.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop_Title.Location = new System.Drawing.Point(0, 0);
            this.pnlTop_Title.Name = "pnlTop_Title";
            this.pnlTop_Title.Size = new System.Drawing.Size(1924, 66);
            this.pnlTop_Title.TabIndex = 4;
            // 
            // lblTitle
            // 
            this.lblTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Tahoma", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(717, 13);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(789, 40);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "THỐNG KÊ - BÁO CÁO: GIAO DỊCH THEO NGÀY";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pnl_Content);
            this.panel1.Controls.Add(this.pnl_Filter);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 66);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1924, 807);
            this.panel1.TabIndex = 5;
            // 
            // pnl_Filter
            // 
            this.pnl_Filter.Controls.Add(this.grp_Filter);
            this.pnl_Filter.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnl_Filter.Location = new System.Drawing.Point(0, 0);
            this.pnl_Filter.Name = "pnl_Filter";
            this.pnl_Filter.Size = new System.Drawing.Size(539, 807);
            this.pnl_Filter.TabIndex = 0;
            // 
            // pnl_Content
            // 
            this.pnl_Content.Controls.Add(this.grp_Content);
            this.pnl_Content.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_Content.Location = new System.Drawing.Point(539, 0);
            this.pnl_Content.Name = "pnl_Content";
            this.pnl_Content.Size = new System.Drawing.Size(1385, 807);
            this.pnl_Content.TabIndex = 1;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reportViewer1.DocumentMapWidth = 90;
            this.reportViewer1.Location = new System.Drawing.Point(3, 30);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(1379, 774);
            this.reportViewer1.TabIndex = 0;
            // 
            // grp_Content
            // 
            this.grp_Content.Controls.Add(this.reportViewer1);
            this.grp_Content.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grp_Content.Font = new System.Drawing.Font("Tahoma", 13.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grp_Content.Location = new System.Drawing.Point(0, 0);
            this.grp_Content.Name = "grp_Content";
            this.grp_Content.Size = new System.Drawing.Size(1385, 807);
            this.grp_Content.TabIndex = 0;
            this.grp_Content.TabStop = false;
            this.grp_Content.Text = "Kết Quả Thống Kê - Báo Cáo:";
            // 
            // grp_Filter
            // 
            this.grp_Filter.Controls.Add(this.label2);
            this.grp_Filter.Controls.Add(this.label1);
            this.grp_Filter.Controls.Add(this.btnFilter);
            this.grp_Filter.Controls.Add(this.dtp_EndDate);
            this.grp_Filter.Controls.Add(this.dtp_StartDate);
            this.grp_Filter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grp_Filter.Font = new System.Drawing.Font("Tahoma", 13.2F);
            this.grp_Filter.Location = new System.Drawing.Point(0, 0);
            this.grp_Filter.Name = "grp_Filter";
            this.grp_Filter.Size = new System.Drawing.Size(539, 807);
            this.grp_Filter.TabIndex = 0;
            this.grp_Filter.TabStop = false;
            this.grp_Filter.Text = "Thông tin";
            // 
            // dtp_StartDate
            // 
            this.dtp_StartDate.Location = new System.Drawing.Point(12, 82);
            this.dtp_StartDate.Name = "dtp_StartDate";
            this.dtp_StartDate.Size = new System.Drawing.Size(521, 34);
            this.dtp_StartDate.TabIndex = 0;
            // 
            // dtp_EndDate
            // 
            this.dtp_EndDate.Location = new System.Drawing.Point(12, 179);
            this.dtp_EndDate.Name = "dtp_EndDate";
            this.dtp_EndDate.Size = new System.Drawing.Size(521, 34);
            this.dtp_EndDate.TabIndex = 1;
            // 
            // btnFilter
            // 
            this.btnFilter.BackColor = System.Drawing.SystemColors.HotTrack;
            this.btnFilter.ForeColor = System.Drawing.Color.White;
            this.btnFilter.Image = ((System.Drawing.Image)(resources.GetObject("btnFilter.Image")));
            this.btnFilter.Location = new System.Drawing.Point(148, 237);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(232, 45);
            this.btnFilter.TabIndex = 2;
            this.btnFilter.Text = "THỐNG KÊ";
            this.btnFilter.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnFilter.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnFilter.UseVisualStyleBackColor = false;
            this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 28);
            this.label1.TabIndex = 3;
            this.label1.Text = "Từ Ngày:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 148);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(119, 28);
            this.label2.TabIndex = 4;
            this.label2.Text = "Đến Ngày:";
            // 
            // ThongKe_GiaoDich_TheoNgay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1924, 873);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pnlTop_Title);
            this.Name = "ThongKe_GiaoDich_TheoNgay";
            this.Text = "THỐNG KÊ GIAO DỊCH THEO NGÀY";
            this.Load += new System.EventHandler(this.ThongKe_GiaoDich_TheoNgay_Load);
            this.pnlTop_Title.ResumeLayout(false);
            this.pnlTop_Title.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.pnl_Filter.ResumeLayout(false);
            this.pnl_Content.ResumeLayout(false);
            this.grp_Content.ResumeLayout(false);
            this.grp_Filter.ResumeLayout(false);
            this.grp_Filter.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTop_Title;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel pnl_Filter;
        private System.Windows.Forms.Panel pnl_Content;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.GroupBox grp_Content;
        private System.Windows.Forms.GroupBox grp_Filter;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnFilter;
        private System.Windows.Forms.DateTimePicker dtp_EndDate;
        private System.Windows.Forms.DateTimePicker dtp_StartDate;
    }
}
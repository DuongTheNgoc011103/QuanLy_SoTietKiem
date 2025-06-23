namespace QuanLy_SoTietKiem.GUI
{
    partial class QuanLy_ChiNhanh
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QuanLy_ChiNhanh));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.group_ChiNhanh = new System.Windows.Forms.GroupBox();
            this.panel9 = new System.Windows.Forms.Panel();
            this.dgvDSChiNhanh = new System.Windows.Forms.DataGridView();
            this.MaCN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TenChiNhanh = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DiaChi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NguoiQuanLy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel8 = new System.Windows.Forms.Panel();
            this.btnReLoad = new System.Windows.Forms.Button();
            this.btnReport = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.txtTotalPages = new System.Windows.Forms.TextBox();
            this.txtToTalCN = new System.Windows.Forms.TextBox();
            this.txtPage = new System.Windows.Forms.TextBox();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnPrev = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lblToTal = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupInfor = new System.Windows.Forms.GroupBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.txtDiaChi = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtNguoiQuanLy = new System.Windows.Forms.TextBox();
            this.txtTenCN = new System.Windows.Forms.TextBox();
            this.txtMaCN = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.groupDSNhanVien = new System.Windows.Forms.GroupBox();
            this.dgvDS_NhanVien = new System.Windows.Forms.DataGridView();
            this.MaNV = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HoTen = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ChucVu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            this.group_ChiNhanh.SuspendLayout();
            this.panel9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDSChiNhanh)).BeginInit();
            this.panel8.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupInfor.SuspendLayout();
            this.panel4.SuspendLayout();
            this.groupDSNhanVien.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDS_NhanVien)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblTitle);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1924, 72);
            this.panel1.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Tahoma", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(804, 16);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(371, 40);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "QUẢN LÝ CHI NHÁNH";
            // 
            // group_ChiNhanh
            // 
            this.group_ChiNhanh.Controls.Add(this.panel9);
            this.group_ChiNhanh.Controls.Add(this.panel8);
            this.group_ChiNhanh.Controls.Add(this.panel6);
            this.group_ChiNhanh.Dock = System.Windows.Forms.DockStyle.Fill;
            this.group_ChiNhanh.Font = new System.Drawing.Font("Tahoma", 13.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.group_ChiNhanh.ForeColor = System.Drawing.SystemColors.ControlText;
            this.group_ChiNhanh.Location = new System.Drawing.Point(0, 0);
            this.group_ChiNhanh.Name = "group_ChiNhanh";
            this.group_ChiNhanh.Size = new System.Drawing.Size(1239, 639);
            this.group_ChiNhanh.TabIndex = 0;
            this.group_ChiNhanh.TabStop = false;
            this.group_ChiNhanh.Text = "Danh Sách Các Chi Nhánh";
            // 
            // panel9
            // 
            this.panel9.Controls.Add(this.dgvDSChiNhanh);
            this.panel9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel9.Location = new System.Drawing.Point(3, 82);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(1233, 493);
            this.panel9.TabIndex = 3;
            // 
            // dgvDSChiNhanh
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.dgvDSChiNhanh.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvDSChiNhanh.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDSChiNhanh.BackgroundColor = System.Drawing.SystemColors.InactiveCaption;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.HotTrack;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 13.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDSChiNhanh.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvDSChiNhanh.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDSChiNhanh.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.MaCN,
            this.TenChiNhanh,
            this.DiaChi,
            this.NguoiQuanLy});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Tahoma", 13.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDSChiNhanh.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvDSChiNhanh.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDSChiNhanh.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvDSChiNhanh.EnableHeadersVisualStyles = false;
            this.dgvDSChiNhanh.Location = new System.Drawing.Point(0, 0);
            this.dgvDSChiNhanh.Name = "dgvDSChiNhanh";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.HotTrack;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Tahoma", 13.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDSChiNhanh.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvDSChiNhanh.RowHeadersWidth = 51;
            this.dgvDSChiNhanh.RowTemplate.Height = 30;
            this.dgvDSChiNhanh.Size = new System.Drawing.Size(1233, 493);
            this.dgvDSChiNhanh.TabIndex = 0;
            this.dgvDSChiNhanh.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDSChiNhanh_CellClick);
            // 
            // MaCN
            // 
            this.MaCN.DataPropertyName = "MaCN";
            this.MaCN.FillWeight = 62.03209F;
            this.MaCN.HeaderText = "Mã Chi Nhánh";
            this.MaCN.MinimumWidth = 6;
            this.MaCN.Name = "MaCN";
            // 
            // TenChiNhanh
            // 
            this.TenChiNhanh.DataPropertyName = "TenChiNhanh";
            this.TenChiNhanh.FillWeight = 60.59462F;
            this.TenChiNhanh.HeaderText = "Tên Chi Nhánh";
            this.TenChiNhanh.MinimumWidth = 6;
            this.TenChiNhanh.Name = "TenChiNhanh";
            // 
            // DiaChi
            // 
            this.DiaChi.DataPropertyName = "DiaChi";
            this.DiaChi.FillWeight = 60.59462F;
            this.DiaChi.HeaderText = "Địa Chỉ";
            this.DiaChi.MinimumWidth = 6;
            this.DiaChi.Name = "DiaChi";
            // 
            // NguoiQuanLy
            // 
            this.NguoiQuanLy.DataPropertyName = "NguoiQuanLy";
            this.NguoiQuanLy.FillWeight = 60.59462F;
            this.NguoiQuanLy.HeaderText = "Người Quản Lý";
            this.NguoiQuanLy.MinimumWidth = 6;
            this.NguoiQuanLy.Name = "NguoiQuanLy";
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.btnReLoad);
            this.panel8.Controls.Add(this.btnReport);
            this.panel8.Controls.Add(this.btnSearch);
            this.panel8.Controls.Add(this.txtSearch);
            this.panel8.Controls.Add(this.label2);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel8.Location = new System.Drawing.Point(3, 30);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(1233, 52);
            this.panel8.TabIndex = 2;
            // 
            // btnReLoad
            // 
            this.btnReLoad.Image = ((System.Drawing.Image)(resources.GetObject("btnReLoad.Image")));
            this.btnReLoad.Location = new System.Drawing.Point(952, 4);
            this.btnReLoad.Name = "btnReLoad";
            this.btnReLoad.Size = new System.Drawing.Size(167, 43);
            this.btnReLoad.TabIndex = 4;
            this.btnReLoad.Text = "Làm Mới";
            this.btnReLoad.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnReLoad.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnReLoad.UseVisualStyleBackColor = true;
            this.btnReLoad.Click += new System.EventHandler(this.btnReLoad_Click);
            // 
            // btnReport
            // 
            this.btnReport.BackColor = System.Drawing.SystemColors.HotTrack;
            this.btnReport.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnReport.ForeColor = System.Drawing.Color.White;
            this.btnReport.Image = ((System.Drawing.Image)(resources.GetObject("btnReport.Image")));
            this.btnReport.Location = new System.Drawing.Point(1001, 0);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(232, 52);
            this.btnReport.TabIndex = 3;
            this.btnReport.Text = "IN DANH SÁCH";
            this.btnReport.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnReport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnReport.UseVisualStyleBackColor = false;
            this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.SystemColors.HotTrack;
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Image = ((System.Drawing.Image)(resources.GetObject("btnSearch.Image")));
            this.btnSearch.Location = new System.Drawing.Point(764, 3);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(184, 46);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.Text = "Tìm Kiếm";
            this.btnSearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSearch.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.BackColor = System.Drawing.SystemColors.Info;
            this.txtSearch.Location = new System.Drawing.Point(189, 10);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(569, 34);
            this.txtSearch.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(164, 27);
            this.label2.TabIndex = 0;
            this.label2.Text = "Tên Chi Nhánh:";
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.label7);
            this.panel6.Controls.Add(this.txtTotalPages);
            this.panel6.Controls.Add(this.txtToTalCN);
            this.panel6.Controls.Add(this.txtPage);
            this.panel6.Controls.Add(this.btnNext);
            this.panel6.Controls.Add(this.btnPrev);
            this.panel6.Controls.Add(this.label1);
            this.panel6.Controls.Add(this.lblToTal);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel6.Location = new System.Drawing.Point(3, 575);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(1233, 61);
            this.panel6.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(282, 10);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(32, 36);
            this.label7.TabIndex = 7;
            this.label7.Text = "/";
            // 
            // txtTotalPages
            // 
            this.txtTotalPages.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTotalPages.Enabled = false;
            this.txtTotalPages.Location = new System.Drawing.Point(320, 11);
            this.txtTotalPages.Multiline = true;
            this.txtTotalPages.Name = "txtTotalPages";
            this.txtTotalPages.Size = new System.Drawing.Size(40, 38);
            this.txtTotalPages.TabIndex = 6;
            this.txtTotalPages.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtToTalCN
            // 
            this.txtToTalCN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtToTalCN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtToTalCN.Enabled = false;
            this.txtToTalCN.Location = new System.Drawing.Point(1167, 14);
            this.txtToTalCN.Name = "txtToTalCN";
            this.txtToTalCN.Size = new System.Drawing.Size(57, 34);
            this.txtToTalCN.TabIndex = 5;
            this.txtToTalCN.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtPage
            // 
            this.txtPage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPage.Enabled = false;
            this.txtPage.Location = new System.Drawing.Point(243, 11);
            this.txtPage.Multiline = true;
            this.txtPage.Name = "txtPage";
            this.txtPage.Size = new System.Drawing.Size(38, 38);
            this.txtPage.TabIndex = 4;
            this.txtPage.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnNext
            // 
            this.btnNext.Image = ((System.Drawing.Image)(resources.GetObject("btnNext.Image")));
            this.btnNext.Location = new System.Drawing.Point(366, 10);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(56, 41);
            this.btnNext.TabIndex = 3;
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnPrev
            // 
            this.btnPrev.Image = ((System.Drawing.Image)(resources.GetObject("btnPrev.Image")));
            this.btnPrev.Location = new System.Drawing.Point(181, 10);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(56, 41);
            this.btnPrev.TabIndex = 2;
            this.btnPrev.UseVisualStyleBackColor = true;
            this.btnPrev.Click += new System.EventHandler(this.btnPrev_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(148, 27);
            this.label1.TabIndex = 1;
            this.label1.Text = "Tổng số trang";
            // 
            // lblToTal
            // 
            this.lblToTal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblToTal.AutoSize = true;
            this.lblToTal.Location = new System.Drawing.Point(949, 17);
            this.lblToTal.Name = "lblToTal";
            this.lblToTal.Size = new System.Drawing.Size(206, 27);
            this.lblToTal.TabIndex = 0;
            this.lblToTal.Text = "Tổng số chi nhánh: ";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.panel5);
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 72);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1924, 801);
            this.panel3.TabIndex = 2;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.panel7);
            this.panel5.Controls.Add(this.panel2);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(685, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1239, 801);
            this.panel5.TabIndex = 1;
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.group_ChiNhanh);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel7.Location = new System.Drawing.Point(0, 162);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(1239, 639);
            this.panel7.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.groupInfor);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1239, 162);
            this.panel2.TabIndex = 1;
            // 
            // groupInfor
            // 
            this.groupInfor.Controls.Add(this.btnCancel);
            this.groupInfor.Controls.Add(this.btnDelete);
            this.groupInfor.Controls.Add(this.btnEdit);
            this.groupInfor.Controls.Add(this.btnAdd);
            this.groupInfor.Controls.Add(this.txtDiaChi);
            this.groupInfor.Controls.Add(this.label6);
            this.groupInfor.Controls.Add(this.txtNguoiQuanLy);
            this.groupInfor.Controls.Add(this.txtTenCN);
            this.groupInfor.Controls.Add(this.txtMaCN);
            this.groupInfor.Controls.Add(this.label5);
            this.groupInfor.Controls.Add(this.label4);
            this.groupInfor.Controls.Add(this.label3);
            this.groupInfor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupInfor.Font = new System.Drawing.Font("Tahoma", 13.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupInfor.Location = new System.Drawing.Point(0, 0);
            this.groupInfor.Name = "groupInfor";
            this.groupInfor.Size = new System.Drawing.Size(1239, 162);
            this.groupInfor.TabIndex = 0;
            this.groupInfor.TabStop = false;
            this.groupInfor.Text = "Thông Tin";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.BackColor = System.Drawing.SystemColors.Highlight;
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
            this.btnCancel.Location = new System.Drawing.Point(1057, 101);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(179, 51);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "Hủy";
            this.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.SystemColors.Highlight;
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
            this.btnDelete.Location = new System.Drawing.Point(963, 101);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(173, 51);
            this.btnDelete.TabIndex = 10;
            this.btnDelete.Text = "Xóa";
            this.btnDelete.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEdit.BackColor = System.Drawing.SystemColors.Highlight;
            this.btnEdit.ForeColor = System.Drawing.Color.White;
            this.btnEdit.Image = ((System.Drawing.Image)(resources.GetObject("btnEdit.Image")));
            this.btnEdit.Location = new System.Drawing.Point(1057, 25);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(179, 54);
            this.btnEdit.TabIndex = 9;
            this.btnEdit.Text = "Sửa";
            this.btnEdit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnEdit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnEdit.UseVisualStyleBackColor = false;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.SystemColors.Highlight;
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.Image")));
            this.btnAdd.Location = new System.Drawing.Point(963, 25);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(173, 54);
            this.btnAdd.TabIndex = 8;
            this.btnAdd.Text = "Thêm";
            this.btnAdd.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAdd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // txtDiaChi
            // 
            this.txtDiaChi.BackColor = System.Drawing.SystemColors.Info;
            this.txtDiaChi.Location = new System.Drawing.Point(609, 28);
            this.txtDiaChi.Multiline = true;
            this.txtDiaChi.Name = "txtDiaChi";
            this.txtDiaChi.Size = new System.Drawing.Size(348, 124);
            this.txtDiaChi.TabIndex = 7;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(506, 29);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(89, 27);
            this.label6.TabIndex = 6;
            this.label6.Text = "Địa Chỉ:";
            // 
            // txtNguoiQuanLy
            // 
            this.txtNguoiQuanLy.BackColor = System.Drawing.SystemColors.Info;
            this.txtNguoiQuanLy.Location = new System.Drawing.Point(196, 118);
            this.txtNguoiQuanLy.Name = "txtNguoiQuanLy";
            this.txtNguoiQuanLy.Size = new System.Drawing.Size(410, 34);
            this.txtNguoiQuanLy.TabIndex = 5;
            // 
            // txtTenCN
            // 
            this.txtTenCN.BackColor = System.Drawing.SystemColors.Info;
            this.txtTenCN.Location = new System.Drawing.Point(196, 73);
            this.txtTenCN.Name = "txtTenCN";
            this.txtTenCN.Size = new System.Drawing.Size(410, 34);
            this.txtTenCN.TabIndex = 4;
            // 
            // txtMaCN
            // 
            this.txtMaCN.BackColor = System.Drawing.SystemColors.Info;
            this.txtMaCN.Enabled = false;
            this.txtMaCN.Location = new System.Drawing.Point(196, 25);
            this.txtMaCN.Name = "txtMaCN";
            this.txtMaCN.Size = new System.Drawing.Size(294, 34);
            this.txtMaCN.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(20, 121);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(165, 27);
            this.label5.TabIndex = 2;
            this.label5.Text = "Người Quản Lý:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 77);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(164, 27);
            this.label4.TabIndex = 1;
            this.label4.Text = "Tên Chi Nhánh:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(156, 27);
            this.label3.TabIndex = 0;
            this.label3.Text = "Mã Chi Nhánh:";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.groupDSNhanVien);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(685, 801);
            this.panel4.TabIndex = 0;
            // 
            // groupDSNhanVien
            // 
            this.groupDSNhanVien.Controls.Add(this.dgvDS_NhanVien);
            this.groupDSNhanVien.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupDSNhanVien.Font = new System.Drawing.Font("Tahoma", 13.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupDSNhanVien.Location = new System.Drawing.Point(0, 0);
            this.groupDSNhanVien.Name = "groupDSNhanVien";
            this.groupDSNhanVien.Size = new System.Drawing.Size(685, 801);
            this.groupDSNhanVien.TabIndex = 0;
            this.groupDSNhanVien.TabStop = false;
            this.groupDSNhanVien.Text = "Danh Sách Nhân Viên Của Chi Nhánh ... :";
            // 
            // dgvDS_NhanVien
            // 
            this.dgvDS_NhanVien.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDS_NhanVien.BackgroundColor = System.Drawing.SystemColors.GradientActiveCaption;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.HotTrack;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Tahoma", 13.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDS_NhanVien.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvDS_NhanVien.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDS_NhanVien.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.MaNV,
            this.HoTen,
            this.ChucVu});
            this.dgvDS_NhanVien.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDS_NhanVien.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvDS_NhanVien.EnableHeadersVisualStyles = false;
            this.dgvDS_NhanVien.Location = new System.Drawing.Point(3, 30);
            this.dgvDS_NhanVien.Name = "dgvDS_NhanVien";
            this.dgvDS_NhanVien.RowHeadersWidth = 51;
            this.dgvDS_NhanVien.RowTemplate.Height = 30;
            this.dgvDS_NhanVien.Size = new System.Drawing.Size(679, 768);
            this.dgvDS_NhanVien.TabIndex = 0;
            // 
            // MaNV
            // 
            this.MaNV.DataPropertyName = "MaNV";
            this.MaNV.HeaderText = "Mã Nhân Viên";
            this.MaNV.MinimumWidth = 6;
            this.MaNV.Name = "MaNV";
            // 
            // HoTen
            // 
            this.HoTen.DataPropertyName = "HoTen";
            this.HoTen.HeaderText = "Họ và Tên";
            this.HoTen.MinimumWidth = 6;
            this.HoTen.Name = "HoTen";
            // 
            // ChucVu
            // 
            this.ChucVu.DataPropertyName = "ChucVu";
            this.ChucVu.HeaderText = "Chức Vụ";
            this.ChucVu.MinimumWidth = 6;
            this.ChucVu.Name = "ChucVu";
            // 
            // QuanLy_ChiNhanh
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1924, 873);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Name = "QuanLy_ChiNhanh";
            this.Text = "QUẢN LÝ CHI NHÁNH";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.group_ChiNhanh.ResumeLayout(false);
            this.panel9.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDSChiNhanh)).EndInit();
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.groupInfor.ResumeLayout(false);
            this.groupInfor.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.groupDSNhanVien.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDS_NhanVien)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.GroupBox group_ChiNhanh;
        private System.Windows.Forms.DataGridView dgvDSChiNhanh;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.GroupBox groupDSNhanVien;
        private System.Windows.Forms.DataGridView dgvDS_NhanVien;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label lblToTal;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnPrev;
        private System.Windows.Forms.TextBox txtPage;
        private System.Windows.Forms.TextBox txtToTalCN;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox groupInfor;
        private System.Windows.Forms.Button btnReport;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtDiaChi;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtNguoiQuanLy;
        private System.Windows.Forms.TextBox txtTenCN;
        private System.Windows.Forms.TextBox txtMaCN;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnReLoad;
        private System.Windows.Forms.DataGridViewTextBoxColumn MaCN;
        private System.Windows.Forms.DataGridViewTextBoxColumn TenChiNhanh;
        private System.Windows.Forms.DataGridViewTextBoxColumn DiaChi;
        private System.Windows.Forms.DataGridViewTextBoxColumn NguoiQuanLy;
        private System.Windows.Forms.TextBox txtTotalPages;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DataGridViewTextBoxColumn MaNV;
        private System.Windows.Forms.DataGridViewTextBoxColumn HoTen;
        private System.Windows.Forms.DataGridViewTextBoxColumn ChucVu;
    }
}
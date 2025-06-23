CREATE DATABASE QuanLySoTietKiem
GO

USE QuanLySoTietKiem;
GO

-- 1. Bảng ChiNhanh
CREATE TABLE ChiNhanh (
    MaCN INT PRIMARY KEY IDENTITY,
    TenChiNhanh NVARCHAR(100) NOT NULL,
    DiaChi NVARCHAR(200),
    NguoiQuanLy NVARCHAR(100)
);
GO

-- 2. Bảng KhachHang
CREATE TABLE KhachHang (
    MaKH INT PRIMARY KEY IDENTITY,
    HoTen NVARCHAR(100) NOT NULL,
    NgaySinh DATE,
    CMND_CCCD NVARCHAR(20) NOT NULL UNIQUE,
    NgayCap DATE,
    DiaChi NVARCHAR(200),
    DienThoai NVARCHAR(15),
    Email NVARCHAR(100),
    TrangThai BIT NOT NULL DEFAULT 1
);
GO

-- 3. Bảng NhanVien
CREATE TABLE NhanVien (
    MaNV INT PRIMARY KEY IDENTITY,
    HoTen NVARCHAR(100) NOT NULL,
	NgaySinh DATE,
    CMND_CCCD NVARCHAR(20) NOT NULL UNIQUE,
    DiaChi NVARCHAR(200),
    DienThoai NVARCHAR(15),
    Email NVARCHAR(100),
    ChucVu NVARCHAR(50),
    PhongBan NVARCHAR(50),
    MaCN INT,
    FOREIGN KEY (MaCN) REFERENCES ChiNhanh(MaCN)
);
GO

-- 4. Bảng TaiKhoanNguoiDung
CREATE TABLE TaiKhoanNguoiDung (
    MaTaiKhoan INT PRIMARY KEY IDENTITY,
    TenDangNhap NVARCHAR(50) NOT NULL UNIQUE,
    MatKhau NVARCHAR(300) NOT NULL,
    QuyenHan NVARCHAR(20) NOT NULL,
    TrangThai BIT NOT NULL DEFAULT 1,
    MaNV INT,
    FOREIGN KEY (MaNV) REFERENCES NhanVien(MaNV)
);
GO

-- 5. Bảng LoaiSoTietKiem
CREATE TABLE LoaiSoTietKiem (
    MaLoai INT PRIMARY KEY IDENTITY,
    TenLoai NVARCHAR(100) NOT NULL,
    LaiSuatCoBan FLOAT NOT NULL,
    MoTa NVARCHAR(500),
    PhiRutTruocHan FLOAT DEFAULT 0,
	KyHanYeuCau INT NULL
);
GO

-- 6. Bảng SoTietKiem
CREATE TABLE SoTietKiem (
    MaSo INT PRIMARY KEY IDENTITY,
    MaKH INT NOT NULL,
    MaLoai INT NOT NULL,
    NgayMo DATE NOT NULL,
    KyHan INT,
    SoTienGoc DECIMAL(18,2) NOT NULL,
    TrangThai NVARCHAR(50) DEFAULT N'Đang hoạt động',
    FOREIGN KEY (MaKH) REFERENCES KhachHang(MaKH),
    FOREIGN KEY (MaLoai) REFERENCES LoaiSoTietKiem(MaLoai)
);
GO

-- 7. Bảng GiaoDichTietKiem
CREATE TABLE GiaoDichTietKiem (
    MaGD INT PRIMARY KEY IDENTITY,
    MaSo INT NOT NULL,
    LoaiGiaoDich NVARCHAR(50) NOT NULL,
    SoTien DECIMAL(18,2) NOT NULL,
    NgayGD DATETIME NOT NULL DEFAULT GETDATE(),
    LaiSuatApDung FLOAT,
    GhiChu NVARCHAR(200),
    MaNV INT,
    FOREIGN KEY (MaSo) REFERENCES SoTietKiem(MaSo),
    FOREIGN KEY (MaNV) REFERENCES NhanVien(MaNV)
);
GO

-- 8. Bảng LichSuThaoTac
CREATE TABLE LichSuThaoTac (
    MaLS INT PRIMARY KEY IDENTITY,
    MaTaiKhoan INT,
    ThaoTac NVARCHAR(100),
    ThoiGian DATETIME DEFAULT GETDATE(),
    DoiTuong NVARCHAR(100),
	FOREIGN KEY (MaTaiKhoan) REFERENCES TaiKhoanNguoiDung(MaTaiKhoan)
);
GO

-- Insert into ChiNhanh Values
INSERT INTO ChiNhanh (TenChiNhanh, DiaChi, NguoiQuanLy)
VALUES (N'Chi nhánh Trung tâm', N'123 Đường ABC, Quận 1, TP.HCM', N'Nguyễn Văn A');
GO
INSERT INTO ChiNhanh (TenChiNhanh, DiaChi, NguoiQuanLy) VALUES
(N'Ngân hàng Vietcombank - CN Cần Thơ', N'01 Hòa Bình, P. Tân An, Q. Ninh Kiều, TP. Cần Thơ', N'Nguyễn Văn A'),
(N'Ngân hàng BIDV - CN Cần Thơ', N'46 Trần Văn Khéo, P. Cái Khế, Q. Ninh Kiều, TP. Cần Thơ', N'Lê Thị B'),
(N'Ngân hàng Agribank - CN Cần Thơ', N'2A Mậu Thân, P. Xuân Khánh, Q. Ninh Kiều, TP. Cần Thơ', N'Trần Văn C'),
(N'Ngân hàng VietinBank - CN Cần Thơ', N'84 Phan Đình Phùng, P. Tân An, Q. Ninh Kiều, TP. Cần Thơ', N'Phạm Thị D'),
(N'Ngân hàng ACB - CN Cần Thơ', N'10 Nguyễn Trãi, P. An Hội, Q. Ninh Kiều, TP. Cần Thơ', N'Đỗ Văn E'),
(N'Ngân hàng MB - CN Cần Thơ', N'123 Nguyễn Văn Cừ, P. An Khánh, Q. Ninh Kiều, TP. Cần Thơ', N'Lê Quốc F'),
(N'Ngân hàng Sacombank - CN Cần Thơ', N'56 Hùng Vương, P. Thới Bình, Q. Ninh Kiều, TP. Cần Thơ', N'Nguyễn Thị G'),
(N'Ngân hàng Eximbank - CN Cần Thơ', N'36 Trần Hưng Đạo, P. An Nghiệp, Q. Ninh Kiều, TP. Cần Thơ', N'Hoàng Văn H'),
(N'Ngân hàng SHB - CN Cần Thơ', N'101 Nguyễn An Ninh, P. Tân An, Q. Ninh Kiều, TP. Cần Thơ', N'Trần Thị I'),
(N'Ngân hàng SCB - CN Cần Thơ', N'78 Phạm Ngọc Thạch, P. Cái Khế, Q. Ninh Kiều, TP. Cần Thơ', N'Võ Quốc K'),
(N'Ngân hàng Techcombank - CN Cần Thơ', N'19-21 Mậu Thân, P. Xuân Khánh, Q. Ninh Kiều, TP. Cần Thơ', N'Nguyễn Minh L'),
(N'Ngân hàng VIB - CN Cần Thơ', N'75A Phan Văn Trị, P. An Phú, Q. Ninh Kiều, TP. Cần Thơ', N'Lý Thanh M'),
(N'Ngân hàng HDBank - CN Cần Thơ', N'59 Nguyễn Trãi, P. An Hội, Q. Ninh Kiều, TP. Cần Thơ', N'Phạm Quang N'),
(N'Ngân hàng SeABank - CN Cần Thơ', N'26 Trần Văn Hoài, P. Xuân Khánh, Q. Ninh Kiều, TP. Cần Thơ', N'Đinh Thị O'),
(N'Ngân hàng TPBank - CN Cần Thơ', N'102 Lý Tự Trọng, P. An Cư, Q. Ninh Kiều, TP. Cần Thơ', N'Ngô Văn P'),
(N'Ngân hàng Maritime Bank - CN Cần Thơ', N'20A Nguyễn Thái Học, P. Tân An, Q. Ninh Kiều, TP. Cần Thơ', N'Lâm Thị Q'),
(N'Ngân hàng OCB - CN Cần Thơ', N'35 Cách Mạng Tháng Tám, P. An Thới, Q. Bình Thủy, TP. Cần Thơ', N'Trương Quốc R'),
(N'Ngân hàng Nam Á Bank - CN Cần Thơ', N'88 Trần Hưng Đạo, P. An Nghiệp, Q. Ninh Kiều, TP. Cần Thơ', N'Tạ Văn S'),
(N'Ngân hàng ABBank - CN Cần Thơ', N'61 Nguyễn Văn Cừ, P. An Khánh, Q. Ninh Kiều, TP. Cần Thơ', N'Trần Thị T'),
(N'Ngân hàng Bắc Á - CN Cần Thơ', N'12 Trần Bình Trọng, P. An Phú, Q. Ninh Kiều, TP. Cần Thơ', N'Nguyễn Văn U');
GO

-- Insert into NhanVien Values
INSERT INTO NhanVien (HoTen, NgaySinh, CMND_CCCD, DiaChi, DienThoai, Email, ChucVu, PhongBan, MaCN)
VALUES (N'Nguyễn Văn B', '1990-01-01', '123456789', N'456 Đường XYZ, Quận 3, TP.HCM', '0909123456', 'nvb@example.com', N'Quản trị', N'IT', 1);
GO
INSERT INTO NhanVien (HoTen, NgaySinh, CMND_CCCD, DiaChi, DienThoai, Email, ChucVu, PhongBan, MaCN)
VALUES (N'Nguyễn Văn A', '1990-01-01', '223456789', N'456 Đường XYZ, Quận 3, TP.HCM', '0909123456', 'ngoc211242@student.nctu.edu.vn', N'Nhân viên', N'IT', 1);
GO

INSERT INTO NhanVien (HoTen, NgaySinh, CMND_CCCD, DiaChi, DienThoai, Email, ChucVu, PhongBan, MaCN)
VALUES
(N'Lê Thị Cẩm Tú', '1990-08-22', '012345670001', N'01 Hòa Bình, Q. Ninh Kiều, TP. Cần Thơ', '0912233445', 'camtu.le@vietcombank.vn', N'Trưởng phòng', N'Tín dụng', 2),
(N'Trần Minh Tâm', '1992-01-05', '012345670002', N'46 Trần Văn Khéo, Cần Thơ', '0934567890', 'tam.tran@bidv.vn', N'Nhân viên', N'Khách hàng cá nhân', 3),
(N'Phạm Thị Lan', '1988-09-10', '012345670003', N'2A Mậu Thân, Cần Thơ', '0922334455', 'lan.pham@agribank.vn', N'Chuyên viên', N'Dịch vụ khách hàng', 4),
(N'Hoàng Văn Long', '1991-06-18', '012345670004', N'84 Phan Đình Phùng, Cần Thơ', '0977888999', 'long.hoang@vietinbank.vn', N'Trưởng nhóm', N'Tín dụng', 5),
(N'Nguyễn Hữu Nghĩa', '1985-12-01', '012345670005', N'10 Nguyễn Trãi, Cần Thơ', '0909554321', 'nghia.nguyen@acb.vn', N'Nhân viên', N'Tín dụng doanh nghiệp', 6),
(N'Lê Quốc Anh', '1993-07-25', '012345670006', N'123 Nguyễn Văn Cừ, Cần Thơ', '0938123456', 'quocanh.le@mbbank.vn', N'Nhân viên', N'Kế toán', 7),
(N'Trần Thị Huyền', '1989-11-03', '012345670007', N'56 Hùng Vương, Cần Thơ', '0987456321', 'huyen.tran@sacombank.vn', N'Trưởng phòng', N'Dịch vụ khách hàng', 8),
(N'Đỗ Thành Đạt', '1994-03-15', '012345670008', N'36 Trần Hưng Đạo, Cần Thơ', '0909333222', 'dat.do@eximbank.vn', N'Chuyên viên', N'Phân tích tín dụng', 9),
(N'Nguyễn Thị Bích Ngọc', '1995-10-20', '012345670009', N'101 Nguyễn An Ninh, Cần Thơ', '0977112233', 'ngoc.nguyen@shb.vn', N'Nhân viên', N'Quản trị rủi ro', 10),
(N'Võ Minh Quang', '1990-05-28', '012345670010', N'78 Phạm Ngọc Thạch, Cần Thơ', '0911445566', 'quang.vo@scb.vn', N'Trưởng nhóm', N'Tín dụng', 11),
(N'Phạm Thu Trang', '1987-09-12', '012345670011', N'21 Mậu Thân, Cần Thơ', '0922446688', 'trang.pham@techcombank.vn', N'Chuyên viên', N'Dịch vụ khách hàng', 12),
(N'Lý Thành Nhân', '1992-02-17', '012345670012', N'75A Phan Văn Trị, Cần Thơ', '0933445566', 'nhan.ly@vib.vn', N'Nhân viên', N'Thẻ & dịch vụ số', 13),
(N'Phạm Quang Hưng', '1986-06-07', '012345670013', N'59 Nguyễn Trãi, Cần Thơ', '0944778899', 'hung.pham@hdbank.vn', N'Trưởng phòng', N'Tín dụng', 14),
(N'Đinh Thị Oanh', '1993-01-30', '012345670014', N'26 Trần Văn Hoài, Cần Thơ', '0911556677', 'oanh.dinh@seabank.vn', N'Nhân viên', N'Giao dịch', 15),
(N'Ngô Văn Phát', '1984-04-11', '012345670015', N'102 Lý Tự Trọng, Cần Thơ', '0955112244', 'phat.ngo@tpbank.vn', N'Chuyên viên', N'Kế toán', 16),
(N'Lâm Thị Quỳnh', '1996-08-14', '012345670016', N'20A Nguyễn Thái Học, Cần Thơ', '0933555777', 'quynh.lam@msb.vn', N'Nhân viên', N'CSKH', 17),
(N'Trương Quốc Tuấn', '1989-12-18', '012345670017', N'35 CMT8, Cần Thơ', '0977665544', 'tuan.truong@ocb.vn', N'Trưởng nhóm', N'Tín dụng doanh nghiệp', 18),
(N'Tạ Thị Mai', '1990-03-19', '012345670018', N'88 Trần Hưng Đạo, Cần Thơ', '0911999888', 'mai.ta@namabank.vn', N'Chuyên viên', N'Kế toán', 19),
(N'Trần Văn Uy', '1991-11-29', '012345670019', N'61 Nguyễn Văn Cừ, Cần Thơ', '0944332211', 'uy.tran@abbank.vn', N'Nhân viên', N'Giao dịch', 20),
(N'Nguyễn Thị Thảo', '1994-09-23', '012345670020', N'12 Trần Bình Trọng, Cần Thơ', '0909445566', 'thao.nguyen@bacabank.vn', N'Trưởng phòng', N'Khách hàng cá nhân', 20);
GO
-- Chi nhánh 1
INSERT INTO NhanVien (HoTen, NgaySinh, CMND_CCCD, DiaChi, DienThoai, Email, ChucVu, PhongBan, MaCN) VALUES
(N'Nguyễn Văn An', '1990-01-01', '100000000001', N'01 Hòa Bình, Cần Thơ', '0901000001', 'an.nguyen1@bank.com', N'Nhân viên', N'Tín dụng', 1),
(N'Lê Thị Bích', '1989-02-02', '100000000002', N'02 Nguyễn Trãi, Cần Thơ', '0901000002', 'bich.le1@bank.com', N'Chuyên viên', N'Kế toán', 1),
(N'Trần Văn Cường', '1988-03-03', '100000000003', N'03 Mậu Thân, Cần Thơ', '0901000003', 'cuong.tran1@bank.com', N'Trưởng phòng', N'Khách hàng', 1),
(N'Phạm Thị Dung', '1991-04-04', '100000000004', N'04 Nguyễn Văn Cừ, Cần Thơ', '0901000004', 'dung.pham1@bank.com', N'Nhân viên', N'Hành chính', 1),
(N'Hoàng Văn Em', '1992-05-05', '100000000005', N'05 Lý Tự Trọng, Cần Thơ', '0901000005', 'em.hoang1@bank.com', N'Chuyên viên', N'CNTT', 1);
GO
-- Chi nhánh 2
INSERT INTO NhanVien (HoTen, NgaySinh, CMND_CCCD, DiaChi, DienThoai, Email, ChucVu, PhongBan, MaCN) VALUES
(N'Nguyễn Thị Hoa', '1990-06-01', '100000000006', N'06 Hòa Bình, Cần Thơ', '0901000006', 'hoa.nguyen2@bank.com', N'Nhân viên', N'Tín dụng', 2),
(N'Võ Minh Hùng', '1992-07-02', '100000000007', N'07 Nguyễn Trãi, Cần Thơ', '0901000007', 'hung.vo2@bank.com', N'Chuyên viên', N'Kế toán', 2),
(N'Lâm Quang Nhật', '1987-08-03', '100000000008', N'08 Mậu Thân, Cần Thơ', '0901000008', 'nhat.lam2@bank.com', N'Trưởng phòng', N'Khách hàng', 2),
(N'Trần Thị Kiều', '1993-09-04', '100000000009', N'09 Nguyễn Văn Cừ, Cần Thơ', '0901000009', 'kieu.tran2@bank.com', N'Nhân viên', N'Hành chính', 2),
(N'Đỗ Văn Long', '1991-10-05', '100000000010', N'10 Lý Tự Trọng, Cần Thơ', '0901000010', 'long.do2@bank.com', N'Chuyên viên', N'CNTT', 2);
GO
-- Chi nhánh 3
INSERT INTO NhanVien (HoTen, NgaySinh, CMND_CCCD, DiaChi, DienThoai, Email, ChucVu, PhongBan, MaCN) VALUES
(N'Lê Thị Mai', '1990-01-11', '100000000011', N'11 Trần Hưng Đạo, Cần Thơ', '0901000011', 'mai.le3@bank.com', N'Nhân viên', N'Tín dụng', 3),
(N'Nguyễn Văn Nam', '1991-02-12', '100000000012', N'12 Hòa Bình, Cần Thơ', '0901000012', 'nam.nguyen3@bank.com', N'Chuyên viên', N'Kế toán', 3),
(N'Phan Thị Oanh', '1992-03-13', '100000000013', N'13 Nguyễn Trãi, Cần Thơ', '0901000013', 'oanh.phan3@bank.com', N'Trưởng phòng', N'Khách hàng', 3),
(N'Bùi Văn Phúc', '1988-04-14', '100000000014', N'14 Mậu Thân, Cần Thơ', '0901000014', 'phuc.bui3@bank.com', N'Nhân viên', N'Hành chính', 3),
(N'Trịnh Thị Quỳnh', '1993-05-15', '100000000015', N'15 Nguyễn Văn Cừ, Cần Thơ', '0901000015', 'quynh.trinh3@bank.com', N'Chuyên viên', N'CNTT', 3);
GO
-- Chi nhánh 4
INSERT INTO NhanVien (HoTen, NgaySinh, CMND_CCCD, DiaChi, DienThoai, Email, ChucVu, PhongBan, MaCN) VALUES
(N'Vũ Thị Trang', '1994-06-16', '100000000016', N'16 Lý Tự Trọng, Cần Thơ', '0901000016', 'trang.vu4@bank.com', N'Nhân viên', N'Tín dụng', 4),
(N'Ngô Văn Quân', '1987-07-17', '100000000017', N'17 Trần Hưng Đạo, Cần Thơ', '0901000017', 'quan.ngo4@bank.com', N'Chuyên viên', N'Kế toán', 4),
(N'Tạ Thị Uyên', '1991-08-18', '100000000018', N'18 Hòa Bình, Cần Thơ', '0901000018', 'uyen.ta4@bank.com', N'Trưởng phòng', N'Khách hàng', 4),
(N'Lý Văn Vũ', '1989-09-19', '100000000019', N'19 Nguyễn Trãi, Cần Thơ', '0901000019', 'vu.ly4@bank.com', N'Nhân viên', N'Hành chính', 4),
(N'Phạm Quang Xuyên', '1990-10-20', '100000000020', N'20 Mậu Thân, Cần Thơ', '0901000020', 'xuyen.pham4@bank.com', N'Chuyên viên', N'CNTT', 4);
GO
-- Kiểm tra mã NV vừa thêm (chắc chắn là 1 nếu chưa thêm ai khác)
-- Sau đó:
INSERT INTO TaiKhoanNguoiDung (TenDangNhap, MatKhau, QuyenHan, TrangThai, MaNV)
VALUES ('admin', 'e10adc3949ba59abbe56e057f20f883e', N'Admin', 1, 1);
GO
INSERT INTO TaiKhoanNguoiDung (TenDangNhap, MatKhau, QuyenHan, TrangThai, MaNV)
VALUES ('nv', 'e10adc3949ba59abbe56e057f20f883e', N'NhanVien', 1, 1);
GO

INSERT INTO LoaiSoTietKiem (TenLoai, LaiSuatCoBan, MoTa, PhiRutTruocHan) VALUES
(N'Tiết kiệm không kỳ hạn', 0.3, N'Linh hoạt rút tiền bất kỳ lúc nào', 0),
(N'Tiết kiệm kỳ hạn 1 tháng', 3.5, N'Lãi suất cố định, rút đúng hạn', 0.2),
(N'Tiết kiệm kỳ hạn 3 tháng', 4.2, N'Lãi suất cao hơn, kỳ hạn trung bình', 0.5),
(N'Tiết kiệm kỳ hạn 6 tháng', 5.0, N'Lãi suất hấp dẫn, dành cho người gửi dài hơn', 1),
(N'Tiết kiệm kỳ hạn 12 tháng', 6.0, N'Tối ưu lãi suất, thích hợp gửi dài hạn', 1.5),
(N'Tiết kiệm kỳ hạn 18 tháng', 6.3, N'Kỳ hạn dài, lãi suất cao', 2),
(N'Tiết kiệm kỳ hạn 24 tháng', 6.5, N'Lãi suất tốt nhất, cam kết giữ tiền lâu dài', 2),
(N'Tiết kiệm tích lũy theo ngày', 4.0, N'Gửi thêm hàng ngày, linh hoạt', 0.2),
(N'Tiết kiệm tích lũy theo tháng', 4.8, N'Định kỳ gửi tiền hàng tháng', 0.5),
(N'Tiết kiệm lãi cuối kỳ', 5.5, N'Lãi nhận cuối kỳ, không chia lãi định kỳ', 1),
(N'Tiết kiệm lãi định kỳ tháng', 5.0, N'Nhận lãi hàng tháng vào tài khoản', 1),
(N'Tiết kiệm lãi định kỳ quý', 5.2, N'Nhận lãi hàng quý, phù hợp cho người dùng chi tiêu định kỳ', 1),
(N'Tiết kiệm gửi góp không kỳ hạn', 3.0, N'Gửi từng phần nhỏ, linh hoạt', 0.3),
(N'Tiết kiệm gửi góp kỳ hạn 6 tháng', 4.5, N'Gửi từng phần nhỏ, kỳ hạn rõ ràng', 0.8),
(N'Tiết kiệm giáo dục', 5.0, N'Dành cho phụ huynh tích lũy học phí cho con', 0.5),
(N'Tiết kiệm hưu trí', 5.8, N'Chuẩn bị tài chính cho tuổi già', 1),
(N'Tiết kiệm cho con', 4.8, N'Dành riêng cho trẻ em tích lũy tương lai', 0.5),
(N'Tiết kiệm an sinh', 4.0, N'Gửi để bảo đảm an sinh, có thể dùng kèm bảo hiểm', 0.5),
(N'Tiết kiệm online kỳ hạn 1 tháng', 4.0, N'Gửi tiền online, kỳ hạn ngắn', 0.2),
(N'Tiết kiệm online kỳ hạn 12 tháng', 6.2, N'Lãi suất cao hơn gửi tại quầy', 1);
GO

INSERT INTO KhachHang (HoTen, NgaySinh, CMND_CCCD, NgayCap, DiaChi, DienThoai, Email, TrangThai) VALUES
(N'Nguyễn Văn A', '1985-03-10', '025648392', '2005-06-15', N'1A Nguyễn Trãi, Q1, TP.HCM', '0901234567', 'vana@gmail.com', 1),
(N'Lê Thị B', '1990-12-21', '123456789', '2010-09-01', N'23 Trần Phú, Q5, TP.HCM', '0912121212', 'leb123@yahoo.com', 1),
(N'Trần Văn C', '1978-07-01', '987654321', '2000-03-12', N'3/5 Nguyễn Thái Học, Cần Thơ', '0933445566', 'tranc78@hotmail.com', 1),
(N'Phạm Thị D', '1995-11-18', '111222333', '2015-01-10', N'145 Lê Lợi, Đà Nẵng', '0909876543', 'phamtd95@gmail.com', 1),
(N'Hoàng Văn E', '1988-04-25', '333222111', '2008-08-22', N'92 Huỳnh Thúc Kháng, Hà Nội', '0945566778', 'hoange88@gmail.com', 1),
(N'Võ Thị F', '1992-09-30', '444555666', '2012-04-20', N'16 Trưng Nữ Vương, Đà Lạt', '0912233445', 'vof92@gmail.com', 1),
(N'Lý Minh G', '1980-06-17', '777888999', '2003-07-30', N'31 Phan Đình Phùng, Huế', '0923445566', 'lyg80@gmail.com', 1),
(N'Đặng Thị H', '1993-10-02', '999888777', '2011-11-01', N'8 Lê Văn Lương, Q7, TP.HCM', '0934556677', 'dangth@gmail.com', 1),
(N'Bùi Văn I', '1975-05-13', '666777888', '1998-06-20', N'45 Nguyễn Văn Cừ, Cần Thơ', '0967788990', 'buivi75@gmail.com', 1),
(N'Ngô Thị K', '1983-08-08', '112233445', '2006-05-18', N'12 Hai Bà Trưng, Hải Phòng', '0977899001', 'ngok83@gmail.com', 1),
(N'Tống Văn L', '1991-01-01', '556677889', '2013-02-15', N'98 Nguyễn Hữu Cảnh, TP.HCM', '0988001122', 'tongvl91@gmail.com', 1),
(N'Tạ Thị M', '1986-09-14', '778899001', '2007-09-09', N'56 Lê Hồng Phong, Buôn Ma Thuột', '0911223344', 'tatm86@yahoo.com', 1),
(N'Huỳnh Văn N', '1979-12-31', '443322110', '1999-12-30', N'60 Cách Mạng Tháng 8, Bình Dương', '0904332110', 'huynhn79@gmail.com', 1),
(N'Trương Thị O', '1994-03-27', '332211009', '2014-03-10', N'77 Điện Biên Phủ, Q3, TP.HCM', '0923456677', 'truongo94@gmail.com', 1),
(N'Thái Văn P', '1982-11-11', '223344556', '2004-11-11', N'19 Nguyễn Trãi, Q1, TP.HCM', '0932112233', 'thaivp82@gmail.com', 1),
(N'Đỗ Thị Q', '1996-07-05', '889900112', '2016-07-20', N'34 Lê Lai, Hà Nội', '0976677889', 'dotq96@gmail.com', 1),
(N'Hà Văn R', '1984-06-06', '667788990', '2005-06-06', N'55 Trường Chinh, Quảng Ninh', '0944332211', 'har84@yahoo.com', 1),
(N'Kiều Thị S', '1990-02-20', '778800112', '2010-02-20', N'6 Nguyễn Khuyến, Ninh Bình', '0955223344', 'kieuts90@gmail.com', 1),
(N'Nguyễn Thị T', '1987-04-03', '111000999', '2006-04-03', N'14 Hùng Vương, An Giang', '0900112233', 'nguyentt87@gmail.com', 1),
(N'Lâm Văn U', '1989-08-19', '998877665', '2009-08-19', N'27 Trần Hưng Đạo, Sóc Trăng', '0988112233', 'lamvu89@gmail.com', 1);
GO

INSERT INTO LoaiSoTietKiem (TenLoai, LaiSuatCoBan, MoTa, PhiRutTruocHan) VALUES
(N'Sổ tiết kiệm không kỳ hạn', 0.5, N'Khách hàng có thể rút tiền bất kỳ lúc nào, lãi suất thấp', 0),
(N'Sổ tiết kiệm kỳ hạn 1 tháng', 3.5, N'Lãi suất cố định, nhận lãi cuối kỳ', 1),
(N'Sổ tiết kiệm kỳ hạn 3 tháng', 4.2, N'Lãi suất hấp dẫn, rút trước hạn chịu phí', 1.5),
(N'Sổ tiết kiệm kỳ hạn 6 tháng', 5.0, N'Lãi suất cao, linh hoạt', 2),
(N'Sổ tiết kiệm kỳ hạn 9 tháng', 5.2, N'Tiết kiệm trung hạn, sinh lời tốt', 2),
(N'Sổ tiết kiệm kỳ hạn 12 tháng', 6.0, N'Phù hợp đầu tư dài hạn', 2.5),
(N'Sổ tiết kiệm kỳ hạn 18 tháng', 6.3, N'Lựa chọn an toàn, lợi nhuận ổn định', 3),
(N'Sổ tiết kiệm kỳ hạn 24 tháng', 6.5, N'An tâm tích lũy lâu dài', 3),
(N'Sổ tiết kiệm kỳ hạn 36 tháng', 7.0, N'Lãi suất tối đa, dành cho khách hàng lâu dài', 3.5),
(N'Sổ tiết kiệm gửi góp định kỳ 12 tháng', 6.0, N'Góp tiền hàng tháng, nhận lãi cao', 2.5),
(N'Sổ tiết kiệm gửi góp linh hoạt', 5.8, N'Tự chọn ngày gửi, tích lũy hiệu quả', 2),
(N'Tiết kiệm cho con', 6.2, N'Dành cho phụ huynh lập kế hoạch tài chính dài hạn', 2.5),
(N'Tiết kiệm hưu trí', 6.5, N'Hỗ trợ khách hàng lớn tuổi, nhận lãi định kỳ', 2),
(N'Tiết kiệm online kỳ hạn 6 tháng', 5.2, N'Giao dịch trực tuyến, lãi suất cao hơn tại quầy', 2),
(N'Tiết kiệm online kỳ hạn 12 tháng', 6.2, N'Gửi tiết kiệm qua app, an toàn, tiện lợi', 2.5),
(N'Tiết kiệm ưu đãi sinh viên', 5.0, N'Dành cho sinh viên, hỗ trợ lãi suất tốt', 1),
(N'Tiết kiệm tích lũy theo ngày', 4.5, N'Gửi từng ngày, lãi suất cộng dồn', 1.5),
(N'Tiết kiệm tích lũy theo tuần', 4.8, N'Linh hoạt, phù hợp cá nhân tự do tài chính', 1.5),
(N'Tiết kiệm theo mục tiêu', 6.1, N'Gắn liền với kế hoạch như mua xe, mua nhà', 2),
(N'Tiết kiệm tự động trích từ tài khoản', 5.7, N'Tự động chuyển tiền mỗi tháng từ tài khoản thanh toán', 1.5);
GO

INSERT INTO SoTietKiem (MaKH, MaLoai, NgayMo, KyHan, SoTienGoc, TrangThai) VALUES
(1, 5, '2023-01-15', 12, 50000000.00, N'Đang hoạt động'),
(2, 3, '2023-02-20', 3, 10000000.00, N'Đang hoạt động'),
(3, 1, '2023-03-01', NULL, 2000000.00, N'Đang hoạt động'),
(4, 6, '2023-04-10', 12, 75000000.00, N'Đang hoạt động'),
(5, 2, '2023-05-05', 1, 8000000.00, N'Đang hoạt động'),
(6, 4, '2023-06-12', 6, 30000000.00, N'Đang hoạt động'),
(7, 7, '2023-07-25', 24, 100000000.00, N'Đang hoạt động'),
(8, 1, '2023-08-01', NULL, 1500000.00, N'Đang hoạt động'),
(9, 10, '2023-09-08', 12, 40000000.00, N'Đang hoạt động'),
(10, 8, '2023-10-17', NULL, 500000.00, N'Đang hoạt động'),
(11, 15, '2023-11-22', 12, 60000000.00, N'Đang hoạt động'),
(12, 13, '2023-12-01', NULL, 1000000.00, N'Đang hoạt động'),
(13, 9, '2024-01-05', NULL, 2500000.00, N'Đang hoạt động'),
(14, 11, '2024-02-14', 12, 35000000.00, N'Đang hoạt động'),
(15, 12, '2024-03-09', 3, 12000000.00, N'Đang hoạt động'),
(16, 14, '2024-04-18', 6, 22000000.00, N'Đang hoạt động'),
(17, 1, '2024-05-01', NULL, 3000000.00, N'Đang hoạt động'),
(18, 16, '2024-06-03', 12, 80000000.00, N'Đang hoạt động'),
(19, 17, '2024-07-10', NULL, 5000000.00, N'Đang hoạt động'),
(20, 19, '2024-08-01', 1, 7000000.00, N'Đang hoạt động');
GO

SELECT HoTen FROM NhanVien WHERE MaNV = 7
use QLSV_TEST;
go
Create table TAIKHOAN
(
	ID int identity(1,1) ,
	TaiKhoan nvarchar(100) not null,
	MatKhau nvarchar(255),
	HoTen nvarchar(255),
	Quyen nvarchar(50) not null,
	constraint UQ_TaiKhoan unique (TaiKhoan),
	CONSTRAINT PK_TaiKhoan  primary key(ID),
);
go
INSERT [dbo].[TAIKHOAN] ([TaiKhoan], [MatKhau], [HoTen], [Quyen]) VALUES (N'admin', N'e10adc3949ba59abbe56e057f20f883e', N'admin', N'quantri')
go
Create table KYTHI
(
	ID int identity(1,1),
	MaKT nvarchar(255) not null,
	TenKT nvarchar(255),
	NgayThi DateTime not null,
	TGLamBai int not null,
	TGBatDau nvarchar(255) not null,
	TGKetThuc nvarchar(255)
	CONSTRAINT PK_KyThi  primary key(ID),
)
go
Create table KHOA
(
	ID int identity(1,1),
	MaKhoa nvarchar(255),
	TenKhoa nvarchar(255) not null,
	CONSTRAINT PK_Khoa  primary key(ID),
)
go
Create table LOP
(
	ID int identity(1,1),
	MaLop nvarchar(255)not null,
	IdKhoa int not null,
	GhiChu nvarchar(255),	
	CONSTRAINT PK_Lop  primary key(ID),
	constraint UQ_MaLop unique(MaLop),
	CONSTRAINT FK_Lop_Khoa FOREIGN KEY (IdKhoa) REFERENCES KHOA(ID)
)
go
Create table SINHVIEN
(
	MaSV int not null, -- Mã sinh viên
	HoSV nvarchar(255) not null,
	TenSV nvarchar(255) not null,
	NgaySinh varchar(255),
	IdLop int not null,
	CONSTRAINT PK_SinhVien  primary key(MaSV),
	CONSTRAINT FK_SinhVien_Lop FOREIGN KEY (IdLop) REFERENCES LOP(ID)
)
go
Create table PHONGTHI
(
	ID int identity(1,1),
	TenPhong nvarchar(255) not null,
	SucChua int not null,
	GhiChu nvarchar(255),
	--constraint UQ_MaPhong unique (TenPhong),
	CONSTRAINT PK_PhongThi  primary key(ID),
)
go
Create table XEPPHONG
(
	IdSV int not null, -- Mã sinh viên
	IdPhong int not null,
	IdKyThi int not null,
	CONSTRAINT PK_XepPhong  primary key(IdSV,IdPhong,IdKyThi),
	CONSTRAINT FK_XepPhong_SinhVien FOREIGN KEY (IdSV) REFERENCES SINHVIEN(MaSV),
	CONSTRAINT FK_XepPhong_PhongThi FOREIGN KEY (IdPhong) REFERENCES PHONGTHI(ID),
	CONSTRAINT FK_XepPhong_KyThi FOREIGN KEY (IdKyThi) REFERENCES KYTHI(ID)
)
go
Create table KT_PHONG
(
	IdPhong int not null,
	IdKyThi int not null,
	SiSo int,
	CONSTRAINT PK_KT_PHONG  primary key(IdPhong,IdKyThi),
	CONSTRAINT FK_KT_PHONG_PhongThi FOREIGN KEY (IdPhong) REFERENCES PHONGTHI(ID),
	CONSTRAINT FK_KT_PHONG_KyThi FOREIGN KEY (IdKyThi) REFERENCES KYTHI(ID)
)
go
CREATE TABLE DAPAN
(
	ID int identity(1,1),
	IdKyThi int not null,
	MaMon nvarchar(255) not null,
	MaDe nvarchar(255) not null,
	CauHoi nvarchar(255) not null,
	DapAn nvarchar(255) not null,
	ThangDiem int,
	CONSTRAINT PK_DapAn  primary key(ID),
	CONSTRAINT FK_DapAn_KyThi FOREIGN KEY (IdKyThi) REFERENCES KYTHI(ID),
)
go
CREATE TABLE BAILAM
(
	IdKyThi int identity(1,1),
	MaSV nvarchar(255) not null,
	MaDe nvarchar(255) not null,
	KetQua nvarchar(255) not null,
	DiemThi int,
	CONSTRAINT PK_BaiLam  primary key(IdKyThi,MaSV),
	CONSTRAINT FK_BaiLam_KyThi FOREIGN KEY (IdKyThi) REFERENCES KYTHI(ID),
)
go
CREATE TABLE THONGKE
(
	MaSV int not null,
	NamHoc varchar(50),
	HocKy varchar(50),
	Diem int,
	CONSTRAINT PK_ThongKe  primary key(MaSV,NamHoc,HocKy),
	CONSTRAINT FK_ThongKe_SinhVien FOREIGN KEY (MaSV) REFERENCES SINHVIEN(MaSV),
)

TRUNCATE TABLE SinhVien
ALTER TABLE XepPhong ADD CONSTRAINT PK_XepPhong primary key(IdSV,IdPhong,IdKyThi)
ALTER TABLE XepPhong ADD CONSTRAINT FK_XepPhong_SinhVien FOREIGN KEY (IdSV) REFERENCES SinhVien(ID)
SELECT ROW_NUMBER() OVER(ORDER BY T.ID) as [STT], T.* FROM TAIKHOAN as T
SELECT MAX(MaSV), TenSV FROM SINHVIEN group by TenSV
CREATE DATABASE QLKinhdoanhoto_admin

USE QLKinhdoanhoto_admin
go

CREATE TABLE Nguoiquantri (
    Id INT PRIMARY KEY IDENTITY,
    Hoten NVARCHAR(150) NOT NULL,
    Email NVARCHAR(150) NOT NULL UNIQUE,
    Matkhau NVARCHAR(255) NOT NULL,
    Mabaomat NVARCHAR(100) NOT NULL,
    Vaitro NVARCHAR(50) NOT NULL, -- AdminTong, Sale, Kythuat
    Trangthai BIT DEFAULT 1,
    Ngaytao DATETIME DEFAULT GETDATE()
);
ALTER TABLE Nguoiquantri
ADD 
    Anhdaidien NVARCHAR(300),
    LanDangNhapCuoi DATETIME NULL;

ALTER TABLE Nguoiquantri
ADD 
    Solandangsai INT NOT NULL DEFAULT 0,
    ThoigianKhoa DATETIME NULL;

CREATE TABLE LogDangnhap (
    Id INT PRIMARY KEY IDENTITY,
    AdminId INT,
    IPAddress NVARCHAR(50),
    Thoigian DATETIME DEFAULT GETDATE(),
    Thanhcong BIT,
    FOREIGN KEY (AdminId) REFERENCES Nguoiquantri(Id)
);

CREATE TABLE Hangxe (
    Id INT PRIMARY KEY IDENTITY,
    Tenhang NVARCHAR(100) NOT NULL
);
ALTER TABLE Hangxe
ADD Logo NVARCHAR(300);

CREATE TABLE Danhmucxe (
    Id INT PRIMARY KEY IDENTITY,
    Tendanhmuc NVARCHAR(100) NOT NULL
);

CREATE TABLE Nhienlieu (
    Id INT PRIMARY KEY IDENTITY,
    Tennhienlieu NVARCHAR(100) NOT NULL
);

-- xe
CREATE TABLE Xe (
    Id INT PRIMARY KEY IDENTITY,
    Tenxe NVARCHAR(200) NOT NULL,
    HangxeId INT,
    DanhmucId INT,
    NhienlieuId INT,
    Gia DECIMAL(18,2),
    Namsanxuat INT,
    Trangthai NVARCHAR(50),
    Motangan NVARCHAR(MAX),
    Ngaytao DATETIME DEFAULT GETDATE()
);
ALTER TABLE Xe
ADD CONSTRAINT FK_Xe_Hangxe
FOREIGN KEY (HangxeId) REFERENCES Hangxe(Id);

ALTER TABLE Xe
ADD CONSTRAINT FK_Xe_Danhmuc
FOREIGN KEY (DanhmucId) REFERENCES Danhmucxe(Id);

ALTER TABLE Xe
ADD CONSTRAINT FK_Xe_Nhienlieu
FOREIGN KEY (NhienlieuId) REFERENCES Nhienlieu(Id);

ALTER TABLE Xe
ADD 
    Anhdaidien NVARCHAR(300),
    Luotxem INT DEFAULT 0,
    Noibat BIT DEFAULT 0;


CREATE TABLE XeHinhanh (
    Id INT PRIMARY KEY IDENTITY,
    XeId INT,
    Duongdan NVARCHAR(300),
    FOREIGN KEY (XeId) REFERENCES Xe(Id)
);
ALTER TABLE XeHinhanh
ADD 
    Laanhchinh BIT DEFAULT 0;

CREATE TABLE ServiceBooking (
    Id INT PRIMARY KEY IDENTITY,

    Hoten NVARCHAR(150) NOT NULL,
    Sodienthoai NVARCHAR(20) NOT NULL,

    Dichvu NVARCHAR(100),

    NgayHen DATETIME NOT NULL,
    NgayTao DATETIME DEFAULT GETDATE()
);
SELECT * FROM ServiceBooking;
ALTER TABLE ServiceBooking
ALTER COLUMN NgayHen datetime2;
-- -- ==================================================================== nhóm khách hàng ================================================================================================
CREATE TABLE Khachhang (
    Id INT PRIMARY KEY IDENTITY,
    Hoten NVARCHAR(150),
    Sodienthoai NVARCHAR(20),
    Email NVARCHAR(150),
    Khuvuc NVARCHAR(150),
    Ngaytao DATETIME DEFAULT GETDATE()
);
ALTER TABLE Khachhang
ADD 
    Matkhau NVARCHAR(255),
    Trangthai BIT DEFAULT 1,
    Solandangsai INT DEFAULT 0,
    ThoigianKhoa DATETIME NULL;
ALTER TABLE Khachhang
ADD Gioitinh NVARCHAR(10),
    Ngaysinh DATE,
    CCCD NVARCHAR(20),
    Diachi NVARCHAR(255);

	SELECT *
FROM Khachhang
SELECT Sodienthoai, Matkhau FROM Khachhang

DELETE FROM Khachhang
WHERE Sodienthoai = '0908371931';

ALTER TABLE Khachhang
ADD CONSTRAINT UQ_Sodienthoai UNIQUE (Sodienthoai)

SELECT COLUMN_NAME
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'Khachhang'

CREATE TABLE OTPDangky (
    Id INT PRIMARY KEY IDENTITY,
    Sodienthoai NVARCHAR(20),
    MaOTP NVARCHAR(10),
    ThoigianTao DATETIME DEFAULT GETDATE(),
    HetHan DATETIME,
    Trangthai BIT DEFAULT 0 -- 0 = chưa dùng, 1 = đã dùng
);
ALTER TABLE OTPDangky
ALTER COLUMN HetHan datetime2
sp_help OTPDangky
ALTER TABLE OTPDangky
ALTER COLUMN ThoigianTao datetime2

ALTER TABLE OTPDangky
ADD CONSTRAINT DF_OTPDangky_ThoigianTao
DEFAULT GETDATE() FOR ThoigianTao

SELECT * FROM OTPDangky
ORDER BY Id DESC

CREATE TABLE Laithu (
    Id INT PRIMARY KEY IDENTITY,
    KhachhangId INT,
    XeId INT,
    Thoigian DATETIME,
    Trangthai NVARCHAR(50), -- ChoDuyet, DaDuyet, TuChoi
    FOREIGN KEY (KhachhangId) REFERENCES Khachhang(Id),
    FOREIGN KEY (XeId) REFERENCES Xe(Id)
);

CREATE TABLE Baogia (
    Id INT PRIMARY KEY IDENTITY,
    KhachhangId INT,
    XeId INT,
    Giabaogia DECIMAL(18,2),
    Trangthai NVARCHAR(50),
    FOREIGN KEY (KhachhangId) REFERENCES Khachhang(Id),
    FOREIGN KEY (XeId) REFERENCES Xe(Id)
);

CREATE TABLE Datcoc (
    Id INT PRIMARY KEY IDENTITY,
    KhachhangId INT,
    XeId INT,
    Sotien DECIMAL(18,2),
    Ngaydat DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (KhachhangId) REFERENCES Khachhang(Id),
    FOREIGN KEY (XeId) REFERENCES Xe(Id)
);
ALTER TABLE Datcoc
ADD CONSTRAINT FK_Datcoc_Cars
FOREIGN KEY (XeId) REFERENCES Cars(Id);

SELECT * FROM INFORMATION_SCHEMA.TABLES
WHERE TABLE_NAME = 'Datcoc';

SELECT COLUMN_NAME
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'Khachhang'
--================================================================================== Quản lý xe ==================================================================================--

CREATE TABLE Cars (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    TenXe NVARCHAR(255),
    HangXe NVARCHAR(100),
    DongXe NVARCHAR(100),
    NamSanXuat INT,
    TinhTrang NVARCHAR(50),
    GiaNiemYet DECIMAL(18,0),
    GiaKhuyenMai DECIMAL(18,0),
    SoKhung NVARCHAR(100),
    SoMay NVARCHAR(100),
    BodyStyle NVARCHAR(100),
    MauNgoaiThat NVARCHAR(100),
    SoCua INT,
    DenXe NVARCHAR(100),
    MamXe NVARCHAR(50),
    NhienLieu NVARCHAR(50),
    DungTich NVARCHAR(50),
    HopSo NVARCHAR(50),
    DanDong NVARCHAR(50),
    CongSuat INT,
    MoMenXoan INT,
    TieuHaoNhienLieu NVARCHAR(50),
    MauNoiThat NVARCHAR(100),
    SoCho INT,
    ChatLieuGhe NVARCHAR(100),
    AmThanh NVARCHAR(100),
    SoTuiKhi INT,
    Camera NVARCHAR(100),
    Thumbnail NVARCHAR(500),
    MoTa NVARCHAR(MAX)
);
ALTER TABLE Cars
ALTER COLUMN HopSo NVARCHAR(MAX);


UPDATE Cars
SET GiaKhuyenMai = 0
WHERE GiaKhuyenMai IS NULL
SELECT * FROM Cars
--================================================================================== Nhóm CMS ==================================================================================--
CREATE TABLE Tintuc (
    Id INT PRIMARY KEY IDENTITY,
    Tieude NVARCHAR(300),
    Noidung NVARCHAR(MAX),
    Ngaydang DATETIME DEFAULT GETDATE(),
    Trangthai BIT
);

CREATE TABLE Banner (
    Id INT PRIMARY KEY IDENTITY,
    Hinhanh NVARCHAR(300),
    Link NVARCHAR(300),
    Trangthai BIT
);

-- ================================================================================== nhóm dịch vụ ==================================================================================
CREATE TABLE Baoduong (
    Id INT PRIMARY KEY IDENTITY,
    KhachhangId INT,
    XeId INT,
    Ngayhen DATETIME,
    Noidung NVARCHAR(500),
    Trangthai NVARCHAR(50),
    FOREIGN KEY (KhachhangId) REFERENCES Khachhang(Id),
    FOREIGN KEY (XeId) REFERENCES Xe(Id)
);

-- ================================================================================== nhóm tài chính ==================================================================================
CREATE TABLE Laixuat (
    Id INT PRIMARY KEY IDENTITY,
    Tennganhang NVARCHAR(150),
    Tyla DECIMAL(5,2),
    Ngaycapnhat DATETIME DEFAULT GETDATE()
);

--================================================================================== cấu hình hệ thống ==================================================================================
CREATE TABLE CauhinhSEO (
    Id INT PRIMARY KEY IDENTITY,
    MetaTitle NVARCHAR(300),
    MetaDescription NVARCHAR(500)
);

--Wishlist(Khách lưu xe yêu thích)
CREATE TABLE Wishlist (
    Id INT PRIMARY KEY IDENTITY,
    KhachhangId INT,
    XeId INT,
    Ngaytao DATETIME DEFAULT GETDATE(),

    FOREIGN KEY (KhachhangId) REFERENCES Khachhang(Id),
    FOREIGN KEY (XeId) REFERENCES Xe(Id)
);

--đánh xá xe
CREATE TABLE Danhgia (
    Id INT PRIMARY KEY IDENTITY,
    KhachhangId INT,
    XeId INT,
    Sosao INT,
    Noidung NVARCHAR(1000),
    Ngaytao DATETIME DEFAULT GETDATE(),
    Trangthai BIT DEFAULT 0,

    FOREIGN KEY (KhachhangId) REFERENCES Khachhang(Id),
    FOREIGN KEY (XeId) REFERENCES Xe(Id)
);

CREATE TABLE Lienhe (
    Id INT PRIMARY KEY IDENTITY,
    Hoten NVARCHAR(150),
    Email NVARCHAR(150),
    Sodienthoai NVARCHAR(20),
    Noidung NVARCHAR(1000),
    Ngaygui DATETIME DEFAULT GETDATE(),
    Trangthai NVARCHAR(50)
);

CREATE TABLE Phanquyen (
    Id INT PRIMARY KEY IDENTITY,
    Tenquyen NVARCHAR(100)
);

CREATE TABLE LogHoatdong (
    Id INT PRIMARY KEY IDENTITY,
    AdminId INT,
    Hanhdong NVARCHAR(500),
    Thoigian DATETIME DEFAULT GETDATE(),

    FOREIGN KEY (AdminId) REFERENCES Nguoiquantri(Id)
);

CREATE TABLE CauhinhHeThong (
    Id INT PRIMARY KEY IDENTITY,
    TenWebsite NVARCHAR(300),
    Hotline NVARCHAR(50),
    Email NVARCHAR(150),
    Diachi NVARCHAR(300),
    Logo NVARCHAR(300)
);

-- dữ liệu:
CREATE PROCEDURE TaoAdminMacDinh
AS
BEGIN
    IF NOT EXISTS (SELECT 1 FROM Nguoiquantri WHERE Email = 'admin@oto.vn')
    BEGIN
        INSERT INTO Nguoiquantri
        (Hoten, Email, Matkhau, Mabaomat, Vaitro, Trangthai, Ngaytao, Solandangsai)
        VALUES
        (N'Phạm Trung Tín', 'admin@oto.vn', '123', '8049', 'AdminTong', 1, GETDATE(), 0);
    END
END;

EXEC TaoAdminMacDinh;

SELECT *
FROM Nguoiquantri
WHERE Email = 'admin@oto.vn';

SELECT Email, Matkhau, Mabaomat
FROM Nguoiquantri;

-- xóa admin
CREATE PROCEDURE XoaAdmin
    @Email NVARCHAR(150)
AS
BEGIN
    DECLARE @AdminId INT

    SELECT @AdminId = Id FROM Nguoiquantri WHERE Email = @Email

    DELETE FROM LogDangnhap WHERE AdminId = @AdminId
    DELETE FROM Nguoiquantri WHERE Id = @AdminId
END
EXEC XoaAdmin 'admin@oto.vn';

-- Trigger lock tài khaonr khi đăng nhập sai quá 3 lần và tự mở khóa sau 5p
CREATE TRIGGER trg_KhoaAdmin
ON LogDangnhap
AFTER INSERT
AS
BEGIN
    UPDATE nq
    SET 
        Solandangsai =
            CASE 
                WHEN i.Thanhcong = 0 THEN nq.Solandangsai + 1
                ELSE 0
            END,
        ThoigianKhoa =
            CASE
                WHEN i.Thanhcong = 0 
                     AND nq.Solandangsai + 1 >= 3
                THEN DATEADD(MINUTE, 5, GETDATE())
                ELSE nq.ThoigianKhoa
            END
    FROM Nguoiquantri nq
    INNER JOIN inserted i ON nq.Id = i.AdminId;
END
GO

DECLARE @AdminId INT = 1;
UPDATE Nguoiquantri
SET 
    Solandangsai = 0,
    ThoigianKhoa = NULL
WHERE Id = @AdminId
AND ThoigianKhoa < GETDATE();

USE QLKinhdoanhoto_admin
GO


ALTER TABLE Cars
ADD NgayTao DATETIME DEFAULT GETDATE()

ALTER TABLE Cars
ADD Noidung NVARCHAR(MAX)
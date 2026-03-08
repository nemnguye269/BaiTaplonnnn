-- 1. XÓA SẠCH DỮ LIỆU CŨ (Để tránh bị trùng lặp)
DELETE FROM KhoaHoc;

-- 2. RESET SỐ ID VỀ 0 (Để khi thêm mới nó bắt đầu từ ID 1)
DBCC CHECKIDENT ('KhoaHoc', RESEED, 0);

-- 3. THÊM ĐÚNG 6 KHÓA HỌC (Hiển thị đẹp trên lưới 3x2)
INSERT INTO KhoaHoc (TenKhoaHoc, HocPhi, HinhAnh, MoTa)
VALUES 
(N'Lập trình Web Fullstack với .NET Core 8', 2500000, 'https://files.fullstack.edu.vn/f8-prod/courses/7.png', N'Học làm web trọn gói từ giao diện đến cơ sở dữ liệu với công nghệ mới nhất của Microsoft.'),

(N'Thành thạo ReactJS trong 30 ngày', 1200000, 'https://files.fullstack.edu.vn/f8-prod/courses/13/6200af9262b30.png', N'Khóa học Frontend chuyên sâu, giúp bạn xây dựng giao diện web hiện đại, mượt mà.'),

(N'Python cho Phân tích dữ liệu (Data Science)', 1800000, 'https://code.visualstudio.com/assets/docs/languages/python/python-tutorial.png', N'Làm chủ ngôn ngữ Python để xử lý dữ liệu lớn, vẽ biểu đồ và nhập môn trí tuệ nhân tạo.'),

(N'Lập trình di động Flutter từ A-Z', 900000, 'https://storage.googleapis.com/cms-storage-bucket/0dbfcc7a59cd1cf16282.png', N'Viết code một lần, chạy được trên cả iOS và Android với Google Flutter.'),

(N'Cơ sở dữ liệu SQL Server chuyên sâu', 600000, 'https://learn.microsoft.com/en-us/sql/media/logo-sql-server.svg', N'Thành thạo truy vấn SQL, thiết kế database chuẩn hóa và tối ưu hiệu suất hệ thống.'),

(N'Khóa học Java Spring Boot cho người mới', 0, 'https://spring.io/img/og-spring.png', N'Nhập môn lập trình Backend mạnh mẽ với Java Spring Boot. Miễn phí cho người mới bắt đầu.');
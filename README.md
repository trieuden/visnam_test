# Hệ thống bán hàng POS (Visnam Test)

Hướng dẫn chi tiết cách cài đặt, cấu hình và khởi chạy dự án.

## 1. Clone Dự Án

Mở terminal và chạy lệnh sau để tải mã nguồn về máy:

```bash
git clone https://github.com/trieuden/visnam_test.git
```

## 2. Cấu Hình Backend (BE)

### Bước 1: Tạo file cấu hình Docker (.env)

Tạo một file mới tên là `.env` trong thư mục `be` và dán nội dung sau vào file `.env`:

```ini
FE_URL=http://localhost:5173
DB_PASSWORD=123456
DB_USER=postgres
DB_NAME=db
```

### Bước 2: Tạo file cấu hình ứng dụng (appsettings.json)

Sao chép file `appsettings.json.example` và đổi tên thành `appsettings.json`. Sau đó, thay thế nội dung của file `appsettings.json` bằng đoạn mã sau:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5433;Database=db;Username=postgres;Password=123456"
  },
  "Jwt": {
    "Token": "k9s82h492b492b4792b4k92b492bk492b492bk49b2k492bk492b2k94b29k4b294kb294kb294kb29k4b294k9b2k4"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

### Bước 3: Cài đặt và Khởi chạy Backend

1. **Mở terminal và di chuyển vào thư mục backend**:

```bash
cd be
```

Tại terminal của thư mục `be`, chạy các lệnh sau:

2. **Cài đặt các thư viện cần thiết**:

   ```bash
   dotnet restore
   ```

3. **(Tùy chọn) Chạy thử server ở local để kiểm tra lỗi**:

   ```bash
   dotnet run
   ```

4. **Build và chạy toàn bộ hệ thống (Database + API) bằng Docker**:

   ```bash
   docker compose up --build
   ```

5. **Tạo migration seed data và cập nhật database**

Sau khi đã cấu hình seed data trong code (HasData), bạn cần chạy các lệnh sau để cập nhật dữ liệu mẫu vào database:

```bash
dotnet ef migrations add SeedData
```

Sau đó chạy tiếp:

```bash
dotnet ef database update
```

Docker sẽ tự động tạo các container cho Database và API.

## 3. Cấu Hình Frontend (FE)

### Bước 1: Tạo file cấu hình môi trường (.env)

Tạo file `.env` trong thư mục `fe` và dán nội dung sau vào file:

```ini
VITE_BE_URL=http://localhost:8080/api
```

### Bước 2: Cài đặt và Khởi chạy Frontend

1. **Mở Terminal mới và di chuyển vào thư mục frontend**

```bash
cd fe
```

Tại terminal của thư mục `fe`, chạy các lệnh sau:

2. **Cài đặt các thư viện node_modules**:

   ```bash
   npm install
   ```

3. **Khởi chạy server development**:
   ```bash
   npm run dev
   ```

Server Frontend sẽ chạy tại **http://localhost:5173**.

## 4. Truy Cập Hệ Thống

Sau khi hoàn thành các bước trên và hệ thống đã được khởi chạy, bạn có thể truy cập vào các địa chỉ sau:

- **Trang web (Frontend)**: [http://localhost:5173](http://localhost:5173)
- **API Swagger (Backend)**: [http://localhost:8080/swagger](http://localhost:8080/swagger)

---

## 5. Kiểm thử đăng nhập với 2 trình duyệt

1. **Mở 2 trình duyệt khác nhau** (hoặc 2 cửa sổ ẩn danh):

- Một trình duyệt cho tài khoản user (vai trò máy POS tạo đơn hàng).
- Một trình duyệt cho tài khoản admin (vai trò admin, xem đơn hàng cập nhật tự động).

2. **Đăng nhập tài khoản user**:

- Tài khoản: `user`
- Mật khẩu: `user123`
- Sau khi đăng nhập, sử dụng chức năng tạo đơn hàng như máy POS.

3. **Đăng nhập tài khoản admin**:

- Tài khoản: `admin`
- Mật khẩu: `admin123`
- Sau khi đăng nhập, vào trang quản lý đơn hàng để theo dõi các đơn hàng được cập nhật tự động (real-time).

4. **Kiểm tra**:

- Khi user tạo đơn hàng mới, trình duyệt admin sẽ thấy đơn hàng mới xuất hiện tự động mà không cần reload trang.

---

## Một số lưu ý

- **Docker**: Đảm bảo Docker đã được cài đặt và chạy trên máy của bạn.
- **Ports**: Kiểm tra xem các cổng không bị xung đột với các ứng dụng khác trên máy.

# Hệ thống bán hàng POS (Visnam Test)

Hướng dẫn chi tiết cách cài đặt, cấu hình và khởi chạy dự án.

## 1. Clone Dự Án

Mở terminal và chạy lệnh sau để tải mã nguồn về máy:

```bash
git clone https://github.com/trieuden/visnam_test.git
```

## 2. Cấu Hình Backend (BE)

### Bước 1: Di chuyển vào thư mục backend

```bash
cd be
```

### Bước 2: Tạo file cấu hình Docker (.env)

Tạo một file mới tên là `.env` trong thư mục `be` và dán nội dung sau vào file `.env`:

```ini
FE_URL=http://localhost:5173
DB_PASSWORD=123456
DB_USER=postgres
DB_NAME=db
```

- **FE_URL**: Địa chỉ Frontend.
- **DB_PASSWORD**, **DB_USER**, **DB_NAME**: Thông tin kết nối đến PostgreSQL.

### Bước 3: Tạo file cấu hình ứng dụng (appsettings.json)

Sao chép file `appsettings.json.example` và đổi tên thành `appsettings.json`. Sau đó, thay thế nội dung của file `appsettings.json` bằng đoạn mã sau:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=db;Port=5432;Database=db;Username=postgres;Password=123456"
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

- **DefaultConnection**: Cấu hình chuỗi kết nối database.
- **Jwt.Token**: Mã token JWT dùng để xác thực.
- **Logging**: Thiết lập logging cho ứng dụng.

### Bước 4: Cài đặt và Khởi chạy Backend

Tại terminal của thư mục `be`, chạy các lệnh sau:

1. **Cài đặt các thư viện cần thiết**:

   ```bash
   dotnet restore
   ```

2. **(Tùy chọn) Chạy thử server ở local để kiểm tra lỗi**:

   ```bash
   dotnet run
   ```

3. **Build và chạy toàn bộ hệ thống (Database + API) bằng Docker**:
   ```bash
   docker compose up --build
   ```

Docker sẽ tự động tạo các container cho Database và API.

## 3. Cấu Hình Frontend (FE)

### Bước 1: Di chuyển vào thư mục frontend

```bash
cd fe
```

### Bước 2: Tạo file cấu hình môi trường (.env)

Tạo file `.env` trong thư mục `fe` và dán nội dung sau vào file:

```ini
VITE_BE_URL=http://localhost:8080/api
```

- **VITE_BE_URL**: Địa chỉ Backend API (mặc định là `localhost:8080`).

### Bước 3: Cài đặt và Khởi chạy Frontend

Tại terminal của thư mục `fe`, chạy các lệnh sau:

1. **Cài đặt các thư viện node_modules**:

   ```bash
   npm install
   ```

2. **Khởi chạy server development**:
   ```bash
   npm run dev
   ```

Server Frontend sẽ chạy tại **http://localhost:5173**.

## 4. Truy Cập Hệ Thống

Sau khi hoàn thành các bước trên và hệ thống đã được khởi chạy, bạn có thể truy cập vào các địa chỉ sau:

- **Trang web (Frontend)**: [http://localhost:5173](http://localhost:5173)
- **API Swagger (Backend)**: [http://localhost:8080/swagger](http://localhost:8080/swagger)

---

## Một số lưu ý

- **Docker**: Đảm bảo Docker đã được cài đặt và chạy trên máy của bạn.
- **Ports**: Kiểm tra xem các cổng không bị xung đột với các ứng dụng khác trên máy.

Nếu bạn gặp bất kỳ vấn đề gì, đừng ngần ngại chia sẻ với mình để hỗ trợ nhé!

---

Hy vọng định dạng Markdown này sẽ dễ đọc hơn cho bạn!

# ⚡ EVBattery Trading Platform – WPF + NestJS + PayOS

> 🚗 **EVBattery** là nền tảng mua bán và quản lý pin & xe điện 2nd-hand.
> Dự án gồm hai phần chính:
>
> * **Backend API (NestJS)** – RESTful service quản lý tài khoản, bài đăng, thanh toán, v.v.
> * **WPF Desktop App (C#/.NET 8)** – Ứng dụng khách thân thiện, hỗ trợ người dùng thao tác nhanh.

---

## 🧩 1️⃣ Giới thiệu tổng quan

**EVBattery Trading Platform** giúp kết nối người dùng có nhu cầu
mua – bán – trao đổi **pin điện và phương tiện EV** (xe điện, xe máy điện, pin rời).

Nền tảng cung cấp:

* 👤 Quản lý tài khoản người dùng, phân quyền admin
* 💾 Đăng tin, duyệt bài, đánh giá & log lịch sử duyệt
* 🔋 Quản lý pin & phương tiện điện (Car, Bike, Battery)
* 💰 Tích hợp **PayOS** cho thanh toán và nạp ví
* 🦩️ Giao diện **WPF MVVM** hiện đại, dễ mở rộng

---

## ⚙️ 2️⃣ Kiến trúc hệ thống

```
Solution 'EVBattery'
│
├── EVBattery.Core/              # Model, DTO, Enum dùng chung
├── EVBattery.Infrastructure/    # Services, API Client, Config
├── EVBattery.UI.WPF/            # Giao diện WPF (MVVM pattern)
└── Backend (NestJS API)/        # API server chạy tại https://kali.mshiroru.site/
```

---

## 🧠 3️⃣ Công nghệ sử dụng

| Layer                  | Tech Stack                              | Ghi chú                      |
| ---------------------- | --------------------------------------- | ---------------------------- |
| **Frontend (Desktop)** | 🦩️ WPF (.NET 8) + MVVM + XAML          | UI client                    |
| **Backend API**        | ⚙️ NestJS + TypeORM + PostgreSQL        | REST API                     |
| **Payment**            | 💳 PayOS SDK                            | Tích hợp nạp ví / thanh toán |
| **Database**           | 🗄️ PostgreSQL + Redis (cache optional) |                              |
| **CI/CD**              | 🤖 GitHub Actions                       | Tự động build + test         |
| **Auth**               | 🔐 JWT / Passport Strategy              | Login / Register             |
| **HTTP**               | 🌐 HttpClient (C#) / Axios (NodeJS)     |                              |

---

## 🚀 4️⃣ Hướng dẫn sử dụng

### 🦩️ **Client App (WPF)**

#### 📥 Cài đặt

1. Clone repo:

   ```bash
   git clone https://github.com/<your-username>/EVBattery.git
   cd EVBattery
   ```
2. Mở `EVBattery.sln` bằng **Visual Studio 2022+**
3. Set **Startup Project** là `EVBattery.UI.WPF`
4. Chạy `dotnet restore` (hoặc để Visual Studio tự khôi phục)

#### ▶️ Chạy app

* **F5** hoặc **Ctrl + F5** để chạy ứng dụng.
* Màn hình đầu tiên: **Login / Register**
* Sau khi đăng nhập → mở **MainWindow** với các tính năng:

  * Xem bài đăng
  * Tạo bài đăng mới
  * Nạp ví qua PayOS
  * Quản lý tài khoản cá nhân

---

## 💡 5️⃣ CI/CD (GitHub Actions)

Mỗi khi **push** hoặc **mở pull request**, hệ thống tự động:

1. Build project .NET + NestJS
2. Chạy unit test (nếu có)
3. Hiển thị “✅ All checks passed” trong PR

Cấu hình nằm tại:
`.github/workflows/ci.yml`

---

## 🗾 9️⃣ License

```
MIT License © 2025 EVBattery Team
```

---

## 🌟 10️⃣ Ghi chú thêm

* Mọi API chạy qua endpoint thật:
  **[https://kali.mshiroru.site/api/](https://kali.mshiroru.site/api/)**
* Nếu test local: thay `BaseAddress` trong `ApiClient.cs`

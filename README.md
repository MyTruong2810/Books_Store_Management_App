# Books Store Management App

## Thành viên nhóm 
- **22120214** Trương Thị Tú My - *Team Leader*
- **22120217** Hoàng Lê Nam
- **20120319** Phan Dương Linh

---

## Notice - Set up 
1. **Database Setup:**
   - Tạo **database mybookstore** và chạy script từ file **Database.txt**.
   - Cấu hình lại đường kết nối trong file **appsettings.json** (nằm trong thư mục gốc).

2. **Thông tin đăng nhập vào app:**
   - **Username:** `user`  
   - **Password:** `123`  
   - Ngoài ra, có thể đăng nhập thông qua Google.

---

## Phân chia thời gian và công việc trong Milestone 3

**Số giờ làm việc tối thiểu:**  
10 giờ (do nhóm đã hoàn thành 10 giờ ở Milestone 1 và 10 giờ ở Milestone 2).

### Phân chia thời gian:
- **1 giờ:** Họp thảo luận timeline và công việc.
- **1 giờ:** Fix lỗi test failed ở Milestone 2.
- **5 giờ:** Implement code theo phân công.
- **2 giờ:** Kiểm thử và sửa lỗi hoàn thiện app.
- **1 giờ:** Tổng kiểm tra tiến độ nghiệm thu và viết báo cáo.

---

## Đánh giá tiến độ theo tiêu chí (100% đáp ứng tiêu chí nghiệm thu)

### **UI/UX (100% hoàn thành)**

#### Công việc thực hiện:
- **Yêu cầu chức năng:**
  - **UI:**
    - **LoginPage:** Hiển thị đầy đủ các trường `username`, `password`, `save password`, `login button`, `google button`.
    - **DashboardPage:** Hiển thị thông tin:
      - `SoldOut`, `BestSeller`, `Total Customers`, `Total Users`, `Total Orders`, `Total Revenue`.
      - Bộ lọc: Daily, Week, Month.
    - **OrderPage**, **StockPage**, **CustomerPage**, **ClassificationPage:**  
      - Hỗ trợ: `update`, `delete`, `sort`, `search`, `filter`, `add button`.
    - **Add/Update Pages:** Hiển thị đầy đủ các trường thông tin.
    - **InvoicePage:** Hoá đơn đầy đủ trường thông tin.
    - **StatisticPage:** Đồ thị doanh thu (Daily, Month, Year), thông báo tồn kho.
    - **AdminPage:** Hiển thị thông tin Admin.
    - **Setting:** Chế độ `Dark/Light mode`.
    - **Thông báo:**  
      - **ContentDialog**, **MessageBox** (lỗi, quyết định xóa).  
      - **Toast thông báo thanh toán**.

  - **Features:**  
    - **LoginPage:**  
      - Đăng nhập, lưu mật khẩu cục bộ, hash password bằng SHA-256.  
      - Xác thực Google (Scope: UserProfile).  
      - Hiển thị lỗi khi thông tin sai.  
    - **DashboardPage:** Hiển thị dữ liệu, lọc theo Daily, Week, Month.  
    - **OrderPage**, **StockPage**, **ClassificationPage**, **CustomerPage:**  
      - Hỗ trợ: `search`, `filter`, `sort`, `add`, `update`, `delete`.  
    - **AdminPage:** Cập nhật thông tin Admin.  
    - **ReadOrderPage:** Xem thông tin order (không chỉnh sửa).  
    - **InvoicePage:** Xuất hoá đơn PDF hoàn chỉnh.  
    - **StatisticPage:** Đồ thị doanh thu, thông báo tồn kho.  
    - **QR Code Payment:**  
      - Chưa hoàn thiện (do lỗi xác thực tài khoản Zalo Sandbox).  
    - **Logo:** Hiển thị logo ứng dụng.

- **Yêu cầu phi chức năng:**  
  - Back-end + Database:  
    - Lưu trữ account, giao dịch giả lập thanh toán.  
    - Kết nối PostgreSQL, xử lý exception (95% lỗi nhập liệu).  
    - Architecture theo mô hình MVVM.

**Kết quả:**  
- Đạt 100% công việc đề ra.

---

### **Design Patterns / Architecture (100% hoàn thành)**

#### Công việc thực hiện:
- Sử dụng mô hình **MVVM**:
  - Logic xử lý chuyển lên ViewModel.
  - Hạn chế logic ở code-behind.

**Kết quả:**  
- App tổ chức theo mô hình MVVM đúng yêu cầu.

---

### **Advanced Topics (100% hoàn thành)**

#### Công việc thực hiện:
- **Google Authentication, Zalo Sandbox (QR Code Payment).**
- **Dark/Light mode:**  
  - Dynamic themes, dynamic logo.  
- **App Notifications:**  
  - Sử dụng **AppNotificationManager** hiển thị trạng thái thanh toán.  

**Kết quả:**  
- Các tính năng nâng cao được triển khai thành công.

---

### **Teamwork - Git Flow (100% hoàn thành)**

#### Công việc thực hiện:
- **Họp nhóm:**  
  - Online qua Google Meet (10/12/2024, 30 phút).  
- **Quản lý công việc:**  
  - Trello (vai trò rõ ràng).
  - Minh chứng
    ![image](https://github.com/user-attachments/assets/f0734253-c395-4072-a9df-bfb4f94bcf4f)
    ![image](https://github.com/user-attachments/assets/ec6120ab-0eb1-4e4b-b372-65aab1c6ccf5)
    ![image](https://github.com/user-attachments/assets/42f9cd51-f910-48c7-b81d-e8fb4ae412ee)
- **Quản lý source:**  
  - GitHub với GitFlow:
    - Mỗi Dev có nhánh **feature** riêng.
    - Merge vào nhánh **development**.
    - Team leader chịu trách nhiệm merge code.
    - Minh chứng
      ![image](https://github.com/user-attachments/assets/d2b37841-b9f4-4083-ab53-e3c4cf5df6e7)
      ![image](https://github.com/user-attachments/assets/ee5dbc1b-2eaa-4683-bb5a-9bfbce39f4a5)
      ![Uploading image.png…]()

**Phân công công việc Milestone 3:**  
- **Trương Thị Tú My:**  
  - Xác thực hash password, Google.  
  - UI thống nhất, merge code, viết test case, báo cáo.  
  - Theo dõi tiến độ.  
- **Hoàng Lê Nam:**  
  - Validate thanh toán, cập nhật database.  
  - Thanh toán giả lập, Zalo Pay (tạo QR).  
  - Logic cập nhật order, quản lý connection strings.  
- **Phan Dương Linh:**  
  - Thiết kế Dark/Light mode.  
  - Logo nhận diện thương hiệu.

**Kết quả:**  
- Teamwork hiệu quả, hoàn thành nhiệm vụ.

---

### **Quality Assurance (100% hoàn thành)**

#### Công việc thực hiện:
- **Manual Test:**  
  - TestDoc3 (100% yêu cầu, tỉ lệ thành công 100%).  
- **Docs:**  
  - **Doxygen:** Summary Object, Function.  
  - ArchitectureDoc.pdf.

**Kết quả:**  
- Đảm bảo chất lượng đúng quy trình.

---

## Tổng kết
- Nhóm hoàn thành tốt các tiêu chí nghiệm thu (>95%).  
- **Điểm đề xuất: 10**.

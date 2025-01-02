# Books_Store_Management_App

## Thành viên nhóm 
- 22120214 Trương Thị Tú My - Team Leader
- 22120217 Hoàng Lê Nam
- 20120319 Phan Dương Linh

## Notice - Set up 
- Tạo **database mybookstore** và chạy scripts file **Database.txt Config lại đường kết nối trong **appsettings.json** file nằm sau cấp thư mục gốc.
- Thông tin Login vào app: **username: user, password: 123** (Có thể đăng nhập qua google).
  
## Phân chia thời gian và công việc trong milestone 3
**Với số giờ làm việc tối thiểu để được nghiệm thu là 10h cho milestone 3 do nhóm đã hoàn thành 10h ở milestone 1 và 10h ở milestone 2**. Phân chia thời gian công việc như sau:
- **1h**: Cho các cuộc họp, thảo luận timeline, công việc cho giai đoạn xây dựng app ở milestone 3.
- **1h**: Cho việc fix bug đã test failed ở milestone 2.
- **5h**: Cho việc implement code theo phân công ở milestone 3.
- **2h**: Cho quá trình kiểm thử và sửa lỗi cuối cùng hoàn thiện app.
- **1h**: Check tổng tiến độ nghiệm thu và viết báo cáo.

## Đánh giá tiến độ theo tiêu chí (100% đáp ứng tiêu chí nghiệm thu)

### UI/UX (Hoàn thành 100% tiêu chí đề ra)
Để hoàn thành tiêu chí này nhóm thực hiện công việc thiết kế và implement code như sau:
- **Yêu cầu chức năng**
  - **UI** (Hoàn thành 100% các components và đáp ứng đầy đủ yêu cầu thiết kế)
    - LoginPage: Hiển thị đầy đủ các trường đăng nhập: username, password, save password, login button, google button.
    - DashboardPage: Hiển thị:
      - ListView với thông tin về SoldOut và BestSeller.
      - Bốn trường: Total Customers, Total Users, Total Orders, Total Revenue.
      - Bộ lọc: Daily, Week, Month.
    - OrderPage, StockPage, CustomerPage, ClassificationPage: ListView hỗ trợ: update, delete, sort, search, filter, add button.
    - Các trang thêm và cập nhật: Hiển thị đầy đủ các trường thông tin: AddStockPage, AddOrderPage, AddCustomerPage, AddClassificationPage, UpdateStockPage, UpdateOrderPage, UpdateCustomerPage, UpdateClassificationPage, AdminPage, UpdateAdminPage, ReadOrderPage.
    - InvoicePage: Hoá đơn định dạng đầy đủ các trường thông tin.
    - StatisticPage: Đồ thị phân tích tổng doanh thu theo Daily, Month, Year và thông báo nhập hàng.
    - AdminPage: Hiển thị thông tin Admin.
    - Setting: Chế độ Dark/Light mode.
    - Thông báo: Đầy đủ ContentDialog, MessageBox (lỗi, quyết định xóa), và thêm Toast thông báo thanh toán.

  - **Features** (Hoàn thành 100% trừ một số ngoại lệ)
    - LoginPage:
        - Đăng nhập và lưu mật khẩu cục bộ.
        - Hash password bằng SHA-256.
        - Thông báo lỗi khi thông tin sai.
        - Xác thực Google (Scope: UserProfile) và cung cấp thông tin đăng nhập.
    - DashboardPage: Hiển thị dữ liệu và lọc theo Daily, Week, Month.
    - OrderPage, StockPage, ClassificationPage, CustomerPage:
      - Hoàn thiện: search, filter, sort.
      - Thực hiện add, update, delete.
    - AdminPage: Cập nhật thông tin Admin.
    - Các trang thêm/cập nhật thông tin: Hoàn thành 100%.
    - ReadOrderPage: Xem thông tin order, không cho phép chỉnh sửa.
    - InvoicePage: Xuất hoá đơn PDF hoàn chỉnh.
    - StatisticPage: Hiển thị đồ thị doanh thu theo thời gian và thông báo tồn kho.
    - QR Code Payment: Chưa hoàn thiện (do lỗi xác thực tài khoản Zalo Sandbox, là hình thức bổ sung cho thanh toán tiền mặt).
    - Hiển thị logo ứng dụng.
    
- **Yêu cầu phi chức năng**
  - Back-end + Database:
    - Hoàn thành 100%, bao gồm lưu trữ thông tin account và giao dịch giả lập thanh toán.
    - Kết nối và xây dựng hàm lấy dữ liệu từ PostgreSQL.
    - Xử lý Exception: Đạt 95% các lỗi nhập liệu từ người dùng.
    - Architecture: Đảm bảo theo mô hình MVVM.

**Kết quả đạt được:**
- App thiết kế đảm bảo 100% các công việc đề ra và hoàn thành phần implement theo công việc đề ra.

### Design patterns / architecture (Hoàn thành 100% tiêu chí đề ra)
Tiếp tục thực hiện xây dựng ứng dụng theo mô hình MVVM, tách biệt logic của lớp View và lớp Model, mọi logic xử lý đều chuyển lên ViewModel, hạn chế xử lý phần logic ở code-behind.

**Kết quả đạt được:**
Tiếp tục tổ chức app theo mô hình MVVM theo hướng đề ra.

### Advanced topics (Hoàn thành 100% tiêu chí đề ra)
Để hoàn thành tiêu chí này nhóm tiếp tục triển khai các tính năng nâng cao đã làm ở milestone 1, 2 ngoài ra bổ sung thêm 1 số tính năng mới:
- Các kỹ thuật về **Google Authentication, ZaloSanbox với QR Code Payment**.
- Các kỹ thuật về xử lý **Dark/Light mode** thông qua **themes dynamic** và **logo dynamic** cho ứng dụng.
- Hiển thị **App notifications** thông qua **class AppNotificationManager** để hiện thị thông báo trạng thái thanh toán của đơn hàng khi thanh toán, và có thể nhấn vào thông báo này để hiển thị màn hình Invoice.

**Kết quả đạt được:**
Các thành viên triển khai được những tính năng nâng cao và áp dụng vào code.

### Teamwork - Git flow (Hoàn thành 100% tiêu chí đề ra)
- Milestone này nhóm thực hiện họp online thông qua Google Meet trong 30' vào ngày 10/12/2024 để thảo luận về các công việc cần giải quyết tiếp theo.
- Quản lý công việc nhóm thông qua Trello với vai trò như đã trình bày trong milestone 1.
- Minh chứng:
![image](https://github.com/user-attachments/assets/f0734253-c395-4072-a9df-bfb4f94bcf4f)
![image](https://github.com/user-attachments/assets/ec6120ab-0eb1-4e4b-b372-65aab1c6ccf5)
![image](https://github.com/user-attachments/assets/42f9cd51-f910-48c7-b81d-e8fb4ae412ee)

- Quản lý source trên **Github**:
- Các Dev phát triển có nhánh **feature** riêng.
- Có nhánh **development** thực hiện merge code từ các nhánh feature.
- Người chịu trách nhiệm merge code là team leader.
- Minh chứng:
  ![image](https://github.com/user-attachments/assets/d2b37841-b9f4-4083-ab53-e3c4cf5df6e7)
  ![image](https://github.com/user-attachments/assets/ee5dbc1b-2eaa-4683-bb5a-9bfbce39f4a5)
  ![Uploading image.png…]()

- Phân chia công việc cho các thành viên trong team ở milstone 2 như sau:
- Trương Thị Tú My:
  - Xác thực với hash password, goole.
  - Chỉnh sửa UI thống nhất giữa cho ứng dụng.
  - Merge code, viết testcase cho các tính năng trên page đảm nhiệm và viết tổng báo cáo.
  - Kiểm tra, theo dõi tiến độ làm việc.
- Hoàng Lê Nam: 
  - Tiến hành validate khi người dùng không chọn phương thức thanh toán mà nhấn Pay.
  - Cập nhật database thêm các table accounts, transactions và thêm cột is_paid cho bảng order để phục vụ việc giả lập thanh toán.
  - Triển khai chiến lược thanh toán giả lập và Zalo pay (Zalo Pay chỉ đến mức tạo được mã QR) bằng pattern strategy và factory.
  - Thêm logic không thể cập nhật các order đã thanh toán và thêm khả năng thanh toán cho các order chưa thanh thanh toán trong các màn hình cập nhập và xem order.
  - Di chuyển ConnectionStrings và Zalopay key vào file appsettings.json.
  - Merge code, viết testcase cho các tính năng trên page đảm nhiệm.
- Phan Dương Linh:
  + Phụ trách thiết kế tính năng Dark/Light mode.
  + Thêm logo cho ứng dụng để nhận diện thương hiệu.
  + Merge code, viết testcase cho các tính năng trên page đảm nhiệm.

**Kết quả đạt được:**
Thực hiện được mô hình team work đề ra.

### Quality assurance (Hoàn thành 100% tiêu chí đề ra)
Để hoàn thành tiêu chí này nhóm thực hiện công việc như sau:
- **Manual test**, giảng viên xem file **TestDoc3** kèm theo, thực hiện test trên 100% yêu cầu đề ra trong đó tỉ lệ đáp ứng thành công 100%.
- **Về Docs**: Nhóm thực hiện summary các Object và function, sau đó sử dụng Doxychen tạo Docs, giảng viên xem thư mục **doxychen_docs**. Ngoài ra có 1 tài liệu cơ bản về architecture giảng viên xem qua file **ArchitectureDoc.pdf**.

**Kết quả đạt được:**
Đảm bảo được quy trình đảm bảo chất lượng đề ra.

## Tổng kết
Nhìn chung nhóm thực hiện tốt những tiêu chí nghiệm thu đề ra, ở milestone này nhóm hoàn thiện các tính năng của ứng dụng đáp ứng trên 95% như ban đầu đề ra xây dựng ứng dụng.
**--> Điểm đánh giá 10.**

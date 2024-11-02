# Books_Store_Management_App

## Thành viên nhóm 
- 22120214 Trương Thị Tú My - Team Leader
- 22120217 Hoàng Lê Nam
- 20120319 Phan Dương Linh

## Notice - Set up 
- Ứng dụng có sử dụng một số theme hệ thống nên có thể sẽ ảnh hưởng đến font, color chính mà nhóm muốn thực hiện. Điều này có thể gây ra trải nghiệm không trực quan và nhóm sẽ giải quyết sau do tính chất thời gian.
- Tạo **database mybookstore** và chạy scripts file **Database.txt ("Server=localhost;Port=5432;User Id=postgres;Password=1234;Database=mybookstore")**
- **Login: Username: user1      Password: 123**
## Phân chia thời gian và công việc trong milestone 1
**Với số giờ làm việc tối thiểu để được nghiệm thu là 10h**. Phân chia thời gian công việc như sau:
- **1h**: Cho các cuộc họp, thảo luận timeline, công việc cho từng giai đoạn xây dựng app ở milestone 1.
- **1h**: Cho việc thiết kế bảng design tổng thể cho app, đáp ứng đầy đủ các phần của milestone 1.
- **1h**: Cho việc tìm hiểu kỹ về mô hình **MVVM** và các tính năng nâng cao được áp dụng vào app.
- **30'**: Cho việc tìm hiểu về cách làm việc trên môi trường **Github**.
- **30'**: Cho việc nghiên cứu và tìm hiểu về cách thức kiểm thử và đưa ra quyết định lựa chọn phương thức kiểm thử cho app ở milestone 1.
- **4h**: Cho việc implement code theo phân công ở milestone 1.
- **1h**: Cho quá trình kiểm thử và sửa lỗi.
- **1h**: Cho việc check tổng tiến độ nghiệm thu và viết báo cáo.

## Đánh giá tiến độ theo tiêu chí (100% đáp ứng tiêu chí nghiệm thu)

### UI/UX (Hoàn thành 100% tiêu chí đề ra)
Để hoàn thành tiêu chí này nhóm thực hiện công việc thiết kế và implement code như sau:
- **Yêu cầu chức năng**
  - **UI** (Hoàn thành 50% tổng thể app): 
    - LoginPage: Hiển thị các trường đăng nhập bao gồm username, password, save password, login button.
    - DashboardPage: Hiển thị Listview về SoldOut và BestSeller.
    - OrderPage, StockPage, CustomerPage, ClassificationPage: Hiển thị một Listview thông tin.
    - AddStockPage, AddOrderPage, UpdateStockPage, UpdateOrderPage, AdminPage: Hiển thị các trường thông tin.
    - InvoicePage: Định dạng một hóa đơn.
  - **Features** (Hoàn thành 50% tổng thể app)
    - LoginPage: Thực hiện tính năng đăng nhập, lưu mật khẩu vào local setting. 
    - OrderPage: 
      - Giải quyết tính năng sort theo các trường.
      - Hoàn thành 50% tính năng search + filter.
      - Thực hiện chức năng add, update, delete order.
    - StockPage:
      - Giải quyết tính năng sort theo các trường.
      - Hoàn thành 50% tính năng search + filter.
      - Thực hiện chức năng add, update, delete stock.
    - AddStockPage: Hoàn thành 80% tính năng trang thêm thông tin cho sách đủ các trường.
    - AddOrderPage: Hoàn thành 80% tính năng trang thêm thông tin cho order đủ các trường.
    - UpdateStockPage: Hoàn thành 80% tính năng cập nhật lại thông tin cho sách.
    - UpdateOrderPage: Hoàn thành 80% tính năng cập nhật lại thông tin đơn hàng cho khách.
    - InvoicePage: Xuất hóa đơn định dạng PDF.

**Kết quả đạt được:**
- 1 Bảng design: [Figma Design](https://www.figma.com/design/36S8ur1xsgoSCQ6U7YhuvR/Untitled?node-id=0-1&node-type=canvas&t=rDrUBGIImtReEX0I-0)
- App thiết kế đảm bảo 100% các công việc đề ra và hoàn thành phần implement theo công việc đề ra.

### Design patterns / architecture (Hoàn thành 100% tiêu chí đề ra)
Để hoàn thành tiêu chí này nhóm thực hiện 1 phần công việc tìm hiểu và áp dụng mô hình MVVM như sau:  
Thực hiện xây dựng ứng dụng theo mô hình MVVM, tách biệt logic của lớp View và lớp Model, mọi logic xử lý đều chuyển lên ViewModel, hạn chế xử lý phần logic ở code-behind.

**Kết quả đạt được:**
Tổ chức app theo mô hình MVVM theo hướng đề ra.

### Advanced topics (Hoàn thành 100% tiêu chí đề ra)
Để hoàn thành tiêu chí này nhóm thực hiện 1 phần công việc nghiên cứu và triển khai các tính năng nâng cao như sau:
- **Thành viên Hoàng Lê Nam**:
  - **INotifyDataErrorInfo**: Triển khai để bắt các lỗi nhập liệu và thông báo ra giao diện bằng cách dùng DataAnnotations.
  - **RelayCommand**: Sử dụng lớp RelayCommand để xử lý các sự kiện đơn giản không cần dùng đến các thành phần giao diện trên View.
  - **Quest PDF**: Tìm hiểu thư viện Quest PDF để tạo trang PDF cho Invoice.
- **Thành viên Phan Dương Linh**:
  - Sử dụng kỹ thuật **BitmapImage** và **WindowHelper** để nâng cao trải nghiệm người dùng và khả năng quản lý UI.
- **Thành viên Trương Thị Tú My**:
  - Đảm bảo chất lượng mô hình MVVM cho ứng dụng.

**Kết quả đạt được:**
Các thành viên triển khai được những tính năng nâng cao và áp dụng vào code.

### Teamwork - Git flow (Hoàn thành 100% tiêu chí đề ra)
Để hoàn thành tiêu chí này nhóm thực hiện công việc họp nhóm và quản lý công việc qua Jira như sau:
- Mỗi milestone họp nhóm 2 lần qua Google Meet, thời gian họp 20' cho mỗi buổi, các biên bản báo cáo được ghi lại bởi một thành viên trong nhóm.
- Nhóm đã thực hiện 2 lần họp nhóm vào ngày 27/09/2024 và 10/10/2024, các văn bản đều được lưu trữ trên Jira.
- Quản lý công việc nhóm thông qua Jira:
  - Trương Thị Tú My: Team leader, chịu trách nhiệm phân chia công việc, theo dõi tiến độ thực hiện, đảm nhận 1 phần công việc QC thông qua review các task.
  - Hoàng Lê Nam: Dev + Tester + đảm nhận 1 phần công việc QC thông qua review các task.
  - Phan Dương Linh: Dev + Tester + đảm nhận 1 phần công việc QC thông qua review các task.
- Mô hình team work:
  - Chia công việc theo mô hình **scrum** qua các **sprint** và chia các **task** trong các sprint.
  - Công việc thực hiện theo luồng **Task --> In Processing --> Review --> Done**.

![image](https://github.com/user-attachments/assets/f5c0eaf3-e66f-4038-92c3-2c8c27808ca9)
![image](https://github.com/user-attachments/assets/174f84e9-9778-4ae0-8853-39d1ec9b107b)

- Quản lý source trên **Github**:
  - Các Dev phát triển có nhánh **feature** riêng.
  - Có nhánh **development** thực hiện merge code từ các nhánh feature.
  - Người chịu trách nhiệm merge code là team leader.

![image](https://github.com/user-attachments/assets/d9115733-b353-4820-b18b-daa7c68a77f7)
![image](https://github.com/user-attachments/assets/252a3260-3f64-4e04-a8aa-2995bc6b3df9)
![image](https://github.com/user-attachments/assets/eb3d2321-3644-4146-ab6c-386d9a1ca68f)

- Phân chia công việc cho các thành viên trong team như sau:
  - Trương Thị Tú My: Cấu trúc source, phụ trách MainPage, DashboardPage, OrderPage, StockPage, LogoutPage và các tính năng trên trang đó.
  - Hoàng Lê Nam: Phụ trách OrderDetailPage, BookPopupPage, OrderReadOnlyDetailPage, InvoicePage và các tính năng trên trang đó.
  - Phan Dương Linh: CustomerPage, AdminPage, ClassificationPage, đang tiến hành SettingPage và StatisticPage và các tính năng trên trang đó.

**Kết quả đạt được:**
Thực hiện được mô hình team work đề ra.

### Quality assurance (Hoàn thành 100% tiêu chí đề ra)
Để hoàn thành tiêu chí này nhóm thực hiện công việc như sau:
- Ban đầu tập trung vào unit test và UI test tự động nhưng do thời gian hạn chế nhóm không đạt được tiêu chí đề ra.
- Chuyển sang **manual test** có **TestDoc** kèm theo, thực hiện test trên 95% yêu cầu đề ra trong đó tỉ lệ đáp ứng thành công 80%.
- Đối với những trường hợp test failed nhóm thực hiện chỉnh sửa ở milestone 2.

**Kết quả đạt được:**
Đảm bảo được quy trình đảm bảo chất lượng đề ra.

## Tổng kết
Nhìn chung nhóm thực hiện tốt những tiêu chí nghiệm thu đề ra, điểm đánh giá 10.

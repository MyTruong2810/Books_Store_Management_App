# Books_Store_Management_App

## Thành viên nhóm 
- 22120214 Trương Thị Tú My - Team Leader
- 22120217 Hoàng Lê Nam
- 20120319 Phan Dương Linh

## Notice - Set up 
- Tạo **database mybookstore** và chạy scripts file **Database.txt ("Server=localhost;Port=5432;User Id=postgres;Password=1234;Database=mybookstore")**
- Thông tin Login vào app: **Username: user1, Password: 123**
  
## Phân chia thời gian và công việc trong milestone 2
**Với số giờ làm việc tối thiểu để được nghiệm thu là 10h cho milestone 2 do nhóm đã hoàn thành 10h ở milestone 1**. Phân chia thời gian công việc như sau:
- **30'**: Cho các cuộc họp, thảo luận timeline, công việc cho giai đoạn xây dựng app ở milestone 2.
- **1h**: Cho việc fix bug đã test failed ở milestone 1.
- **5h**: Cho việc implement code theo phân công ở milestone 2.
- **2h**: Cho việc thiết kế cấu trúc và tìm dữ liệu cho database.
- **45'**: Cho quá trình kiểm thử và sửa lỗi.
- **45'**: Check tổng tiến độ nghiệm thu và viết báo cáo.

## Đánh giá tiến độ theo tiêu chí (100% đáp ứng tiêu chí nghiệm thu)

### UI/UX (Hoàn thành 100% tiêu chí đề ra)
Để hoàn thành tiêu chí này nhóm thực hiện công việc thiết kế và implement code như sau:
- **Yêu cầu chức năng**
  - **UI** (Hoàn thành các components 100% - Đáp ứng yêu cầu UI 80% về mặt thiết kế): 
    -	LoginPage: Hiển thị đủ các trường đăng nhập bao gồm username, password, save password, login button.
    -	DashboardPage: Hiển thị được đủ thông số của Listview về SoldOut và BestSeller, đủ 4 trường thông tin về Total Customers, Total Users, Total Orders và Total Revenue, có thêm 2 trường filter theo Week và Month.
    -	OrderPage, StockPage, CustomerPage, ClassificationPage: 1 Listview hiển thị thông tin update/delete list item, đầy đủ các phần về về sort và search, add button.
    -	 AddStockPage, AddOrderPage, AddCutomerPage, AddClassificationPage, UpdateClassificationPage, UpdateCustomerPage,  UpdateStockPage, UpdateOrderPage, AdminPage, UpdateAdminPage, ReadOrderPage: Hiển thị đủ các trường thông tin.
    -	InvoicePage: Định dạng 1 hoá đơn hoàn chỉnh đủ các trường.
    -	StatisticPage: Thực hiện vẽ được đồ thị phân tích về tổng doanh thu của cửa hàng và có phần thông báo nhập hàng.
    -	AdminPage: Hiện thị thông tin về Admin.
    -	ContentDialog: Có đủ các ContentDialog thông báo về lỗi và quyết định delete item.
    
    **==> Ở milestone 1 nhóm thực hiện 50% về các tính năng UI. Trong milestone 2 này nâng khối lượng lên 100%, hoàn thành tất cả các components cho ứng dụng về UI. Tuy nhiên do công việc thực hiện theo tasks nên độ nhất quán về UI chưa đảm bảo, dù có bảng thiết kế nhưng trong lúc thực hiện có chỉnh sửa và chưa thống nhất.**

  - **Features** (Hoàn thành 80% tổng thể app)
    -	LoginPage: Thực hiện được tính năng đăng nhập, lưu lại mật khẩu vào local setting, thông báo lỗi khi người dùng nhập sai các trường thông tin.
    -	DashboardPage: Hiển thị được dữ liệu về các trường thông tin Total, xử lý được sự kiện filter theo Week hoặc Month.
    -	OrderPage, StockPage, ClassificationPage, CustomerPage:
    + Giải quyết tính năng sort theo các trường.
    + 100% phần tính năng searh + filter.
    + Thực hiện được chức năng add, update, delete stock.
    -	AdminPage: Thực hiện được chức năng update thông tin cho admin.
    -	AddStockPage, AddOrderPage, AddClassifiationPage, AddCustomerPage, Hoàn thành 100% tính năng trang thêm thông tin cho đủ các trường.
    -	UpdateStockPage, UpdateOrderPage, UpdateClassificationPage, UpdateCustomerPage, UpdateAdminPage: Hoàn thành 100% tính năng cập nhật lại thông tin các trương thông tin.
    -	ReadOrderPage: Hoàn thiện tính năng xem thông tin order không được thực hiện các thao tác thêm, xoá, cập nhật trên trang đang xem.
    -	InvoicePage: Xuất được hoá đơn định dạng PDF hoàn chỉnh các trường thông tin của order.
    -	StatisticPage: Thể hiện được đồ thị phân tích tổng doanh thu theo ngày, tháng, năm và có hiện thi thông báo về số lượng tồn kho của sách.
    
- **Yêu cầu phi chức năng**
    - Về Back-end + Database: Hoàn thành 80%, 20% còn lại ở việc lưu trữ thông tin account và transaction cho giả lập thanh toán.
    - Kết nối và xây dựng các hàm lấy dữ liệu từ PostgresSQL server.
    - Kiểm tra trên 80% các Exception  nhập liệu từ user.

    **==>	Ở milestone 1 nhóm thực hiện 50% về các features ở các trang. Trong milestone 2 này nâng khối lượng lên 80%, trong đó còn 20% lại là phần lớn về giải lập thanh toán.**

**Kết quả đạt được:**
- App thiết kế đảm bảo 100% các công việc đề ra và hoàn thành phần implement theo công việc đề ra.

### Design patterns / architecture (Hoàn thành 100% tiêu chí đề ra)
Tiếp tục thực hiện xây dựng ứng dụng theo mô hình MVVM, tách biệt logic của lớp View và lớp Model, mọi logic xử lý đều chuyển lên ViewModel, hạn chế xử lý phần logic ở code-behind.

**Kết quả đạt được:**
Tiếp tục tổ chức app theo mô hình MVVM theo hướng đề ra.

### Advanced topics (Hoàn thành 100% tiêu chí đề ra)
Để hoàn thành tiêu chí này nhóm tiếp tục triển khai các tính năng nâng cao đã làm ở milestone 1 ngoài ra bổ sung thêm 1 số tính năng mới:
- Các kỹ thuật về **LiveCharts**
- **CRUD với Database Postgres**.

**Kết quả đạt được:**
Các thành viên triển khai được những tính năng nâng cao và áp dụng vào code.

### Teamwork - Git flow (Hoàn thành 100% tiêu chí đề ra)
Ở milestone 1 nhóm thực hiện quản lý công việc thông qua Jira nhưng do một số phát sinh về việc tính phí nên nhóm chuyển việc quản lý qua Trello:
- Milestone 2 nhóm thực hiện họp online thông qua Google Meet trong 30' vào ngày 15/11/2024 để thảo luận về các công việc cần giải quyết tiếp theo.
- Quản lý công việc nhóm thông qua Trello với vai trò như đã trình bày trong milestone 1.
- Minh chứng:
![image](https://github.com/user-attachments/assets/defb4330-3318-4068-9416-9241400eb9a8)
![image](https://github.com/user-attachments/assets/fbb5f728-b3b7-4a0b-a620-6ecec0bc1d3e)

- Quản lý source trên **Github**:
  - Các Dev phát triển có nhánh **feature** riêng.
  - Có nhánh **development** thực hiện merge code từ các nhánh feature.
  - Người chịu trách nhiệm merge code là team leader.
  - Minh chứng:
![image](https://github.com/user-attachments/assets/7918bf2e-bc98-44b3-999c-aa971be9d065)
![image](https://github.com/user-attachments/assets/7a74c5a4-7aa7-48d1-9e4d-f961e57dd0e4)
![image](https://github.com/user-attachments/assets/8d76b59c-bf99-4911-90dd-26283f37a3a5)

- Phân chia công việc cho các thành viên trong team ở milstone 2 như sau:
  - Trương Thị Tú My:
    + Fix bug ở trang LoginPage.
    + Chỉnh sửa UI thống nhất giữa OrderPage, ClassificationPage, CustomerPage, OrderPage.
    + Đảm nhận các feature CRUD cho trang ClassificationPage và CustomerPage.
    + Merge code, viết testcase cho các tính năng trên page đảm nhiệm và viết tổng báo cáo.
  - Hoàng Lê Nam: 
    + Fix các bug về validation đã phát hiện ở milestone 1 trên các màn hình Order Detail, Book Popup Control.
    + Chuyển các thao tác thêm (book, order), sửa (book, order) từ dữ liệu Mock sang Database.
    + Thêm tính năng hội viên cho cho màn hình Order Detail (khi thêm nếu là hội viên thì nhập sđt) và đã là hội viên thì khi sửa order sẽ không thể sửa tên customer.
    + Viết testcase trên các trang mình đảm nhiệm và phụ trách chính việc thiết kế và đảm bảo tính nhất quán database.
  - Phan Dương Linh:
    + Tiếp tục chỉnh sửa UI của AdminPage.
    + Phụ trách toàn bộ hiển thị và xử lý StatisticPage.
    + Viết các testcase trên các page đảm nhận.

**Kết quả đạt được:**
Thực hiện được mô hình team work đề ra.

### Quality assurance (Hoàn thành 100% tiêu chí đề ra)
Để hoàn thành tiêu chí này nhóm thực hiện công việc như sau:
- **Manual test**, giảng viên xem **TestDoc2** kèm theo, thực hiện test trên 95% yêu cầu đề ra trong đó tỉ lệ đáp ứng thành công 80%.
--> Đối với những trường hợp test failed nhóm thực hiện chỉnh sửa ở milestone 3.
- **Về Docs**: Nhóm thực hiện summary các Object và function, sau đó sử dụng Doxychen tạo Docs, giảng viên xem thư mục **html_docs**.

**Kết quả đạt được:**
Đảm bảo được quy trình đảm bảo chất lượng đề ra.

### Link video Demo (Hoàn thành 100% tiêu chí đề ra)

## Tổng kết
Nhìn chung nhóm thực hiện tốt những tiêu chí nghiệm thu đề ra, ở milestone 2 nhóm gần như hoàn thiện cac tính năng của ứng dụng, trong milestone tiếp theo với các công việc dự kiến như sau:
- Hoàn thành tính năng login với 1 số biện pháp bảo mật.
- Chỉnh lại login khi thực hiện order.
- Giả lập hệ thống thanh toán.
- Thống nhất UI 100%.
- Tập trung phần test tổng thể cho ứng dụng.

  **--> Điểm đánh giá 10.**

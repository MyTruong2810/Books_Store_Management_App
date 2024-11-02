# Books_Store_Management_App
## Thành viên nhóm 
- 22120214	Trương Thị Tú My - Team Leader
- 22120217	Hoàng Lê Nam
- 20120319	Phan Dương Linh
## Notice
Ứng dụng có sử dụng một só theme hệ thống nên có thể sẽ ảnh hưởng đến font, color chính mà nhóm muốn thực hiện ==> Gây ra trãi nghiệm không trực quan ==> Vì tính chất thời gian nên nhóm sẽ giải quyết sau.
## Phân chia thời gian và công việc trong milestone 1
** Với số giờ làm việc tối thiểu đê được nghiệm thu là **10h**
** Phân chia thời gian công việc như sau:
  * **1h**: Cho các cuộc họp, thảo luận timeline, công việc cho từng gia đoạn xây dựng app ở milestone 1. (1)
  * **1h**: Cho việc thiết kế bảng design tổng thể cho app, đáp ứng đầy đủ các phần của milestone 1. (2)
  * **1h**: Cho việc tìm hiểu kỹ về mô hình **MVVM** và các tính năng nâng cao được áp dụng vào app. (3)
  * **30'**: Cho việc tìm hiểu về cách làm việc trên môi trường **Github**. (4)
  * **30'**: Cho việc nghiên cứu và tìm hiểu về cách thức kiểm thử và đưa ra quyết định lựa chọn phương thức kiểm thử cho app ở milestone 1. (5)
  * **4h**: Cho việc implement code theo phân công ở milestone 1. (6)
  * **1h**: Cho quá quá trình kiểm thử và sửa lỗi. (7)
  * **1h**: Cho việc check tổng tiến độ nghiệm thu và viết báo cáo. (8)
## Đánh giá tiến độ theo tiêu chí (100% đáp ứng tiêu chí nghiệm thu)
  ### UI/UX (Hoàn thành 100% tiêu chí đề ra)
    ** Để hoàn thành tiêu chí này nhóm thực hiện công việc (2) + (6) và quyết định chọn công việc hoàn thành ở milestone 1 như sau:  
       * Yêu cầu chức năng
          -	Về UI (Hoàn thành 50% tổng thể app): 
            + LoginPage: Hiẻn thị được các trường đăng nhập bao gồm username, password, save password, login button.
            +	DashboardPage: Hiển thị được Llistview về SoldOut và BestSeller.
            +	OrderPage, StockPage, CustomerPage, ClassificationPage: 1 Listview hiển thị thông tin.
            +	AddStockPage, AddOrderPage, UpdateStockPage, UpdateOrderPage,AdminPage: Hiẻn thị được các trường thông tin.
            +	InvoicePage: Định dạng 1 hoá đơn.
        -	Về Features (Hoàn thành 50% tổng thể app)
          +	LoginPage: Thực hiện được tính năng đăng nhập, lưu lại mật khẩu vào local setting. 
          +	OrderPage: 
              Giải quyết tính năng sort theo các trường.
              Hoàn thành 50% phần tính năng searh + filter.
              Thực hiện được chức năng add, update, delete order.
          + StockPage:
              Giải quyết tính năng sort theo các trường.
              Hoàn thành 50% phần tính năng searh + filter.
              Thực hiện được chức năng add, update, delete stock.
          +	AddStockPage: Hoàn thành 80% tính năng trang thêm thông tin cho sách đủ các trường.
          +	AddOrderPage: Hoàn thành 80% tính năng trang thêm thông tin cho order đủ các trường.
          +	UpdateStockPage: Hoàn thành 80% tính năng cập nhật lại thông tin cho sách.
          +	UpdateOrderPage: Hoàn thành 80% tính năng cập nhật lại thông tin đơn hàng cho khách.
          +	InvoicePage: Xuất được hoá đơn định dạng PDF.
    ** Kết quả đạt được:
      * 1 Bảng design như sau:
          https://www.figma.com/design/36S8ur1xsgoSCQ6U7YhuvR/Untitled?node-id=0-1&node-type=canvas&t=rDrUBGIImtReEX0I-0
      * App thiết kế đảm bảo 100% các công việc đặt ra và hoàn thành phần implement theo công việc đặt ra.
  ### Design patterns / architecture (Hoàn thành 100% tiêu chí đề ra)
    ** Để hoàn thành tiêu chí này nhóm thực hiện 1 phần công việc (3) quyết định chọn kiến trúc phần mềm hoàn thành chính ở milestone 1 như sau:  
      Thực hiện tập trung xây dựng ứng dụng theo mô hình MVVM, hướng tới việc tách biệt hoàn toàn logic của lớp View và lớp Model, mọi logic xử lý đều chuyển hết lên ViewModel, hạn chế xử          lý phần logic ở code-behind. 
    ** Kết quả đạt được:
      Tổ chức được app theo mô hình MVVM theo hướng đề ra.
  ### Advanced topics (Hoàn thành 100% tiêu chí đề ra)
     **Để hoàn thành tiêu chí này nhóm thực hiện 1 phần công việc (3) quyết định chọn được các tính điểm nâng cao hoàn thành ở milestone 1 như sau: 
     * Thành viên Hoàng Lê Nam: 
        -   **INotifyDataErrorInfo**: Tìm hiểu và triển khai INotifyDataErrorInfo để bắt các lỗi về nhập liệu, và thông báo ra giao diện. Bằng cách dùng DataAnnotations để khai báo các điều                 kiện (VD: [Required(ErrorMessage = "Customer name is required.")])
        -   **RelayCommand**: Có sử dụng lớp RelayCommand để hạn chế việc xử lý các sự kiện đơn giản không cần dùng đến các thành phần giao diện trên View để giao cho ViewModel xử lý.
        -   **Quest PDF**: Tìm hiểu thư viện Quest PDF để tạo ra một trang PDF cho Invoice một cách dễ dàng thông qua việc tạo lớp InvoiceDocument.
    * Thành viên: Phan Dương Linh
          Hai kỹ thuật nâng cao BitmapImage và WindowHelper đã nâng cao đáng kể trải nghiệm người dùng và khả năng quản lý UI trong dự án. BitmapImage giúp tùy chỉnh ảnh đại diện khách hàng,           trong khi WindowHelper đảm bảo tích hợp mượt mà giữa các thành phần WinUI và API hệ thống. Những kỹ thuật này không chỉ giúp cải thiện giao diện, mà còn thể hiện các giải pháp                thiết thực trong việc xây dựng ứng dụng WinUI hiệu quả và thân thiện.
    * Thành viên Trương Thị Tú My: 
          Đảm bảo chất lượng mô hình MVVM cho ứng dụng
    ** Kết quả đạt được: 
     Các thành viên điều triển khai được những tính năng năng cao để ra và áp dụng vào cho code.
  ### Teamwork - Git flow (Hoàn thành 100% tiêu chí đề ra)
     ** Để hoàn thành tiêu chí này nhóm thực hiện công việc (1) + (4) quyết định chọn cách team work ở milestone 1 như sau: 
        * Mỗi milestone tiếng hành họp nhóm, thảo luận 2 lần. 
           - Hình thức hợp nhóm **online**, qua nền tảng **Google Meet**, thời gian họp **20'** cho mỗi buổi, các biên bản báo cáo được ghi lại bởi 1 thành viên trong nhóm.
           - Nhóm đã thực hiện được 2 lần họp nhóm theo tiêu chí đề ra vào ngày 27/09/2024 và 10/10/2024, các văn bản đều được lưu trữ trên Jira.
        * Hình thức quản lý công việc nhóm thông qua nền tảng **Jira**:
        * Chia vai trò của các thành viên: 
          - Trương Thị Tú My : Team leader, chịu trách nhiệm phân chia công việc, theo dõi tiếng độ thực hiện, đảm nhận 1 phần công việc QC thông qua việc review các task. Chốt done task.
          - Hoàng Lê Nam: Dev + Tester + Đảng nhiệm 1 phần công việc QC thông qua việc review các task.
          - Phan Dương Linh: Dev + Tester + Đảng nhiệm 1 phần công việc QC thông qua việc review các task.
        * Mô hình team work:
          - Nhóm thực hiện chia công việc theo mô hình **scrum** qua các **sprint** và chia các **task** trong các sprint.
          - Công việc được thực hiện theo luồng **Task --> In Processing --> Review --> Done**.
          - Ảnh minh hoạ: 
              ![image](https://github.com/user-attachments/assets/f5c0eaf3-e66f-4038-92c3-2c8c27808ca9)
              ![image](https://github.com/user-attachments/assets/174f84e9-9778-4ae0-8853-39d1ec9b107b)
              ![image](https://github.com/user-attachments/assets/f6b216ba-19b8-4ae2-9dd6-4473a22870d3)
        * Quản lý source trên **Github**:
          - Các Dev khi phát triển có những nhánh **feature** riêng.
          - Có các nhánh **development** thực hiện merge code từ các nhánh feature.
          - Người chịu trách nhiệm merge code team leader
          - Ảnh minh hoạ:
              ![image](https://github.com/user-attachments/assets/d9115733-b353-4820-b18b-daa7c68a77f7)
              ![image](https://github.com/user-attachments/assets/252a3260-3f64-4e04-a8aa-2995bc6b3df9)
              ![image](https://github.com/user-attachments/assets/eb3d2321-3644-4146-ab6c-386d9a1ca68f)
        * Công việc **phân chia** cho các thành viên trong team như sau:
          - Trương Thị Tú My: Cấu trúc source, phụ trách MainPage, DashboardPage, OrderPage, StockPage, LogoutPage và các tính năng trên trang đó.
          - Hoàng Lê Nam: Phụ trách OrderDetailPage, BookPopupPage, OrderReadOnlyDetailPage, InvoicePage và các tính năng trên trang đó.
          - Phan Dương Linh: CustomerPage, AdminPage, ClassificationPage, đang tiến hành SettingPage và StatisticPage và các tính năng trên trang đó.
        --> Các thành viên hoàn thành công việc
    ** Kết quả đạt được: 
      Thực hiện được mô hình team work đề ra.
  ### Quality assurance (Hoàn thành 100% tiêu chí đề ra)
    ** Để hoàn thành tiêu chí này nhóm thực hiện công việc (5) + (7) quyết định chọn cách đảm bảo chất lượng quy trình milestone 1 như sau: 
      * Dự kiến ban đầu nhóm tập trung vào unit test và UI test 1 cách tự động nhưng do thời gian hạn chế nhóm không đạt được tiêu chí đề ra.
      * Biện pháp dự trù chuyển sang **manual test** có **TestDoc** kèm theo, thực hiện test trên 95% yêu cầu đề ra trong số đố tỉ lệ đáp ứng thành công 90%.
      * Đối với những trường hợp test failed nhóm thực hiện chỉnh sửa ở milestone 2.
    ** Kết quả đạt được: 
      Đảm bảo được quy trình đảm bảo chất lượng đề ra.
## Tổng kết
  Nhìn chung nhóm thực hiện tổt những tiêu chí nghiệm thu để ra, điểm đánh giá 10.

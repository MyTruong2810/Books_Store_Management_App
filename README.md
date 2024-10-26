#Books_Store_Management_App

##Set up

###Để chạy được PslqDao cài thực hiện chon Tools --> NuGet Package Manager --> Package Manager Console và gõ lệnh:
```bash
Install-Package Npgsql -Version 5.0.4

Ngoài ra nếu code có bug thì có thể làm theo hướng dẫn Install hoặc Using khi hover trên bug chọn "Show potential fixes".

###Postgres
1. Có thể Install Postgres hoặc sử dụng Docker như thầy Demo trên lớp.
2. Cần đổi lại Password ở đường dẫn Database và các thông số khác nếu cần.
3. Có mẫu book.sql và order_book.sql


##Advance

1. Tổ chức ứng dụng theo mô hình MVVM không sử dụng MVVM Toolkit.
2. Có sử dụng lớp RelayCommand để tách biệt giữa code-behind và UI mọi xủ lý thông qua ViewModels.

##Code chưa cài đặt các Feature sau:
1. Khi minimize cửa sổ ứng dụng tự reset? --> Đang tìm cách khắc phục.
2. Phần search thì mới implement cho search theo tên sách và tên user --> Sẽ cài đặt tiếp search theo trường filter.
3. Một số lớp ViewModels chưa cài hoàn chỉnh ==> Implement later.
4. Các hiệu ứng về animation và transition chưa thực hiện --> Implement later --> Cái này cũng được xem là một advance hôm trước thầy có nói.
5. Chưa đang kí các Service ==> Implement later.

##Yêu cầu code của mọi người cần đáp ứng:
1. Bắt các Exception.
2. Có comment đầy đủ chức năng hàm và lớp.
3. Khi clone code nên làm riêng trên một branch và thực hiện unit test và ui test theo các hướng dẫn sau:
  "Về Unit test, tham khảo ở đây: 
  1. https://learn.microsoft.com/en-us/visualstudio/test/walkthrough-creating-and-running-unit-tests-for-managed-code?view=vs-2022
  2. https://learn.microsoft.com/en-us/windows/apps/winui/winui3/testing/create-winui-unit-test-project
  Về UI Automation testing, có thể tham khảo ở đây
  https://learn.microsoft.com/en-us/dotnet/framework/ui-automation/ui-automation-fundamentals"

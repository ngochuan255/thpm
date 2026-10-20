# DailyForecastMVC - Bài kiểm tra thường xuyên 1

Project ASP.NET MVC 5 / .NET Framework 4.8 theo 7 yêu cầu trong đề.

## Chạy dự án
1. Cài Visual Studio 2022 và workload ASP.NET and web development.
2. Mở DailyForecastMVC.sln, restore NuGet packages và build.
3. Nhấn Ctrl+F5, chọn Hiển thị danh sách hoặc Nhập thông tin.

## Các chức năng
- Model DailyForecast có AvgTemperature tự tính.
- 6 dự báo mẫu cho ba thành phố ngày 19/10 và 20/10/2026.
- Hai bảng danh sách ngày 20/10 và Hà Nội.
- Form Create dùng DropDownListFor và LabelFor theo Display attribute.
- Details hiển thị toàn bộ thông tin vừa nhập.
- Layout font Times New Roman, header #0099CC, menu #D5F1FF.
- Route mặc định DailyForecast/Index.

Sửa "Họ và tên sinh viên" trong Views/Shared/_Layout.cshtml trước khi nộp.
Dữ liệu chỉ lưu trong bộ nhớ, không sử dụng database.
Môi trường tạo dự án chưa thể chạy MSBuild để xác nhận biên dịch thực tế.
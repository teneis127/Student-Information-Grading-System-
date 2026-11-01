# AI Usage Log: Student Information & Grading System

Đồ án môn Lập trình hướng đối tượng, HK1 2026-2027 đợt 1.



---

## 1. Thông tin chung

- Công cụ AI: Claude, dùng qua giao diện claude.ai
- Phiên bản / model: [sonnet]
- Ngôn ngữ lập trình: C# (.NET), kiểm thử bằng xUnit
- Cách dùng: một cuộc trò chuyện dài, nhờ AI đóng vai giảng viên hướng dẫn
- Link GitHub: [https://github.com/teneis127/Student-Information-Grading-System-]

Quy ước: "AI" là phần Claude tạo ra hoặc gợi ý. "Em" là phần em tự quyết định, tự gõ, tự chạy hoặc tự sửa.

---

## 2. Cách em dùng AI

1. Em yêu cầu làm theo thứ tự: phân tích đề, thiết kế tổng quát, rồi mới viết code từng phần.
2. Từ tuần 2, em dùng quy trình sau:
   - Commit khung mẫu do AI tạo lên GitHub trước (các hàm chỉ có NotImplementedException và gợi ý TODO).
   - Sau đó em tự viết code vào khung và commit lại.
3. AI cũng đưa các bộ test hoàn chỉnh để em có thể kiểm tra chương trình, và kèm theo đó là các hướng dẫn để em có thể tự viết code
4. Em còn nhờ AI nhận xét phần code của mình và đưa ra các giải pháp giúp chương trình tốt hơn nếu có
4. Khi gặp lỗi build hoặc test, em chụp thông báo lỗi, nhờ AI giải thích, rồi tự sửa.
5. Em nhờ AI giải thích những đoạn em chưa hiểu (DFS, FormatRow...) và tự trả lời lại các câu hỏi kiểm tra bằng lời của em.

---

## 3. Nhật ký theo tuần

### Tuần 1: Phân tích, thiết kế và phần chấm điểm

**Em nhờ AI:**
- Đóng vai giảng viên, đọc đề và chia kế hoạch 4 tuần.
- Phân tích đề và thiết kế tổng quát trước khi code.
- Thiết lập giới hạn thành tối đa 28 và tối thiểu 15 tín chỉ.
- Thiết lập điều kiện qua môn thành điểm từ 5 trở lên.
- Làm theo bảng quy đổi điểm của quy chế trường do em gửi.
- AI bắt đầu hướng dẫn chi tiết vào từng phần
- Giải thích cách đọc test và đưa bài tập nhỏ vì em chưa quen viết test.
- Hướng dẫn em tự code lại từng file, giải thích namespace và các lỗi build.
- Phân tích việc môn Pass/Fail có nên có điểm hệ 4 không.
- Giải thích cách chia Grading và Services.

**AI tạo ra:**
- Phân tích đề, kiến trúc tổng quát, bảng quyết định nghiệp vụ, kế hoạch 4 tuần.
- Hướng dẫn code các phần: IGradingScheme, GradingPolicy, GradeBand, LetterGradeScheme, PassFailScheme, AcademicRankClassifier.
- 42 test ban đầu.
- Hướng dẫn từng chặng, bài tập test, mẫu báo cáo.

**Em tự làm:**
- Chọn ngôn ngữ C#.
- Quyết định các quy tắc: 28 và 15 tín chỉ, qua môn từ 5.0, giữ môn Pass/Fail ngoài GPA.
- Cung cấp bảng quy chế điểm của trường.
- Chạy dotnet test: 42 test đạt.
- Tự viểt code của 5 file: IGradingScheme, GradingPolicy, PassFailScheme, GradeBand, LetterGradeScheme.
- Tự sửa các lỗi build (tên không khớp interface, chạy sai thư mục, chữ PASS và Pass).
- Làm bài tập test: đổi giá trị trong test để thấy test báo đỏ, rồi tự viết 3 test (MyFirstTest). Tổng 45 test đạt.

### Tuần 2: Mô hình dữ liệu, luật ghi danh, DFS tiên quyết

**Em nhờ AI:**
- Hướng dẫn tuần 2: phân tích, thiết kế, lộ trình từng bước.
- Cập nhật quy tắc: mỗi năm chỉ có 2 học kỳ.
- Tạo khung mẫu để commit lên GitHub trước, rồi em tự viết vào khung.
- Bổ sung Term, Instructor và DuplicateEnrollmentRule vào khung (em phát hiện thiếu).
- Giải thích các lỗi build, test và git.
- Viết bản tham khảo hoàn chỉnh kèm test để em đối chiếu.
- Giải thích hàm CanReach (DFS), EnrollmentException, DuplicateEnrollmentRule.

**AI tạo ra:**
- Thiết kế chi tiết: bảng trách nhiệm các class, hai loại luật, thuật toán DFS.
- Khung mẫu tuần 2 (Models, Rules, Services, khung test).
- Bản tham khảo hoàn chỉnh: Term, Instructor, Course, Enrollment, Student, các luật, EnrollmentService.
- Khoảng 73 test.
- Hướng dẫn xử lý lỗi (sai thư mục, test bị Skip, bin và obj trong git, xóa file add nhầm).

**Em tự làm:**
- Đưa ra quy tắc mỗi năm 2 kỳ.
- Đề xuất quy trình git: commit khung mẫu trước, tự viết sau.
- Chép khung vào repo, build, commit.
- Tự viết code vào khung rồi nhờ AI nhận xét.
- Tự tìm và sửa lỗi: ném sai loại ngoại lệ ở Course (ArgumentException thành ArgumentOutOfRangeException).
- Thêm .gitignore và gỡ bin, obj khỏi git.
- Chạy dotnet test và sửa cho đến khi đạt.

### Tuần 3: GPA, bảng điểm, ba kịch bản bắt buộc

**Em nhờ AI:**
- Hướng dẫn tuần 3: phân tích, thiết kế.
- Tạo khung mẫu để commit lên GitHub trước, rồi em tự viết vào khung.
- Viết bản tham khảo hoàn chỉnh để đối chiếu.
- Hướng dẫn chạy thử chương trình và tạo dữ liệu mẫu.

**AI tạo ra:**
- Phân tích, thiết kế GpaCalculator và TranscriptPrinter, bảng quyết định nghiệp vụ.
- Khung mẫu tuần 3.
- 19 test (gồm ba kịch bản bắt buộc).
- Chương trình chạy thử Program.cs mẫu.

**Em tự làm:**
- Quyết định: loại môn Pass/Fail khỏi GPA, môn rớt vẫn tính, học lại lấy lần gần nhất, GPA là null khi chưa có môn nào, làm tròn 2 chữ số.
- Commit khung mẫu, rồi tự viết GpaCalculator và TranscriptPrinter.
- Chạy test và chạy chương trình để xem bảng điểm thật.
- Sửa các lỗi gặp phải.

### Tuần 4: Hoàn thiện, báo cáo cuối

**Em nhờ AI:**
- Hướng dẫn tuần 4: kế hoạch, README.
- Tổng hợp lại những gì em đã sử dụng AI.
- Hướng dẫn soạn báo cáo cuối kỳ.
- Soạn tài liệu ôn bảo vệ.

**AI tạo ra:**
- Kế hoạch tuần 4, khung README.
- Tổng hợp lại tất cả các hoạt động của em đã làm với AI, hướng dẫn viết báo cáo cuối kỳ.
- Tài liệu câu hỏi và gợi ý trả lời khi bảo vệ.
- Nội dung mẫu cho form nộp báo cáo.

**Em tự làm:**
- đọc và viết lại báo cáo, chụp ảnh, chạy lại test, điền AI Usage Log, đưa file lên GitHub

---

## 4. Các tệp do AI tạo ra

- File zip tuần 1: 42 test.
- File zip khung mẫu tuần 2 và các test. Em commit lên GitHub trước, rồi tự viết vào khung.
- File zip khung mẫu tuần 3 và các test. Em commit lên GitHub trước, rồi tự viết vào khung.

---

## 5. Phần code: AI viết và em viết

Em xác nhận từng dòng đúng với thực tế trước khi nộp .

- IGradingScheme, GradingPolicy, GradeBand, LetterGradeScheme, PassFailScheme, AcademicRankClassifier
  - Nguồn ban đầu: AI hướng dẫn từng bước để viết
  - Em: tự gõ lại từng file theo hướng dẫn .
- 42 test ban đầu của tuần 1
  - Nguồn: AI viết. 
  - Em chạy và đọc kết quả.
- MyFirstTest (3 test)
  - Nguồn: khung gợi ý của AI. 
  - Em tự điền kết quả mong đợi và tự viết test thứ ba.
- Term, Instructor
  - Nguồn: khung mẫu do AI tạo. 
  - Em: tự viết theo khung mẫu
- Course
  - Nguồn: khung mẫu do AI tạo. 
  - Em: tự viết theo khung mẫu
- Enrollment, Student
  - Nguồn: khung mẫu do AI tạo. 
  - Em: tự viết theo khung mẫu
- Các luật (AlreadyPassedRule, PrerequisiteRule, MaxCreditsRule, MinCreditsRule, DuplicateEnrollmentRule)
  - Nguồn: khung mẫu do AI tạo. 
  - Em: tự viết theo khung mẫu
- EnrollmentService
  - Nguồn: khung mẫu do AI tạo. 
  - Em: tự viết theo khung mẫu
- GpaCalculator
  - Nguồn: khung mẫu do AI tạo. 
  - Em: tự viết theo khung mẫu
- TranscriptPrinter (kể cả FormatRow)
  - Nguồn: khung mẫu do AI tạo. 
  - Em: tự viết theo khung mẫu
- Bộ test tuần 2 và tuần 3
  - Nguồn: AI viết (bản tham khảo). 
  - Em: em sử dụng các test đó để chạy và đọc kết quả
- Program.cs chạy thử
  - Nguồn: AI viết mẫu. 
  - Em: em chạy và xem kết quả

---

## 6. Các quyết do em chọn

- Ngôn ngữ: C#.
- Qua môn: điểm từ 5.0 trở lên (thang 10).
- Bảng quy đổi điểm: theo quy chế của trường (A+ đến F). Chữ D (4.0 đến 4.9) vẫn có 1.0 điểm hệ 4 nhưng không qua môn.
- Học kỳ: mỗi năm có 2 học kỳ.
- Tín chỉ: tối đa 28 khi thêm môn, tối thiểu 15 khi chốt đăng ký.
- Hai loại luật: kiểm tra khi thêm môn và kiểm tra khi chốt đăng ký.
- Môn Pass/Fail: không tính vào GPA (GradePoints trả về null).
- Môn rớt: vẫn tính vào GPA.
- Môn học lại: chỉ tính lần học gần nhất.
- Môn chưa có điểm: không tính.
- Chưa có môn nào để tính GPA: GPA là null, không phải 0.
- Làm tròn: điểm 1 chữ số thập phân khi ghi, GPA 2 chữ số.
- Quy trình git: commit khung mẫu của AI trước, rồi commit phần em viết.

---

## 7. Các lỗi em gặp và tự xử lý

- Lỗi MSB1003 (Specify a project or solution file): chạy dotnet ở sai thư mục. Em chạy ở thư mục có file .slnx.
- Terminal không nhận lệnh dotnet: em đóng và mở lại VS Code.
- Lỗi CS0535 (không cài đặt thành viên của interface): tên thành viên khác tên trong IGradingScheme. Em đổi tên cho khớp.
- Test đỏ vì PASS và Pass khác nhau: C# phân biệt chữ hoa chữ thường. Em thống nhất giá trị trả về.
- Lỗi CS0103 (không tìm thấy CanReach): file Course.cs thiếu hàm. Em thêm hàm vào trong class Course.
- Test đỏ vì ArgumentException khác ArgumentOutOfRangeException: em sửa Course ném đúng loại và đúng thứ tự tham số.
- File bin và obj bị đưa lên git: em thêm .gitignore và gỡ bằng git rm --cached.
- [em bổ sung các lỗi khác em thật sự gặp]

---

## 8. Điều em đã học và phần em còn chưa chắc

- Em đã hiểu: interface và đa hình, đọc thông báo lỗi build, đọc kết quả test
- Em còn chưa chắc: viết các test, đệ quy trong DFS

---

## 9. Lời kết

Em xác nhận nhật ký này mô tả trung thực việc em dùng AI trong đồ án.


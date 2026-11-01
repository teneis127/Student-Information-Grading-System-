# Student Information & Grading System

Đồ án môn Lập trình hướng đối tượng, HK1 2026-2027 đợt 1. Giảng viên hướng dẫn: Huỳnh Xuân Phụng.

Chương trình mô phỏng một trường đại học: sinh viên ghi danh vào môn học, ghi điểm, tính GPA và in bảng điểm. Việc ghi danh phải theo các luật như môn tiên quyết và giới hạn tín chỉ mỗi kỳ. Viết bằng C#, kiểm thử bằng xUnit.

## 1. Yêu cầu để chạy

- .NET SDK 10 
- Git (nếu muốn tải code từ GitHub).

## 2. Cách chạy

Mở terminal trong thư mục có file GradingSystem.slnx rồi chạy các lệnh sau.

---
Build:

dotnet build

---
Chạy test:

dotnet test

---
Chạy chương trình demo (tạo một sinh viên, ghi danh, nhập điểm, in bảng điểm):

dotnet run --project GradingSystem.App


## 3. Kết quả demo 

Chương trình demo in ra bảng điểm có dạng như sau (số liệu có thể khác tùy dữ liệu trong Program.cs):

---
BẢNG ĐIỂM
Sinh viên: Nguyễn Văn A (SV01)

Học kỳ 2026-1
  ENG     Tiếng Anh            3 TC  6.0           C     2.0
  MATH1   Toán 1               3 TC  7.0           B     3.0
  PE1     Thể dục              2 TC  8.0           PASS  -
  PHY     Vật lý               3 TC  9.0           A+    4.0
  PROG    Lập trình            4 TC  8.5           A     3.7

Học kỳ 2026-2
  MATH2   Toán 2               3 TC  chưa có điểm  -     -

GPA tích lũy: 3.22
Xếp loại: High Merit / Giỏi
---

Môn PE1 là môn Pass/Fail nên không tính vào GPA. Môn MATH2 chưa có điểm nên cũng chưa tính.

## 4. Cấu trúc thư mục

- GradingSystem.Core: phần logic chính
  - Grading: các thang điểm (IGradingScheme, LetterGradeScheme, PassFailScheme, GradeBand, GradingPolicy)
  - Models: dữ liệu (Student, Course, Enrollment, EnrollmentStatus, Term, Instructor)
  - Rules: luật ghi danh (IEnrollmentRule, IRegistrationRule, AlreadyPassedRule, PrerequisiteRule, MaxCreditsRule, DuplicateEnrollmentRule, MinCreditsRule, EnrollmentException)
  - Services: EnrollmentService, GpaCalculator, AcademicRankClassifier
  - Reports: TranscriptPrinter
- GradingSystem.App: chương trình chạy thử trên dòng lệnh
- GradingSystem.Tests: bộ test xUnit
- docs: báo cáo cuối kỳ, AI Usage Log, ảnh minh chứng

## 5. Quy tắc nghiệp vụ

- Qua môn khi điểm từ 5.0 trở lên (thang 10).
- Điểm quy đổi theo bảng của trường (từ A+ đến F). Chữ D (4.0 đến 4.9) có 1.0 điểm hệ 4 nhưng không qua môn.
- Mỗi năm học có 2 học kỳ.
- Tối đa 28 tín chỉ khi thêm môn. Tối thiểu 15 tín chỉ khi chốt đăng ký.
- Môn học thang Pass/Fail không tính vào GPA. Môn rớt (D, F) vẫn tính vào GPA.
- Môn học lại chỉ tính lần học gần nhất. Môn đã qua thì không được ghi danh lại.
- Không ghi danh cùng một môn hai lần trong cùng một kỳ.
- Chỉ hủy môn được khi chưa chốt đăng ký.
- Điểm làm tròn 1 chữ số thập phân khi ghi. GPA làm tròn 2 chữ số. Chưa có môn nào để tính thì GPA là null, bảng điểm in "chưa có GPA".
- Môn tiên quyết là một đồ thị có hướng, không được có chu trình. Khi thêm tiên quyết, chương trình dùng DFS để kiểm tra chu trình.

## 6. Các nguyên lý OOP thể hiện trong code

**Polymorphism (đa hình)**
- IGradingScheme với hai cài đặt LetterGradeScheme và PassFailScheme.
- Enrollment, GpaCalculator và TranscriptPrinter chỉ làm việc qua interface này.
- Thuộc tính CountsTowardGpa quyết định một môn có tính vào GPA hay không.

**Encapsulation (đóng gói)**
- Enrollment.RecordScore: điểm là private set, được kiểm tra và làm tròn.
- Course.AddPrerequisite: danh sách môn tiên quyết là private, có kiểm tra chu trình.

**Single Responsibility (một trách nhiệm)**
- Mỗi luật ghi danh là một class riêng.
- EnrollmentService chỉ điều phối.
- GpaCalculator chỉ tính, TranscriptPrinter chỉ định dạng bảng điểm.

## 7. Tài liệu

- Báo cáo cuối kỳ: docs/BAO_CAO_CUOI_KY.md
- Nhật ký sử dụng AI: docs/AI_USAGE_LOG.md
- Ảnh minh chứng (class diagram, kết quả test, bảng điểm): thư mục docs/images

## 8. Thực hiện

- Sinh viên: [Trần Nhật Tiến: 25110063]
- Việc dùng AI: có dùng Claude để hỗ trợ phân tích, thiết kế, tạo khung code và giải thích lỗi. Chi tiết trong docs/AI_USAGE_LOG.md.

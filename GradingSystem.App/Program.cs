using System.Text;
using GradingSystem.Core.Grading;
using GradingSystem.Core.Models;
using GradingSystem.Core.Reports;
using GradingSystem.Core.Rules;
using GradingSystem.Core.Services;

// Để console hiển thị đúng tiếng Việt
Console.OutputEncoding = Encoding.UTF8;

// 1. Dịch vụ ghi danh cùng các luật
var service = new EnrollmentService(
    new IEnrollmentRule[]
    {
        new AlreadyPassedRule(),
        new DuplicateEnrollmentRule(),
        new PrerequisiteRule(),
        new MaxCreditsRule(28),
    },
    new IRegistrationRule[]
    {
        new MinCreditsRule(15),
    });

// 2. Hai học kỳ
var term1 = new Term(2026, 1);
var term2 = new Term(2026, 2);

// 3. Các môn học (PE1 dùng thang Pass/Fail, các môn còn lại dùng thang chữ)
var math1 = new Course("MATH1", "Toán 1", 3, new LetterGradeScheme());
var math2 = new Course("MATH2", "Toán 2", 3, new LetterGradeScheme());
var prog = new Course("PROG", "Lập trình", 4, new LetterGradeScheme());
var eng = new Course("ENG", "Tiếng Anh", 3, new LetterGradeScheme());
var phy = new Course("PHY", "Vật lý", 3, new LetterGradeScheme());
var pe1 = new Course("PE1", "Thể dục", 2, new PassFailScheme());

// MATH2 yêu cầu phải học MATH1 trước
math2.AddPrerequisite(math1);

// 4. Sinh viên
var student = new Student("SV01", "Nguyễn Văn A");

// 5. Kỳ 1: ghi danh đủ 15 tín chỉ rồi chốt đăng ký
var eMath1 = service.Enroll(student, math1, term1);
var eProg = service.Enroll(student, prog, term1);
var eEng = service.Enroll(student, eng, term1);
var ePhy = service.Enroll(student, phy, term1);
var ePe1 = service.Enroll(student, pe1, term1);
service.SubmitRegistration(student, term1);

// 6. Nhập điểm cuối kỳ 1
eMath1.RecordScore(7.0);
eProg.RecordScore(8.5);
eEng.RecordScore(6.0);
ePhy.RecordScore(9.0);
ePe1.RecordScore(8.0);

// 7. Kỳ 2: đã qua MATH1 nên ghi danh được MATH2 (chưa có điểm)
service.Enroll(student, math2, term2);

// 8. In bảng điểm
var printer = new TranscriptPrinter(new GpaCalculator(), new AcademicRankClassifier());
Console.WriteLine(printer.Print(student));

// 9. Thử một thao tác bị từ chối: sinh viên khác chưa học MATH1 mà đăng ký MATH2
Console.WriteLine("--- Thử ghi danh sai ---");
var student2 = new Student("SV02", "Trần Thị B");
try
{
    service.Enroll(student2, math2, term1);
    Console.WriteLine("Ghi danh thành công (không đúng như mong đợi).");
}
catch (EnrollmentException ex)
{
    Console.WriteLine($"Bị từ chối: {ex.Message}");
}
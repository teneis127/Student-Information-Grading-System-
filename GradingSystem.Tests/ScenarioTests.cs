using GradingSystem.Core.Grading;
using GradingSystem.Core.Reports;
using GradingSystem.Core.Rules;
using GradingSystem.Core.Services;
using Xunit;
using static GradingSystem.Tests.TestData;

namespace GradingSystem.Tests;

/// <summary>Ba kịch bản bắt buộc của đề, chạy thật qua toàn bộ hệ thống.</summary>
public class ScenarioTests
{
    private static EnrollmentService CreateService() => new(
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

    // ---------- Kịch bản 1: đường đi đúng, từ ghi danh đến bảng điểm ----------

    [Fact]
    public void Scenario1_HappyPath_EnrollGradeComputeGpaAndPrintTranscript()
    {
        var service = CreateService();
        var student = MakeStudent();

        var math1 = MakeCourse("MATH1", 3);
        var math2 = MakeCourse("MATH2", 3);
        math2.AddPrerequisite(math1);
        var prog = MakeCourse("PROG", 4);
        var eng = MakeCourse("ENG", 3);
        var phy = MakeCourse("PHY", 3);
        var pe = MakeCourse("PE1", 2, new PassFailScheme());   // môn Pass/Fail, không tính GPA

        // Kỳ 1: ghi danh đủ 15 tín chỉ rồi chốt đăng ký
        var eMath1 = service.Enroll(student, math1, T1);
        var eProg = service.Enroll(student, prog, T1);
        var eEng = service.Enroll(student, eng, T1);
        var ePhy = service.Enroll(student, phy, T1);
        var ePe = service.Enroll(student, pe, T1);
        service.SubmitRegistration(student, T1);

        // Nhập điểm cuối kỳ
        eMath1.RecordScore(7.0);   // B  3.0
        eProg.RecordScore(8.5);    // A  3.7
        eEng.RecordScore(6.0);     // C  2.0
        ePhy.RecordScore(9.0);     // A+ 4.0
        ePe.RecordScore(8.0);      // PASS, không tính GPA

        // Kỳ 2: đã qua MATH1 nên ghi danh được MATH2 (chưa có điểm)
        service.Enroll(student, math2, T2);

        // GPA = (3.0x3 + 3.7x4 + 2.0x3 + 4.0x3) / 13 = 41.8 / 13 = 3.2153... làm tròn 3.22
        double? gpa = new GpaCalculator().Compute(student);
        Assert.Equal<double?>(3.22, gpa);

        string transcript = new TranscriptPrinter(new GpaCalculator(), new AcademicRankClassifier()).Print(student);
        Assert.Contains("GPA tích lũy: 3.22", transcript);
        Assert.Contains("High Merit", transcript);   // 3.2 <= 3.22 < 3.6
        Assert.Contains("MATH1", transcript);
        Assert.Contains("MATH2", transcript);
        Assert.Contains("chưa có điểm", transcript);
    }

    // ---------- Kịch bản 2: thao tác sai bị từ chối đúng ----------

    [Fact]
    public void Scenario2_MissingPrerequisite_IsRejected()
    {
        var service = CreateService();
        var student = MakeStudent();
        var math1 = MakeCourse("MATH1");
        var math2 = MakeCourse("MATH2");
        math2.AddPrerequisite(math1);

        var ex = Assert.Throws<EnrollmentException>(() => service.Enroll(student, math2, T1));

        Assert.Contains("MATH1", ex.Message);
        Assert.Empty(student.Enrollments);   // bị từ chối thì không thêm gì
    }

    [Fact]
    public void Scenario2_ExceedingMaxCredits_IsRejected()
    {
        var service = CreateService();
        var student = MakeStudent();
        for (int i = 1; i <= 9; i++)
            service.Enroll(student, MakeCourse($"C{i}", 3), T1);   // 27 tín chỉ

        Assert.Throws<EnrollmentException>(() => service.Enroll(student, MakeCourse("BIG", 2), T1));   // sẽ là 29

        Assert.Equal(9, student.Enrollments.Count);
    }

    // ---------- Kịch bản 3: đổi thang điểm thì hành vi đổi theo (Polymorphism) ----------

    [Fact]
    public void Scenario3_Polymorphism_SwappingSchemeChangesBehavior()
    {
        // Hai sinh viên học cùng một môn, cùng 8.5 điểm, chỉ khác thang điểm của môn.
        var withLetter = MakeStudent();
        var withPassFail = MakeStudent();
        AddResult(withLetter, MakeCourse("X", 3, new LetterGradeScheme()), T1, 8.5);
        AddResult(withPassFail, MakeCourse("X", 3, new PassFailScheme()), T1, 8.5);

        var calculator = new GpaCalculator();
        var printer = new TranscriptPrinter(calculator, new AcademicRankClassifier());

        // Thang chữ: có GPA 3.7. Thang Pass/Fail: không có GPA.
        Assert.Equal<double?>(3.7, calculator.Compute(withLetter));
        Assert.Null(calculator.Compute(withPassFail));

        // Bảng điểm cũng khác theo: chữ "A" so với "PASS".
        Assert.Contains("GPA tích lũy: 3.70", printer.Print(withLetter));
        string passFailTranscript = printer.Print(withPassFail);
        Assert.Contains("PASS", passFailTranscript);
        Assert.Contains("chưa có GPA", passFailTranscript);
    }
}

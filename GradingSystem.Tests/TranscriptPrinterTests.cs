using GradingSystem.Core.Grading;
using GradingSystem.Core.Models;
using GradingSystem.Core.Reports;
using GradingSystem.Core.Services;
using Xunit;
using static GradingSystem.Tests.TestData;

namespace GradingSystem.Tests;

public class TranscriptPrinterTests
{
    private static TranscriptPrinter CreatePrinter()
        => new(new GpaCalculator(), new AcademicRankClassifier());

    [Fact]
    public void Transcript_ContainsStudentTermCourseGpaAndRank()
    {
        var student = MakeStudent();
        AddResult(student, MakeCourse("MATH1", 3), T1, 8.5);   // A, 3.7

        string text = CreatePrinter().Print(student);

        Assert.Contains("Nguyen Van A", text);
        Assert.Contains("SV01", text);
        Assert.Contains("2026-1", text);
        Assert.Contains("MATH1", text);
        Assert.Contains("GPA tích lũy: 3.70", text);
        Assert.Contains("Distinction", text);   // 3.7 >= 3.6
    }

    [Fact]
    public void Transcript_ListsTermsInAscendingOrder()
    {
        var student = MakeStudent();
        AddResult(student, MakeCourse("B", 3), T2, 7.0);   // thêm kỳ 2 trước
        AddResult(student, MakeCourse("A", 3), T1, 7.0);

        string text = CreatePrinter().Print(student);

        Assert.True(
            text.IndexOf("2026-1", StringComparison.Ordinal) < text.IndexOf("2026-2", StringComparison.Ordinal));
    }

    [Fact]
    public void Transcript_CourseWithoutScore_ShowsNoScoreMessage()
    {
        var student = MakeStudent();
        student.AddEnrollment(new Enrollment(MakeCourse("A", 3), T1));

        string text = CreatePrinter().Print(student);

        Assert.Contains("chưa có điểm", text);
    }

    [Fact]
    public void Transcript_NoGpa_ShowsMessageAndNoRank()
    {
        var student = MakeStudent();
        AddResult(student, MakeCourse("PF", 3, new PassFailScheme()), T1, 9.0);

        string text = CreatePrinter().Print(student);

        Assert.Contains("PASS", text);
        Assert.Contains("chưa có GPA", text);
        Assert.DoesNotContain("Xếp loại", text);
    }

    [Fact]
    public void NullStudent_IsRejected()
    {
        Assert.Throws<ArgumentNullException>(() => CreatePrinter().Print(null!));
    }
}

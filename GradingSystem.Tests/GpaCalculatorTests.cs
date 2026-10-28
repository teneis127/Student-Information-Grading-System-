using GradingSystem.Core.Grading;
using GradingSystem.Core.Models;
using GradingSystem.Core.Services;
using Xunit;
using static GradingSystem.Tests.TestData;

namespace GradingSystem.Tests;

public class GpaCalculatorTests
{
    private readonly GpaCalculator _calculator = new();

    [Fact]
    public void SingleCourse_GpaEqualsItsGradePoints()
    {
        var student = MakeStudent();
        AddResult(student, MakeCourse("A", 3), T1, 8.5);   // chữ A: 3.7

        Assert.Equal<double?>(3.7, _calculator.Compute(student));
    }

    [Fact]
    public void Gpa_IsWeightedByCredits()
    {
        var student = MakeStudent();
        AddResult(student, MakeCourse("A", 3), T1, 8.5);   // 3.7 điểm, 3 tín chỉ
        AddResult(student, MakeCourse("C", 4), T1, 5.5);   // 2.0 điểm, 4 tín chỉ

        // (3.7 x 3 + 2.0 x 4) / 7 = 2.7285... làm tròn 2.73
        Assert.Equal<double?>(2.73, _calculator.Compute(student));
    }

    [Fact]
    public void PassFailCourse_IsExcludedFromGpa()
    {
        var student = MakeStudent();
        AddResult(student, MakeCourse("A", 3), T1, 8.5);
        AddResult(student, MakeCourse("PF", 3, new PassFailScheme()), T1, 9.0);

        Assert.Equal<double?>(3.7, _calculator.Compute(student));
    }

    [Fact]
    public void FailedCourse_StillCountsTowardGpa()
    {
        var student = MakeStudent();
        AddResult(student, MakeCourse("A", 3), T1, 8.5);   // 3.7
        AddResult(student, MakeCourse("D", 3), T1, 4.5);   // chữ D: 1.0 điểm nhưng rớt

        // (3.7 x 3 + 1.0 x 3) / 6 = 2.35
        Assert.Equal<double?>(2.35, _calculator.Compute(student));
    }

    [Fact]
    public void Retake_UsesLatestAttempt()
    {
        var student = MakeStudent();
        var course = MakeCourse("X", 3);
        AddResult(student, course, T1, 3.0);   // kỳ 1: F, 0.0 điểm
        AddResult(student, course, T2, 7.0);   // kỳ 2: học lại được B, 3.0 điểm

        // Chỉ tính lần kỳ 2. Nếu tính cả hai lần thì ra 1.5.
        Assert.Equal<double?>(3.0, _calculator.Compute(student));
    }

    [Fact]
    public void Retake_LatestIsDecidedByTerm_NotByInsertionOrder()
    {
        var student = MakeStudent();
        var course = MakeCourse("X", 3);
        AddResult(student, course, T2, 7.0);   // thêm kỳ 2 trước
        AddResult(student, course, T1, 3.0);   // rồi mới thêm kỳ 1

        Assert.Equal<double?>(3.0, _calculator.Compute(student));
    }

    [Fact]
    public void NoEnrollments_GpaIsNull()
    {
        Assert.Null(_calculator.Compute(MakeStudent()));
    }

    [Fact]
    public void CourseWithoutScore_IsNotCounted_AndGpaIsNull()
    {
        var student = MakeStudent();
        student.AddEnrollment(new Enrollment(MakeCourse("A", 3), T1));   // chưa có điểm

        Assert.Null(_calculator.Compute(student));
    }

    [Fact]
    public void OnlyPassFailCourses_GpaIsNull()
    {
        var student = MakeStudent();
        AddResult(student, MakeCourse("PF", 3, new PassFailScheme()), T1, 9.0);

        Assert.Null(_calculator.Compute(student));
    }

    [Fact]
    public void NullStudent_IsRejected()
    {
        Assert.Throws<ArgumentNullException>(() => _calculator.Compute(null!));
    }
}

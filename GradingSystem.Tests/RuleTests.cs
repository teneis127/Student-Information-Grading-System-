using GradingSystem.Core.Models;
using GradingSystem.Core.Rules;
using Xunit;
using static GradingSystem.Tests.TestData;

namespace GradingSystem.Tests;

public class RuleTests
{
    // ---------- AlreadyPassedRule ----------

    [Fact]
    public void AlreadyPassedRule_PassedCourse_IsRejected()
    {
        var student = MakeStudent();
        var course = MakeCourse("A");
        AddResult(student, course, T1, 7.0);

        Assert.Throws<EnrollmentException>(() => new AlreadyPassedRule().Validate(student, course, T2));
    }

    [Fact]
    public void AlreadyPassedRule_FailedCourse_CanBeRetaken()
    {
        var student = MakeStudent();
        var course = MakeCourse("A");
        AddResult(student, course, T1, 3.0);

        new AlreadyPassedRule().Validate(student, course, T2);   // không ném lỗi
    }

    // ---------- PrerequisiteRule ----------

    [Fact]
    public void PrerequisiteRule_MissingPrerequisite_IsRejected()
    {
        var student = MakeStudent();
        var math1 = MakeCourse("MATH1");
        var math2 = MakeCourse("MATH2");
        math2.AddPrerequisite(math1);

        var ex = Assert.Throws<EnrollmentException>(
            () => new PrerequisiteRule().Validate(student, math2, T1));

        Assert.Contains("MATH1", ex.Message);   // thông báo nêu đúng môn còn thiếu
    }

    [Fact]
    public void PrerequisiteRule_PrerequisiteFailed_IsRejected()
    {
        var student = MakeStudent();
        var math1 = MakeCourse("MATH1");
        var math2 = MakeCourse("MATH2");
        math2.AddPrerequisite(math1);
        AddResult(student, math1, T1, 4.9);   // rớt thì chưa tính là qua

        Assert.Throws<EnrollmentException>(() => new PrerequisiteRule().Validate(student, math2, T2));
    }

    [Fact]
    public void PrerequisiteRule_PrerequisitePassed_IsAllowed()
    {
        var student = MakeStudent();
        var math1 = MakeCourse("MATH1");
        var math2 = MakeCourse("MATH2");
        math2.AddPrerequisite(math1);
        AddResult(student, math1, T1, 5.0);   // đúng ngưỡng qua môn

        new PrerequisiteRule().Validate(student, math2, T2);   // không ném lỗi
    }

    // ---------- MaxCreditsRule ----------

    [Theory]
    [InlineData(24, true)]    // 24 + 3 = 27
    [InlineData(25, true)]    // 25 + 3 = 28, đúng biên tối đa
    [InlineData(26, false)]   // 26 + 3 = 29, vượt
    public void MaxCreditsRule_Boundary(int existingCredits, bool allowed)
    {
        var rule = new MaxCreditsRule(28);
        var student = MakeStudent();
        student.AddEnrollment(new Enrollment(MakeCourse("OLD", existingCredits), T1));
        var newCourse = MakeCourse("NEW", 3);

        if (allowed)
            rule.Validate(student, newCourse, T1);
        else
            Assert.Throws<EnrollmentException>(() => rule.Validate(student, newCourse, T1));
    }

    [Fact]
    public void MaxCreditsRule_InvalidLimit_IsRejected()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new MaxCreditsRule(0));
    }

    // ---------- MinCreditsRule ----------

    [Theory]
    [InlineData(14, false)]
    [InlineData(15, true)]    // đúng biên tối thiểu
    [InlineData(20, true)]
    public void MinCreditsRule_Boundary(int totalCredits, bool allowed)
    {
        var rule = new MinCreditsRule(15);
        var student = MakeStudent();
        student.AddEnrollment(new Enrollment(MakeCourse("A", totalCredits), T1));

        if (allowed)
            rule.Validate(student, T1);
        else
            Assert.Throws<EnrollmentException>(() => rule.Validate(student, T1));
    }

    [Fact]
    public void MinCreditsRule_InvalidLimit_IsRejected()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new MinCreditsRule(0));
    }

    // ---------- DuplicateEnrollmentRule ----------

    [Fact]
    public void DuplicateEnrollmentRule_SameCourseSameTerm_IsRejected()
    {
        var student = MakeStudent();
        var course = MakeCourse("A");
        student.AddEnrollment(new Enrollment(course, T1));

        Assert.Throws<EnrollmentException>(
            () => new DuplicateEnrollmentRule().Validate(student, course, T1));
    }

    [Fact]
    public void DuplicateEnrollmentRule_SameCourseDifferentTerm_IsAllowed()
    {
        var student = MakeStudent();
        var course = MakeCourse("A");
        student.AddEnrollment(new Enrollment(course, T1));

        new DuplicateEnrollmentRule().Validate(student, course, T2);   // không ném lỗi
    }
}

using GradingSystem.Core.Models;
using Xunit;
using static GradingSystem.Tests.TestData;

namespace GradingSystem.Tests;

public class StudentTests
{
    [Theory]
    [InlineData("", "Nguyen Van A")]
    [InlineData("   ", "Nguyen Van A")]
    [InlineData("SV01", "")]
    [InlineData("SV01", "   ")]
    public void EmptyIdOrName_IsRejected(string id, string name)
    {
        Assert.Throws<ArgumentException>(() => new Student(id, name));
    }

    [Fact]
    public void GetRegisteredCredits_CountsOnlyTheGivenTerm()
    {
        var student = MakeStudent();
        student.AddEnrollment(new Enrollment(MakeCourse("A", 3), T1));
        student.AddEnrollment(new Enrollment(MakeCourse("B", 4), T1));
        student.AddEnrollment(new Enrollment(MakeCourse("C", 3), T2));

        Assert.Equal(7, student.GetRegisteredCredits(T1));
        Assert.Equal(3, student.GetRegisteredCredits(T2));
    }

    [Fact]
    public void HasPassed_PassingScore_IsTrue()
    {
        var student = MakeStudent();
        var course = MakeCourse("A");
        AddResult(student, course, T1, 7.0);

        Assert.True(student.HasPassed(course));
    }

    [Fact]
    public void HasPassed_FailingScore_IsFalse()
    {
        var student = MakeStudent();
        var course = MakeCourse("A");
        AddResult(student, course, T1, 4.9);

        Assert.False(student.HasPassed(course));
    }

    [Fact]
    public void HasPassed_NeverEnrolled_IsFalse()
    {
        var student = MakeStudent();

        Assert.False(student.HasPassed(MakeCourse("A")));
    }

    [Fact]
    public void HasPassed_FailedThenPassedOnRetake_IsTrue()
    {
        var student = MakeStudent();
        var course = MakeCourse("A");
        AddResult(student, course, T1, 3.0);   // rớt kỳ 1
        AddResult(student, course, T2, 6.0);   // học lại, qua kỳ 2

        Assert.True(student.HasPassed(course));
    }
}

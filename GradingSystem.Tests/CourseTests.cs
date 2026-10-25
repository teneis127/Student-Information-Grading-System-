using GradingSystem.Core.Grading;
using GradingSystem.Core.Models;
using Xunit;
using static GradingSystem.Tests.TestData;

namespace GradingSystem.Tests;

public class CourseTests
{
    [Fact]
    public void ValidData_KeepsValues()
    {
        var scheme = new PassFailScheme();
        var instructor = new Instructor("GV01", "Nguyen Van A");

        var course = new Course("CS101", "OOP", 3, scheme, instructor);

        Assert.Equal("CS101", course.Code);
        Assert.Equal("OOP", course.Name);
        Assert.Equal(3, course.Credits);
        Assert.Same(scheme, course.Scheme);
        Assert.Same(instructor, course.Instructor);
        Assert.Empty(course.Prerequisites);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void NonPositiveCredits_IsRejected(int credits)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => MakeCourse("C1", credits));
    }

    [Theory]
    [InlineData("", "OOP")]
    [InlineData("   ", "OOP")]
    [InlineData("CS101", "")]
    [InlineData("CS101", "   ")]
    public void EmptyCodeOrName_IsRejected(string code, string name)
    {
        Assert.Throws<ArgumentException>(() => new Course(code, name, 3, new LetterGradeScheme()));
    }

    [Fact]
    public void NullScheme_IsRejected()
    {
        Assert.Throws<ArgumentNullException>(() => new Course("C1", "OOP", 3, null!));
    }

    [Fact]
    public void AddPrerequisite_Valid_IsAdded()
    {
        var math1 = MakeCourse("MATH1");
        var math2 = MakeCourse("MATH2");

        math2.AddPrerequisite(math1);

        Assert.Single(math2.Prerequisites);
        Assert.Same(math1, math2.Prerequisites[0]);
    }

    [Fact]
    public void AddPrerequisite_Null_IsRejected()
    {
        var course = MakeCourse("C1");

        Assert.Throws<ArgumentNullException>(() => course.AddPrerequisite(null!));
    }

    [Fact]
    public void AddPrerequisite_Itself_IsRejected()
    {
        var course = MakeCourse("C1");

        Assert.Throws<InvalidOperationException>(() => course.AddPrerequisite(course));
    }

    [Fact]
    public void AddPrerequisite_Duplicate_IsRejected()
    {
        var a = MakeCourse("A");
        var b = MakeCourse("B");
        a.AddPrerequisite(b);

        Assert.Throws<InvalidOperationException>(() => a.AddPrerequisite(b));
    }

    [Fact]
    public void AddPrerequisite_DirectCycle_IsRejected()
    {
        // A yêu cầu B. Thêm "B yêu cầu A" là vô lý.
        var a = MakeCourse("A");
        var b = MakeCourse("B");
        a.AddPrerequisite(b);

        Assert.Throws<InvalidOperationException>(() => b.AddPrerequisite(a));
    }

    [Fact]
    public void AddPrerequisite_IndirectCycle_IsRejected()
    {
        // A yêu cầu B, B yêu cầu C. Thêm "C yêu cầu A" tạo vòng A -> B -> C -> A.
        var a = MakeCourse("A");
        var b = MakeCourse("B");
        var c = MakeCourse("C");
        a.AddPrerequisite(b);
        b.AddPrerequisite(c);

        Assert.Throws<InvalidOperationException>(() => c.AddPrerequisite(a));
        Assert.Empty(c.Prerequisites);   // cạnh bị từ chối thì không được thêm
    }

    [Fact]
    public void AddPrerequisite_Diamond_IsAllowed()
    {
        // D yêu cầu B và C, cả B và C cùng yêu cầu A. Đây là DAG hợp lệ (không có vòng).
        var a = MakeCourse("A");
        var b = MakeCourse("B");
        var c = MakeCourse("C");
        var d = MakeCourse("D");

        b.AddPrerequisite(a);
        c.AddPrerequisite(a);
        d.AddPrerequisite(b);
        d.AddPrerequisite(c);

        Assert.Equal(2, d.Prerequisites.Count);
    }
}

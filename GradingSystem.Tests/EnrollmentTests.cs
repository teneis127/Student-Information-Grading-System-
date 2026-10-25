using GradingSystem.Core.Grading;
using GradingSystem.Core.Models;
using Xunit;
using static GradingSystem.Tests.TestData;

namespace GradingSystem.Tests;

public class EnrollmentTests
{
    [Theory]
    [InlineData(8.95, 9.0)]
    [InlineData(8.94, 8.9)]
    [InlineData(7.0, 7.0)]
    public void RecordScore_RoundsToOneDecimal(double input, double expected)
    {
        var enrollment = new Enrollment(MakeCourse("C1"), T1);

        enrollment.RecordScore(input);

        Assert.Equal<double?>(expected, enrollment.Score);
    }

    [Theory]
    [InlineData(-1.0)]
    [InlineData(10.5)]
    [InlineData(double.NaN)]
    public void RecordScore_InvalidScore_IsRejected(double score)
    {
        var enrollment = new Enrollment(MakeCourse("C1"), T1);

        Assert.Throws<ArgumentOutOfRangeException>(() => enrollment.RecordScore(score));
        Assert.Null(enrollment.Score);   // điểm sai thì không được ghi
    }

    [Fact]
    public void IsPassed_NoScoreYet_IsFalse()
    {
        var enrollment = new Enrollment(MakeCourse("C1"), T1);

        Assert.False(enrollment.IsPassed());
    }

    [Fact]
    public void IsPassed_FollowsTheSchemeOfTheCourse()
    {
        // Cùng điểm 6.0 nhưng hai môn dùng hai thang khác nhau cho kết quả khác nhau (Polymorphism).
        var letterCourse = MakeCourse("C1", 3, new LetterGradeScheme());
        var strictCourse = MakeCourse("C2", 3, new PassFailScheme(7.0));

        var a = new Enrollment(letterCourse, T1);
        var b = new Enrollment(strictCourse, T1);
        a.RecordScore(6.0);
        b.RecordScore(6.0);

        Assert.True(a.IsPassed());
        Assert.False(b.IsPassed());
    }

    [Fact]
    public void GetGradePoints_LetterScheme_ReturnsPoints()
    {
        var enrollment = new Enrollment(MakeCourse("C1", 3, new LetterGradeScheme()), T1);
        enrollment.RecordScore(8.5);

        Assert.Equal<double?>(3.7, enrollment.GetGradePoints());
    }

    [Fact]
    public void GetGradePoints_PassFailScheme_IsNull()
    {
        var enrollment = new Enrollment(MakeCourse("C1", 3, new PassFailScheme()), T1);
        enrollment.RecordScore(9.0);

        Assert.Null(enrollment.GetGradePoints());
    }

    [Fact]
    public void GetGradePoints_NoScoreYet_IsNull()
    {
        var enrollment = new Enrollment(MakeCourse("C1"), T1);

        Assert.Null(enrollment.GetGradePoints());
    }

    [Fact]
    public void Confirm_ChangesStatusFromDraftToConfirmed()
    {
        var enrollment = new Enrollment(MakeCourse("C1"), T1);
        Assert.Equal(EnrollmentStatus.Draft, enrollment.Status);

        enrollment.Confirm();

        Assert.Equal(EnrollmentStatus.Confirmed, enrollment.Status);
    }
}

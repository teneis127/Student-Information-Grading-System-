using GradingSystem.Core.Grading;
using Xunit;

namespace GradingSystem.Tests;

public class LetterGradeSchemeTests
{
    private readonly IGradingScheme _scheme = new LetterGradeScheme();

    [Theory]
    [InlineData(10.0, "A+", 4.0, true)]
    [InlineData(9.0,  "A+", 4.0, true)]    // biên dưới A+
    [InlineData(8.9,  "A",  3.7, true)]    // sát dưới A+
    [InlineData(8.5,  "A",  3.7, true)]
    [InlineData(8.0,  "B+", 3.5, true)]
    [InlineData(7.0,  "B",  3.0, true)]
    [InlineData(6.5,  "C+", 2.5, true)]
    [InlineData(5.5,  "C",  2.0, true)]
    [InlineData(5.0,  "D+", 1.5, true)]    // biên qua môn
    [InlineData(4.9,  "D",  1.0, false)]   // rớt nhưng vẫn có 1.0 điểm hệ 4
    [InlineData(4.0,  "D",  1.0, false)]
    [InlineData(3.9,  "F",  0.0, false)]
    [InlineData(0.0,  "F",  0.0, false)]
    public void FollowsOfficialConversionTable(double score, string letter, double points, bool passing)
    {
        Assert.Equal(letter, _scheme.Evaluate(score));
        Assert.Equal<double?>(points, _scheme.GradePoints(score));
        Assert.Equal(passing, _scheme.IsPassing(score));
    }

    [Fact]
    public void CountsTowardGpa_IsTrue() => Assert.True(_scheme.CountsTowardGpa);

    [Theory]
    [InlineData(-0.1)]
    [InlineData(10.1)]
    [InlineData(double.NaN)]
    [InlineData(double.PositiveInfinity)]
    [InlineData(double.NegativeInfinity)]
    public void InvalidScore_IsRejected(double score)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => _scheme.Evaluate(score));
        Assert.Throws<ArgumentOutOfRangeException>(() => _scheme.GradePoints(score));
        Assert.Throws<ArgumentOutOfRangeException>(() => _scheme.IsPassing(score));
    }
}

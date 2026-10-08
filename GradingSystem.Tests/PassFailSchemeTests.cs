using GradingSystem.Core.Grading;
using Xunit;

namespace GradingSystem.Tests;

public class PassFailSchemeTests
{
    private readonly IGradingScheme _scheme = new PassFailScheme();

    [Theory]
    [InlineData(10.0, "PASS", true)]
    [InlineData(5.0,  "PASS", true)]    // biên qua môn
    [InlineData(4.9,  "FAIL", false)]
    [InlineData(0.0,  "FAIL", false)]
    public void EvaluatesPassOrFail(double score, string result, bool passing)
    {
        Assert.Equal(result, _scheme.Evaluate(score));
        Assert.Equal(passing, _scheme.IsPassing(score));
    }

    [Fact]
    public void DoesNotCountTowardGpa_AndHasNoGradePoints()
    {
        Assert.False(_scheme.CountsTowardGpa);
        Assert.Null(_scheme.GradePoints(9.0));   // null, không phải 0
    }

    [Fact]
    public void CustomPassMark_IsRespected()
    {
        IGradingScheme strict = new PassFailScheme(7.0);
        Assert.False(strict.IsPassing(6.9));
        Assert.True(strict.IsPassing(7.0));
    }

    [Fact]
    public void InvalidPassMark_IsRejected()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new PassFailScheme(11));
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(double.NaN)]
    [InlineData(double.PositiveInfinity)]
    [InlineData(double.NegativeInfinity)]
    public void InvalidScore_IsRejected(double score)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => _scheme.Evaluate(score));
        Assert.Throws<ArgumentOutOfRangeException>(() => _scheme.IsPassing(score));
    }
}

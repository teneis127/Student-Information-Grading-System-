using GradingSystem.Core.Grading;
using Xunit;

namespace GradingSystem.Tests;

public class MyFirstTest
{
    [Fact]
    public void Score7toLetterB()
    {
        IGradingScheme scheme = new LetterGradeScheme();
        string result = scheme.Evaluate(7.0);
        Assert.Equal("B", result);
    }

    [Fact]
    public void Score4_9toNotPassing()
    {
        IGradingScheme scheme = new LetterGradeScheme();
        bool passed = scheme.IsPassing(4.9);
        Assert.False(passed);
    }

    [Fact]
    public void Score5toPassing()
    {
        IGradingScheme scheme = new PassFailScheme();
        string passed = scheme.Evaluate(5.0);
        Assert.Equal("PASS", passed);
    }
}
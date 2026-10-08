using GradingSystem.Core.Grading;
using Xunit;

namespace GradingSystem.Tests;

/// <summary>
/// Bằng chứng cho test case bắt buộc của đề: đổi scheme (cùng một điểm số)
/// thì hành vi đổi theo, mà code gọi không cần biết scheme cụ thể.
/// </summary>
public class PolymorphismTests
{
    // Hàm này chỉ biết IGradingScheme - giống cách GpaCalculator/Enrollment sẽ dùng sau này.
    private static (string Result, double? Points, bool Gpa) Describe(IGradingScheme scheme, double score)
        => (scheme.Evaluate(score), scheme.GradePoints(score), scheme.CountsTowardGpa);

    [Fact]
    public void SameScore_DifferentScheme_DifferentBehavior()
    {
        const double score = 8.5;

        var letter = Describe(new LetterGradeScheme(), score);
        var passFail = Describe(new PassFailScheme(), score);

        Assert.Equal(("A", (double?)3.7, true), letter);
        Assert.Equal(("PASS", (double?)null, false), passFail);
    }

    [Fact]
    public void FailingScore_BehavesDifferentlyPerScheme()
    {
        const double score = 4.5;

        var letter = Describe(new LetterGradeScheme(), score);
        var passFail = Describe(new PassFailScheme(), score);

        Assert.Equal(("D", (double?)1.0, true), letter);   // rớt nhưng vẫn có điểm hệ 4
        Assert.Equal(("FAIL", (double?)null, false), passFail);
    }

    [Fact]
    public void BothSchemes_AgreeOnPassingThreshold()
    {
        IGradingScheme[] schemes = { new LetterGradeScheme(), new PassFailScheme() };

        foreach (var scheme in schemes)
        {
            Assert.False(scheme.IsPassing(4.9));
            Assert.True(scheme.IsPassing(5.0));
        }
    }
}

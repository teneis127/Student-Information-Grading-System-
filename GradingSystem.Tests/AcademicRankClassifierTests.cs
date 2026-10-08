using GradingSystem.Core.Services;
using Xunit;

namespace GradingSystem.Tests;

public class AcademicRankClassifierTests
{
    private readonly AcademicRankClassifier _classifier = new();

    [Theory]
    [InlineData(4.0,  "Distinction / Xuất sắc")]
    [InlineData(3.6,  "Distinction / Xuất sắc")]
    [InlineData(3.59, "High Merit / Giỏi")]
    [InlineData(3.2,  "High Merit / Giỏi")]
    [InlineData(3.19, "Merit / Khá")]
    [InlineData(2.5,  "Merit / Khá")]
    [InlineData(2.49, "Average / Trung bình")]
    [InlineData(2.0,  "Average / Trung bình")]
    [InlineData(1.99, "Low performed / Yếu")]
    [InlineData(1.0,  "Low performed / Yếu")]
    [InlineData(0.99, "Very Low performed / Kém")]
    [InlineData(0.0,  "Very Low performed / Kém")]
    public void FollowsOfficialRankingTable(double gpa, string rank)
    {
        Assert.Equal(rank, _classifier.Classify(gpa));
    }

    [Theory]
    [InlineData(-0.01)]
    [InlineData(4.01)]
    [InlineData(double.NaN)]
    [InlineData(double.PositiveInfinity)]
    [InlineData(double.NegativeInfinity)]
    public void InvalidGpa_IsRejected(double gpa)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => _classifier.Classify(gpa));
    }
}

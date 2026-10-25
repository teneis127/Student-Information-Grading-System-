using GradingSystem.Core.Models;
using Xunit;

namespace GradingSystem.Tests;

public class ModelTests
{
    [Fact]
    public void Term_SameYearAndNumber_AreEqual()
    {
        var a = new Term(2026, 1);
        var b = new Term(2026, 1);

        Assert.Equal(a, b);
    }

    [Fact]
    public void Term_DifferentNumber_AreNotEqual()
    {
        var a = new Term(2026, 1);
        var b = new Term(2026, 2);

        Assert.NotEqual(a, b);
    }

    [Fact]
    public void Term_ToString_ShowsYearAndNumber()
    {
        var term = new Term(2026, 1);

        Assert.Equal("2026-1", term.ToString());
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    public void Term_ValidNumber_IsAccepted(int number)
    {
        var term = new Term(2026, number);

        Assert.Equal(number, term.Number);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(3)]
    public void Term_InvalidNumber_IsRejected(int number)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new Term(2026, number));
    }

    [Fact]
    public void Term_InvalidYear_IsRejected()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new Term(0, 1));
    }

    [Fact]
    public void Instructor_ValidData_KeepsValues()
    {
        var instructor = new Instructor("GV01", "Nguyen Van A");

        Assert.Equal("GV01", instructor.Id);
        Assert.Equal("Nguyen Van A", instructor.Name);
    }

    [Theory]
    [InlineData("", "Nguyen Van A")]
    [InlineData("   ", "Nguyen Van A")]
    [InlineData("GV01", "")]
    [InlineData("GV01", "   ")]
    public void Instructor_EmptyIdOrName_IsRejected(string id, string name)
    {
        Assert.Throws<ArgumentException>(() => new Instructor(id, name));
    }
}

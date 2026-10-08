namespace GradingSystem.Core.Grading;

/// <summary>Đại diện cho một dải điểm trong bảng quy đổi.</summary>
public record GradeBand(double MinScore, string Letter, double Points);

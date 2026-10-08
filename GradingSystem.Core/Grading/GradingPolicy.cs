namespace GradingSystem.Core.Grading;

/// <summary>
/// Quy tắc chung của toàn hệ thống về điểm số.
/// Đặt ở một nơi duy nhất để đổi quy chế chỉ phải sửa một chỗ.
/// </summary>
public static class GradingPolicy
{
    public const double MinScore = 0.0;
    public const double MaxScore = 10.0;
    /// <summary>
    /// điểm tối thiểu qua môn
    /// </summary>
    public const double PassingScore = 5.0;

    public static void EnsureValidScore(double score)
    {
        if (!double.IsFinite(score) || score < MinScore || score > MaxScore)
        {
            throw new ArgumentOutOfRangeException(nameof(score), $"Điểm phải nằm trong khoảng [{MinScore}, {MaxScore}]");
        }
    }
}
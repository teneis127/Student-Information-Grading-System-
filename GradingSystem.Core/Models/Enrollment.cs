using GradingSystem.Core.Grading;

namespace GradingSystem.Core.Models;

/// <summary>Một lần sinh viên học một môn trong một học kỳ. KHUNG MẪU.</summary>
public class Enrollment
{
    public Course Course { get; }
    public Term Term { get; }
    public EnrollmentStatus Status { get; private set; }

    /// <summary>Điểm hệ 10. null nghĩa là chưa có điểm. Chỉ được đặt qua RecordScore.</summary>
    public double? Score { get; private set; }

    public Enrollment(Course course, Term term)
    {
        ArgumentNullException.ThrowIfNull(course);
        ArgumentNullException.ThrowIfNull(term);

        Course = course;
        Term = term;
        Status = EnrollmentStatus.Draft;
    }

    /// <summary>Ghi điểm cho lượt học này.</summary>
    public void RecordScore(double score)
    {
        GradingPolicy.EnsureValidScore(score);

        // Làm tròn bằng decimal để tránh sai số của số nhị phân (ví dụ 8.95 phải ra 9.0).
        decimal rounded = Math.Round((decimal)score, 1, MidpointRounding.AwayFromZero);
        Score = (double)rounded;
    }

    /// <summary>Có qua môn không? Chưa có điểm thì là false.</summary>
    public bool IsPassed()
    {
        return Score.HasValue && Course.Scheme.IsPassing(Score.Value);
    }

    /// <summary>Điểm hệ 4 theo thang của môn. null nếu chưa có điểm hoặc thang không có điểm hệ 4.</summary>
    public double? GetGradePoints()
    {
        return Score.HasValue ? Course.Scheme.GradePoints(Score.Value) : null;
    }

    /// <summary>Chốt lượt ghi danh này (gọi khi sinh viên chốt đăng ký cả kỳ).</summary>
    public void Confirm()
    {
       Status = EnrollmentStatus.Confirmed;
    }
}

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
        // TODO: course, term không được null
        // TODO: gán thuộc tính, Status ban đầu là Draft
        throw new NotImplementedException();
    }

    /// <summary>Ghi điểm cho lượt học này.</summary>
    public void RecordScore(double score)
    {
        // TODO: dùng GradingPolicy.EnsureValidScore để kiểm tra điểm
        // TODO: làm tròn 1 chữ số thập phân (Math.Round, MidpointRounding.AwayFromZero)
        // TODO: gán vào Score
        throw new NotImplementedException();
    }

    /// <summary>Có qua môn không? Chưa có điểm thì là false.</summary>
    public bool IsPassed()
    {
        // TODO: nhờ Course.Scheme trả lời (đây là chỗ dùng Polymorphism)
        throw new NotImplementedException();
    }

    /// <summary>Điểm hệ 4 theo thang của môn. null nếu chưa có điểm hoặc thang không có điểm hệ 4.</summary>
    public double? GetGradePoints()
    {
        // TODO: nhờ Course.Scheme.GradePoints
        throw new NotImplementedException();
    }

    /// <summary>Chốt lượt ghi danh này (gọi khi sinh viên chốt đăng ký cả kỳ).</summary>
    public void Confirm()
    {
        // TODO: đổi Status thành Confirmed
        throw new NotImplementedException();
    }
}

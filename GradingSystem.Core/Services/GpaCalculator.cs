using GradingSystem.Core.Models;

namespace GradingSystem.Core.Services;

/// <summary>Chỉ lo việc tính GPA (điểm trung bình hệ 4). KHUNG MẪU: em tự viết phần thân.</summary>
public class GpaCalculator
{
    /// <summary>
    /// Tính GPA tích lũy của sinh viên. Trả về null nếu chưa có môn nào đủ điều kiện để tính
    /// (null nghĩa là "chưa có GPA", khác với GPA bằng 0).
    /// </summary>
    public double? Compute(Student student)
    {
         ArgumentNullException.ThrowIfNull(student);

        // Bước 1: chọn các lượt học được tính.
        //  - Thang điểm của môn có tính vào GPA (môn Pass/Fail bị loại).
        //  - Đã có điểm (GetGradePoints trả null nếu chưa có điểm).
        // Bước 2: mỗi môn chỉ giữ LẦN HỌC GẦN NHẤT (xét theo năm rồi đến kỳ, không theo thứ tự thêm vào).
        var counted = student.Enrollments
            .Where(e => e.Course.Scheme.CountsTowardGpa && e.GetGradePoints().HasValue)
            .GroupBy(e => e.Course)
            .Select(group => group.OrderBy(e => e.Term.Year).ThenBy(e => e.Term.Number).Last())
            .ToList();

        // Dùng decimal để cộng và chia chính xác, tránh sai số của số nhị phân.
        decimal totalPoints = 0;
        int totalCredits = 0;

        foreach (Enrollment enrollment in counted)
        {
            decimal points = (decimal)enrollment.GetGradePoints()!.Value;
            totalPoints += points * enrollment.Course.Credits;
            totalCredits += enrollment.Course.Credits;
        }

        if (totalCredits == 0)
            return null;

        decimal gpa = totalPoints / totalCredits;
        return (double)Math.Round(gpa, 2, MidpointRounding.AwayFromZero);
    }
}

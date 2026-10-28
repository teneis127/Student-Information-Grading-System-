using GradingSystem.Core.Models;
using GradingSystem.Core.Services;
using System.Globalization;
using System.Text;

namespace GradingSystem.Core.Reports;

/// <summary>Chỉ lo việc định dạng bảng điểm thành chuỗi. Không tự tính GPA. KHUNG MẪU.</summary>
public class TranscriptPrinter
{
    private readonly GpaCalculator _gpaCalculator;
    private readonly AcademicRankClassifier _rankClassifier;

    public TranscriptPrinter(GpaCalculator gpaCalculator, AcademicRankClassifier rankClassifier)
    {
        ArgumentNullException.ThrowIfNull(gpaCalculator);
        ArgumentNullException.ThrowIfNull(rankClassifier);

        _gpaCalculator = gpaCalculator;
        _rankClassifier = rankClassifier;
    }

    /// <summary>Tạo bảng điểm của một sinh viên dưới dạng chuỗi nhiều dòng.</summary>
    public string Print(Student student)
    {
         ArgumentNullException.ThrowIfNull(student);

        var sb = new StringBuilder();
        sb.AppendLine("BẢNG ĐIỂM");
        sb.AppendLine($"Sinh viên: {student.Name} ({student.Id})");

        // Nhóm theo học kỳ, học kỳ cũ in trước (sắp theo năm rồi đến kỳ).
        var terms = student.Enrollments
            .GroupBy(e => e.Term)
            .OrderBy(group => group.Key.Year)
            .ThenBy(group => group.Key.Number);

        foreach (var termGroup in terms)
        {
            sb.AppendLine();
            sb.AppendLine($"Học kỳ {termGroup.Key}");

            foreach (Enrollment enrollment in termGroup.OrderBy(e => e.Course.Code))
                sb.AppendLine(FormatRow(enrollment));
        }

        sb.AppendLine();

        double? gpa = _gpaCalculator.Compute(student);
        if (gpa.HasValue)
        {
            sb.AppendLine($"GPA tích lũy: {FormatNumber(gpa.Value, 2)}");
            sb.AppendLine($"Xếp loại: {_rankClassifier.Classify(gpa.Value)}");
        }
        else
        {
            sb.AppendLine("GPA tích lũy: chưa có GPA");
        }

        return sb.ToString();
    }

    private static string FormatRow(Enrollment enrollment)
    {
        string score = enrollment.Score.HasValue
            ? FormatNumber(enrollment.Score.Value, 1)
            : "chưa có điểm";

        string result = enrollment.Score.HasValue
            ? enrollment.Course.Scheme.Evaluate(enrollment.Score.Value)
            : "-";

        double? points = enrollment.GetGradePoints();
        string pointsText = points.HasValue ? FormatNumber(points.Value, 1) : "-";

        return $"  {enrollment.Course.Code,-8}{enrollment.Course.Name,-20}{enrollment.Course.Credits,2} TC  {score,-14}{result,-6}{pointsText}";
    }

    // Luôn dùng dấu chấm thập phân (3.70), không phụ thuộc cài đặt ngôn ngữ của máy (máy tiếng Việt dùng dấu phẩy).
    private static string FormatNumber(double value, int decimals)
        => value.ToString("F" + decimals, CultureInfo.InvariantCulture);
}

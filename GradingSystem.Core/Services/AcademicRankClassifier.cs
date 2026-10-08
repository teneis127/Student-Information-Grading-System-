namespace GradingSystem.Core.Services;

/// <summary>Xếp loại học lực theo điểm trung bình (GPA hệ 4). Chỉ lo việc phân loại, không tính GPA.</summary>
public class AcademicRankClassifier
{
    private static readonly (double MinGpa, string Rank)[] Ranks =
    {
        (3.6, "Distinction / Xuất sắc"),
        (3.2, "High Merit / Giỏi"),
        (2.5, "Merit / Khá"),
        (2.0, "Average / Trung bình"),
        (1.0, "Low performed / Yếu"),
        (0.0, "Very Low performed / Kém"),
    };

    public string Classify(double gpa)
    {
        if (!double.IsFinite(gpa) || gpa < 0 || gpa > 4.0)
            throw new ArgumentOutOfRangeException(nameof(gpa), gpa, "GPA phải nằm trong [0, 4].");

        // Làm tròn 2 chữ số để khớp các mốc như 3.59, 2.49 trong quy chế.
        double rounded = Math.Round(gpa, 2, MidpointRounding.AwayFromZero);

        foreach (var (minGpa, rank) in Ranks)
        {
            if (rounded >= minGpa)
                return rank;
        }

        throw new InvalidOperationException("Bảng xếp loại GPA bị thiếu dải thấp nhất.");
    }
}

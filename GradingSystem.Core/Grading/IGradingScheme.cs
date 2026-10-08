namespace GradingSystem.Core.Grading;

/// <summary>
/// Mẫu chung cho các thang điểm khác nhau.
/// </summary>
public interface IGradingScheme
{
    /// <summary>
    /// tên của thang điểm
    /// </summary>
    string Name { get; }
    /// <summary>
    /// có được tính vào GPA hay không
    /// </summary>
    bool CountsTowardGpa { get; }
    /// <summary>
    /// đổi điếm số thành xếp loại bằng chữ, hoặc qua môn / trượt môn
    /// </summary>
    string Evaluate(double score); 
    /// <summary>
    /// đổi điểm thành hệ 4
    /// </summary>
    double? GradePoints(double score);
    /// <summary>
    /// kiểm tra xem sinh viên có vượt qua môn học này không
    /// </summary>
    bool IsPassing(double score);
}

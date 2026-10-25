using GradingSystem.Core.Grading;
using GradingSystem.Core.Models;

namespace GradingSystem.Tests;

/// <summary>Các hàm tạo dữ liệu mẫu dùng chung cho nhiều file test, để mỗi test ngắn gọn.</summary>
internal static class TestData
{
    public static readonly Term T1 = new(2026, 1);
    public static readonly Term T2 = new(2026, 2);

    public static Course MakeCourse(string code, int credits = 3, IGradingScheme? scheme = null)
        => new(code, "Course " + code, credits, scheme ?? new LetterGradeScheme());

    public static Student MakeStudent() => new("SV01", "Nguyen Van A");

    /// <summary>Cho sinh viên học một môn và có điểm (dùng để dựng tình huống "đã qua / đã rớt").</summary>
    public static Enrollment AddResult(Student student, Course course, Term term, double score)
    {
        var enrollment = new Enrollment(course, term);
        enrollment.RecordScore(score);
        student.AddEnrollment(enrollment);
        return enrollment;
    }
}

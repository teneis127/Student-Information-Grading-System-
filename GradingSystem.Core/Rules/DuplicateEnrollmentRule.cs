using GradingSystem.Core.Models;

namespace GradingSystem.Core.Rules;

/// <summary>Từ chối nếu sinh viên đã ghi danh môn này trong cùng học kỳ (luật thêm, không có trong khung).</summary>
public class DuplicateEnrollmentRule : IEnrollmentRule
{
    public void Validate(Student student, Course course, Term term)
    {
        if (student.GetEnrollments(term).Any(e => e.Course == course))
            throw new EnrollmentException(
                $"Môn {course.Code} đã được ghi danh trong kỳ {term}.");
    }
}

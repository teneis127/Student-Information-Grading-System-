using GradingSystem.Core.Models;

namespace GradingSystem.Core.Rules;

/// <summary>Từ chối nếu sinh viên đã qua môn này. KHUNG MẪU.</summary>
public class AlreadyPassedRule : IEnrollmentRule
{
    public void Validate(Student student, Course course, Term term)
    {
        if (student.HasPassed(course))
            throw new EnrollmentException(
                $"Sinh viên {student.Name} đã qua môn {course.Code}, không thể ghi danh lại.");
    }
}

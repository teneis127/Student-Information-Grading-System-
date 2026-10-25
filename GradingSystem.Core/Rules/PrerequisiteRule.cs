using GradingSystem.Core.Models;

namespace GradingSystem.Core.Rules;

/// <summary>Từ chối nếu sinh viên chưa qua các môn tiên quyết TRỰC TIẾP của môn. KHUNG MẪU.</summary>
public class PrerequisiteRule : IEnrollmentRule
{
    public void Validate(Student student, Course course, Term term)
    {
        var missing = course.Prerequisites
            .Where(p => !student.HasPassed(p))
            .ToList();

        if (missing.Count > 0)
            throw new EnrollmentException(
                $"Chưa qua môn tiên quyết: {string.Join(", ", missing.Select(p => p.Code))}.");
    }
}

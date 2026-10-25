using GradingSystem.Core.Models;

namespace GradingSystem.Core.Rules;

/// <summary>Mẫu chung cho các luật kiểm tra khi THÊM một môn. Luật vi phạm thì ném EnrollmentException.</summary>
public interface IEnrollmentRule
{
    void Validate(Student student, Course course, Term term);
}

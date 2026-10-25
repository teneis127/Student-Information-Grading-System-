using GradingSystem.Core.Models;

namespace GradingSystem.Core.Rules;

/// <summary>Từ chối nếu sinh viên đã qua môn này. KHUNG MẪU.</summary>
public class AlreadyPassedRule : IEnrollmentRule
{
    public void Validate(Student student, Course course, Term term)
    {
        // TODO: nếu student.HasPassed(course) thì ném EnrollmentException kèm lý do
        throw new NotImplementedException();
    }
}

using GradingSystem.Core.Models;

namespace GradingSystem.Core.Rules;

/// <summary>Từ chối nếu sinh viên chưa qua các môn tiên quyết TRỰC TIẾP của môn. KHUNG MẪU.</summary>
public class PrerequisiteRule : IEnrollmentRule
{
    public void Validate(Student student, Course course, Term term)
    {
        // TODO: duyệt course.Prerequisites, môn nào student chưa qua (HasPassed)
        //       thì ném EnrollmentException, nêu rõ tên môn còn thiếu
        throw new NotImplementedException();
    }
}

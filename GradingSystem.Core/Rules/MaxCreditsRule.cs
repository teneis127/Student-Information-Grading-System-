using GradingSystem.Core.Models;

namespace GradingSystem.Core.Rules;

/// <summary>Từ chối nếu thêm môn làm tổng tín chỉ của kỳ vượt mức tối đa (28). KHUNG MẪU.</summary>
public class MaxCreditsRule : IEnrollmentRule
{
    private readonly int _maxCredits;

    public MaxCreditsRule(int maxCredits)
    {
        // TODO: maxCredits phải > 0
        _maxCredits = maxCredits;
    }

    public void Validate(Student student, Course course, Term term)
    {
        // TODO: tổng = student.GetRegisteredCredits(term) + course.Credits
        //       nếu tổng > _maxCredits thì ném EnrollmentException (bằng đúng mức tối đa vẫn hợp lệ)
        throw new NotImplementedException();
    }
}

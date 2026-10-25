using GradingSystem.Core.Models;

namespace GradingSystem.Core.Rules;

/// <summary>Từ chối chốt đăng ký nếu tổng tín chỉ của kỳ dưới mức tối thiểu (15). KHUNG MẪU.</summary>
public class MinCreditsRule : IRegistrationRule
{
    private readonly int _minCredits;

    public MinCreditsRule(int minCredits)
    {
        // TODO: minCredits phải > 0
        _minCredits = minCredits;
    }

    public void Validate(Student student, Term term)
    {
        // TODO: nếu student.GetRegisteredCredits(term) < _minCredits thì ném EnrollmentException
        //       (bằng đúng mức tối thiểu vẫn hợp lệ)
        throw new NotImplementedException();
    }
}

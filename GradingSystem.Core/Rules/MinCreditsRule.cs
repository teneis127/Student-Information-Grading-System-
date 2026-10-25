using GradingSystem.Core.Models;

namespace GradingSystem.Core.Rules;

/// <summary>Từ chối chốt đăng ký nếu tổng tín chỉ của kỳ dưới mức tối thiểu (15). KHUNG MẪU.</summary>
public class MinCreditsRule : IRegistrationRule
{
    private readonly int _minCredits;

    public MinCreditsRule(int minCredits)
    {
        if (minCredits <= 0)
            throw new ArgumentOutOfRangeException(nameof(minCredits), "Mức tối thiểu phải lớn hơn 0.");

        _minCredits = minCredits;
    }

    public void Validate(Student student, Term term)
    {
       int total = student.GetRegisteredCredits(term);

        if (total < _minCredits)
            throw new EnrollmentException(
                $"Chưa đủ tối thiểu {_minCredits} tín chỉ trong kỳ {term}: hiện có {total} tín chỉ.");
    }
}

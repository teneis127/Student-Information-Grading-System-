using GradingSystem.Core.Models;

namespace GradingSystem.Core.Rules;

/// <summary>Từ chối nếu thêm môn làm tổng tín chỉ của kỳ vượt mức tối đa (28). KHUNG MẪU.</summary>
public class MaxCreditsRule : IEnrollmentRule
{
    private readonly int _maxCredits;

    public MaxCreditsRule(int maxCredits)
    {
         if (maxCredits <= 0)
            throw new ArgumentOutOfRangeException(nameof(maxCredits), "Mức tối đa phải lớn hơn 0.");

        _maxCredits = maxCredits;
    }

    public void Validate(Student student, Course course, Term term)
    {
       int total = student.GetRegisteredCredits(term) + course.Credits;

        if (total > _maxCredits)
            throw new EnrollmentException(
                $"Vượt tối đa {_maxCredits} tín chỉ trong kỳ {term}: sẽ là {total} tín chỉ.");
    }
}

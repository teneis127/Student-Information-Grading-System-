using GradingSystem.Core.Models;

namespace GradingSystem.Core.Rules;

/// <summary>Mẫu chung cho các luật kiểm tra khi CHỐT đăng ký cả kỳ. Luật vi phạm thì ném EnrollmentException.</summary>
public interface IRegistrationRule
{
    void Validate(Student student, Term term);
}

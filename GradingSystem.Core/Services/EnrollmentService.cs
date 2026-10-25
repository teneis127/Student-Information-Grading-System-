using GradingSystem.Core.Models;
using GradingSystem.Core.Rules;

namespace GradingSystem.Core.Services;

/// <summary>Điều phối việc ghi danh: chạy các luật rồi mới thay đổi dữ liệu. KHUNG MẪU.</summary>
public class EnrollmentService
{
    private readonly List<IEnrollmentRule> _enrollmentRules;
    private readonly List<IRegistrationRule> _registrationRules;

    public EnrollmentService(
        IEnumerable<IEnrollmentRule> enrollmentRules,
        IEnumerable<IRegistrationRule> registrationRules)
    {
        _enrollmentRules = enrollmentRules.ToList();
        _registrationRules = registrationRules.ToList();
    }

    /// <summary>Ghi danh sinh viên vào môn trong một học kỳ.</summary>
    public Enrollment Enroll(Student student, Course course, Term term)
    {
        // TODO 1: chạy từng rule trong _enrollmentRules (rule vi phạm sẽ ném EnrollmentException)
        // TODO 2: nếu qua hết thì tạo Enrollment, student.AddEnrollment, rồi trả về
        throw new NotImplementedException();
    }

    /// <summary>Hủy một môn. Chỉ hủy được khi chưa chốt đăng ký.</summary>
    public void Drop(Student student, Course course, Term term)
    {
        // TODO: tìm lượt ghi danh, nếu đã Confirmed thì ném EnrollmentException, không thì xóa
        throw new NotImplementedException();
    }

    /// <summary>Chốt đăng ký của cả kỳ.</summary>
    public void SubmitRegistration(Student student, Term term)
    {
        // TODO 1: chạy từng rule trong _registrationRules
        // TODO 2: nếu qua hết thì gọi Confirm() cho mọi lượt ghi danh của kỳ đó
        throw new NotImplementedException();
    }
}

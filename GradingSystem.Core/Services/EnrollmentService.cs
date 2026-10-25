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
        ArgumentNullException.ThrowIfNull(student);
        ArgumentNullException.ThrowIfNull(course);
        ArgumentNullException.ThrowIfNull(term);

        foreach (IEnrollmentRule rule in _enrollmentRules)
            rule.Validate(student, course, term);

        var enrollment = new Enrollment(course, term);
        student.AddEnrollment(enrollment);
        return enrollment;
    }

    /// <summary>Hủy một môn. Chỉ hủy được khi chưa chốt đăng ký.</summary>
    public void Drop(Student student, Course course, Term term)
    {
        ArgumentNullException.ThrowIfNull(student);
        ArgumentNullException.ThrowIfNull(course);
        ArgumentNullException.ThrowIfNull(term);

        Enrollment enrollment = student.GetEnrollments(term).FirstOrDefault(e => e.Course == course)
            ?? throw new EnrollmentException($"Sinh viên chưa ghi danh môn {course.Code} trong kỳ {term}.");

        if (enrollment.Status == EnrollmentStatus.Confirmed)
            throw new EnrollmentException($"Đã chốt đăng ký kỳ {term}, không thể hủy môn {course.Code}.");

        student.RemoveEnrollment(enrollment);
    }

    /// <summary>Chốt đăng ký của cả kỳ.</summary>
    public void SubmitRegistration(Student student, Term term)
    {
        ArgumentNullException.ThrowIfNull(student);
        ArgumentNullException.ThrowIfNull(term);

        foreach (IRegistrationRule rule in _registrationRules)
            rule.Validate(student, term);

        foreach (Enrollment enrollment in student.GetEnrollments(term).ToList())
            enrollment.Confirm();
    }
}

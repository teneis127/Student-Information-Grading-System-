using GradingSystem.Core.Models;
using GradingSystem.Core.Rules;
using GradingSystem.Core.Services;
using Xunit;
using static GradingSystem.Tests.TestData;

namespace GradingSystem.Tests;

public class EnrollmentServiceTests
{
    private static EnrollmentService CreateService() => new(
        new IEnrollmentRule[]
        {
            new AlreadyPassedRule(),
            new DuplicateEnrollmentRule(),
            new PrerequisiteRule(),
            new MaxCreditsRule(28),
        },
        new IRegistrationRule[]
        {
            new MinCreditsRule(15),
        });

    [Fact]
    public void Enroll_ValidRequest_AddsEnrollment()
    {
        var service = CreateService();
        var student = MakeStudent();
        var course = MakeCourse("A");

        var enrollment = service.Enroll(student, course, T1);

        Assert.Single(student.Enrollments);
        Assert.Same(course, enrollment.Course);
        Assert.Equal(EnrollmentStatus.Draft, enrollment.Status);
    }

    [Fact]
    public void Enroll_MissingPrerequisite_IsRejectedAndNothingIsAdded()
    {
        var service = CreateService();
        var student = MakeStudent();
        var math1 = MakeCourse("MATH1");
        var math2 = MakeCourse("MATH2");
        math2.AddPrerequisite(math1);

        Assert.Throws<EnrollmentException>(() => service.Enroll(student, math2, T1));

        Assert.Empty(student.Enrollments);
    }

    [Fact]
    public void Enroll_ExceedingMaxCredits_IsRejected_ButExactlyMaxIsAllowed()
    {
        var service = CreateService();
        var student = MakeStudent();
        for (int i = 1; i <= 9; i++)
            service.Enroll(student, MakeCourse($"C{i}", 3), T1);   // 27 tín chỉ

        Assert.Throws<EnrollmentException>(() => service.Enroll(student, MakeCourse("BIG", 2), T1));   // sẽ là 29

        service.Enroll(student, MakeCourse("OK", 1), T1);   // đúng 28
        Assert.Equal(28, student.GetRegisteredCredits(T1));
    }

    [Fact]
    public void SubmitRegistration_BelowMinCredits_IsRejected()
    {
        var service = CreateService();
        var student = MakeStudent();
        service.Enroll(student, MakeCourse("A", 7), T1);
        service.Enroll(student, MakeCourse("B", 7), T1);   // 14 tín chỉ

        Assert.Throws<EnrollmentException>(() => service.SubmitRegistration(student, T1));

        Assert.All(student.Enrollments, e => Assert.Equal(EnrollmentStatus.Draft, e.Status));
    }

    [Fact]
    public void SubmitRegistration_ExactlyMinCredits_ConfirmsEverything()
    {
        var service = CreateService();
        var student = MakeStudent();
        for (int i = 1; i <= 5; i++)
            service.Enroll(student, MakeCourse($"C{i}", 3), T1);   // 15 tín chỉ

        service.SubmitRegistration(student, T1);

        Assert.All(student.Enrollments, e => Assert.Equal(EnrollmentStatus.Confirmed, e.Status));
    }

    [Fact]
    public void Drop_DraftEnrollment_RemovesIt()
    {
        var service = CreateService();
        var student = MakeStudent();
        var course = MakeCourse("A");
        service.Enroll(student, course, T1);

        service.Drop(student, course, T1);

        Assert.Empty(student.Enrollments);
    }

    [Fact]
    public void Drop_ConfirmedEnrollment_IsRejected()
    {
        var service = CreateService();
        var student = MakeStudent();
        var first = MakeCourse("C1", 3);
        service.Enroll(student, first, T1);
        for (int i = 2; i <= 5; i++)
            service.Enroll(student, MakeCourse($"C{i}", 3), T1);
        service.SubmitRegistration(student, T1);

        Assert.Throws<EnrollmentException>(() => service.Drop(student, first, T1));
        Assert.Equal(5, student.Enrollments.Count);
    }

    [Fact]
    public void Drop_NotEnrolled_IsRejected()
    {
        var service = CreateService();
        var student = MakeStudent();

        Assert.Throws<EnrollmentException>(() => service.Drop(student, MakeCourse("A"), T1));
    }

    [Fact]
    public void HappyPath_PassPrerequisiteThenEnrollAndSubmit()
    {
        var service = CreateService();
        var student = MakeStudent();
        var math1 = MakeCourse("MATH1", 3);
        var math2 = MakeCourse("MATH2", 3);
        math2.AddPrerequisite(math1);
        AddResult(student, math1, T1, 7.0);   // kỳ 1: đã qua MATH1

        service.Enroll(student, math2, T2);   // kỳ 2: ghi danh MATH2 được vì đã qua MATH1
        for (int i = 1; i <= 4; i++)
            service.Enroll(student, MakeCourse($"EXTRA{i}", 3), T2);   // đủ 15 tín chỉ
        service.SubmitRegistration(student, T2);

        Assert.Equal(15, student.GetRegisteredCredits(T2));
        Assert.All(student.GetEnrollments(T2), e => Assert.Equal(EnrollmentStatus.Confirmed, e.Status));
    }
}

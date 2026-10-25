using Xunit;

namespace GradingSystem.Tests;

/// <summary>
/// KHUNG MẪU test tuần 2. Mỗi test đang bị Skip. Em viết nội dung theo 3 bước
/// (chuẩn bị, thực hiện, khẳng định) rồi XÓA phần Skip để test chạy thật.
/// </summary>
public class Week2SkeletonTests
{
    [Fact(Skip = "TODO")]
    public void Course_ZeroCredits_IsRejected()
    {
        // TODO: Assert.Throws<ArgumentOutOfRangeException>(() => new Course(...))
    }

    [Fact(Skip = "TODO")]
    public void Course_AddPrerequisite_IndirectCycle_IsRejected()
    {
        // TODO: A yêu cầu B, B yêu cầu C. Thêm "C yêu cầu A" phải bị từ chối.
    }

    [Fact(Skip = "TODO")]
    public void Enrollment_RecordScore_RoundsToOneDecimal()
    {
        // TODO: ghi 8.95 thì Score phải là 9.0
    }

    [Fact(Skip = "TODO")]
    public void Student_HasPassed_ReturnsTrueAfterPassingScore()
    {
        // TODO
    }

    [Fact(Skip = "TODO")]
    public void PrerequisiteRule_MissingPrerequisite_IsRejected()
    {
        // TODO: kịch bản "bị từ chối đúng" mà đề yêu cầu
    }

    [Theory(Skip = "TODO")]
    [InlineData(27, true)]
    [InlineData(28, true)]
    [InlineData(29, false)]
    public void MaxCreditsRule_Boundary(int totalAfterAdding, bool allowed)
    {
        // TODO: test biên 28 tín chỉ
    }

    [Theory(Skip = "TODO")]
    [InlineData(14, false)]
    [InlineData(15, true)]
    public void MinCreditsRule_Boundary(int totalCredits, bool allowed)
    {
        // TODO: test biên 15 tín chỉ
    }

    [Fact(Skip = "TODO")]
    public void EnrollmentService_HappyPath_EnrollThenSubmit()
    {
        // TODO: kịch bản "chạy trọn vẹn" mà đề yêu cầu
    }
}

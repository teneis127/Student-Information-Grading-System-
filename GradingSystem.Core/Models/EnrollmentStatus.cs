namespace GradingSystem.Core.Models;

/// <summary>Trạng thái của một lượt ghi danh.</summary>
public enum EnrollmentStatus
{
    /// <summary>Đang đăng ký, chưa chốt. Còn hủy được.</summary>
    Draft,

    /// <summary>Đã chốt đăng ký của cả kỳ. Không hủy được nữa.</summary>
    Confirmed
}

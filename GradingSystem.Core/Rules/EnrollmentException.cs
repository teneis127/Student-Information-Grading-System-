namespace GradingSystem.Core.Rules;

/// <summary>Lỗi nghiệp vụ khi ghi danh bị từ chối. Message cho biết lý do.</summary>
public class EnrollmentException : Exception
{
    public EnrollmentException(string message) : base(message)
    {
    }
}

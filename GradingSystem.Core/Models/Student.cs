namespace GradingSystem.Core.Models;

/// <summary>Sinh viên. Chỉ giữ dữ liệu và danh sách ghi danh của mình. KHUNG MẪU.</summary>
public class Student
{
    private readonly List<Enrollment> _enrollments = new();

    public string Id { get; }
    public string Name { get; }
    public IReadOnlyList<Enrollment> Enrollments => _enrollments;

    public Student(string id, string name)
    {
        // TODO: id và name không được rỗng (ArgumentException)
        // TODO: gán giá trị
        throw new NotImplementedException();
    }

    public void AddEnrollment(Enrollment enrollment)
    {
        // TODO: enrollment không được null, rồi thêm vào _enrollments
        throw new NotImplementedException();
    }

    public void RemoveEnrollment(Enrollment enrollment)
    {
        // TODO: xóa khỏi _enrollments
        throw new NotImplementedException();
    }

    /// <summary>Các lượt ghi danh của sinh viên trong một học kỳ.</summary>
    public IEnumerable<Enrollment> GetEnrollments(Term term)
    {
        // TODO: lọc _enrollments theo term (Term là record nên so sánh bằng == được)
        throw new NotImplementedException();
    }

    /// <summary>Tổng tín chỉ đang đăng ký trong một học kỳ.</summary>
    public int GetRegisteredCredits(Term term)
    {
        // TODO: cộng Course.Credits của các lượt ghi danh trong term
        throw new NotImplementedException();
    }

    /// <summary>Sinh viên đã qua môn này chưa?</summary>
    public bool HasPassed(Course course)
    {
        // TODO: có lượt ghi danh nào của course mà IsPassed() là true không?
        throw new NotImplementedException();
    }
}

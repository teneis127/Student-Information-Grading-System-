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
        if (string.IsNullOrWhiteSpace(id))
            throw new ArgumentException("Id không được rỗng.", nameof(id));

        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Tên không được rỗng.", nameof(name));

        Id = id;
        Name = name;
    }

    public void AddEnrollment(Enrollment enrollment)
    {
        ArgumentNullException.ThrowIfNull(enrollment);
        _enrollments.Add(enrollment);
    }

    public void RemoveEnrollment(Enrollment enrollment)
    {
         _enrollments.Remove(enrollment);
    }

    /// <summary>Các lượt ghi danh của sinh viên trong một học kỳ.</summary>
    public IEnumerable<Enrollment> GetEnrollments(Term term)
    {
        return _enrollments.Where(e => e.Term == term);
    }

    /// <summary>Tổng tín chỉ đang đăng ký trong một học kỳ.</summary>
    public int GetRegisteredCredits(Term term)
    {
       return GetEnrollments(term).Sum(e => e.Course.Credits);
    }

    /// <summary>Sinh viên đã qua môn này chưa?</summary>
    public bool HasPassed(Course course)
    {
       return _enrollments.Any(e => e.Course == course && e.IsPassed());
    }
}

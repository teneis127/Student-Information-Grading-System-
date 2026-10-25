using GradingSystem.Core.Grading;

namespace GradingSystem.Core.Models;

/// <summary>Một môn học. KHUNG MẪU: em tự viết phần thân các hàm.</summary>
public class Course
{
    private readonly List<Course> _prerequisites = new();

    public string Code { get; }
    public string Name { get; }
    public int Credits { get; }
    public IGradingScheme Scheme { get; }
    public Instructor? Instructor { get; }

    /// <summary>Các môn mà môn này yêu cầu phải học trước. Bên ngoài chỉ được đọc.</summary>
    public IReadOnlyList<Course> Prerequisites => _prerequisites;

    public Course(string code, string name, int credits, IGradingScheme scheme, Instructor? instructor = null)
    {
        if (string.IsNullOrWhiteSpace(code))
            throw new ArgumentException("Mã môn học không được rỗng.", nameof(code));

        if(string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Tên môn học không được rỗng.", nameof(code));

        if (credits <= 0)
            throw new ArgumentOutOfRangeException(nameof(credits), "Số tín chỉ phải lớn hơn 0.");
        
        ArgumentNullException.ThrowIfNull(scheme);

        Code = code;
        Name = name;
        Credits = credits;
        Scheme = scheme;
        Instructor = instructor;
    }

    private static bool CanReach(Course from, Course target, HashSet<Course> visited)
    {
        if (ReferenceEquals(from, target))
            return true;
        

        if (!visited.Add(from))
            return false;

        foreach (Course next in from._prerequisites)
        {
            if (CanReach(next, target, visited))
                return true;
        }

        return false;
    }

    /// <summary>Thêm một môn tiên quyết cho môn này.</summary>
    public void AddPrerequisite(Course prerequisite)
    {
        ArgumentNullException.ThrowIfNull(prerequisite);

        if (ReferenceEquals(prerequisite, this))
            throw new InvalidOperationException("Một môn học không thể là tiên quyết của chính nó.");

        if (_prerequisites.Contains(prerequisite))
            throw new InvalidOperationException($"Môn {prerequisite.Code} đã là tiên quyết của {Code}.");
        
        if (CanReach(prerequisite, this, new HashSet<Course>()))
            throw new InvalidOperationException($"Thêm {prerequisite.Code} làm tiên quyết của {Code} sẽ tạo chu trình. ");
        _prerequisites.Add(prerequisite);
    }
}

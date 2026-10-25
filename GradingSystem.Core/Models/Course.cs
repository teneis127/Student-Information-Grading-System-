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
        // TODO: code và name không được rỗng (ArgumentException)
        // TODO: credits phải > 0 (ArgumentOutOfRangeException)
        // TODO: scheme không được null (ArgumentNullException)
        // TODO: gán giá trị vào các thuộc tính
        throw new NotImplementedException();
    }

    /// <summary>Thêm một môn tiên quyết cho môn này.</summary>
    public void AddPrerequisite(Course prerequisite)
    {
        // TODO 1: prerequisite không được null
        // TODO 2: không cho môn tự làm tiên quyết của chính nó
        // TODO 3: không cho thêm trùng
        // TODO 4: dùng DFS kiểm tra chu trình: từ 'prerequisite' đi theo các Prerequisites,
        //         nếu đến được 'this' thì ném InvalidOperationException
        // TODO 5: nếu hợp lệ thì thêm vào _prerequisites
        throw new NotImplementedException();
    }
}

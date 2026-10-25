namespace GradingSystem.Core.Models;

/// <summary>Giảng viên phụ trách môn học. KHUNG MẪU.</summary>
public class Instructor
{
    public string Id { get; }
    public string Name { get; }

    public Instructor(string id, string name)
    {
        // TODO 1: id không được rỗng (string.IsNullOrWhiteSpace), nếu rỗng ném ArgumentException
        // TODO 2: name không được rỗng, nếu rỗng ném ArgumentException
        // TODO 3: gán giá trị vào Id và Name
        throw new NotImplementedException();
    }
}

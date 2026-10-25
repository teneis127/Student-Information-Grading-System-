namespace GradingSystem.Core.Models;

/// <summary>Giảng viên phụ trách môn học. KHUNG MẪU.</summary>
public class Instructor
{
    public string Id { get; }
    public string Name { get; }

    public Instructor(string id, string name)
    {
        if (string.IsNullOrWhiteSpace(id))
            throw new ArgumentException("Id không được rỗng.", nameof(id));

        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Tên không được rỗng.", nameof(name));

        Id = id;
        Name = name;
    }
}

namespace GradingSystem.Core.Models;

/// <summary>
/// Một học kỳ, ví dụ năm 2026 kỳ 1. Mỗi năm học có 2 kỳ.
/// Hai học kỳ cùng năm và cùng kỳ thì bằng nhau (nhờ dùng record). KHUNG MẪU.
/// </summary>
public record Term
{
    public int Year { get; }
    public int Number { get; }

    public Term(int year, int number)
    {
        // TODO 1: year phải > 0, nếu không ném ArgumentOutOfRangeException
        // TODO 2: number chỉ được là 1 hoặc 2, nếu không ném ArgumentOutOfRangeException
        // TODO 3: gán giá trị vào Year và Number
        throw new NotImplementedException();
    }

    public override string ToString()
    {
        // TODO: trả về dạng "năm-kỳ", ví dụ "2026-1" (dùng chuỗi $"...")
        throw new NotImplementedException();
    }
}

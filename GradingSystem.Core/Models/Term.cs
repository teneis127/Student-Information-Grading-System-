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
        if (year <= 0)
            throw new ArgumentOutOfRangeException(nameof(year), "Năm phải lớn hơn 0.");

        if (number < 1 || number > 2)
            throw new ArgumentOutOfRangeException(nameof(number), "Kỳ phải là 1 hoặc 2.");

        Year = year;
        Number = number;
    }

    public override string ToString()
    {
         return $"{Year}-{Number}";
    }
}

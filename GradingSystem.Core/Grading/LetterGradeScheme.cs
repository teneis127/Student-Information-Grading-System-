namespace GradingSystem.Core.Grading;

/// <summary>Thang điểm chữ theo bảng quy đổi của quy chế (thang 10 -> chữ -> thang 4).</summary>
public class LetterGradeScheme : IGradingScheme
{
    private static readonly GradeBand[] Bands =
    {
        new(9.0, "A+", 4.0),
        new(8.5, "A",  3.7),
        new(8.0, "B+", 3.5),
        new(7.0, "B",  3.0),
        new(6.5, "C+", 2.5),
        new(5.5, "C",  2.0),
        new(5.0, "D+", 1.5),
        new(4.0, "D",  1.0),   
        new(0.0, "F",  0.0),
    };

    public string Name => "Letter grade";
    public bool CountsTowardGpa => true;

    public string Evaluate(double score) => FindBand(score).Letter;

    public double? GradePoints(double score) => FindBand(score).Points;

    public bool IsPassing(double score)
    {
        GradingPolicy.EnsureValidScore(score);
        return score >= GradingPolicy.PassingScore;
    }

    private static GradeBand FindBand(double score)
    {
        GradingPolicy.EnsureValidScore(score);

        foreach (GradeBand band in Bands)
        {
            if (score >= band.MinScore)
                return band;
        }

        throw new InvalidOperationException("Bảng quy đổi điểm bị thiếu dải thấp nhất.");
    }
}

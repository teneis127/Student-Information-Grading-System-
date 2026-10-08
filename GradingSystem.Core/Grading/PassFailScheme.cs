namespace GradingSystem.Core.Grading;

public class PassFailScheme : IGradingScheme
{
    public string Name => "Pass/Fail";
    public bool CountsTowardGpa => false;
    private readonly double _passingScore;
    public PassFailScheme(double passingScore = GradingPolicy.PassingScore)
    {
        GradingPolicy.EnsureValidScore(passingScore);
        _passingScore = passingScore;
    }
    public bool IsPassing(double score)
    {
        GradingPolicy.EnsureValidScore(score);
        return score >= _passingScore;
    }
    public string Evaluate(double score)
    {
        GradingPolicy.EnsureValidScore(score);
        return IsPassing(score) ? "PASS" : "FAIL";
    }
    public double? GradePoints(double score)
    {
        GradingPolicy.EnsureValidScore(score);
        return null;
    }
}

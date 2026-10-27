using GradingSystem.Core.Models;

namespace GradingSystem.Core.Services;

/// <summary>Chỉ lo việc tính GPA (điểm trung bình hệ 4). KHUNG MẪU: em tự viết phần thân.</summary>
public class GpaCalculator
{
    /// <summary>
    /// Tính GPA tích lũy của sinh viên. Trả về null nếu chưa có môn nào đủ điều kiện để tính
    /// (null nghĩa là "chưa có GPA", khác với GPA bằng 0).
    /// </summary>
    public double? Compute(Student student)
    {
        // TODO 1: student không được null (ArgumentNullException)
        // TODO 2: chỉ lấy các lượt ghi danh ĐÃ CÓ ĐIỂM (Score có giá trị)
        // TODO 3: chỉ lấy môn mà Course.Scheme.CountsTowardGpa là true (môn Pass/Fail bị loại)
        // TODO 4: môn học lại thì chỉ tính LẦN HỌC GẦN NHẤT của môn đó
        //         (gợi ý: GroupBy theo Course, rồi OrderBy Term.Year, ThenBy Term.Number, lấy phần tử cuối)
        // TODO 5: GPA = tổng(điểm hệ 4 x số tín chỉ) / tổng số tín chỉ
        //         (điểm hệ 4 lấy từ Enrollment.GetGradePoints(); môn rớt D, F vẫn được tính)
        // TODO 6: không có môn nào đủ điều kiện thì trả về null
        // TODO 7: làm tròn 2 chữ số thập phân (MidpointRounding.AwayFromZero)
        throw new NotImplementedException();
    }
}

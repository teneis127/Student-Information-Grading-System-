using GradingSystem.Core.Models;
using GradingSystem.Core.Services;

namespace GradingSystem.Core.Reports;

/// <summary>Chỉ lo việc định dạng bảng điểm thành chuỗi. Không tự tính GPA. KHUNG MẪU.</summary>
public class TranscriptPrinter
{
    private readonly GpaCalculator _gpaCalculator;
    private readonly AcademicRankClassifier _rankClassifier;

    public TranscriptPrinter(GpaCalculator gpaCalculator, AcademicRankClassifier rankClassifier)
    {
        // TODO: kiểm tra hai tham số không null rồi gán vào hai biến trên
        throw new NotImplementedException();
    }

    /// <summary>Tạo bảng điểm của một sinh viên dưới dạng chuỗi nhiều dòng.</summary>
    public string Print(Student student)
    {
        // TODO 1: student không được null
        // TODO 2: in tiêu đề, tên và mã sinh viên
        // TODO 3: nhóm các lượt ghi danh theo học kỳ, sắp xếp học kỳ tăng dần (Year rồi Number)
        // TODO 4: mỗi môn in: mã môn, tên môn, số tín chỉ, điểm hệ 10, kết quả (Evaluate), điểm hệ 4
        //         chưa có điểm thì in "chưa có điểm"; môn Pass/Fail không có điểm hệ 4 thì in "-"
        // TODO 5: in GPA tích lũy (nhờ GpaCalculator) và xếp loại học lực (nhờ AcademicRankClassifier)
        //         nếu GPA là null thì in "chưa có GPA" và không xếp loại
        // Gợi ý: dùng StringBuilder và AppendLine; PadRight để canh cột
        throw new NotImplementedException();
    }
}

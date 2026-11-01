# Báo cáo cuối kỳ: Student Information & Grading System

- Môn học: Lập trình hướng đối tượng, HK1 2026-2027 đợt 1
- Giảng viên hướng dẫn: Huỳnh Xuân Phụng
- Sinh viên: Trần Nhật Tiến ; 25110063
- Ngôn ngữ: C# (.NET), test bằng xUnit
- GitHub: [https://github.com/teneis127/Student-Information-Grading-System-]

## 1. Bài toán và những gì em đã làm

Đề yêu cầu làm một hệ thống cho trường đại học: sinh viên ghi danh vào môn học, ghi điểm, tính GPA và in bảng điểm. Việc ghi danh phải theo một số luật, ví dụ phải học xong môn tiên quyết và không đăng ký quá số tín chỉ cho phép.

**Năm yêu cầu của đề và chỗ em làm:**

- R1, mô hình dữ liệu: các class Student, Course, Enrollment, Instructor, Term.
- R2, ít nhất hai thang điểm dùng chung một interface: IGradingScheme, LetterGradeScheme, PassFailScheme.
- R3, tính GPA khi có nhiều loại thang điểm: GpaCalculator.Compute.
- R4, luật ghi danh có phương thức kiểm tra riêng: IEnrollmentRule, IRegistrationRule và các class luật, cùng EnrollmentService.
- R5, bảng điểm của một sinh viên: TranscriptPrinter.Print.

**Các quy tắc em đã chọn:**

- Qua môn khi điểm từ 5.0 trở lên (thang 10), lưu ở một chỗ là GradingPolicy.PassingScore.
- Quy đổi điểm theo bảng của trường (từ A+ đến F). Chữ D (4.0 đến 4.9) vẫn có 1.0 điểm hệ 4 nhưng không qua môn.
- Mỗi năm học có 2 học kỳ.
- Tối đa 28 tín chỉ khi thêm môn, tối thiểu 15 tín chỉ khi chốt đăng ký.
- Môn Pass/Fail không tính vào GPA. Môn rớt (D, F) vẫn tính vào GPA.
- Học lại thì chỉ tính lần học gần nhất. Môn đã qua thì không được ghi danh lại.
- Không ghi danh một môn hai lần trong cùng một kỳ. Chỉ được hủy môn khi chưa chốt đăng ký.
- Điểm làm tròn 1 chữ số thập phân khi ghi, GPA làm tròn 2 chữ số.

## 2. Thuật toán kiểm tra môn tiên quyết: DFS trên đồ thị có hướng

**Tóm tắt.** Em dùng thuật toán DFS (duyệt theo chiều sâu) trên một đồ thị có hướng để quản lý các môn tiên quyết. Đồ thị được lưu bằng danh sách kề: mỗi Course giữ danh sách các môn tiên quyết của nó. DFS dùng để chặn chu trình khi thêm môn tiên quyết, nằm ở hàm Course.CanReach và được gọi từ Course.AddPrerequisite.

**Cách biểu diễn.** Em xem các môn học như một đồ thị có hướng. Mỗi môn (Course) là một đỉnh. Nếu môn A yêu cầu môn B thì có một cạnh có hướng từ A sang B. Ví dụ:

- Toán 2 yêu cầu Toán 1: có cạnh từ Toán 2 sang Toán 1.
- Toán 3 yêu cầu Toán 2: có cạnh từ Toán 3 sang Toán 2.

Đồ thị phải có hướng vì quan hệ này không đối xứng: Toán 2 cần Toán 1, nhưng Toán 1 không cần Toán 2. Đồ thị này cũng không được có chu trình, vì nếu A cần B và B cần A thì không ai học được cả hai.

**Cấu trúc dữ liệu.** Em dùng danh sách kề: mỗi Course có một danh sách riêng các môn tiên quyết của nó (trường private _prerequisites, bên ngoài chỉ đọc qua IReadOnlyList Prerequisites). Em chọn vậy vì mỗi môn chỉ có vài môn tiên quyết nên đồ thị thưa, dùng danh sách kề gọn hơn ma trận. Thêm môn mới cũng chỉ là tạo thêm một đối tượng Course.

**Chặn chu trình khi thêm tiên quyết.** Trong Course.AddPrerequisite, trước khi thêm "môn A yêu cầu môn P", em kiểm tra xem từ P đi theo các môn tiên quyết có đến được A không. Nếu đến được thì thêm cạnh sẽ tạo vòng nên em từ chối. Việc kiểm tra này là hàm CanReach, dùng DFS (duyệt theo chiều sâu):

1. Nếu môn đang xét chính là môn cần tìm thì trả về true.
2. Nếu môn này đã duyệt rồi (nằm trong tập visited) thì trả về false.
3. Ngược lại, thêm vào visited rồi thử từng môn tiên quyết của nó. Nếu có nhánh nào tìm thấy thì true.
4. Thử hết mà không thấy thì false.

Ví dụ: A yêu cầu B, B yêu cầu C. Em thử thêm "C yêu cầu A". CanReach đi từ A sang B rồi sang C, gặp C nên trả về true, nghĩa là sẽ tạo vòng A, B, C, A, và em ném InvalidOperationException. Tập visited giúp mỗi môn chỉ duyệt một lần nên độ phức tạp là O(V + E) (V là số môn, E là số quan hệ tiên quyết). Hàm này cũng chặn thêm trường hợp tự làm tiên quyết của chính mình và thêm trùng.

**Vì sao em dùng DFS.** Câu hỏi cần trả lời là "từ môn P có đi theo các môn tiên quyết để đến được môn A hay không", tức là tìm đường đi trong đồ thị có hướng. DFS làm được việc này và viết bằng đệ quy khá ngắn. Duyệt theo chiều rộng (BFS) cũng cho cùng kết quả, em chọn DFS vì code đệ quy dễ viết và dễ đọc hơn.

**Khi ghi danh.** PrerequisiteRule.Validate chỉ kiểm tra các môn tiên quyết trực tiếp của môn đó, xem sinh viên đã qua chưa (gọi Student.HasPassed). Em không cần duyệt cả đồ thị, vì để qua được môn tiên quyết trực tiếp thì sinh viên đã phải qua tiên quyết của môn đó từ trước rồi.

## 3. Self-Explanation

### 3.1 Theo từng yêu cầu

**R1.** Em tách Enrollment thành một class riêng (mỗi Enrollment gắn với một Course và một Term, và có điểm). Lý do là một sinh viên có thể học lại cùng một môn ở học kỳ khác với điểm khác. Nếu để điểm trong Student hoặc Course thì không biểu diễn được việc học lại.

**R2.** IGradingScheme có Name, CountsTowardGpa, Evaluate, GradePoints, IsPassing. LetterGradeScheme tra bảng 9 mức, PassFailScheme chỉ trả PASS hoặc FAIL. Mỗi Course giữ một IGradingScheme.

**R3.** GpaCalculator.Compute chỉ lấy các lượt học đã có điểm và có Course.Scheme.CountsTowardGpa là true. Với môn học lại, nó chỉ giữ lần học gần nhất (sắp theo năm rồi đến kỳ). GPA bằng tổng của điểm hệ 4 nhân tín chỉ, chia cho tổng tín chỉ. Nếu không có môn nào để tính thì trả về null chứ không trả 0, vì chưa có GPA khác với GPA bằng 0 (trả 0 thì sẽ bị xếp loại Kém sai). Em dùng decimal để cộng và làm tròn cho khỏi sai số.

**R4.** Mỗi luật là một class có phương thức Validate. Em chia hai interface: IEnrollmentRule.Validate(student, course, term) cho các luật kiểm tra lúc thêm một môn (AlreadyPassedRule, PrerequisiteRule, MaxCreditsRule, DuplicateEnrollmentRule), và IRegistrationRule.Validate(student, term) cho luật lúc chốt đăng ký (MinCreditsRule). Luật tối thiểu 15 tín chỉ không kiểm tra được lúc thêm từng môn (mới thêm môn đầu thì tất nhiên chưa đủ), nên phải để lúc chốt. EnrollmentService.Enroll chạy hết các luật trước, nếu có luật vi phạm thì ném EnrollmentException và chưa thêm gì vào sinh viên.

**R5.** TranscriptPrinter.Print gom các lượt học theo học kỳ, mỗi môn in một dòng bằng hàm FormatRow, cuối bảng in GPA và xếp loại. Nếu chưa có GPA thì in "chưa có GPA" và không xếp loại. Em dùng CultureInfo.InvariantCulture để số luôn có dấu chấm (3.70), vì máy tiếng Việt mặc định in dấu phẩy.

### 3.2 Theo từng nguyên lý

**Polymorphism (đa hình)**

- Ở đâu: interface IGradingScheme (Evaluate, GradePoints, IsPassing và thuộc tính CountsTowardGpa) với hai class LetterGradeScheme và PassFailScheme. Nơi dùng: Enrollment.IsPassed, Enrollment.GetGradePoints, GpaCalculator.Compute, TranscriptPrinter.FormatRow, tất cả đều gọi qua Course.Scheme.
- Bài toán: có môn chấm theo chữ (có điểm hệ 4, tính vào GPA), có môn chỉ đạt hoặc không đạt (không có điểm hệ 4, không tính vào GPA). Khi tính GPA hỗn hợp, cùng một đoạn code phải xử lý được cả hai loại.
- Vì sao hợp: cái khác nhau giữa các môn là cách xử lý điểm, không chỉ là dữ liệu. Nếu em dùng if/else theo loại môn thì phải viết ở bốn chỗ (IsPassed, GetGradePoints, Compute, FormatRow), thêm thang mới là phải sửa cả bốn chỗ và dễ sót. Dùng interface thì mỗi thang tự trả lời về chính nó. Riêng CountsTowardGpa giúp GpaCalculator.Compute không bao giờ phải hỏi "đây có phải môn Pass/Fail không", nó chỉ hỏi cờ này. Em chọn interface thay vì class cha vì hai thang không có code chung. Em cũng tách IsPassing khỏi GradePoints vì chữ D có 1.0 điểm nhưng vẫn không qua môn.
- Test: PolymorphismTests và ScenarioTests.Scenario3 (cùng 8.5 điểm, thang chữ cho GPA 3.7, thang Pass/Fail cho GPA null), EnrollmentTests.IsPassed_FollowsTheSchemeOfTheCourse.

**Encapsulation (đóng gói)**

- Ở đâu: Enrollment.Score có private set, chỉ đặt qua RecordScore (kiểm tra điểm trong 0 đến 10 bằng GradingPolicy.EnsureValidScore rồi làm tròn). Course có _prerequisites là private, bên ngoài chỉ thêm qua AddPrerequisite. Bảng Bands trong LetterGradeScheme là private. Term, Instructor, Student, Course có thuộc tính chỉ đọc, được kiểm tra ngay trong constructor.
- Bài toán: có hai điều luôn phải đúng. Một là điểm nằm trong 0 đến 10 (vì GPA dựa vào đó), hai là đồ thị tiên quyết không có chu trình.
- Vì sao hợp: nếu Score là trường public thì chỗ nào cũng gán được 11.5 hay NaN, lỗi sẽ lộ ra ở một chỗ khác khá xa. Nếu danh sách tiên quyết là List public thì code bên ngoài gọi Add thẳng và bỏ qua kiểm tra chu trình. Đóng gói làm mỗi ràng buộc chỉ có một cửa vào (RecordScore, AddPrerequisite), nên các đối tượng đang có luôn hợp lệ.
- Test: EnrollmentTests.RecordScore_InvalidScore_IsRejected, RecordScore_RoundsToOneDecimal, CourseTests.AddPrerequisite_IndirectCycle_IsRejected.

**Single Responsibility (một trách nhiệm)**

- Ở đâu: EnrollmentService chỉ điều phối (chạy các luật rồi mới đổi dữ liệu), mỗi luật (AlreadyPassedRule, PrerequisiteRule, MaxCreditsRule, DuplicateEnrollmentRule, MinCreditsRule) chỉ kiểm tra một điều. GpaCalculator chỉ tính GPA, AcademicRankClassifier chỉ xếp loại, TranscriptPrinter chỉ định dạng bảng điểm. Student chỉ giữ dữ liệu.
- Bài toán: đề yêu cầu tách luật ghi danh khỏi việc định dạng bảng điểm. Ba loại thay đổi có thể xảy ra riêng: đổi luật (ví dụ 28 thành 30 tín chỉ), đổi bảng xếp loại, hoặc đổi cách in bảng điểm.
- Vì sao hợp: mỗi class chỉ có một lý do để sửa. Đổi 28 thành 30 chỉ là đổi con số khi tạo MaxCreditsRule, không đụng EnrollmentService hay TranscriptPrinter. Đổi cách in chỉ sửa FormatRow. Nếu em nhét hết vào Student thì Student sẽ thành một class làm mọi việc, sửa gì cũng đụng vào nó. Tách luật ra còn giúp test từng luật riêng.
- Test: mỗi luật có test riêng trong RuleTests (kể cả biên 28 và 15), TranscriptPrinterTests và GpaCalculatorTests kiểm tra riêng phần in và phần tính.

## 4. Kiểm thử

Kết quả dotnet test: [145] test, [145] đạt, [0] lỗi, [0] bị bỏ qua.

Ba kịch bản bắt buộc của đề nằm trong ScenarioTests:

- Chạy trọn vẹn: Scenario1_HappyPath_EnrollGradeComputeGpaAndPrintTranscript. Ghi danh 15 tín chỉ, chốt đăng ký, nhập điểm, tính GPA (3.22), in bảng điểm, rồi ghi danh môn tiếp theo vì đã qua môn tiên quyết.
- Bị từ chối đúng: Scenario2_MissingPrerequisite_IsRejected (chưa qua môn tiên quyết) và Scenario2_ExceedingMaxCredits_IsRejected (vượt 28 tín chỉ). Bị từ chối thì không thêm gì vào sinh viên.
- Đổi thang điểm: Scenario3_Polymorphism_SwappingSchemeChangesBehavior.

Một số test biên em thấy quan trọng: điểm 4.9 và 5.0 (ngưỡng qua môn), đúng 28 và 29 tín chỉ, đúng 14 và 15 tín chỉ, kỳ 0 và kỳ 3 bị từ chối, điểm 8.95 làm tròn thành 9.0, học lại chỉ tính lần gần nhất dù thêm vào theo thứ tự nào, và GPA là null khi chưa có môn nào để tính. Em cũng chạy thử chương trình GradingSystem.App để nhìn bảng điểm thật.

## 5. Việc dùng AI

Em có dùng AI (Claude, qua claude.ai) trong cả đồ án. AI giúp em phân tích đề, đề xuất thiết kế, tạo khung code và bản tham khảo, giải thích lỗi. Em chọn các quyết định cho chương trình (ví dụ 28 và 15 tín chỉ, qua môn từ 5.0, môn Pass/Fail không tính GPA), commit khung code lên GitHub trước, rồi tự viết vào khung, chạy test, sửa lỗi và đối chiếu với bản tham khảo. Nhật ký chi tiết từng tuần nằm trong file docs/AI_USAGE_LOG.md.

## 6. Hạn chế

- Dữ liệu chỉ nằm trong bộ nhớ, chưa lưu vào file hay cơ sở dữ liệu.
- Chưa có giao diện, chỉ có chương trình chạy thử trên dòng lệnh.
- Luật tối thiểu 15 tín chỉ chưa có ngoại lệ cho học kỳ cuối.



using System.ComponentModel;

namespace Karma.Core.Enums
{
    public enum MilitaryServiceStatus
    {
        [Description("نامشخص")]
        Unknown,
        [Description("انجام شده")]
        Done,
        [Description("معافیت دائم")]
        PermanentExemption,
        [Description("معافیت تحصیلی")]
        AcademicExemption,
        [Description("در حال انجام")]
        InProgress,
        [Description("مشمول")]
        SubjectToService
    }
}

using System.ComponentModel;

namespace Karma.Core.Enums
{
    public enum DegreeLevel
    {
        [Description]
        HighSchool,
        [Description("دیپلم")]
        Diploma,
        [Description("کاردانی")]
        Associate,
        [Description("کارشناسی")]
        Bachelor,
        [Description("کارشناسی ارشد")]
        Master,
        [Description("دکترا")]
        Doctorate
    }
}

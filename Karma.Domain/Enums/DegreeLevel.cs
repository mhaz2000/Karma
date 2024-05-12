using System.ComponentModel;

namespace Karma.Core.Enums
{
    public enum DegreeLevel
    {
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

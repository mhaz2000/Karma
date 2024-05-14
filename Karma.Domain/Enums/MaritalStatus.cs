using System.ComponentModel;

namespace Karma.Core.Enums
{
    public enum MaritalStatus
    {
        [Description("نامشخص")]
        Unknown,
        [Description("مجرد")]
        Single,
        [Description("متاهل")]
        Married
    }
}

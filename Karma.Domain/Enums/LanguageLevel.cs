using System.ComponentModel;

namespace Karma.Core.Enums
{
    public enum LanguageLevel
    {
        [Description("مقدماتی")]
        Basic,
        [Description("پایین تر از متوسط")]
        PreIntermediate,
        [Description("متوسط")]
        Intermediate,
        [Description("بالاتر از متوسط")]
        UpperIntermediate,
        [Description("پیشرفته")]
        Advanced,
        [Description("در حد زبان مادری")]
        Native
    }
}

using System.ComponentModel;

namespace Karma.Core.Enums
{
    public enum SeniorityLevel
    {
        [Description("کارگر")]
        Worker,
        [Description("کارمند")]
        Employee,
        [Description("کارشناس")]
        Specialist,
        [Description("کارشناس ارشد")]
        SeniorSpecialist,
        [Description("مدیر میانی")]
        Manager,
        [Description("معاونت")]
        Director,
        [Description("مدیر ارشد")]
        Business_Head_CEO
    }
}

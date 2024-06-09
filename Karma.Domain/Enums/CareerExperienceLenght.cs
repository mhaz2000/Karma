using System.ComponentModel;

namespace Karma.Core.Enums
{
    public enum CareerExperienceLength
    {
        [Description("بدون سابقه کاری")]
        WithoutExperience,
        [Description("کمتر از یک سال")]
        LessThanOneYear,
        [Description("بین 1 تا 3 سال")]
        BetweenOneAndThreeYears,
        [Description("بین 3 تا 5 سال")]
        BetweenThreeAndFiveYears,
        [Description("بین 5 تا 10 سال")]
        BetweenFiveAndTenYears,
        [Description("بیش از 10 سال")]
        MoreThanTenYears
    }
}

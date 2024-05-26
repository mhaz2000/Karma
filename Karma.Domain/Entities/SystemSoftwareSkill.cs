namespace Karma.Core.Entities
{
    public class SystemSoftwareSkill
    {
        public SystemSoftwareSkill()
        {

        }

        public SystemSoftwareSkill(string title)
        {
            Title = title;
        }

        public int Id { get; set; }
        public required string Title { get; set; }
    }
}

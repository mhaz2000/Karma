namespace Karma.Core.Entities
{
    public class SystemLanguage
    {
        public SystemLanguage()
        {

        }

        public SystemLanguage(string title)
        {
            Title = title;
        }

        public int Id { get; set; }
        public required string Title { get; set; }
    }
}

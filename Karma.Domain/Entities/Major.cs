namespace Karma.Core.Entities
{
    public class Major
    {
        public Major()
        {
        }

        public Major(string title)
        {
            Title = title;
        }

        public int Id { get; set; }
        public required string Title { get; set; }
    }
}

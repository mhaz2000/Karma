namespace Karma.Core.Entities
{
    public class University
    {
        public University()
        {
        }
        public University(string title)
        {
            Title = title;
        }
        public int Id { get; set; }
        public required string Title { get; set; }
    }
}

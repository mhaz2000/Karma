namespace Karma.Core.Entities
{
    public class University
    {
        public University()
        {
            Id = Guid.NewGuid();
        }
        public University(string title)
        {
            Title = title;
        }
        public Guid Id { get; set; }
        public required string Title { get; set; }
    }
}

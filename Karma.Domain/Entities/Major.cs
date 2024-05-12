namespace Karma.Core.Entities
{
    public class Major
    {
        public Major()
        {
            Id = Guid.NewGuid();
        }

        public Major(string title)
        {
            Id = Guid.NewGuid();
            Title = title;
        }

        public Guid Id { get; set; }
        public required string Title { get; set; }
    }
}

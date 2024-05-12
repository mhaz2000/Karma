namespace Karma.Core.Entities
{
    public class JobCategory
    {
        public JobCategory()
        {
            Id = Guid.NewGuid();
        }

        public JobCategory(string title)
        {
            Id = Guid.NewGuid();
            Title = title;
        }

        public Guid Id { get; set; }
        public required string Title { get; set; }
    }
}

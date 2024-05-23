namespace Karma.Core.Entities
{
    public class JobCategory
    {
        public JobCategory()
        {
        }

        public JobCategory(string title)
        {
            Title = title;
        }

        public int Id { get; set; }
        public required string Title { get; set; }
    }
}

namespace Karma.Core.Entities
{
    public class Country
    {
        public Country()
        {

        }

        public Country(string title)
        {
            Title = title;
        }
        public required string Title { get; set; }
        public int Id { get; set; }
    }
}

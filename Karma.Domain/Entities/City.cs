namespace Karma.Core.Entities
{
    public class City
    {
        public City()
        {

        }
        public City(string title)
        {
            Title = title;
        }
        public required string Title { get; set; }
        public int Id { get; set; }
    }
}

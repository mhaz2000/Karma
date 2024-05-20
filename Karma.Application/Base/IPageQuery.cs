namespace Karma.Application.Base
{
    public interface IPageQuery
    {
        int PageSize { get; set; }
        int PageIndex { get; set; }
        string? OrderBy { get; set; }
    }
}

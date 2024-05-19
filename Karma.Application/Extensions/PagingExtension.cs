using Karma.Application.Base;
using System.Reflection;

namespace Karma.Application.Extensions
{
    public static class PagingExtension
    {
        public static IEnumerable<T> ToPagingAndSorting<T>(this IEnumerable<T> dataModels, IPageQuery objectQuery)
        {
            return objectQuery.PageSize == 0
                ? dataModels.ToSorting(objectQuery)
                : dataModels.ToSorting(objectQuery)
                    .Skip((objectQuery.PageIndex - 1) * objectQuery.PageSize)
                    .Take(objectQuery.PageSize);
        }

        public static IEnumerable<T> ToSorting<T>(this IEnumerable<T> data, IPageQuery objectQuery)
        {
            if (string.IsNullOrEmpty(objectQuery.OrderBy))
                return data;

            var orders = objectQuery.OrderBy.Split(",");
            foreach (var order in orders)
            {
                string direction;
                string field;

                try
                {
                    direction = order.Split(' ')[1];
                    field = order.Split(' ')[0];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new ManagedException("فرمت مرتب سازی صحیح نیست.");
                }

                var propertyInfo = typeof(T).GetProperty(field, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                if (propertyInfo is null)
                    throw new ManagedException("فیلد مرتب سازی موجود نیست.");
                else
                {
                    if (direction.ToLower() == "desc")
                        data = data.OrderByDescending(c => propertyInfo.GetValue(c, null));
                    else
                        data = data.OrderBy(c => propertyInfo.GetValue(c, null));
                }
            }

            return data;
        }

    }

}

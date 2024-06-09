namespace Karma.Infrastructure.Extensions
{
    public static class GenerateRandomAlphanumericCode
    {
        public static string Generate(this IEnumerable<string> values)
        {
            Random random = new Random();

            const string pool = "abcdefghijklmnopqrstuvwxyz0123456789";
            var chars = Enumerable.Range(0, 6)
                .Select(x => pool[random.Next(0, pool.Length)]);

            var result = new string(chars.ToArray());
            if(values.Contains(result))
                return Generate(values);

            return result;
        }
    }
}

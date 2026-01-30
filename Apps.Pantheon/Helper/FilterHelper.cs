namespace Apps.Pantheon.Helper;

public static class FilterHelper
{
    public static IEnumerable<T> FilterByStringEquals<T>(
        this IEnumerable<T> source,
        string? filterValue,
        Func<T, string?> selector)
    {
        if (string.IsNullOrWhiteSpace(filterValue))
            return source;

        return source.Where(item => selector(item)?.Equals(filterValue, StringComparison.OrdinalIgnoreCase) == true);
    }

    public static IEnumerable<T> FilterByStringContains<T>(
        this IEnumerable<T> source,
        string? filterValue,
        Func<T, string?> selector)
    {
        if (string.IsNullOrWhiteSpace(filterValue))
            return source;

        return source.Where(item => selector(item)?.Contains(filterValue, StringComparison.OrdinalIgnoreCase) == true);
    }

    public static IEnumerable<T> FilterByStringArray<T>(
        this IEnumerable<T> source,
        IEnumerable<string>? filterValues,
        Func<T, string?> selector)
    {
        if (filterValues is null || !filterValues.Any())
            return source;

        return source.Where(item => filterValues.Contains(selector(item)));
    }

    public static IEnumerable<T> FilterByDateBefore<T>(
        this IEnumerable<T> source,
        DateTime? before,
        Func<T, DateTime?> selector)
    {
        if (before is null)
            return source;

        return source.Where(item => selector(item) is not null && selector(item) < before);
    }

    public static IEnumerable<T> FilterByDateAfter<T>(
        this IEnumerable<T> source,
        DateTime? after,
        Func<T, DateTime?> selector)
    {
        if (after is null)
            return source;

        return source.Where(item => selector(item) is not null && selector(item) > after);
    }
}

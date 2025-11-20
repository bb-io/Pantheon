namespace Apps.Pantheon.Extensions;

public static class StringExtensions
{
    public static string Capitalize(this string source)
    {
        string lowerCase = source.Trim().ToLower();
        return char.ToUpper(lowerCase[0]) + lowerCase.Substring(1);
    }
}

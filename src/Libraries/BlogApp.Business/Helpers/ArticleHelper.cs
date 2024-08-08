using System.Text.RegularExpressions;

namespace BlogApp.Business.Helpers;
public static partial class ArticleHelper
{
    private static readonly List<char> specialCharacters = [' ', ',', '.', '@', '(', ')', '"'];
    public static int CalculateReadTime(string text)
    {
        List<char> letters = [];
        text = ReadRegex().Replace(text, string.Empty);

        foreach (char item in text)
        {
            if (specialCharacters.Contains(item))
                continue;

            letters.Add(item);
        }

        return letters.Count / 150;
    }

    [GeneratedRegex("<.*?>")]
    private static partial Regex ReadRegex();
}

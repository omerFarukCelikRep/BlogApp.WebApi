using System.Text.RegularExpressions;

namespace BlogApp.Business.Helpers;
public static class ArticleHelper
{
    private static List<char> specialCharacters = new List<char>() { ' ', ',', '.', '@', '(', ')', '"' };
    public static int CalculateReadTime(string text)
    {
        List<char> letters = new List<char>();
        text = Regex.Replace(text, "<.*?>", string.Empty);

        foreach (char item in text)
        {
            if (specialCharacters.Contains(item)) continue;

            letters.Add(item);
        }

        return letters.Count / 150;
    }
}

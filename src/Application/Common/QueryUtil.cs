namespace CleanBooks.Application.Common;

public static class QueryUtil
{
    public static string GetStringBetweenStrings(string input, string stringFrom, string stringTo)
    {
        int pos1 = input.IndexOf(stringFrom, StringComparison.Ordinal) + stringFrom.Length;
        int pos2 = input.IndexOf(stringTo, StringComparison.Ordinal);
        string finalString = input.Substring(pos1, pos2 - pos1);
        return finalString;
    }
    public static string GetStringBetweenCharacters(string input, char charFrom, char charTo)
    {
        int posFrom = input.IndexOf(charFrom);
        if (posFrom != -1) //if found char
        {
            int posTo = input.IndexOf(charTo, posFrom + 1);
            if (posTo != -1) //if found char
            {
                return input.Substring(posFrom + 1, posTo - posFrom - 1);
            }
        }

        return string.Empty;
    }
}
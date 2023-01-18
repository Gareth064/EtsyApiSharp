namespace EtsyApiSharp.Helpers.Extensions;

public static class StringExtension
{
    public static string ListOfLongToCommaSeperatedString(this string resultingString, List<long> listOfInts)
    {
        for (int i = 0; i < listOfInts.Count; i++)
        {
            if (i != listOfInts.Count)
            {
                resultingString += $"{listOfInts[i]},";
            }
            else
            {
                resultingString += listOfInts[i].ToString();
            }
        }

        return resultingString;
    }
}

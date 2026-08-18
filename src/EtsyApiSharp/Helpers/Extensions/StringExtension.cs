namespace EtsyApiSharp.Helpers.Extensions;
/// <summary>
/// Represents String Extension.
/// </summary>

public static class StringExtension
{
    /// <summary>
    /// Executes the List Of Long To Comma Seperated String operation.
    /// </summary>
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

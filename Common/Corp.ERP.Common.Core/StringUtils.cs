using System.Text;

namespace Corp.ERP.Common.Core;

public static class StringUtils
{
    public static char GetRandomChar()
    {
        string chars = "$%#@!*abcdefghijklmnopqrstuvwxyz1234567890?;:ABCDEFGHIJKLMNOPQRSTUVWXYZ^&";
        return GetRandomChar(chars);
    }
    public static char GetRandomChar(string chars)
    {
        Random rand = new Random();
        int num = rand.Next(0, chars.Length);
        return chars[num];
    }

    public static string GetRandomString(int length)
    {
        StringBuilder sb = new StringBuilder();
        for(int i = 0; i < length; i++)
        {
            sb.Append(GetRandomChar());
        }
        return sb.ToString();
    }

    public static string GetRandomString(int length, string chars)
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < length; i++)
        {
            sb.Append(GetRandomChar(chars));
        }
        return sb.ToString();
    }
}

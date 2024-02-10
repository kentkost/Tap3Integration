using System.Text;

namespace Tap0309;

public static class Converters
{
    public static string BCDConverter(string input)
    {
        var res = CleanString(input).ToUpper();
        // Remove padding
        while(res.Last() == 'F')
        {
            res = res[..^1];
        }
        return res;
    }
      

    public static string ASCIIConverter(string input)
    {
        var bytes = ToBytes(input);
        var res = Encoding.ASCII.GetString(bytes);
        return res;
    }

    public static string IntegerConverter(string input)
    {
        var bytes = ToBytes(input);
        return input;
    }

    private static string CleanString(string input)
        => new string(input.ToCharArray().Where(c => !Char.IsWhiteSpace(c)).ToArray());

    private static byte[] ToBytes(string input)
    {
        var cleanedStr = CleanString(input);
        
        if (cleanedStr.Length % 2 != 0)
            cleanedStr += "0";
        
        var res = Convert.FromHexString(cleanedStr);
       
        return res;
    }
}

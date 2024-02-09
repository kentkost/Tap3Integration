
using Tap0309;

namespace TAP0309Tests;

public class ConvertersTests
{
    [Theory]
    [InlineData("44 4E 4B 54 44", "DNKTD")]
    [InlineData("44 4E 4B 54 4", "DNKT@")]
    public void ASCIIConverter(string input, string expected)
    {
        var res = Converters.ASCIIConverter(input);
        Assert.Equal(expected, res);
    }
}
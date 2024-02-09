using System.Runtime.Serialization;

namespace Tap0309;

[Serializable]
internal class MissingConverterException : Exception
{
    private string baseType;
    private string baseXsdType;
    private string rawHexValue;

    public MissingConverterException()
    {
    }

    public MissingConverterException(string message) : base(message)
    {
    }

    public MissingConverterException(string message, Exception innerException) : base(message, innerException)
    {
    }

    public MissingConverterException(string baseType, string baseXsdType, string rawHexValue)
    {
        this.baseType = baseType;
        this.baseXsdType = baseXsdType;
        this.rawHexValue = rawHexValue;
    }

    protected MissingConverterException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
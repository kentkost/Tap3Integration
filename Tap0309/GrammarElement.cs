using System.Numerics;

namespace Tap0309;

public enum GrammarType {SIMPLE, COMPLEX, SEQUENCE, CHOICE}

public class GrammarElement
{
    public GrammarType Type;
    public string Name;
    public string TagInfo;
    public int ClassNumber;
    
    public string TagType; // implicit, explicit.
    public string BaseType;

    public ASN1Choice Choice { get; set; }
    public List<ASN1Element> Sequence = new List<ASN1Element>();

    public GrammarElement()
    {

    }
}

public class ASN1Choice
{
    public UInt64 MinOccurs = 0;
    public UInt64 MaxOccurs = UInt64.MaxValue;

    public List<ASN1Element> Elements; 


    public ASN1Choice()
    {
    }

    public bool GetAvailableChoices() { return true; }
}

public class ASN1Element
{
    public string Name;
    public string Type;

    public ASN1Element()
    {
    }
}

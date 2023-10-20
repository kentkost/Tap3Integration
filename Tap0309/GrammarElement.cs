using System.Numerics;

namespace Tap0309;

public enum GrammarType {SIMPLE, COMPLEX, SEQUENCE, CHOICE, UNDEFINED = 999}

public class GrammarElement
{
    public GrammarType Type;
    public string Name;
    public string TagInfo;
    public int ClassNumber;
    
    public string TagType; // implicit, explicit.
    public string BaseType;

    public ASN1ElementsContainer elements { get;set; }

    public GrammarElement()
    {

    }
}

public class ASN1ElementsContainer
{
    public UInt64 MinOccurs = 0;
    public UInt64 MaxOccurs = 1;

    public List<ASN1Element> Elements;


    public ASN1ElementsContainer()
    {
    }
}

public class ASN1Choice
{
    public UInt64 MinOccurs = 0;
    public UInt64 MaxOccurs = 1;

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

    public string MinOccurs { get; set; }
    public string MaxOccurs { get; set; }
}

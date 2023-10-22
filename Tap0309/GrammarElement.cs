using System.Numerics;
using System.Xml;

namespace Tap0309;

public enum GrammarType {SIMPLE, COMPLEX, SEQUENCE, CHOICE, UNDEFINED = 999}

public class GrammarElement
{
    public GrammarType Type;
    
    public readonly string Name;
    public readonly string TagInfo;
    public readonly int ClassNumber;

    public readonly string TagType; // implicit, explicit. Should be presen on both 
    public readonly string BaseType; // Only present on simpletypes

    public ASN1ElementsContainer elements { get;set; } // only has elements on complextypes

    public GrammarElement()
    {

    }

    public GrammarElement(string name, string className, string classNumber, string tagtype, string baseType)
    {
        Type = GrammarType.SIMPLE;
        
        Name = name;
        TagType = tagtype;
        BaseType = baseType;
    }

    public void SetType(GrammarType t)
    {
        this.Type = t;
    }
}

// I know whether this is a sequence or choice based on GrammarElement.
public class ASN1ElementsContainer
{
    public UInt64 MinOccurs = 0;
    public UInt64 MaxOccurs = 1;

    public List<ASN1Element> Elements;


    public ASN1ElementsContainer()
    {
    }
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

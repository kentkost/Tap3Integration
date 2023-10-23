using System.Diagnostics;

namespace Tap0309;

public enum GrammarType {SIMPLE, COMPLEX, SEQUENCE, CHOICE, UNDEFINED = 999}
public enum ClassType { APPLICATION, UNIVERSAL, PRIVATE, CONTEXT, UNDEFINED = 999 }
public enum TagType { EXPLICIT, IMPLICIT, UNDEFINED = 999 }
public class GrammarElement
{
    private GrammarType type = GrammarType.SIMPLE;
    private readonly string name = "";
    private readonly int classNumber = -1;
    private readonly ClassType classType = ClassType.UNDEFINED;
    private readonly TagType tagType = TagType.UNDEFINED; // implicit, explicit. Should be present on both 
    private readonly string baseType = ""; // Only present on simpletypes

    private ASN1ElementsContainer elementsContainer = null;

    //Getters
    public string Name => name;
    public GrammarType Type { get => type;}
    public int ClassNumber => classNumber;
    public ClassType ClassType => classType;
    public TagType TagType => tagType;
    public string BaseType => baseType;

    public ASN1ElementsContainer ElementsContainer { get => elementsContainer; set => elementsContainer = value; }

    public GrammarElement() { }

    public GrammarElement(string name, string className, string classNumber, string tagType, string baseType)
    {
        this.name = name;
        this.classType = ResolveClassType(className);
        int.TryParse(classNumber, out this.classNumber);
        this.tagType = ResolveTagType(tagType);
        this.baseType = baseType;

        // Initialize container
        if(Debugger.IsAttached)
        {
            LogWarnings();
        }
    }

    public void SetType(string nodeName)
    {
        if (nodeName.Contains("choice", StringComparison.OrdinalIgnoreCase))
        {
            type = GrammarType.CHOICE;
        }
        else if (nodeName.Contains("sequence", StringComparison.OrdinalIgnoreCase))
        {
            type = GrammarType.SEQUENCE;
        }
        else
        {
            Console.WriteLine("Could not resolve grammar type for: " + nodeName);
            type = GrammarType.UNDEFINED;
        }
    }    

    // Don't know how the other classes are resolved yet. So far there is only Application.
    private ClassType ResolveClassType(string className) => className switch
    {
        "APPL" => ClassType.APPLICATION,
        _ => ClassType.UNDEFINED,
    };

    private TagType ResolveTagType(string tagType) => tagType switch
    {
        "IMPLICIT" => TagType.IMPLICIT,
        "EXPLICIT" => TagType.EXPLICIT,
        _ => TagType.UNDEFINED,
    };

    private void LogWarnings()
    {
    }
}

// I know whether this is a sequence or choice based on GrammarElement.
public class ASN1ElementsContainer
{
    private UInt64 minOccurs = 0;
    private UInt64 maxOccurs = 1;
    private GrammarType type = GrammarType.UNDEFINED;
    private List<ASN1Element> elements = new List<ASN1Element>();

    public ASN1ElementsContainer(GrammarType type, UInt64 minOccurs, UInt64 maxOccurs)
    {
        this.type = type;
        this.minOccurs = minOccurs;
        this.maxOccurs = maxOccurs;
    }

    public ulong MinOccurs { get => minOccurs;}
    public ulong MaxOccurs { get => maxOccurs;}
    public List<ASN1Element> Elements { get => elements; set => elements = value; }
}

public class ASN1Element
{
    private string name = "";
    private string type = "";
    private UInt64 minOccurs = 0;
    private UInt64 maxOccurs = 1;

    public ASN1Element()
    {
    }

    public ASN1Element(string name, string type)
    {
        this.name = name;
        this.type = type;
    }

    public string Name { get => name;}
    public string Type { get => type;}
    public ulong MinOccurs { get => minOccurs;}
    public ulong MaxOccurs { get => maxOccurs;}

    public void SetSizeConstraints(string minOccurs, string maxOccurs)
    {
        //Set min and max occurence based on these strings.
        // If empty min string then min occurence is 1 and max occurence is 1 
        // if max occurs is empty string then max occurence is 1.
        // if max occurs is "unbounded" then max occurence is int32.maxValue


        throw new NotImplementedException();
    }
}

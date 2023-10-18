using System.Xml;
using System.Xml.Linq;

namespace Tap0309;

public class AnyReader
{
    private static string inFile = @"E:/repos/Tap3Integration/SampleData/tdcsample.xer";
    private static string grammerFile = @"E:/repos/Tap3Integration/SampleData/some.xsd";
    private static OldNode root;
    public readonly string version = "3.09";
    private List<GrammarElement> grammarElements= new List<GrammarElement>();

    public AnyReader()
    {

    }

    public void ReadStructure()
    {
        // Read the structure into nodes.
        //FileStream fs = new FileStream(inFile, FileMode.Open, FileAccess.Read);
        //var content = XElement.Load(fs);
        //TraverseElement(content);
        
        CreateLookUpNodes();

        // The structure is read into an XElment.
        // In theory we can now always check based on the XElement what kind of type it is by looking it up in XSD.
        // I am wondering if there is a better way to populate the tree.
        //PopulateNodes(content);
    }

    private void PopulateNodes(XElement content)
    {
        throw new NotImplementedException();
    }

    private void TraverseElement(XElement element)
    {
        if (element == null)
            return;

        // Process the current element
        Console.WriteLine("Element Name: " + element.Name);
        if (!element.HasElements)
        {
            Console.WriteLine("Leaf of sort value: " + element.Value.ToString());
        }

        // Recursively traverse all child elements
        foreach (XElement childElement in element.Elements())
        {
            TraverseElement(childElement);
        }
    }

    private void CreateLookUpNodes()
    {
        //look up in XSD. to see what kind of element it is
        string filePath = "E:/repos/Tap3Integration/tmp/TAP-0309(all options).xsd";

        //read this. I am also sure I have a separate project for this where I've done the same. But meh
        // https://github.com/kentkost/tap3teststemp/blob/master/testing/testing/AsnXmlGrammarReader.cs This is the project
        // Skip element.

        // Get all Complextypes and simpletypes first.
        XmlDocument document = new XmlDocument();
        var mngr = new XmlNamespaceManager(document.NameTable);
        mngr.AddNamespace("xsd", @"http://www.w3.org/2001/XMLSchema");
        mngr.AddNamespace("asn1", @"http://www.obj-sys.com/v1.0/XMLSchema");
        document.Load(filePath);

        if (document.DocumentElement == null) return;

        var simpleTypes = document.DocumentElement.SelectNodes(".//xsd:simpleType[@name='RadioChannelUsed']", mngr);
        var complexTypes= document.DocumentElement.SelectNodes(".//xsd:complexType", mngr);
        
        //Handle each type separately.
        if (simpleTypes != null)
        {
            foreach (XmlNode n in simpleTypes)
            {
                ReadSimpleType(n,mngr);
            }
        }
    }

    private GrammarElement ReadSimpleType(XmlNode node, XmlNamespaceManager mngr)
    {
        var e = new GrammarElement();
        if (node == null) return e;
        
        var typeName = GetAttributeValue(node, "name");
        var tagInfo = node.SelectSingleNode(".//asn1:taginfo",mngr);
        
        var className   = GetAttributeValue(tagInfo, "class");
        var classNumber = GetAttributeValue(tagInfo, "classnumber");
        var tagtype     = GetAttributeValue(tagInfo, "tagtype");

        var restriction = node.SelectSingleNode(".//xsd:restriction", mngr);
        var baseType = GetAttributeValue(restriction, "base");
        return e;
    }

    private GrammarElement ReadComplexType(XmlNode node, XmlNamespaceManager mngr)
    {
        var e = ReadSimpleType(node, mngr);
        
        //Find choices

        //Find Sequences
        
        return e;
    }

    private string GetAttributeValue(XmlNode? node, string attribute)
    {
        if (node == null) return string.Empty;
        if (node.Attributes == null) return string.Empty;

#pragma warning disable CS8602 // Dereference of a possibly null reference.
        return node.Attributes[attribute] != null 
            ? node.Attributes[attribute].Value 
            : string.Empty;
#pragma warning restore CS8602 // Dereference of a possibly null reference.
    }

    private bool ValidateXmlToSchema(string xmlContent)
    {
        return true;
    }
}

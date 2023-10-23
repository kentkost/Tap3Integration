using System.Xml;
using System.Xml.Linq;

namespace Tap0309;

public class AnyReader
{
    private static string inFile = @"E:/repos/Tap3Integration/SampleData/tdcsample.xer";
    private static string grammerFile = @"E:/repos/Tap3Integration/SampleData/some.xsd";
    private static OldNode root;
    public readonly string version = "3.09";
    //private List<GrammarElement> grammarElements= new List<GrammarElement>();
    private Dictionary<string, GrammarElement> grammarElements = new Dictionary<string, GrammarElement>();

    private XmlNamespaceManager mngr;

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
        mngr = new XmlNamespaceManager(document.NameTable);
        mngr.AddNamespace("xsd", @"http://www.w3.org/2001/XMLSchema");
        mngr.AddNamespace("asn1", @"http://www.obj-sys.com/v1.0/XMLSchema");
        document.Load(filePath);

        if (document.DocumentElement == null) return;

        var simpleTypes = document.DocumentElement.SelectNodes(".//xsd:simpleType[@name='RadioChannelUsed']", mngr);
        var complexTypes= document.DocumentElement.SelectNodes(".//xsd:complexType[@name='TotalChargeValueList']", mngr);

        //Handle each type separately.
        if (simpleTypes != null)
        {
            foreach (XmlNode n in simpleTypes)
            {
                ReadSimpleType(n);
            }
        }

        if (complexTypes != null)
        {
            foreach(XmlNode n in complexTypes)
            {
                //get name first 
                var e = ReadComplexType(n);
                
                if (string.IsNullOrWhiteSpace(e.Name)) 
                    continue;

                grammarElements.Add(e.Name,e);
            }
        }
    }

    private GrammarElement ReadSimpleType(XmlNode node)
    {
        if (node == null) 
            return new GrammarElement();
        
        var typeName = GetAttributeValue(node, "name");
        var tagInfo = node.SelectSingleNode(".//asn1:taginfo",mngr);
        
        var className   = GetAttributeValue(tagInfo, "class");
        var classNumber = GetAttributeValue(tagInfo, "classnumber");
        var tagtype     = GetAttributeValue(tagInfo, "tagtype");

        var restriction = node.SelectSingleNode(".//xsd:restriction", mngr); // No restriction on complex types and therefore no basetype
        var baseType = GetAttributeValue(restriction, "base");

        var e = new GrammarElement(typeName, className, classNumber, tagtype, baseType);

        return e;
    }

    private GrammarElement ReadComplexType(XmlNode node)
    {
        var e = ReadSimpleType(node);

        //Find choices
        var elementsParentNode = node.SelectSingleNode(".//xsd:choice | .//xsd:sequence", mngr);

        if (elementsParentNode == null)
        {
            Console.WriteLine("Could not resolve complex type for: " + node.InnerXml);
        }
        else
        {
            e.SetType(elementsParentNode.Name);
        } 

        if (e.Type == GrammarType.UNDEFINED)
            Console.WriteLine("Could not resolve complex type for: " + node.InnerXml);

        e.ElementsContainer = ReadInnerElements(elementsParentNode);
        e.ElementsContainer.Type = e.Type;

        return e;
    }

    private ASN1ElementsContainer ReadInnerElements(XmlNode node)
    {
        if (node == null)
            return new ASN1ElementsContainer();
        
        var sizeContraints = GetSizeContraints(node);
        var container = new ASN1ElementsContainer(sizeContraints.minOccurs, sizeContraints.maxOccurs);

        var elementNodes = node.SelectNodes(".//xsd:element", mngr);

        foreach(XmlNode element in elementNodes)
        {
            container.Elements.Add(ReadElement(element));
        }

        return container;
    }

    private ASN1Element ReadElement(XmlNode node)
    {
        if (node == null) 
            return new ASN1Element();

        string name = GetAttributeValue(node, "name");
        string type = GetAttributeValue(node, "type");
        var sizeContraints = GetSizeContraints(node);
        var e = new ASN1Element(name, type, sizeContraints.minOccurs, sizeContraints.maxOccurs);
        return e;
    }

    private string GetAttributeValue(XmlNode node, string attribute)
    {
        if (node == null) return string.Empty;
        if (node.Attributes == null) return string.Empty;

        return node.Attributes[attribute] != null 
            ? node.Attributes[attribute].Value 
            : string.Empty;
    }

    private (UInt64 minOccurs, UInt64 maxOccurs) GetSizeContraints(XmlNode node)
    {
        //Default is optional and max one occurs
        UInt64 minValue = 1;
        UInt64 maxValue = 1;

        if (node == null)
            return (minValue, maxValue);

        string min = GetAttributeValue(node, "minOccurs");
        string max = GetAttributeValue(node, "maxOccurs");

        if (!string.IsNullOrWhiteSpace(min))
            UInt64.TryParse(min, out minValue);

        if (!string.IsNullOrWhiteSpace(max))
            if (string.Equals(max, "unbounded", StringComparison.InvariantCultureIgnoreCase))
                maxValue = UInt64.MaxValue;
            else
                UInt64.TryParse(min, out maxValue);

        return (minValue, maxValue);
    }

    /// <summary>
    /// Used to validate that the XML generated by the user/program is still compliant with XSD.
    /// </summary>
    /// <param name="xmlContent">The XML content that is being validated</param>
    /// <param name="version">The tap3 version it is being validated against</param>
    /// <returns>True if the generated xml is compliant with xsd.</returns>
    private bool ValidateXmlToSchema(string xmlContent, string version)
    {
        return true;
    }
}

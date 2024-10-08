﻿using System.Runtime.CompilerServices;
using System.Xml;
using System.Xml.Linq;

namespace Tap0309;

public class AnyReader
{
    private static readonly string inFile = @"E:/repos/Tap3Integration/SampleData/tdcsample.xer";
    //private static string grammerFile = @"E:/repos/Tap3Integration/SampleData/some.xsd";
    string grammarFile = "E:/repos/Tap3Integration/tmp/TAP-0309(all options).xsd";
    private Node root;
    public readonly string version = "3.09";

    // Used for checking which elements are possible under each element in an xml structure
    // Also used for finding base element of a GrammarElement.
    private Dictionary<string, GrammarElement> grammarStructure = new Dictionary<string, GrammarElement>();
    // Used for checking which type an element in an xml structure is
    private Dictionary<string, ASN1Element> grammarElements = new Dictionary<string, ASN1Element>();
    private XmlDocument dataDocument;
    private string dataString;

    private XmlNamespaceManager mngr;

    // These are the different ways to read the data into a tree structure
    public XmlDocument DataDocument { get => dataDocument; set => dataDocument = value; }
    public string DataString { get => dataString; set => dataString = value; }
    public Node Root { get => root; set => root = value; }

    public AnyReader()
    {

    }

    public void PrepareStructures()
    {
        // Read XML file created by Tap3Magic
        dataString = File.ReadAllText(inFile);
        dataDocument = new XmlDocument();
        dataDocument.Load(inFile);
        // Read the grammar file to create a grammar rule set to know which children a node kan have
        ReadGrammarAndCreateStructure();
        // Read the actual data and read into Node/BaseNode structure such that it can be displayed in a treeview
        ReadDataAndCreateStructure();
    }

    private void ReadDataAndCreateStructure()
    {
        FileStream fs = new FileStream(inFile, FileMode.Open, FileAccess.Read);
        var content = XElement.Load(fs);

        var node = new Node();

        node.Value = content.HasElements ? "" : content.Value;
        node.Path = content.Name.ToString();
        var lookUpElement = grammarElements[content.Name.ToString()];
        node.TypeName = lookUpElement.Type;
        node.BaseXsdType = FindBaseType(node.TypeName);
        node.TranslatedValue = "reeee";
        
        root = node;

        TraverseElement(root, content, 0);
    }

    private string FindBaseType(string typeName)
    {
        // Find basetype from grammar structure
        return "FindBaseType(): not implemented";
    }


    private void TraverseElement(Node parent, XElement element, int childNumber)
    {
        //parent.Children.Add(ReadElement(parent, element, childNumber));

        if (element.Elements().Count() == 0)
        {
            //resolve parents type here?
            parent.IsLeaf = true;
            ResolveType(ref parent);
            parent.StringRepresentationValue = ConvertValue(ref parent);
        }

        // Recursively traverse all child elements
        int cn = 1;
        foreach (XElement childElement in element.Elements())
        {
            var child = ReadElement(parent, childElement, childNumber);
            parent.Children.Add(child);
            TraverseElement(child, childElement, cn);
            cn++;
        }
    }

    // This can be done better
    private void ResolveType(ref Node node)
    {
        GrammarElement el = grammarStructure[node.TypeName];
        while(!el.BaseType.StartsWith("xsd:"))
        {
            el = grammarStructure[el.BaseType];       
        }
        node.BaseXsdType = el.BaseType;
        node.BaseType = el.Name;
    }

    // This can also be done better
    //  * BCDString xsd:hexbinary and simpletype = "BCDString"
    //  * ASCIIString, Numberstring, etc.xsd:hexbinary
    //  * Integer xsd:integer
    private string ConvertValue(ref Node node)
    {
        if (node.BaseType.ToLower().Equals("bcdstring"))
        {
            return Converters.BCDConverter(node.RawValue);
        }
        else if (node.BaseXsdType.ToLower().Equals("xsd:hexbinary"))
        {
            return Converters.ASCIIConverter(node.RawValue);
        }
        else if (node.BaseXsdType.ToLower().Equals("xsd:integer"))
        {
            return node.RawValue;
        }
        throw new MissingConverterException(node.BaseType, node.BaseXsdType, node.RawValue);
    }

    private Node ReadElement(Node parent, XElement element, int childNumber)
    {
        if (element == null) 
            return null;

        var node = new Node();

        node.Value = element.HasElements ? "" : element.Value;
        node.Parent = parent;
        node.Path = parent.Path+"/"+element.Name.ToString()+$"[{childNumber}]";
        var lookUpElement = grammarElements[element.Name.ToString()];
        node.TypeName = lookUpElement.Type;

        return node;
    }

    private void ReadGrammarAndCreateStructure()
    {
        // Get all Complextypes and simpletypes first.
        var document = new XmlDocument();
        mngr = new XmlNamespaceManager(document.NameTable);

        mngr.AddNamespace("xsd", @"http://www.w3.org/2001/XMLSchema");
        mngr.AddNamespace("asn1", @"http://www.obj-sys.com/v1.0/XMLSchema");
        document.Load(grammarFile);

        if (document.DocumentElement == null) 
            return;

        var simple = "xsd:simpleType";
        var complex = "xsd:complexType";
        var element = "xsd:element";

        var grammarNodes = document.DocumentElement.SelectNodes($".//{simple} | .//{complex}", mngr);
        var elementNodes = document.DocumentElement.SelectNodes($".//{element}", mngr);

        //Handle each type separately.
        if (grammarNodes == null) 
            goto checkElements;
        
        foreach (XmlNode n in grammarNodes)
        {
            var grammarElement = new GrammarElement();
            if (n.Name == simple)
                grammarElement = ReadSimpleType(n);
            else if (n.Name == complex)
                grammarElement = ReadComplexType(n);
            else
                continue;

            if (string.IsNullOrWhiteSpace(grammarElement.Name))
                continue;

            grammarStructure.Add(grammarElement.Name, grammarElement);
        }

        // Split into two methods
        checkElements:

        if(elementNodes == null) return;

        foreach (XmlNode n in elementNodes)
        {
            var e = ReadElement(n);

            if (string.IsNullOrWhiteSpace(e.Name))
                continue;
            if (grammarElements.ContainsKey(e.Name))
                continue;
            grammarElements.Add(e.Name, e);
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
        var complexContent = node.SelectSingleNode(".//xsd:complexContent//xsd:extension", mngr);

        if (complexContent != null)
        {
            var baseType = GetAttributeValue(complexContent, "base");
            e.BaseType = baseType;
            e.SetType(GrammarType.COMPLEX);
        }

        if (elementsParentNode != null)
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

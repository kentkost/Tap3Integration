﻿using System.Xml;
using System.Xml.Linq;

namespace Tap0309;

public class AnyReader
{
    private static string inFile = @"E:/repos/Tap3Integration/SampleData/tdcsample.xer";
    private static string grammerFile = @"E:/repos/Tap3Integration/SampleData/some.xsd";
    private static OldNode root;
    public readonly string version = "3.09";
    private List<GrammarElement> grammarElements= new List<GrammarElement>();

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
        List<GrammarElement> elements = new List<GrammarElement>();

        //Handle each type separately.
        //if (simpleTypes != null)
        //{
        //    foreach (XmlNode n in simpleTypes)
        //    {
        //        ReadSimpleType(n);
        //    }
        //}

        if(complexTypes != null)
        {
            foreach(XmlNode n in complexTypes)
            {
                var e = ReadComplexType(n);
                
                if (string.IsNullOrWhiteSpace(e.Name)) 
                    continue;

                elements.Add(e);
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
            //Add setter to GrammarElement for innerElements
            var sizeContraint = GetSizeContraints(elementsParentNode);
            e.ElementsContainer = new ASN1ElementsContainer(e.Type, sizeContraint.minOccurs, sizeContraint.maxOccurs);
            var elements = ReadInnerElements(elementsParentNode);
            e.ElementsContainer.Elements = elements;
        } 

        if (e.Type == GrammarType.UNDEFINED)
        {
            Console.WriteLine("Could not resolve complex type for: " + node.InnerXml);
            return e;
        }

        return e;
    }

    private List<ASN1Element> ReadInnerElements(XmlNode node)
    {
        var elements = new List<ASN1Element>();

        var minOccurs = GetAttributeValue(node, "minOccurs");
        var maxOccurs= GetAttributeValue(node, "maxOccurs");

        var elementNodes = node.SelectNodes(".//xsd:element");

        foreach(XmlNode element in elementNodes)
        {
            elements.Add(ReadElement(element));
        }

        return elements;
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

    private ASN1Element ReadElement(XmlNode node)
    {
        if (node == null) 
            return new ASN1Element();

        string name = GetAttributeValue(node, "name");
        string type = GetAttributeValue(node, "type");
        string minOccurs = GetAttributeValue(node, "minOccurs");
        string maxOccurs = GetAttributeValue(node, "maxOccurs");
        var e = new ASN1Element(name, type);
        e.SetSizeConstraints(minOccurs, maxOccurs);

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

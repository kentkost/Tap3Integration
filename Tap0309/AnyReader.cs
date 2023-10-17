using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Tap0309;

public class AnyReader
{
    private static string inFile = @"E:/repos/Tap3Integration/SampleData/tdcsample.xer";
    private static string grammerFile = @"E:/repos/Tap3Integration/SampleData/some.xsd";
    private static OldNode root;
    public readonly string version = "3.09";

    public AnyReader()
    {

    }

    public void ReadStructure()
    {
        // Read the structure into nodes.
        FileStream fs = new FileStream(inFile, FileMode.Open, FileAccess.Read);
        var content = XElement.Load(fs);
        TraverseElement(content);
        // The structure is read into an XElment.
        // In theory we can now always check based on the XElement what kind of type it is by looking it up in XSD.
        // I am wondering if there is a better way to populate the tree.
        PopulateNodes(content);
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

    private void LookUpNode(string elementName)
    {
        //look up in XSD. to see what kind of element it is
        string filePath = "E:/repos/Tap3Integration/tmp/TAP - 0309(all options).xsd";

        //read this. I am also sure I have a separate project for this where I've done the same. But meh
        // https://github.com/kentkost/tap3teststemp/blob/master/testing/testing/AsnXmlGrammarReader.cs This is the project
    }
}

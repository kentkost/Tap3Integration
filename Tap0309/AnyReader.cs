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
    private static Node root;
    public readonly string version = "3.09";

    public AnyReader()
    {

    }

    public void ReadStructure()
    {
        // Read the structure into nodes.
        FileStream fs = new FileStream(inFile, FileMode.Open, FileAccess.Read);
        var content = XElement.Load(fs);

        // The structure is read into an XElment.
        // In theory we can now always check based on the XElement what kind of type it is by looking it up in XSD.
        // I am wondering if there is a better way to populate the tree.
        PopulateNodes(content);
    }

    private void PopulateNodes(XElement content)
    {
        //Depth or breadth tracersing XElement to populate it based on information in XSD
        throw new NotImplementedException();
    }
}

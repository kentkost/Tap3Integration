using System.Text;
using Tap3Integration;
using static Tap3Integration.Tap3MagicWrapper;
using System.IO;
using System.Runtime.InteropServices;
using System.Xml;
using System.Xml.Serialization;

namespace Tap3Integration;

class Program
{
    static string inFile = @"E:/repos/tap3magic/build/debug-readers/results.xer";

    static void Main(string[] args)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(DataInterChangeType));
        FileStream fileStream = new FileStream(inFile,FileMode.Open,FileAccess.Read);

        var kek = serializer.Deserialize(fileStream);

    }
}
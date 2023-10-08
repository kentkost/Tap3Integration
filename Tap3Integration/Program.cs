using System.Text;
using Tap3Integration;
using static Tap3Integration.Tap3MagicWrapper;
using System.IO;
using System.Runtime.InteropServices;
using System.Xml;
using System.Xml.Serialization;
using Tap0309;

namespace Tap3Integration;

class Program
{
    static string inFile = @"E:/repos/tap3magic/build/debug-readers/results.xer";

    static void Main(string[] args)
    {
        //Tap0309Reader reader = new Tap0309Reader();
        AnyReader reader = new AnyReader();
        reader.ReadStructure();
    }
}
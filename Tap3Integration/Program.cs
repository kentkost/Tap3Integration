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
        //AnyReader reader = new AnyReader();
        //reader.PrepareStructures();

        BaseReader reader = new BaseReader();
        reader.ReadStructure();
    }


    public class Test
    {
        private string text;
        private Test child = null;

        public string Text { get => text; set => text = value; }
        public Test Child { get => child; set => child = value; }

        public Test()
        {
        }
    }

    private static void SomeReflectionTest()
    {
        Test test = new Test();
        test.Text = "reee";
        test.Child = new Test() { Text = "Skree" };
        List<Test> list1 = new List<Test>();
        list1.Add(test);
        List<Test> list2 = new List<Test>();
        list2.Add(test);

        var properties = test.GetType().GetProperties();
        foreach (var property in properties)
        {
            if (property.Name == "Text") { continue; }
            var child = property.GetValue(test) as Test;
            if (child != null)
            {
                list2.Add(child);
            }
        }


        test.Text = "askas";
        test.Child.Text = "Chiiiildreee";
    }
}
using System.Text;
using System.Xml.Serialization;

namespace TestXSD;

class Program
{
    static string inFile = @"E:/repos/tap3magic/build/debug-readers/results.xer";
    static string xml = @"<persons><father><firstname>Kent</firstname><lastname>kost</lastname></father><mother><firstname>lulu</firstname><lastname>tonk</lastname></mother> </persons>";
    static string someFile = @"E:\repos\Tap3Integration\Tap0309\DataInterChangeTypeRaw.cs";
    static string newFile = @"E:\repos\Tap3Integration\Tap0309\DataInterChangeTypeTest.cs";
    static void Main(string[] args)
    {

        var lines = File.ReadAllLines(someFile);
        List<string> newLines = new List<string>();
        foreach(var line in lines)
        {
            if(line.Contains("public partial class") && !line.Contains(":"))
            {
                newLines.Add(line+ ": Node");
            }
            else
            {
                newLines.Add(line);
            }
        }
        File.WriteAllLines(newFile,newLines);




        //XmlSerializer serializer = new XmlSerializer(typeof(persons));
        //var content = Encoding.ASCII.GetBytes(xml);
        //MemoryStream xmlContent = new MemoryStream(content);
        
        //var kek = serializer.Deserialize(xmlContent);
        
        //if (kek is persons)
        //{
        //    var ps= (persons)kek;
        //    var re = ps.GetAllProperties();
        //}
        //if(kek is Node)
        //{
        //    Console.WriteLine("eaeada");
        //}

        Console.WriteLine("Ending now");
    }
}
using System.Xml.Serialization;
using System.IO;

namespace Tap0309
{
    public class Tap0309Reader
    {
        static string inFile = @"E:/repos/Tap3Integration/SampleData/tdcsample.xer";

        public Tap0309Reader()
        {

            XmlSerializer serializer = new XmlSerializer(typeof(DataInterChangeType));
            FileStream fileStream = new FileStream(inFile, FileMode.Open, FileAccess.Read);

            var kek = serializer.Deserialize(fileStream);
            if(kek is DataInterChangeType)
            {
                var datainterchange = (DataInterChangeType)kek;
                var props = datainterchange.GetAllProperties();
                if(datainterchange.Item is TransferBatch)
                {
                    var transferBatch = (TransferBatch)datainterchange.Item;
                    var props2 = transferBatch.GetAllProperties();
                    var val = transferBatch.GetProperty();
                    if(val is BaseNode)
                    {
                        var newVal = (BaseNode)val;
                        var s = newVal.GetProperty();
                    }
                }
            }
        }

        public string HelloWorld()
        {
            Console.WriteLine("reeeee");
            return "This is a hello world";
        }
    }
}

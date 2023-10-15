using System.Xml.Serialization;

namespace Tap0309;

public class OldAnyReader
{
    private static string inFile = @"E:/repos/Tap3Integration/SampleData/tdcsample.xer";

    public Node root = null;


    public OldAnyReader()
    {
    }

    public void ReadStructure()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(DataInterChangeType));
        FileStream fileStream = new FileStream(inFile, FileMode.Open, FileAccess.Read);
        
        var first = serializer.Deserialize(fileStream);

        if(first is Node)
        {
            root = (Node)first;
        }

        if (root is DataInterChangeType)
        {
            var datainterchange = (DataInterChangeType)root;
            var props = datainterchange.GetAllProperties();
            if (datainterchange.Item is TransferBatch)
            {
                var transferBatch = (TransferBatch)datainterchange.Item;
                var batchControlInfo = transferBatch.batchControlInfo;
                var props2 = batchControlInfo.GetAllProperties();
                var val = batchControlInfo.GetProperty();
                batchControlInfo.InitializeNodes();
                //if (val is Node)
                //{
                //    var newVal = (Node)val;
                //    var s = newVal.GetProperty();
                //}
            }
        }
    }

    public void ReadNode()
    {

    }
}


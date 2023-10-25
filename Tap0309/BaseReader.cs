using System.Xml.Serialization;

namespace Tap0309;

public class BaseReader
{
    private static string inFile = @"E:/repos/Tap3Integration/SampleData/tdcsample.xer";

    public BaseNode root = null;
    public AnyReader grammar = null;


    public BaseReader()
    {
        // I might need the grammar. But really the grammar should be elsewhere
        // I'll try not to use the grammar.
        grammar = new AnyReader();
        grammar.PrepareStructures();
    }

    public void ReadStructure()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(DataInterChangeType));
        FileStream fileStream = new FileStream(inFile, FileMode.Open, FileAccess.Read);
        
        var first = serializer.Deserialize(fileStream);

        if(first is BaseNode)
        {
            root = (BaseNode)first;
            var children = root.GetChildren();

        }

        //if (root is DataInterChangeType)
        //{
        //    var datainterchange = (DataInterChangeType)root;
        //    var props = datainterchange.GetAllProperties();
        //    if (datainterchange.Item is TransferBatch)
        //    {
        //        var transferBatch = (TransferBatch)datainterchange.Item;
        //        var batchControlInfo = transferBatch.batchControlInfo;
        //        var props2 = batchControlInfo.GetAllProperties();
        //        var val = batchControlInfo.GetProperty();
        //        batchControlInfo.InitializeNodes();
        //        //if (val is Node)
        //        //{
        //        //    var newVal = (Node)val;
        //        //    var s = newVal.GetProperty();
        //        //}
        //    }
        //}
    }

    public void PopulateMissingProperties()
    {

    }

    public void ReadNode()
    {

    }
}


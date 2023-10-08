using System.Reflection.Metadata.Ecma335;

namespace Tap0309;

public abstract class Node
{
    //public Node Parent { get => parent; set => parent = value; }
    //public Dictionary<string, Node> Children { get => children; set => children = value; }
    //public Dictionary<string, string> Fields { get => fields; set => fields = value; }

    //private Dictionary<string,Node> children = new Dictionary<string, Node>();
    //private Dictionary<string, string> fields = new Dictionary<string, string>();
    //private Node parent = null;

    public Node()
    {
    }

    public List<string> GetAllProperties() 
    {
        var props = this.GetType().GetProperties();
        return props.ToList().Select(x=> x.Name).ToList();
    }

    public object GetProperty()
    {
        var props = this.GetType().GetProperties();
        string propName = "no prop";
        object res = 213;
        foreach(var prop in props)
        {
            if (!prop.CanRead || !prop.CanWrite) 
                continue;
            propName = prop.Name;
            res = prop.GetValue(this, null);
        }

        return res;
    }

    public void SetProperty(string name, object value)
    {
        var props = this.GetType().GetProperties();
        var prop = props.Where(x => x.Name == name);
        //should not be allowed to do this if the property is of type Node. I guess.
        this.SetProperty(name, value);
    }

    public void InitializeNodes()
    {

    }

    public void InitializeFields()
    {

    }

    public void SetField()
    {

    }
}
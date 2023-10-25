using System.ComponentModel.Design;
using System.Reflection.Metadata.Ecma335;
using System.Text.RegularExpressions;

namespace Tap0309;

public abstract class BaseNode
{
    //public BaseNode Parent { get => parent; set => parent = value; }
    //public List<BaseNode> Children { get => children; set => children = value; }
    //public List<string> Fields { get => fields; set => fields = value; }
    //public string Name { get => name; set => name = value; }

    //private string name;
    //private List<BaseNode> children = new List<BaseNode>();
    //private List<string> fields = new List<string>();
    //private BaseNode parent = null;

    public BaseNode()
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

    string serilizableAtt = "System.Xml.Serialization.XmlElementAttribute";
    public List<BaseNode> GetChildren()
    {
        List<BaseNode> children = new List<BaseNode>();

        var currentNodeType = this.GetType().Name;
        var properties = this.GetType().GetProperties();
        foreach(var property in properties)
        {
            var t = property.CustomAttributes.FirstOrDefault().AttributeType.FullName;
            if(t == serilizableAtt)
            {
                Console.WriteLine("Is child");
                var child = property.GetValue(this, null) as BaseNode;
                if (child != null)
                {
                    children.Add(child);
                }
            }
        }

        children.First().GetChildren();
        return children;
    }

    // This one is probably not possible to implement. But it shouldn't be necessary either.
    public BaseNode GetParent()
    {
        return null;
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
        Console.WriteLine("initializing properties of: ");

        var props = this.GetType().GetProperties();
        string propName = "no prop";
        object res = 213;

        var nodeName = typeof(BaseNode).Name;
        var byteArrayName = typeof(byte[]).Name;
        var stringName = typeof(string).Name;
        var dateTimeLongName = typeof(DateTimeLong).Name;

        foreach (var prop in props)
        {
            var propType = prop.PropertyType?.Name;
            var propBaseType = prop.PropertyType?.BaseType?.Name;
            var type = GetAbsoluteBaseType(prop.PropertyType);

            if (propBaseType == nodeName)
            {
                Console.WriteLine("Handling node: " + prop.Name);
            }
            else if (propType == nodeName)
            {
                Console.WriteLine("Handling node: "+prop.Name);
            }
            else if(byteArrayName == propType)
            {
                Console.WriteLine("Handling byteArray: " + prop.Name);
            }
            else if(stringName == propType)
            {
                Console.WriteLine("Handling string: " + prop.Name);
            }
            else if (dateTimeLongName == propBaseType)
            {
                Console.WriteLine("Handling date: " + prop.Name);
            }
            else
            {
                Console.WriteLine("Unhandled property type");
            }
            //if (!prop.CanRead || !prop.CanWrite)
            //    continue;
            //propName = prop.Name;
            //res = prop.GetValue(this, null);
        }
    }

    private static string GetAbsoluteBaseType(Type? type)
    {
        string basetype = "already based";
        if (type == null)
        {
            return basetype;
        }

        if (!type.FullName.StartsWith("Tap"))
        {
            return basetype;
        }
        
        var options = RegexOptions.CultureInvariant | RegexOptions.Compiled;
        Regex regexNode = new Regex("^Tap[0-9]{4}\\.Node", options);
        // Keep getting based type until Node or base

        var res = type.Name;
        var nextType = type;

        while (!regexNode.Match(nextType.FullName).Success)
        {
            nextType = nextType.BaseType;
            res = nextType.FullName;
        }

        return res;
    }

    public void InitializeFields()
    {

    }

    public void SetField()
    {

    }
}
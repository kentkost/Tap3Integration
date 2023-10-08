using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestXSD;

public abstract class Node
{
    public string Name => "yeet";
    // method for getting all the properties

    public List<string> GetAllProperties() 
    {
        var props = this.GetType().GetProperties();
        return props.ToList().Select(x=> x.Name).ToList();
    }
    
    // Method for seting a specific property


}
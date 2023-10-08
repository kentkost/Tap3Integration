using System.Xml.Serialization;

namespace Tap3Integration
{
    /// <summary>
    /// How the fuck would this even work. There is a different serializer for each?
    /// 
    /// </summary>
    public abstract class BaseTap3Reader
    {
        BaseTap3Reader(XmlSerializer serializer) 
        {
            // Use serializer.
            // When encountering a certain element then handle it according.
            // How do we override each element? Create virtual method for each element aand then override when necessary?
                // I guess that makes the most sense. There are a lot of elements that are handled the same way.
                // For this either have a base method that each element can call.
        }
    }
}

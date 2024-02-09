using System.Collections.ObjectModel;

namespace Tap0309;

public class Node 
{
    private Node parent = null;
    private string path = "";
    private ObservableCollection<Node> children = new ObservableCollection<Node>();
    private string value;
    private string typeName;
    private string translatedValue;
    private string convertedValue;

    public Node Parent { get => parent; set => parent = value; }
    public string Path { get => path; set => path = value; }
    public ObservableCollection<Node> Children { get => children; set => children = value; }
    public string Value { get => value; set => this.value = value; }
    public string TypeName { get => typeName; set => typeName = value; }
    public string DiplayValue { get => typeName + " " + Value; }
    public string RawHexValue { get => Value; }
    public string TranslatedValue { get => translatedValue; set => translatedValue = value; }
    public string BaseXsdType { get;  set; }
    public string BaseType { get;  set; }
    public bool IsLeaf { get; set; } = false;
    public string ConvertedValue { get => convertedValue; set => convertedValue = value; }

    public Node()
    {

    }

    // Future: Get possible children
}

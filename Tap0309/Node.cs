using System.Collections.ObjectModel;

namespace Tap0309;

public class Node 
{
    private Node parent = null;
    private string path = "";
    private ObservableCollection<Node> children = new ObservableCollection<Node>();
    private string value;
    private string typeName;


    public Node Parent { get => parent; set => parent = value; }
    public string Path { get => path; set => path = value; }
    public ObservableCollection<Node> Children { get => children; set => children = value; }
    public string Value { get => value; set => this.value = value; }
    public string TypeName { get => typeName; set => typeName = value; }
    public string DiplayValue { get => typeName + " " + Value; }
    public string RawHexValue { get => Value; }
    public string TranslatedValue { get => Value; }


    public Node()
    {

    }

    // Future: Get possible children
}

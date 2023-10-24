namespace Tap0309;

public class Node
{
    private Node parent = null;
    private string path = "";
    private List<Node> children = new List<Node>();
    private string value;
    private string typeName;


    public Node Parent { get => parent; set => parent = value; }
    public string Path { get => path; set => path = value; }
    public List<Node> Children { get => children; set => children = value; }
    public string Value { get => value; set => this.value = value; }
    public string TypeName { get => typeName; set => typeName = value; }

    public Node()
    {

    }
}

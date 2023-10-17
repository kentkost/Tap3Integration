namespace Tap0309;

public class Node
{
    private Node parent;
    private string path;
    private List<Node> children;
    private string value;
    private string classType; // Change to enum Application, Universal, Context-Specific, Private
    private int classValue; 

    public Node Parent { get => parent; set => parent = value; }
    public string Path { get => path; set => path = value; }
    public List<Node> Children { get => children; set => children = value; }
    public string Value { get => value; set => this.value = value; }
    public string ClassType { get => classType; set => classType = value; }

    public Node()
    {

    }
}

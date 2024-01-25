using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using Tap0309;

namespace Tap3Studio.ViewModels;

public partial class MainWindowViewModel : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<Node> tap3Root = new ObservableCollection<Node>();

    [ObservableProperty]
    private Node selectedNode;

    [ObservableProperty]
    private string greeting;

    [ObservableProperty]
    private string fuckOff;


    AnyReader reader;
    public MainWindowViewModel()
    {
        reader = new AnyReader();
        reader.PrepareStructures();
        tap3Root.Add(reader.Root);
        greeting = "Hello shithead" + reader.Root.TypeName;
        fuckOff = "Fuck off" + reader.Root.TypeName;

    }

    //public XmlNode RootNode => reader.DataDocument.DocumentElement.FirstChild;

    //public Node ObjectNode => reader.Root;
}
using System.Formats.Asn1;
using Tap0309;

namespace Tap3Studio.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    public MainWindowViewModel()
    {
        AnyReader reader = new AnyReader();
        reader.PrepareStructures();
    }

    public string Greeting => "Welcome to Avalonia!";
}
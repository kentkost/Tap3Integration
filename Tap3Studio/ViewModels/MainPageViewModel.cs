using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Tap3Studio.Models;

namespace Tap3Studio.ViewModels;


public partial class MainPageViewModel : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<SampleItem> samples;

    public MainPageViewModel()
    {
        samples = new ObservableCollection<SampleItem>();
    }

    [RelayCommand]
    void AddSample()
    {
        Samples.Add(new SampleItem()
        {
            Description = "yeert",
            IsCompleted = false,
            Title = "fuuuark"
        });
    }
}

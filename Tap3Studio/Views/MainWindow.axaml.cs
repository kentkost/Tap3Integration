using Avalonia.Controls;
using Tap3Studio.ViewModels;

namespace Tap3Studio.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainWindowViewModel vm = (MainWindowViewModel)this.DataContext;

            //dataTree.ItemsSource = vm.ObjectNode;
        }
    }
}
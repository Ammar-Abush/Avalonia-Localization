using System;
using System.Threading;

namespace Localization.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
#pragma warning disable CA1822 // Mark members as static
    public string Greeting => Resources.Greeting;
#pragma warning restore CA1822 // Mark members as static
    public MainWindowViewModel()
    {
        Console.WriteLine(Thread.CurrentThread.CurrentCulture);
    }
}
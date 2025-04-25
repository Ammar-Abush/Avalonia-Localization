using System.ComponentModel;
using LocalisationJSON.Service;

namespace LocalisationJSON.ViewModels;

public class MainWindowViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    public LocalizationService Localization => App.Localization;

    public string Greeting => Localization.Greeting;

    public MainWindowViewModel()
    {
        Localization.PropertyChanged += (sender, args) =>
        {
            if (args.PropertyName == nameof(Localization.Greeting))
            {
                OnPropertyChanged(nameof(Greeting));
            }
        };
    }

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
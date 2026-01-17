using BeFit.Models;
using BeFit.ViewModels;

namespace BeFit.Views;

public partial class SessionPage : ContentPage
{
    public SessionPage(SessionViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }

    private async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is TrainingSession session)
        {
            await Shell.Current.GoToAsync($"{nameof(SessionDetailPage)}?SessionId={session.Id}");
            ((CollectionView)sender).SelectedItem = null;
        }
    }
}
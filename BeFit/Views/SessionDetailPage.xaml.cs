using BeFit.ViewModels;

namespace BeFit.Views;

public partial class SessionDetailPage : ContentPage
{
    public SessionDetailPage(SessionDetailViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}
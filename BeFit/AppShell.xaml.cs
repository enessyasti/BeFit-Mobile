using BeFit.Views;

namespace BeFit;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute(nameof(SessionDetailPage), typeof(SessionDetailPage));
    }
}
using BeFit.ViewModels;

namespace BeFit.Views;

public partial class ExercisePage : ContentPage
{
    public ExercisePage(ExerciseViewModel vm)
    {
        InitializeComponent();

        BindingContext = vm;
    }
}
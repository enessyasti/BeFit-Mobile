using BeFit.Models;
using BeFit.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace BeFit.ViewModels;

public class ExerciseViewModel : INotifyPropertyChanged
{
    private readonly DatabaseService _databaseService;
    private string _name;
    private string _description;

    public ObservableCollection<ExerciseType> Exercises { get; set; } = new();

    public string Name
    {
        get => _name;
        set { _name = value; OnPropertyChanged(); }
    }

    public string Description
    {
        get => _description;
        set { _description = value; OnPropertyChanged(); }
    }

    public ICommand SaveCommand { get; }
    public ICommand DeleteCommand { get; }

    public ExerciseViewModel(DatabaseService databaseService)
    {
        _databaseService = databaseService;
        SaveCommand = new Command(async () => await SaveAsync());
        DeleteCommand = new Command<ExerciseType>(async (x) => await DeleteAsync(x));
        LoadData();
    }

    private async void LoadData()
    {
        var list = await _databaseService.GetExerciseTypesAsync();
        Exercises.Clear();
        foreach (var item in list) Exercises.Add(item);
    }

    private async Task SaveAsync()
    {
        if (string.IsNullOrWhiteSpace(Name)) return;

        var exercise = new ExerciseType { Name = Name, Description = Description };
        await _databaseService.SaveExerciseTypeAsync(exercise);
        Name = string.Empty;
        Description = string.Empty;
        LoadData();
    }

    private async Task DeleteAsync(ExerciseType exercise)
    {
        await _databaseService.DeleteExerciseTypeAsync(exercise);
        LoadData();
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string name = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}
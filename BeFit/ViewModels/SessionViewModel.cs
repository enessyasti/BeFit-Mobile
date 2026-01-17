using BeFit.Models;
using BeFit.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace BeFit.ViewModels;

[QueryProperty(nameof(SessionId), "SessionId")]
public class SessionDetailViewModel : INotifyPropertyChanged
{
    private readonly DatabaseService _databaseService;
    private int _sessionId;
    private ExerciseType _selectedExercise;
    private double _load;
    private int _sets;
    private int _reps;

    public ObservableCollection<ExerciseType> AvailableExercises { get; set; } = new();
    public ObservableCollection<SessionExercise> SessionExercises { get; set; } = new();

    public int SessionId
    {
        get => _sessionId;
        set
        {
            _sessionId = value;
            LoadData();
        }
    }

    public ExerciseType SelectedExercise
    {
        get => _selectedExercise;
        set { _selectedExercise = value; OnPropertyChanged(); }
    }

    public double Load
    {
        get => _load;
        set { _load = value; OnPropertyChanged(); }
    }

    public int Sets
    {
        get => _sets;
        set { _sets = value; OnPropertyChanged(); }
    }

    public int Reps
    {
        get => _reps;
        set { _reps = value; OnPropertyChanged(); }
    }

    public ICommand AddExerciseCommand { get; }

    public SessionDetailViewModel(DatabaseService databaseService)
    {
        _databaseService = databaseService;
        AddExerciseCommand = new Command(async () => await AddAsync());
    }

    private async void LoadData()
    {
        var exercises = await _databaseService.GetExerciseTypesAsync();
        AvailableExercises.Clear();
        foreach (var e in exercises) AvailableExercises.Add(e);

        var sessionExercises = await _databaseService.GetSessionExercisesAsync(SessionId);
        SessionExercises.Clear();
        foreach (var se in sessionExercises) SessionExercises.Add(se);
    }

    private async Task AddAsync()
    {
        if (SelectedExercise == null || Sets <= 0 || Reps <= 0) return;

        var link = new SessionExercise
        {
            TrainingSessionId = SessionId,
            ExerciseTypeId = SelectedExercise.Id,
            LoadKg = Load,
            Sets = Sets,
            Repetitions = Reps
        };

        await _databaseService.SaveSessionExerciseAsync(link);
        LoadData();
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string name = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}
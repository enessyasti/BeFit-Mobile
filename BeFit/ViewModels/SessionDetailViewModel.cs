using BeFit.Models;
using BeFit.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace BeFit.ViewModels;

public class SessionViewModel : INotifyPropertyChanged
{
    private readonly DatabaseService _databaseService;
    private DateTime _start = DateTime.Now;
    private DateTime _end = DateTime.Now.AddHours(1);

    public ObservableCollection<TrainingSession> Sessions { get; set; } = new();

    public DateTime Start
    {
        get => _start;
        set { _start = value; OnPropertyChanged(); }
    }

    public DateTime End
    {
        get => _end;
        set { _end = value; OnPropertyChanged(); }
    }

    public ICommand CreateCommand { get; }

    public SessionViewModel(DatabaseService databaseService)
    {
        _databaseService = databaseService;
        CreateCommand = new Command(async () => await CreateAsync());
        LoadData();
    }

    private async void LoadData()
    {
        var list = await _databaseService.GetSessionsAsync();
        Sessions.Clear();
        foreach (var item in list) Sessions.Add(item);
    }

    private async Task CreateAsync()
    {
        if (End < Start) return;

        var session = new TrainingSession { StartTime = Start, EndTime = End };
        await _databaseService.SaveSessionAsync(session);
        LoadData();
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string name = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}
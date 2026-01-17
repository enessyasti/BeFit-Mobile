using SQLite;
using BeFit.Models;

namespace BeFit.Services;

public class DatabaseService
{
    private SQLiteAsyncConnection _database;

    async Task Init()
    {
        if (_database is not null)
            return;

        var dbPath = Path.Combine(FileSystem.AppDataDirectory, "BeFit.db3");
        _database = new SQLiteAsyncConnection(dbPath);

        await _database.CreateTableAsync<ExerciseType>();
        await _database.CreateTableAsync<TrainingSession>();
        await _database.CreateTableAsync<SessionExercise>();
    }

    public async Task<List<ExerciseType>> GetExerciseTypesAsync()
    {
        await Init();
        return await _database.Table<ExerciseType>().ToListAsync();
    }

    public async Task<int> SaveExerciseTypeAsync(ExerciseType item)
    {
        await Init();
        if (item.Id != 0)
            return await _database.UpdateAsync(item);
        else
            return await _database.InsertAsync(item);
    }

    public async Task<int> DeleteExerciseTypeAsync(ExerciseType item)
    {
        await Init();
        return await _database.DeleteAsync(item);
    }

    public async Task<List<TrainingSession>> GetSessionsAsync()
    {
        await Init();
        return await _database.Table<TrainingSession>().OrderByDescending(x => x.StartTime).ToListAsync();
    }

    public async Task<int> SaveSessionAsync(TrainingSession item)
    {
        await Init();
        if (item.Id != 0)
            return await _database.UpdateAsync(item);
        else
            return await _database.InsertAsync(item);
    }

    public async Task<int> DeleteSessionAsync(TrainingSession item)
    {
        await Init();
        return await _database.DeleteAsync(item);
    }

    public async Task<List<SessionExercise>> GetSessionExercisesAsync(int sessionId)
    {
        await Init();
        return await _database.Table<SessionExercise>().Where(x => x.TrainingSessionId == sessionId).ToListAsync();
    }

    public async Task<int> SaveSessionExerciseAsync(SessionExercise item)
    {
        await Init();
        if (item.Id != 0)
            return await _database.UpdateAsync(item);
        else
            return await _database.InsertAsync(item);
    }

    public async Task<int> DeleteSessionExerciseAsync(SessionExercise item)
    {
        await Init();
        return await _database.DeleteAsync(item);
    }
}
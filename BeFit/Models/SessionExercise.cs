using SQLite;
using System.ComponentModel.DataAnnotations;

namespace BeFit.Models;

public class SessionExercise
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    [Indexed]
    public int TrainingSessionId { get; set; }

    [Indexed]
    public int ExerciseTypeId { get; set; }

    [Range(0, 1000)]
    public double LoadKg { get; set; }

    [Range(1, 100)]
    public int Sets { get; set; }

    [Range(1, 500)]
    public int Repetitions { get; set; }
}
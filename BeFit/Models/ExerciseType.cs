using SQLite;
using System.ComponentModel.DataAnnotations;

namespace BeFit.Models;

public class ExerciseType
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    [System.ComponentModel.DataAnnotations.MaxLength(50)]
    public string Name { get; set; }

    [System.ComponentModel.DataAnnotations.MaxLength(200)]
    public string Description { get; set; }
}
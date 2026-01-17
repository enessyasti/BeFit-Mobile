using SQLite;
using System.ComponentModel.DataAnnotations;

namespace BeFit.Models;

public class TrainingSession
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    [Required]
    public DateTime StartTime { get; set; }

    [Required]
    public DateTime EndTime { get; set; }

    [System.ComponentModel.DataAnnotations.MaxLength(100)]
    public string Notes { get; set; }
}
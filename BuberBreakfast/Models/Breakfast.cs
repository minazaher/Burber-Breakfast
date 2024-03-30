using System.ComponentModel.DataAnnotations;

namespace BuberBreakfast.Models;

public class Breakfast
{
    public Guid Id { get; }

    public string Name{ get; }

    public string Description{ get; }

    public DateTime StartDateTime{ get; }
    public DateTime EndDateTime{ get; }

    public DateTime LastModifiedDateTime{ get; }
    public List<string> Savory{ get; }
    public List<string> Sweet{ get; }

    public Breakfast(
        Guid id,
        [MinLength(5, ErrorMessage = "Minimum length of name should be 5 characters")]
        string name,
        string description,
        DateTime startDateTime,
        DateTime endDateTime,
        DateTime lastModifiedDateTime,
        List<string> savory,
        List<string> sweet)
    {
        Id = id;
        Name = name;
        Description = description;
        StartDateTime = startDateTime;
        EndDateTime = endDateTime;
        LastModifiedDateTime = lastModifiedDateTime;
        Savory = savory;
        Sweet = sweet;
    }
}
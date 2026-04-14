namespace actividad_2.Models;
using actividad_2.Models.Enums;

public class Driver
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Licence { get; set; } = string.Empty;
    public Status Status { get; set; } = Status.Available;

    public Driver(string id, string name, string licence, Status status)
    {
        Id = id;
        Name = name;
        Licence = licence;
        Status = status;
    }
}
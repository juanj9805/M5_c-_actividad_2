using actividad_2.Models.Enums;

namespace actividad_2.Models;

public enum VehicleType {Car, Bike, Truck}

public class Vehicle
{
    public string Id { get; set; }
    public string LicensePlate { get; set; } = string.Empty;
    public int Capacity { get; set; } = 0;
    public Status Status { get; set; } = Status.Available;
    public VehicleType Type { get; set; }

    public Vehicle(string id, string licensePlate, int capacity, Status status, VehicleType type)
    {
        Id = id;
        LicensePlate = licensePlate;
        Capacity = capacity;
        Status = status;
        Type = type;
    }
}
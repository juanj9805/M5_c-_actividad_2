namespace actividad_2.Models;

public enum ServiceStatus { Pending, OnGoing, Finished }

public class TransportService
{
    public string Id { get; set; }
    public string Origin { get; set; }
    public string Destination { get; set; }
    public double Distance { get; set; }
    public decimal Cost { get; set; } = 0;
    public ServiceStatus Status { get; set; } = ServiceStatus.Pending;

    public string? DriverId { get; set; }
    public Driver? Driver { get; set; }

    public string? VehicleId { get; set; }
    public Vehicle? Vehicle { get; set; }

    public TransportService(string id, string origin, string destination, double distance)
    {
        Id = id;
        Origin = origin;
        Destination = destination;
        Distance = distance;
    }
}

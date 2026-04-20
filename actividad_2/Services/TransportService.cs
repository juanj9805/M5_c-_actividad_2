using actividad_2.Models;
using actividad_2.Models.Enums;
using TService = actividad_2.Models.TransportService;

namespace actividad_2.Services;

public class TransportManager
{
    private readonly List<Driver> _drivers = [];
    private readonly List<Vehicle> _vehicles = [];
    private readonly List<TService> _services = [];

    public (bool Success, string Message) RegisterDriver(string id, string name, string licence)
    {
        if (string.IsNullOrWhiteSpace(id) || string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(licence))
            return (false, "All fields are required.");
        if (_drivers.Any(d => d.Id == id))
            return (false, $"Driver with ID '{id}' already exists.");
        _drivers.Add(new Driver(id, name, licence, Status.Available));
        return (true, "Driver registered.");
    }

    public (bool Success, string Message) RegisterVehicle(string plate, int capacity, VehicleType type)
    {
        if (string.IsNullOrWhiteSpace(plate))
            return (false, "License plate is required.");
        if (capacity <= 0)
            return (false, "Capacity must be greater than zero.");
        if (_vehicles.Any(v => v.LicensePlate == plate))
            return (false, $"Vehicle '{plate}' already exists.");
        var id = Guid.NewGuid().ToString()[..8];
        _vehicles.Add(new Vehicle(id, plate, capacity, Status.Available, type));
        return (true, "Vehicle registered.");
    }

    public (bool Success, string Message) RegisterService(string origin, string destination, double distance)
    {
        if (string.IsNullOrWhiteSpace(origin) || string.IsNullOrWhiteSpace(destination))
            return (false, "Origin and destination are required.");
        if (distance <= 0)
            return (false, "Distance must be greater than zero.");
        var id = Guid.NewGuid().ToString()[..8];
        _services.Add(new TService(id, origin, destination, distance));
        return (true, $"Service registered. ID: {id}");
    }

    public (bool Success, string Message) AssignResources(string serviceId, string driverId, string vehiclePlate)
    {
        var service = _services.FirstOrDefault(s => s.Id == serviceId);
        if (service is null) return (false, "Service not found.");
        if (service.Status != ServiceStatus.Pending) return (false, "Service must be pending.");
        if (service.Driver is not null) return (false, "Service already has resources assigned.");

        var driver = _drivers.FirstOrDefault(d => d.Id == driverId);
        if (driver is null) return (false, "Driver not found.");
        if (driver.Status != Status.Available) return (false, "Driver is not available.");

        var vehicle = _vehicles.FirstOrDefault(v => v.LicensePlate == vehiclePlate);
        if (vehicle is null) return (false, "Vehicle not found.");
        if (vehicle.Status != Status.Available) return (false, "Vehicle is not available.");

        service.Driver = driver;
        service.DriverId = driver.Id;
        service.Vehicle = vehicle;
        service.VehicleId = vehicle.Id;
        driver.Status = Status.InService;
        vehicle.Status = Status.InService;

        return (true, "Resources assigned.");
    }

    public (bool Success, string Message) StartService(string serviceId)
    {
        var service = _services.FirstOrDefault(s => s.Id == serviceId);
        if (service is null) return (false, "Service not found.");
        if (service.Driver is null || service.Vehicle is null) return (false, "No driver or vehicle assigned.");
        if (service.Status != ServiceStatus.Pending) return (false, "Service must be pending to start.");
        service.Status = ServiceStatus.OnGoing;
        return (true, "Service started.");
    }

    public (bool Success, string Message) FinishService(string serviceId)
    {
        var service = _services.FirstOrDefault(s => s.Id == serviceId);
        if (service is null) return (false, "Service not found.");
        if (service.Status != ServiceStatus.OnGoing) return (false, "Service is not in progress.");

        service.Cost = CalculateCost(service.Distance, service.Vehicle!.Type);
        service.Status = ServiceStatus.Finished;
        service.Driver!.Status = Status.Available;
        service.Vehicle!.Status = Status.Available;

        return (true, $"Service finished. Cost: ${service.Cost:F2}");
    }

    private static decimal CalculateCost(double km, VehicleType type)
    {
        decimal rate = type switch
        {
            VehicleType.Car   => 2.50m,
            VehicleType.Bike  => 1.50m,
            VehicleType.Truck => 4.00m,
            _                 => 2.50m
        };
        decimal cost = (decimal)km * rate;
        if (km > 500) cost *= 1.15m;
        return Math.Round(cost, 2);
    }

    public IReadOnlyList<TService> GetServices() => _services.AsReadOnly();
    public IReadOnlyList<Driver> GetDrivers() => _drivers.AsReadOnly();
    public IReadOnlyList<Vehicle> GetVehicles() => _vehicles.AsReadOnly();
}

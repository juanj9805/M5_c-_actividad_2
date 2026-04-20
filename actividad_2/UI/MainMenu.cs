using actividad_2.Models;
using actividad_2.Models.Enums;
using actividad_2.Services;

namespace actividad_2.UI;

public class MainMenu
{
    public static void Run(TransportManager manager)
    {
        while (true)
        {
            Console.WriteLine("\n--- Transport Management System ---");
            Console.WriteLine("1. Register module");
            Console.WriteLine("2. Service module");
            Console.WriteLine("3. Reports module");
            Console.WriteLine("0. Exit");
            Console.Write("Option: ");
            switch (Console.ReadLine())
            {
                case "1": RegisterMenu(manager); break;
                case "2": ServiceMenu(manager); break;
                case "3": ReportsMenu(manager); break;
                case "0": Console.WriteLine("Goodbye."); return;
                default: Console.WriteLine("Invalid option."); break;
            }
        }
    }

    private static void RegisterMenu(TransportManager manager)
    {
        while (true)
        {
            Console.WriteLine("\n-- Register Module --");
            Console.WriteLine("1. Register driver");
            Console.WriteLine("2. Register vehicle");
            Console.WriteLine("3. Register transport service");
            Console.WriteLine("4. Assign driver and vehicle to service");
            Console.WriteLine("0. Back");
            Console.Write("Option: ");
            switch (Console.ReadLine())
            {
                case "1": RegisterDriver(manager); break;
                case "2": RegisterVehicle(manager); break;
                case "3": RegisterService(manager); break;
                case "4": AssignResources(manager); break;
                case "0": return;
                default: Console.WriteLine("Invalid option."); break;
            }
        }
    }

    private static void RegisterDriver(TransportManager manager)
    {
        Console.WriteLine("\n-- Register Driver --");
        Console.Write("ID: ");
        var id = Console.ReadLine() ?? "";
        Console.Write("Name: ");
        var name = Console.ReadLine() ?? "";
        Console.Write("Licence: ");
        var licence = Console.ReadLine() ?? "";

        var (success, message) = manager.RegisterDriver(id, name, licence);
        Console.WriteLine(success ? $"[OK] {message}" : $"[ERROR] {message}");
    }

    private static void RegisterVehicle(TransportManager manager)
    {
        Console.WriteLine("\n-- Register Vehicle --");
        Console.Write("License plate: ");
        var plate = Console.ReadLine() ?? "";

        Console.Write("Capacity: ");
        if (!int.TryParse(Console.ReadLine(), out var capacity))
        {
            Console.WriteLine("[ERROR] Invalid capacity.");
            return;
        }

        Console.WriteLine("Type: 1=Car  2=Bike  3=Truck");
        Console.Write("Type: ");
        VehicleType? type = Console.ReadLine() switch
        {
            "1" => VehicleType.Car,
            "2" => VehicleType.Bike,
            "3" => VehicleType.Truck,
            _   => null
        };
        if (type is null) { Console.WriteLine("[ERROR] Invalid vehicle type."); return; }

        var (success, message) = manager.RegisterVehicle(plate, capacity, type.Value);
        Console.WriteLine(success ? $"[OK] {message}" : $"[ERROR] {message}");
    }

    private static void RegisterService(TransportManager manager)
    {
        Console.WriteLine("\n-- Register Transport Service --");
        Console.Write("Origin: ");
        var origin = Console.ReadLine() ?? "";
        Console.Write("Destination: ");
        var destination = Console.ReadLine() ?? "";
        Console.Write("Distance (km): ");
        if (!double.TryParse(Console.ReadLine(), out var distance))
        {
            Console.WriteLine("[ERROR] Invalid distance.");
            return;
        }

        var (success, message) = manager.RegisterService(origin, destination, distance);
        Console.WriteLine(success ? $"[OK] {message}" : $"[ERROR] {message}");
    }

    private static void AssignResources(TransportManager manager)
    {
        Console.WriteLine("\n-- Assign Resources --");

        var pending = manager.GetServices()
            .Where(s => s.Status == ServiceStatus.Pending && s.Driver is null)
            .ToList();
        if (!pending.Any()) { Console.WriteLine("No pending services without assignment."); return; }

        Console.WriteLine("Pending services:");
        foreach (var s in pending)
            Console.WriteLine($"  ID: {s.Id} | {s.Origin} -> {s.Destination} | {s.Distance} km");
        Console.Write("Service ID: ");
        var serviceId = Console.ReadLine() ?? "";

        var availableDrivers = manager.GetDrivers()
            .Where(d => d.Status == Status.Available)
            .ToList();
        if (!availableDrivers.Any()) { Console.WriteLine("[ERROR] No available drivers."); return; }

        Console.WriteLine("Available drivers:");
        foreach (var d in availableDrivers)
            Console.WriteLine($"  ID: {d.Id} | {d.Name} | Licence: {d.Licence}");
        Console.Write("Driver ID: ");
        var driverId = Console.ReadLine() ?? "";

        var availableVehicles = manager.GetVehicles()
            .Where(v => v.Status == Status.Available)
            .ToList();
        if (!availableVehicles.Any()) { Console.WriteLine("[ERROR] No available vehicles."); return; }

        Console.WriteLine("Available vehicles:");
        foreach (var v in availableVehicles)
            Console.WriteLine($"  Plate: {v.LicensePlate} | {v.Type} | Capacity: {v.Capacity}");
        Console.Write("Vehicle plate: ");
        var plate = Console.ReadLine() ?? "";

        var (success, message) = manager.AssignResources(serviceId, driverId, plate);
        Console.WriteLine(success ? $"[OK] {message}" : $"[ERROR] {message}");
    }

    private static void ServiceMenu(TransportManager manager)
    {
        while (true)
        {
            Console.WriteLine("\n-- Service Module --");
            Console.WriteLine("1. Start service");
            Console.WriteLine("2. Finish service");
            Console.WriteLine("0. Back");
            Console.Write("Option: ");
            switch (Console.ReadLine())
            {
                case "1": StartService(manager); break;
                case "2": FinishService(manager); break;
                case "0": return;
                default: Console.WriteLine("Invalid option."); break;
            }
        }
    }

    private static void StartService(TransportManager manager)
    {
        Console.WriteLine("\n-- Start Service --");
        var ready = manager.GetServices()
            .Where(s => s.Status == ServiceStatus.Pending && s.Driver is not null)
            .ToList();
        if (!ready.Any()) { Console.WriteLine("No services ready to start."); return; }

        Console.WriteLine("Services ready to start:");
        foreach (var s in ready)
            Console.WriteLine($"  ID: {s.Id} | {s.Origin} -> {s.Destination} | Driver: {s.Driver!.Name} | Vehicle: {s.Vehicle!.LicensePlate}");
        Console.Write("Service ID: ");
        var id = Console.ReadLine() ?? "";

        var (success, message) = manager.StartService(id);
        Console.WriteLine(success ? $"[OK] {message}" : $"[ERROR] {message}");
    }

    private static void FinishService(TransportManager manager)
    {
        Console.WriteLine("\n-- Finish Service --");
        var ongoing = manager.GetServices()
            .Where(s => s.Status == ServiceStatus.OnGoing)
            .ToList();
        if (!ongoing.Any()) { Console.WriteLine("No services in progress."); return; }

        Console.WriteLine("Services in progress:");
        foreach (var s in ongoing)
            Console.WriteLine($"  ID: {s.Id} | {s.Origin} -> {s.Destination} | Driver: {s.Driver!.Name} | Vehicle: {s.Vehicle!.LicensePlate}");
        Console.Write("Service ID: ");
        var id = Console.ReadLine() ?? "";

        var (success, message) = manager.FinishService(id);
        Console.WriteLine(success ? $"[OK] {message}" : $"[ERROR] {message}");
    }

    private static void ReportsMenu(TransportManager manager)
    {
        while (true)
        {
            Console.WriteLine("\n-- Reports Module --");
            Console.WriteLine("1. Show services");
            Console.WriteLine("2. Show drivers and vehicles");
            Console.WriteLine("3. Operative reports");
            Console.WriteLine("0. Back");
            Console.Write("Option: ");
            switch (Console.ReadLine())
            {
                case "1": ShowServices(manager); break;
                case "2": ShowDriversAndVehicles(manager); break;
                case "3": ShowOperativeReports(manager); break;
                case "0": return;
                default: Console.WriteLine("Invalid option."); break;
            }
        }
    }

    private static void ShowServices(TransportManager manager)
    {
        Console.WriteLine("\n-- All Services --");
        var services = manager.GetServices();
        if (!services.Any()) { Console.WriteLine("No services registered."); return; }

        foreach (var s in services)
        {
            Console.WriteLine($"ID: {s.Id} | {s.Origin} -> {s.Destination} | {s.Distance} km | Status: {s.Status} | Cost: ${s.Cost:F2}");
            Console.WriteLine($"  Driver: {s.Driver?.Name ?? "Not assigned"} | Vehicle: {s.Vehicle?.LicensePlate ?? "Not assigned"}");
        }
    }

    private static void ShowDriversAndVehicles(TransportManager manager)
    {
        Console.WriteLine("\n-- Drivers --");
        var drivers = manager.GetDrivers();
        if (!drivers.Any()) Console.WriteLine("No drivers registered.");
        foreach (var d in drivers)
            Console.WriteLine($"  ID: {d.Id} | {d.Name} | Licence: {d.Licence} | Status: {d.Status}");

        Console.WriteLine("\n-- Vehicles --");
        var vehicles = manager.GetVehicles();
        if (!vehicles.Any()) Console.WriteLine("No vehicles registered.");
        foreach (var v in vehicles)
            Console.WriteLine($"  Plate: {v.LicensePlate} | Type: {v.Type} | Capacity: {v.Capacity} | Status: {v.Status}");
    }

    private static void ShowOperativeReports(TransportManager manager)
    {
        Console.WriteLine("\n-- Operative Reports --");
        var services = manager.GetServices();
        var drivers  = manager.GetDrivers();
        var vehicles = manager.GetVehicles();

        Console.WriteLine($"Total services  : {services.Count}");
        Console.WriteLine($"  Pending       : {services.Count(s => s.Status == ServiceStatus.Pending)}");
        Console.WriteLine($"  Ongoing       : {services.Count(s => s.Status == ServiceStatus.OnGoing)}");
        Console.WriteLine($"  Finished      : {services.Count(s => s.Status == ServiceStatus.Finished)}");
        Console.WriteLine($"Total income    : ${services.Where(s => s.Status == ServiceStatus.Finished).Sum(s => s.Cost):F2}");

        Console.WriteLine($"\nDrivers  — Available: {drivers.Count(d => d.Status == Status.Available)}  |  In service: {drivers.Count(d => d.Status == Status.InService)}  |  Inactive: {drivers.Count(d => d.Status == Status.Inactive)}");
        Console.WriteLine($"Vehicles — Available: {vehicles.Count(v => v.Status == Status.Available)}  |  In service: {vehicles.Count(v => v.Status == Status.InService)}  |  Out of operation: {vehicles.Count(v => v.Status == Status.Inactive)}");
    }
}

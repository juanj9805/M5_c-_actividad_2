using actividad_2.Models;
using actividad_2.Models.Enums;
using actividad_2.UI;

Driver juan = new Driver("1","juan","mit",Status.Available);
Vehicle car = new Vehicle("1","123sda", 1, Status.Available, VehicleType.Car);

TransportService service1 = new TransportService("1","medellin","bogota",123212,12341213, ServiceStatus.OnGoing, "1", "1");

service1.Driver = juan; 
service1.Vehicle = car;

 MainMenu.Run();
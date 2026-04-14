using System.Net.Http.Headers;

namespace actividad_2.UI;

public class MainMenu
{
    public static void Run()
    {
        while (true)
        {
            Console.WriteLine("Choose one option");
            Console.WriteLine("1: Register module");
            Console.WriteLine("2: Service module");
            Console.WriteLine("3: Reports module");
            Console.WriteLine("4: Exit");
            Console.Write("Option: ");
            var option = Console.ReadLine();
            switch (option)
            {
                case "1":
                    RegisterMenu();
                    break;
                case "2":
                    ServiceMenu();
                    break;
                case "3":
                    ReportsMenu();
                    break;
                case "4":
                    Console.WriteLine("Back");
                    return;
                default:
                    Console.WriteLine("Something went wrong");
                    break;
            }
        }
    }
    
    public static void RegisterMenu()
    {
        Console.WriteLine("Choose one option");
        Console.WriteLine("1: Register driver");
        Console.WriteLine("2: Register vehicle");
        Console.WriteLine("3: Register Transport service");
        Console.WriteLine("4: Exit");
        Console.Write("Option: ");
        while (true)
        {
            var option = Console.ReadLine();
            switch (option)
            {
                case "1":
                    Console.WriteLine("Registrar conductor");
                    break;
                case "2":
                    Console.WriteLine("Registrar vehículo");
                    break;
                case "3":
                    Console.WriteLine("Registrar servicio de transporte");
                    break;
                case "4":
                    Console.WriteLine("Exit");
                    return;
                default:
                    Console.WriteLine("Something went wrong");
                    break;
            }
        }
    }
    public static void ServiceMenu()
    {
        Console.WriteLine("Choose one option");
        Console.WriteLine("1: Start service");
        Console.WriteLine("2: End service");
        Console.WriteLine("3: Exit");
        Console.Write("Option: ");
        while (true)
        {
            var option = Console.ReadLine();
            switch (option)
            {
                case "1":
                    Console.WriteLine("Iniciar servicio");
                    break;
                case "2":
                    Console.WriteLine("Finalizar servicio");
                    break;
                case "3":
                    Console.WriteLine("Back");
                    return;
                default:
                    Console.WriteLine("Something went wrong");
                    break;
            }
        }
    }
    public static void ReportsMenu()
    {
        Console.WriteLine("Choose one option");
        Console.WriteLine("1: Show services");
        Console.WriteLine("2: Show drivers and vehicles");
        Console.WriteLine("3: Operative reports");
        Console.WriteLine("4: Exit");
        Console.Write("Option: ");
        while (true)
        {
            var option = Console.ReadLine();
            switch (option)
            {
                case "1":
                    Console.WriteLine("Consultar servicios");
                    break;
                case "2":
                    Console.WriteLine("Consultar conductores y vehículos");
                    break;
                case "3":
                    Console.WriteLine("Reportes operativos");
                    break;
                case "4":
                    Console.WriteLine("Exit");
                    return;
                default:
                    Console.WriteLine("Something went wrong");
                    break;
            }
        }
    }
}



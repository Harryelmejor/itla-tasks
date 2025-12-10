using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
class Program
{

    static List<Client> clients = new List<Client>();
    static List<Employee> employees = new List<Employee>();
    static List<Service> services = new List<Service>();
    static List<Contract> contracts = new List<Contract>();

    static int clientId = 1;
    static int employeeId = 1;
    static int serviceId = 1;
    static int contractId = 1;

    static void Main(string[] args)
    {
        bool exit = false;
        while (!exit)
        {
            Console.Clear();
            Console.WriteLine("===== CLEANING SERVICE MANAGER =====");
            Console.WriteLine("\n--- CLIENTS ---");
            Console.WriteLine("1. Add Client");
            Console.WriteLine("2. Delete Client");
            Console.WriteLine("3. Update Client");
            Console.WriteLine("4. View Clients");

            Console.WriteLine("\n--- EMPLOYEES ---");
            Console.WriteLine("5. Add Employee");
            Console.WriteLine("6. Delete Employee");
            Console.WriteLine("7. Update Employee");
            Console.WriteLine("8. View Employees");

            Console.WriteLine("\n--- SERVICES ---");
            Console.WriteLine("9. Add Service");
            Console.WriteLine("10. Delete Service");
            Console.WriteLine("11. Update Service");
            Console.WriteLine("12. View Services");

            Console.WriteLine("\n--- CONTRACTS ---");
            Console.WriteLine("13. Create Contract");
            Console.WriteLine("14. Delete Contract");
            Console.WriteLine("15. View Contracts");

            Console.WriteLine("\n16. Exit");
            Console.Write("\nChoose an option: ");

            string option = Console.ReadLine();

            switch (option)
            {
                // === CLIENTS ===
                case "1": AddClient(); break;
                case "2": DeleteClient(); break;
                case "3": UpdateClient(); break;
                case "4": ViewClients(); break;

                // === EMPLOYEES ===
                case "5": AddEmployee(); break;
                case "6": DeleteEmployee(); break;
                case "7": UpdateEmployee(); break;
                case "8": ViewEmployees(); break;

                // === SERVICES ===
                case "9": AddService(); break;
                case "10": DeleteService(); break;
                case "11": UpdateService(); break;
                case "12": ViewServices(); break;

                // === CONTRACTS ===
                case "13": CreateContract(); break;
                case "14": DeleteContract(); break;
                case "15": ViewContracts(); break;

                case "16": exit = true; Console.WriteLine("Thank you for using the system!"); break;
                default: Console.WriteLine("Invalid option."); Console.ReadKey(); break;
            }
        }
    }

    // ==================================================================
    //                            CLIENTS
    // ==================================================================

    static void AddClient()
    {
        Console.Clear();
        Console.WriteLine("=== ADD CLIENT ===");

        Console.Write("Name: ");
        string name = Console.ReadLine();
        Console.Write("Phone: ");
        string phone = Console.ReadLine();
        Console.Write("Email: ");
        string email = Console.ReadLine();
        Console.Write("Address: ");
        string address = Console.ReadLine();

        Database.InsertClient(name, phone, email, address);

        Console.WriteLine(" Client inserted into database!");
        Console.ReadKey();
    }

    static void DeleteClient()
    {
        Console.Clear();
        Console.Write("Enter Client ID to delete: ");

        if (int.TryParse(Console.ReadLine(), out int id))
        {
            Database.DeleteClient(id);
            Console.WriteLine(" Client deleted from DB.");
        }
        else
        {
            Console.WriteLine("Invalid ID.");
        }

        Console.ReadKey();
    }


    static void UpdateClient()
    {
        Console.Clear();
        Console.Write("Enter Client ID to update: ");

        if (int.TryParse(Console.ReadLine(), out int id))
        {
            Console.Write("New Name: ");
            string name = Console.ReadLine();

            Console.Write("New Phone: ");
            string phone = Console.ReadLine();

            Console.Write("New Email: ");
            string email = Console.ReadLine();

            Console.Write("New Address: ");
            string address = Console.ReadLine();

            Database.UpdateClient(id, name, phone, email, address);

            Console.WriteLine(" Client updated in DB.");
        }
        else
        {
            Console.WriteLine("Invalid ID.");
        }

        Console.ReadKey();
    }


    static void ViewClients()
    {
        Console.Clear();
        Console.WriteLine("=== CLIENT LIST (DB) ===");

        var dt = Database.GetClients();

        if (dt.Rows.Count == 0)
        {
            Console.WriteLine("No clients found.");
        }
        else
        {
            foreach (DataRow row in dt.Rows)
            {
                Console.WriteLine($"ID: {row["Id"]} | Name: {row["Name"]} | Phone: {row["Phone"]} | Email: {row["Email"]} | Address: {row["Address"]}");
            }
        }

        Console.ReadKey();
    }


    // ==================================================================
    //                           EMPLOYEES
    // ==================================================================

    static void AddEmployee()
    {
        Console.Clear();
        Console.WriteLine("=== ADD EMPLOYEE ===");

        Console.Write("Name: ");
        string name = Console.ReadLine();
        Console.Write("Phone: ");
        string phone = Console.ReadLine();
        Console.Write("Email: ");
        string email = Console.ReadLine();
        Console.Write("Availability (e.g., Mon-Fri): ");
        string availability = Console.ReadLine();

        Database.InsertEmployee(name, phone, email, availability);

        Console.WriteLine("Employee added successfully!");
        Console.ReadKey();
    }


    static void DeleteEmployee()
    {
        Console.Clear();
        Console.WriteLine("=== DELETE EMPLOYEE ===");

        var dt = Database.GetEmployees();

        if (dt.Rows.Count == 0)
        {
            Console.WriteLine("No employees available.");
            Console.ReadKey();
            return;
        }

        foreach (DataRow row in dt.Rows)
            Console.WriteLine($"{row["Id"]} - {row["Name"]}");

        Console.Write("Employee ID to delete: ");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            Database.DeleteEmployee(id);
            Console.WriteLine("Employee deleted.");
        }
        else
        {
            Console.WriteLine("Invalid ID.");
        }

        Console.ReadKey();
    }


    static void UpdateEmployee()
    {
        Console.Clear();
        Console.WriteLine("=== UPDATE EMPLOYEE ===");

        var dt = Database.GetEmployees();

        if (dt.Rows.Count == 0)
        {
            Console.WriteLine("No employees available.");
            Console.ReadKey();
            return;
        }

        foreach (DataRow row in dt.Rows)
            Console.WriteLine($"{row["Id"]} - {row["Name"]}");

        Console.Write("Employee ID to update: ");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            var row = dt.Select($"Id = {id}").FirstOrDefault();
            if (row == null)
            {
                Console.WriteLine("Employee not found.");
                Console.ReadKey();
                return;
            }

            Console.Write($"New name ({row["Name"]}): ");
            string name = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(name)) name = row["Name"].ToString();

            Console.Write($"New phone ({row["Phone"]}): ");
            string phone = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(phone)) phone = row["Phone"].ToString();

            Console.Write($"New email ({row["Email"]}): ");
            string email = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(email)) email = row["Email"].ToString();

            Console.Write($"New availability ({row["Availability"]}): ");
            string availability = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(availability)) availability = row["Availability"].ToString();

            Database.UpdateEmployee(id, name, phone, email, availability);

            Console.WriteLine("Employee updated.");
        }
        else
        {
            Console.WriteLine("Invalid ID.");
        }

        Console.ReadKey();
    }


    static void ViewEmployees()
    {
        Console.Clear();
        Console.WriteLine("=== EMPLOYEES ===");

        var dt = Database.GetEmployees();

        if (dt.Rows.Count == 0)
        {
            Console.WriteLine("No employees.");
        }
        else
        {
            foreach (DataRow row in dt.Rows)
            {
                Console.WriteLine($"{row["Id"]} | {row["Name"]} | {row["Phone"]} | {row["Email"]} | {row["Availability"]}");
            }
        }

        Console.ReadKey();
    }


    // ==================================================================
    //                           SERVICES
    // ==================================================================

    static void AddService()
    {
        Console.Clear();
        Console.WriteLine("=== ADD SERVICE ===");

        Console.Write("Service name: ");
        string name = Console.ReadLine();

        Console.Write("Price: ");
        if (!double.TryParse(Console.ReadLine(), out double price))
        {
            Console.WriteLine("Invalid price.");
            Console.ReadKey();
            return;
        }

        Database.InsertService(name, price);

        Console.WriteLine("Service added successfully!");
        Console.ReadKey();
    }

    static void DeleteService()
    {
        Console.Clear();
        Console.WriteLine("=== DELETE SERVICE ===");

        var dt = Database.GetServices();

        if (dt.Rows.Count == 0)
        {
            Console.WriteLine("No services available.");
            Console.ReadKey();
            return;
        }

        foreach (DataRow row in dt.Rows)
            Console.WriteLine($"{row["Id"]} - {row["Name"]}");

        Console.Write("Service ID to delete: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Invalid ID.");
            Console.ReadKey();
            return;
        }

        // ⚠ Verificar si el servicio está en contratos
        var contracts = Database.GetContracts();
        bool inUse = contracts.Select($"Service = '{dt.Select($"Id = {id}")[0]["Name"]}'").Length > 0;

        if (inUse)
        {
            Console.WriteLine("This service is used in contracts. Delete anyway? (y/n)");
            if (Console.ReadLine().ToLower() != "y")
            {
                Console.WriteLine("Cancelled.");
                Console.ReadKey();
                return;
            }
        }

        Database.DeleteService(id);

        Console.WriteLine("Service deleted.");
        Console.ReadKey();
    }

    static void UpdateService()
    {
        Console.Clear();
        Console.WriteLine("=== UPDATE SERVICE ===");

        var dt = Database.GetServices();

        if (dt.Rows.Count == 0)
        {
            Console.WriteLine("No services available.");
            Console.ReadKey();
            return;
        }

        foreach (DataRow row in dt.Rows)
            Console.WriteLine($"{row["Id"]} - {row["Name"]} (${row["Price"]})");

        Console.Write("Service ID to update: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Invalid ID.");
            Console.ReadKey();
            return;
        }

        var selected = dt.Select($"Id = {id}").FirstOrDefault();
        if (selected == null)
        {
            Console.WriteLine("Service not found.");
            Console.ReadKey();
            return;
        }

        Console.WriteLine($"Updating: {selected["Name"]} (${selected["Price"]})");

        Console.Write($"New name ({selected["Name"]}): ");
        string name = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(name))
            name = selected["Name"].ToString();

        Console.Write($"New price ({selected["Price"]}): ");
        string priceStr = Console.ReadLine();
        double price = selected["Price"] is double d ? d : Convert.ToDouble(selected["Price"]);

        if (!string.IsNullOrWhiteSpace(priceStr) &&
            !double.TryParse(priceStr, out price))
        {
            Console.WriteLine("Invalid price. Update cancelled.");
            Console.ReadKey();
            return;
        }

        Database.UpdateService(id, name, price);

        Console.WriteLine("Service updated.");
        Console.ReadKey();
    }


    static void ViewServices()
    {
        Console.Clear();
        Console.WriteLine("=== SERVICES ===");

        var dt = Database.GetServices();

        if (dt.Rows.Count == 0)
        {
            Console.WriteLine("No services.");
        }
        else
        {
            foreach (DataRow row in dt.Rows)
            {
                Console.WriteLine(
                    $"{row["Id"]} | {row["Name"]} | ${row["Price"]}"
                );
            }
        }

        Console.ReadKey();
    }


    // ==================================================================
    //                           CONTRACTS
    // ==================================================================

    static void CreateContract()
    {
        var dtClients = Database.GetClients();
        clients.Clear();
        foreach (DataRow row in dtClients.Rows)
        {
            clients.Add(new Client(
                Convert.ToInt32(row["Id"]),
                row["Name"].ToString(),
                row["Phone"].ToString(),
                row["Email"].ToString(),
                row["Address"].ToString()
            ));
        }

        var dtEmployees = Database.GetEmployees();
        employees.Clear();
        foreach (DataRow row in dtEmployees.Rows)
        {
            employees.Add(new Employee(
                Convert.ToInt32(row["Id"]),
                row["Name"].ToString(),
                row["Phone"].ToString(),
                row["Email"].ToString(),
                row["Availability"].ToString()
            ));
        }

        var dtServices = Database.GetServices();
        services.Clear();
        foreach (DataRow row in dtServices.Rows)
        {
            services.Add(new Service(
                Convert.ToInt32(row["Id"]),
                row["Name"].ToString(),
                Convert.ToDouble(row["Price"])
            ));
        }



        if (clients.Count == 0 || employees.Count == 0 || services.Count == 0)
        {
            Console.WriteLine(" You need at least 1 client, 1 employee, and 1 service.");
            Console.ReadKey();
            return;
        }

        Console.Clear();
        Console.WriteLine("=== CREATE CONTRACT ===");

        Console.WriteLine("Clients:");
        foreach (var c in clients) c.Display();
        Console.Write("Client ID: ");
        if (!int.TryParse(Console.ReadLine(), out int clientIdInput)) { Console.WriteLine("Invalid ID."); Console.ReadKey(); return; }
        var client = clients.Find(c => c.Id == clientIdInput);
        if (client == null) { Console.WriteLine("Client not found."); Console.ReadKey(); return; }

        Console.WriteLine("Employees:");
        foreach (var e in employees) e.Display();
        Console.Write("Employee ID: ");
        if (!int.TryParse(Console.ReadLine(), out int employeeIdInput)) { Console.WriteLine("Invalid ID."); Console.ReadKey(); return; }
        var employee = employees.Find(e => e.Id == employeeIdInput);
        if (employee == null) { Console.WriteLine("Employee not found."); Console.ReadKey(); return; }

        Console.WriteLine("Services:");
        foreach (var s in services) s.Display();
        Console.Write("Service ID: ");
        if (!int.TryParse(Console.ReadLine(), out int serviceIdInput)) { Console.WriteLine("Invalid ID."); Console.ReadKey(); return; }
        var service = services.Find(s => s.Id == serviceIdInput);
        if (service == null) { Console.WriteLine("Service not found."); Console.ReadKey(); return; }

        Console.Write("Day(s) of the week: ");
        string days = Console.ReadLine();
        Console.Write("Is recurring? (y/n): ");
        bool isRecurring = Console.ReadLine().ToLower() == "y";

        Contract ct = new Contract(contractId++, client, employee, service, days, isRecurring);
        contracts.Add(ct);
        Console.WriteLine("✅ Contract created successfully!");
        Console.ReadKey();
    }

    static void DeleteContract()
    {
        if (contracts.Count == 0) { Console.WriteLine("No contracts available."); Console.ReadKey(); return; }
        Console.Clear();
        Console.WriteLine("=== DELETE CONTRACT ===");
        ViewContracts();
        Console.Write("Contract ID to delete: ");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            var ct = contracts.Find(x => x.Id == id);
            if (ct != null)
            {
                contracts.Remove(ct);
                Console.WriteLine(" Contract deleted.");
            }
            else Console.WriteLine("Contract not found.");
        }
        else Console.WriteLine("Invalid ID.");
        Console.ReadKey();
    }

    static void ViewContracts()
    {
        Console.Clear();
        Console.WriteLine("=== CONTRACTS ===");
        if (contracts.Count == 0) Console.WriteLine("No contracts.");
        else foreach (var ct in contracts) ct.Display();
        Console.ReadKey();
    }
}

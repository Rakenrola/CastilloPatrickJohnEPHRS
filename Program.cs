using System;
using System.Collections.Generic;
using System.Linq;

public class Employee
{
    public int Id { get; set; }                     // Column 1
    public string FirstName { get; set; }           // Column 2
    public string LastName { get; set; }            // Column 3
    public string Department { get; set; }          // Column 4
    public string JobTitle { get; set; }           // Column 5
    public decimal MonthlySalary { get; set; }      // Column 6
    public double TaxRate { get; set; }             // Column 7
    public DateTime HireDate { get; set; }          // Column 8
    public bool IsActive { get; set; }              // Column 9
    public string Email { get; set; }               // Column 10
}


class Program
{
    // Pre-populated with 5 initial records
    static List<Employee> employees = new List<Employee>();
        static void Main(string[] args)
    {
        bool running = true;

        while (running)
        {
            Console.Clear();
            Console.WriteLine("=== Employee Payroll & HR Management System ===");
            Console.WriteLine("1. Create (Add Employee)");
            Console.WriteLine("2. Read (View All Employees)");
            Console.WriteLine("3. Search/Filter Employees");
            Console.WriteLine("4. Update Employee");
            Console.WriteLine("5. Delete Employee");
            Console.WriteLine("6. Exit");
            Console.Write("\nSelect an option: ");
            string choice = Console.ReadLine();
            Console.WriteLine();

            switch (choice)
            {
                case "1":
                    CreateEmployee();
                    break;
                case "2":
                    ReadEmployees();
                    break;
                case "3":
                    SearchEmployees();
                    break;
                case "4":
                    UpdateEmployee();
                    break;
                case "5":
                    DeleteEmployee();
                    break;
                case "6":
                    running = false;
                    break;
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
            if (running)
            {
                Console.WriteLine("\nPress Enter to continue...");
                Console.ReadLine();
            }
        }
    }


    static void CreateEmployee()
    {
        Console.WriteLine("--- Add New Employee ---");

        // Auto-incrementing Id (1 if the list is empty)
        int id = employees.Any() ? employees.Max(e => e.Id) + 1 : 1;

        string firstName;
        while (true)
        {
            Console.Write("Enter First Name: ");
            firstName = Console.ReadLine()?.Trim();
            if (!string.IsNullOrWhiteSpace(firstName)) break;
            Console.WriteLine("First name cannot be empty. Try again.");
        }

        string lastName;
        while (true)
        {
            Console.Write("Enter Last Name: ");
            lastName = Console.ReadLine()?.Trim();
            if (!string.IsNullOrWhiteSpace(lastName)) break;
            Console.WriteLine("Last name cannot be empty. Try again.");
        }

        string department;
        while (true)
        {
            Console.Write("Enter Department: ");
            department = Console.ReadLine()?.Trim();
            if (!string.IsNullOrWhiteSpace(department)) break;
            Console.WriteLine("Department cannot be empty. Try again.");
        }

        string jobTitle;
        while (true)
        {
            Console.Write("Enter Job Title: ");
            jobTitle = Console.ReadLine()?.Trim();
            if (!string.IsNullOrWhiteSpace(jobTitle)) break;
            Console.WriteLine("Job title cannot be empty. Try again.");
        }

        decimal salary;
        while (true)
        {
            Console.Write("Enter Monthly Salary: ");
            if (decimal.TryParse(Console.ReadLine(), out salary) && salary > 0) break;
            Console.WriteLine("Invalid salary. Enter a positive number (e.g., 2500.50).");
        }

        double taxRate;
        while (true)
        {
            Console.Write("Enter Tax Rate (%, 0-100): ");
            if (double.TryParse(Console.ReadLine(), out taxRate) && taxRate >= 0 && taxRate <= 100) break;
            Console.WriteLine("Invalid tax rate. Enter a number between 0 and 100.");
        }

        DateTime hireDate;
        while (true)
        {
            Console.Write("Enter Hire Date (yyyy-MM-dd): ");
            if (DateTime.TryParseExact(Console.ReadLine(), "yyyy-MM-dd",
                System.Globalization.CultureInfo.InvariantCulture,
                System.Globalization.DateTimeStyles.None, out hireDate)) break;
            Console.WriteLine("Invalid date. Use the format yyyy-MM-dd (e.g., 2024-03-15).");
        }

        bool isActive;
        while (true)
        {
            Console.Write("Is the employee active? (y/n): ");
            string input = Console.ReadLine()?.Trim().ToLower();
            if (input == "y" || input == "yes") { isActive = true; break; }
            if (input == "n" || input == "no") { isActive = false; break; }
            Console.WriteLine("Invalid input. Enter 'y' or 'n'.");
        }

        string email;
        while (true)
        {
            Console.Write("Enter Email: ");
            email = Console.ReadLine()?.Trim();
            if (!string.IsNullOrWhiteSpace(email) && !email.Contains(" ") &&
                email.IndexOf('@') > 0 && email.LastIndexOf('.') > email.IndexOf('@') + 1 &&
                !email.EndsWith(".")) break;
            Console.WriteLine("Invalid email. Use a format like name@example.com.");
        }

        employees.Add(new Employee
        {
            Id = id,
            FirstName = firstName,
            LastName = lastName,
            Department = department,
            JobTitle = jobTitle,
            MonthlySalary = salary,
            TaxRate = taxRate,
            HireDate = hireDate,
            IsActive = isActive,
            Email = email
        });

        Console.WriteLine($"\nEmployee added successfully! (Id: {id})");
    }

    static void ReadEmployees()
    {
        Console.WriteLine("--- Employee Records ---");

        if (!employees.Any())
        {
            Console.WriteLine("No employees found.");
            return;
        }

        Console.WriteLine($"{"ID",-4}{"First",-12}{"Last",-12}{"Dept",-14}{"Job Title",-22}{"Salary",14}{"Tax%",7}  {"Hired",-20}{"Active",-8}{"Email"}");
        Console.WriteLine(new string('-', 120));

        foreach (var e in employees.OrderBy(e => e.Id))
        {
            Console.WriteLine($"{e.Id,-4}{e.FirstName,-12}{e.LastName,-12}{e.Department,-14}{e.JobTitle,-22}{e.MonthlySalary,14:N2}{e.TaxRate,7:F1}  {e.HireDate,-20:yyyy-MM-dd}{(e.IsActive ? "Yes" : "No"),-8}{e.Email}");
        }
    }

    static void SearchEmployees()
    {
        Console.WriteLine("--- Search Employee by Id ---");

        int id;
        while (true)
        {
            Console.Write("Enter Employee Id: ");
            if (int.TryParse(Console.ReadLine(), out id) && id > 0) break;
            Console.WriteLine("Invalid Id. Enter a positive whole number.");
        }

        Employee emp = employees.FirstOrDefault(e => e.Id == id);

        if (emp == null)
        {
            Console.WriteLine("\nEmployee not found.");
            return;
        }

        Console.WriteLine();
        Console.WriteLine($"Id:             {emp.Id}");
        Console.WriteLine($"First Name:     {emp.FirstName}");
        Console.WriteLine($"Last Name:      {emp.LastName}");
        Console.WriteLine($"Department:     {emp.Department}");
        Console.WriteLine($"Job Title:      {emp.JobTitle}");
        Console.WriteLine($"Monthly Salary: {emp.MonthlySalary:N2}");
        Console.WriteLine($"Tax Rate:       {emp.TaxRate:F1}%");
        Console.WriteLine($"Hire Date:      {emp.HireDate:yyyy-MM-dd}");
        Console.WriteLine($"Active:         {(emp.IsActive ? "Yes" : "No")}");
        Console.WriteLine($"Email:          {emp.Email}");
    }

    static void UpdateEmployee()
    {
        ReadEmployees();
        if (!employees.Any()) return;

        int id;
        while (true)
        {
            Console.Write("\nEnter the Id of the employee to update: ");
            if (int.TryParse(Console.ReadLine(), out id) && id > 0) break;
            Console.WriteLine("Invalid Id. Enter a positive whole number.");
        }

        Employee emp = employees.FirstOrDefault(e => e.Id == id);
        if (emp == null)
        {
            Console.WriteLine("\nEmployee not found.");
            return;
        }

        Console.WriteLine("\nPress Enter on any field to keep its current value.\n");

        Console.Write($"First Name (current: {emp.FirstName}): ");
        string input = Console.ReadLine()?.Trim();
        if (!string.IsNullOrWhiteSpace(input)) emp.FirstName = input;

        Console.Write($"Last Name (current: {emp.LastName}): ");
        input = Console.ReadLine()?.Trim();
        if (!string.IsNullOrWhiteSpace(input)) emp.LastName = input;

        Console.Write($"Department (current: {emp.Department}): ");
        input = Console.ReadLine()?.Trim();
        if (!string.IsNullOrWhiteSpace(input)) emp.Department = input;

        Console.Write($"Job Title (current: {emp.JobTitle}): ");
        input = Console.ReadLine()?.Trim();
        if (!string.IsNullOrWhiteSpace(input)) emp.JobTitle = input;

        while (true)
        {
            Console.Write($"Monthly Salary (current: {emp.MonthlySalary:N2}): ");
            input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input)) break;
            if (decimal.TryParse(input, out decimal salary) && salary > 0)
            {
                emp.MonthlySalary = salary;
                break;
            }
            Console.WriteLine("Invalid salary. Enter a positive number or press Enter to skip.");
        }

        while (true)
        {
            Console.Write($"Tax Rate % (current: {emp.TaxRate:F1}): ");
            input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input)) break;
            if (double.TryParse(input, out double taxRate) && taxRate >= 0 && taxRate <= 100)
            {
                emp.TaxRate = taxRate;
                break;
            }
            Console.WriteLine("Invalid tax rate. Enter 0-100 or press Enter to skip.");
        }

        while (true)
        {
            Console.Write($"Hire Date yyyy-MM-dd (current: {emp.HireDate:yyyy-MM-dd}): ");
            input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input)) break;
            if (DateTime.TryParseExact(input.Trim(), "yyyy-MM-dd",
                System.Globalization.CultureInfo.InvariantCulture,
                System.Globalization.DateTimeStyles.None, out DateTime hireDate))
            {
                emp.HireDate = hireDate;
                break;
            }
            Console.WriteLine("Invalid date. Use yyyy-MM-dd or press Enter to skip.");
        }

        while (true)
        {
            Console.Write($"Active? y/n (current: {(emp.IsActive ? "y" : "n")}): ");
            input = Console.ReadLine()?.Trim().ToLower();
            if (string.IsNullOrWhiteSpace(input)) break;
            if (input == "y" || input == "yes") { emp.IsActive = true; break; }
            if (input == "n" || input == "no") { emp.IsActive = false; break; }
            Console.WriteLine("Invalid input. Enter 'y', 'n', or press Enter to skip.");
        }

        while (true)
        {
            Console.Write($"Email (current: {emp.Email}): ");
            input = Console.ReadLine()?.Trim();
            if (string.IsNullOrWhiteSpace(input)) break;
            if (!input.Contains(" ") && input.IndexOf('@') > 0 &&
                input.LastIndexOf('.') > input.IndexOf('@') + 1 && !input.EndsWith("."))
            {
                emp.Email = input;
                break;
            }
            Console.WriteLine("Invalid email. Use name@example.com or press Enter to skip.");
        }

        Console.WriteLine("\nEmployee updated successfully!");
    }

    static void DeleteEmployee()
    {
        ReadEmployees();
        if (!employees.Any()) return;

        int id;
        while (true)
        {
            Console.Write("\nEnter the Id of the employee to delete: ");
            if (int.TryParse(Console.ReadLine(), out id) && id > 0) break;
            Console.WriteLine("Invalid Id. Enter a positive whole number.");
        }

        Employee emp = employees.FirstOrDefault(e => e.Id == id);
        if (emp == null)
        {
            Console.WriteLine("\nEmployee not found.");
            return;
        }

        while (true)
        {
            Console.Write($"Delete {emp.FirstName} {emp.LastName}? (y/n): ");
            string confirm = Console.ReadLine()?.Trim().ToLower();
            if (confirm == "y" || confirm == "yes")
            {
                employees.RemoveAll(e => e.Id == id);
                Console.WriteLine("\nEmployee deleted successfully.");
                return;
            }
            if (confirm == "n" || confirm == "no")
            {
                Console.WriteLine("\nDeletion cancelled.");
                return;
            }
            Console.WriteLine("Invalid input. Enter 'y' or 'n'.");
        }
    }

}
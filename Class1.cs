using System;
using System.Collections.Generic;
using System.IO;

namespace Assignment
{
    public class Employee
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public decimal Salary { get; set; }
        public long PF { get; set; }
    }

    public class EmployeeChar
    {
        public void Run()
        {
            string path = @"D:\Tutorial\DotNet\Sample.txt";

            // Ensure the directory exists
            Directory.CreateDirectory(Path.GetDirectoryName(path));

            List<Employee> employees = new List<Employee>
            {
                new Employee { Name = "Satish", Address = "Kathmandu", Salary = 2000, PF = 10000 },
                new Employee { Name = "Ram", Address = "Bhaktapur", Salary = 20000, PF = 100000 }
            };

            // Write employees to the file
            WriteEmployeesToFile(path, employees);

            // Read and parse employees from the file
            List<Employee> parsedEmployees = ReadEmployeesFromFile(path);

            // Display parsed employees
            foreach (var emp in parsedEmployees)
            {
                Console.WriteLine($"Name: {emp.Name}, Address: {emp.Address}, Salary: {emp.Salary}, PF: {emp.PF}");
            }
        }

        public void AddEmployee()
        {
            string path = @"D:\Tutorial\DotNet\Sample.txt";

            Console.WriteLine("Enter the name");
            string name = Console.ReadLine();
            Console.WriteLine("Enter the address");
            string address = Console.ReadLine();

            Console.WriteLine("Enter the salary");
            decimal salary;
            while (!decimal.TryParse(Console.ReadLine(), out salary))
            {
                Console.WriteLine("Invalid input. Please enter a valid salary.");
            }

            Console.WriteLine("Enter the PF");
            long pf;
            while (!long.TryParse(Console.ReadLine(), out pf))
            {
                Console.WriteLine("Invalid input. Please enter a valid PF.");
            }

            Employee newEmployee = new Employee
            {
                Name = name,
                Address = address,
                Salary = salary,
                PF = pf
            };

            // Read the existing list of employees from file
            List<Employee> employees = ReadEmployeesFromFile(path);

            // Add the new employee to the list
            employees.Add(newEmployee);

            // Write the updated list back to the file
            WriteEmployeesToFile(path, employees);

            Console.WriteLine("Employee added successfully.");
            foreach (var emp in employees)
            {
                Console.WriteLine($"Name: {emp.Name}, Address: {emp.Address}, Salary: {emp.Salary}, PF: {emp.PF}");
            }
        }

        private List<Employee> ReadEmployeesFromFile(string path)
        {
            List<Employee> parsedEmployees = new List<Employee>();
            foreach (var line in File.ReadAllLines(path))
            {
                var splitEmp = line.Split("||");
                if (splitEmp.Length == 4)
                {
                    parsedEmployees.Add(new Employee
                    {
                        Name = splitEmp[0],
                        Address = splitEmp[1],
                        Salary = decimal.Parse(splitEmp[2]),
                        PF = long.Parse(splitEmp[3])
                    });
                }
            }
            return parsedEmployees;
        }

        private void WriteEmployeesToFile(string path, List<Employee> employees)
        {
            using (StreamWriter writer = new StreamWriter(path, false))
            {
                foreach (var emp in employees)
                {
                    string formattedString = $"{emp.Name}||{emp.Address}||{emp.Salary}||{emp.PF}";
                    writer.WriteLine(formattedString);
                }
            }
        }
    }
}

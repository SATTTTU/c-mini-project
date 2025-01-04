using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Assignment
{
    public class SalaryCalculator
    {
        public void CalculateSalary()
        {
            string employeeFilePath = @"D:\Tutorial\DotNet\Sample.txt";
            string taxFilePath = @"D:\Tutorial\DotNet\EmployeeTax.txt";

            // Ensure files exist
            if (!File.Exists(employeeFilePath) || !File.Exists(taxFilePath))
            {
                Console.WriteLine("Required files are missing.");
                return;
            }

            // Read employees
            var employees = ReadEmployeesFromFile(employeeFilePath);

            Console.WriteLine("Enter the employee's name:");
            string employeeName = Console.ReadLine();

            // Find the employee
            var employee = employees.FirstOrDefault(e => e.Name.Equals(employeeName, StringComparison.OrdinalIgnoreCase));
            if (employee == null)
            {
                Console.WriteLine("Employee not found.");
                return;
            }

            // Calculate taxable income
            decimal yearlySalary = employee.Salary * 12;
            decimal taxableIncome = yearlySalary - employee.PF;

            Console.WriteLine($"Name: {employee.Name}");
            Console.WriteLine($"Total Yearly Salary: {yearlySalary}");
            Console.WriteLine($"Taxable Income (Total Salary - PF): {yearlySalary} - {employee.PF} = {taxableIncome}");

            // Read tax slabs
            var taxSlabs = ReadTaxSlabsFromFile(taxFilePath);

            // Calculate tax based on slabs
            decimal totalTax = 0;
            foreach (var slab in taxSlabs)
            {
                if (taxableIncome > slab.TaxableAmount)
                {
                    decimal slabTax = slab.TaxableAmount * slab.Rate / 100;
                    totalTax += slabTax;
                    taxableIncome -= slab.TaxableAmount;

                    Console.WriteLine($"Slab {slab.Slab}: {slab.TaxableAmount} x {slab.Rate}% = {slabTax}");
                }
                else
                {
                    decimal slabTax = taxableIncome * slab.Rate / 100;
                    totalTax += slabTax;

                    Console.WriteLine($"Slab {slab.Slab}: {taxableIncome} x {slab.Rate}% = {slabTax}");
                    break;
                }
            }

            Console.WriteLine($"Total Tax: {totalTax}");
            Console.WriteLine($"Net Yearly Income: {yearlySalary - totalTax}");
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

        private List<Incometax> ReadTaxSlabsFromFile(string path)
        {
            List<Incometax> parsedTaxInfo = new List<Incometax>();
            foreach (var line in File.ReadAllLines(path))
            {
                var splitTax = line.Split("||");
                if (splitTax.Length == 3)
                {
                    parsedTaxInfo.Add(new Incometax
                    {
                        Slab = int.Parse(splitTax[0]),
                        TaxableAmount = int.Parse(splitTax[1]),
                        Rate = int.Parse(splitTax[2])
                    });
                }
            }
            return parsedTaxInfo;
        }
    }
}


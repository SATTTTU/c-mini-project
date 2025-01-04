using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Assignment
{
    public class Program
    {
        public static void Main(string[] args)
        {
            while (true) // Infinite loop
            {
                Console.WriteLine("Enter your choice:");
                Console.WriteLine("1: Go to incometax list");
                Console.WriteLine("2: Go to Employee list with their monthly salary");
                Console.WriteLine("3: Add  new Employee");
                Console.WriteLine("4: Calculate  yearly Salary");
                Console.WriteLine("5: Exit");

                string val = Console.ReadLine();

                switch (val)
                {
                    case "1": // Comparing as string
                        slabEditor rot= new slabEditor();
                        rot.ManageTaxSlabs();
                        IncomeTaxEmployee value = new IncomeTaxEmployee();
                        value.Runfor();
                        break;

                    case "2": // Comparing as string
                        EmployeeChar van = new EmployeeChar();
                        van.Run();
                        break;

                    case "3":
                        EmployeeChar num = new EmployeeChar();
                        num.AddEmployee();
                        break;

                    case "4":
                        SalaryCalculator calc = new SalaryCalculator();
                        calc.CalculateSalary();
                        break;

                    case "5":
                        Console.WriteLine("Exiting program. Goodbye!");
                        return;

                    default:
                        Console.WriteLine("Invalid choice! Please enter a number between 1 and 5.");
                        break;
                }
            }
        }
    }
}

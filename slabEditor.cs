using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Assignment
{
    public class slabEditor
    {
        public void ManageTaxSlabs()
        {
            string taxFilePath = @"D:\Tutorial\DotNet\EmployeeTax.txt";

            // Ensure the file exists
            if (!File.Exists(taxFilePath))
            {
                Console.WriteLine("Tax file is missing.");
                return;
            }

            while (true)
            {
                Console.WriteLine("\nTax Slab Management:");
                Console.WriteLine("1. Add a new slab");
                Console.WriteLine("2. Remove an existing slab");
                Console.WriteLine("3. Exit");

                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddTaxSlab(taxFilePath);
                        break;
                    case "2":
                        RemoveTaxSlab(taxFilePath);
                        break;
                    case "3":
                        Console.WriteLine("Exiting slab management.");
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        private void AddTaxSlab(string taxFilePath)
        {
            Console.Write("Enter slab number: ");
            if (!int.TryParse(Console.ReadLine(), out int slab))
            {
                Console.WriteLine("Invalid input for slab number.");
                return;
            }

            Console.Write("Enter taxable amount: ");
            if (!int.TryParse(Console.ReadLine(), out int taxableAmount))
            {
                Console.WriteLine("Invalid input for taxable amount.");
                return;
            }

            Console.Write("Enter tax rate: ");
            if (!int.TryParse(Console.ReadLine(), out int rate))
            {
                Console.WriteLine("Invalid input for tax rate.");
                return;
            }

            // Append the new slab to the file
            using (StreamWriter writer = new StreamWriter(taxFilePath, true))
            {
                writer.WriteLine($"{slab}||{taxableAmount}||{rate}");
            }

            Console.WriteLine("Tax slab added successfully.");
        }

        private void RemoveTaxSlab(string taxFilePath)
        {
            // Read the existing slabs
            var taxSlabs = ReadTaxSlabsFromFile(taxFilePath);

            Console.WriteLine("Existing slabs:");
            foreach (var slab in taxSlabs)
            {
                Console.WriteLine($"Slab {slab.Slab}: Taxable Amount = {slab.TaxableAmount}, Rate = {slab.Rate}%");
            }

            Console.Write("Enter the slab number to remove: ");
            if (!int.TryParse(Console.ReadLine(), out int slabToRemove))
            {
                Console.WriteLine("Invalid input for slab number.");
                return;
            }

            // Filter out the slab to remove
            var updatedSlabs = taxSlabs.Where(s => s.Slab != slabToRemove).ToList();

            if (updatedSlabs.Count == taxSlabs.Count)
            {
                Console.WriteLine("Slab not found.");
                return;
            }

            // Write the updated slabs back to the file
            using (StreamWriter writer = new StreamWriter(taxFilePath, false))
            {
                foreach (var slab in updatedSlabs)
                {
                    writer.WriteLine($"{slab.Slab}||{slab.TaxableAmount}||{slab.Rate}");
                }
            }

            Console.WriteLine("Tax slab removed successfully.");
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


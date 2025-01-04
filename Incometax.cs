using System;
using System.Collections.Generic;
using System.IO;

namespace Assignment
{
    internal class Incometax
    {
        public int Slab { get; set; }
        public int TaxableAmount { get; set; }
        public int Rate { get; set; }
    }

    public class IncomeTaxEmployee
    {
        public void Runfor()
        {
            string path = @"D:\Tutorial\DotNet\EmployeeTax.txt";

            // Ensure the directory exists
            Directory.CreateDirectory(Path.GetDirectoryName(path));

            List<Incometax> taxInfo = new List<Incometax>
            {
                new Incometax { Slab = 1, TaxableAmount = 100000, Rate = 10 },
                new Incometax { Slab = 2, TaxableAmount = 150000, Rate = 20 },
                new Incometax { Slab = 3, TaxableAmount = 20000, Rate = 15 },
                new Incometax { Slab = 4, TaxableAmount = 3000, Rate = 5 },
            };

            // Write tax information to the file
            using (StreamWriter writer = new StreamWriter(path, false))
            {
                foreach (var val in taxInfo)
                {
                    string formattedString = $"{val.Slab}||{val.TaxableAmount}||{val.Rate}";
                    writer.WriteLine(formattedString);
                }
            }

            // Read and parse tax information from the file
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

            // Display parsed tax information
            foreach (var tax in parsedTaxInfo)
            {
                Console.WriteLine($"Slab: {tax.Slab}, Taxable Amount: {tax.TaxableAmount}, Rate: {tax.Rate}%");
            }
        }
    }
}

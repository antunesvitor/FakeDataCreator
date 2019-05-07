using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using CsvHelper;
using MatthiWare.CommandLine;
using MockingData.DataMockers;
using MockingData.Utils;

namespace MockingData
{
    class Program
    {
        static void Main(string[] args)
        {
            try{
                var parser = new CommandLineParser<Options>();

                var results = parser.Parse(args);

                if(results.HasErrors)
                {
                    Console.WriteLine("Had Errors parsing the arguments");
                    return;
                }

                var optionsArgs = results.Result;

                string[] coefficientsStrings = optionsArgs.coefficient.Split(',');
                double[] coefficientsDoubles = new double[coefficientsStrings.Length];
                for (int i = 0; i < coefficientsStrings.Length; i++)
                {
                    Console.WriteLine(coefficientsStrings[i]);
                    coefficientsDoubles[i] = Double.Parse(coefficientsStrings[i].Trim(), CultureInfo.InvariantCulture);
                    Console.WriteLine(coefficientsDoubles[i]);
                }

                if(optionsArgs.minimumValue > optionsArgs.maximumValue)
                    throw new Exception("The minimum value is greater than the maximum value");

                PolynomialMocker linearMocker = new PolynomialMocker(coefficientsDoubles, optionsArgs.noiseRange, optionsArgs.minimumValue, optionsArgs.maximumValue, optionsArgs.numberOfSamples);
                // PolynomialMocker linearMocker = new PolynomialMocker(new double[] { 12,-15, -8, 15,22 }, 0.5, -2, 2, 50);
                linearMocker.Mock();

                if (string.IsNullOrWhiteSpace(optionsArgs.filename))
                    optionsArgs.filename = "defaultName";

                using (var writer = new StreamWriter( optionsArgs.path +"\\"+ optionsArgs.filename + ".csv"))
                using (var csv = new CsvWriter(writer))
                {
                    csv.WriteRecords(linearMocker.values);
                }
            }
            catch(System.FormatException){
                Console.WriteLine("TASK FAILED: It failed to parse the coefficients string");
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}

using System;
using System.Collections.Generic;
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
            var parser = new CommandLineParser<Options>();

            var results = parser.Parse(args);

            if(results.HasErrors)
            {
                Console.WriteLine("Had Errors parsing the arguments");
                return;
            }

            var optionsArgs = results.Result;

            LinearMocker linearMocker = new LinearMocker(optionsArgs.coefficient,optionsArgs.constant,optionsArgs.noiseRange, optionsArgs.minimumValue, optionsArgs.maximumValue, optionsArgs.numberOfSamples);

            linearMocker.Mock();

            if (string.IsNullOrWhiteSpace(optionsArgs.filename))
                optionsArgs.filename = "defaultName";

            using (var writer = new StreamWriter( optionsArgs.path +"\\"+ optionsArgs.filename + ".csv"))
            using (var csv = new CsvWriter(writer))
            {
                csv.WriteRecords(linearMocker.values);
            }
        }
    }
}

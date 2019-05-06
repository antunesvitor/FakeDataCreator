using System;
using System.IO;
using CsvHelper;
using MockingData.DataMockers;

namespace MockingData
{
    class Program
    {
        static void Main(string[] args)
        {
            LinearMocker linearMocker = new LinearMocker(coefficient: 1.5, constant: 5, noiseRange: 2, minimumValue: 20, maximumValue: 200, numberOfSamples: 200);

            linearMocker.Mock();

            string filename;
            if(!string.IsNullOrWhiteSpace(args[0]))
                filename = args[0];
            else 
                filename = "defaultName";

            using(var writer = new StreamWriter("F:\\MockedData\\" + filename +".csv" ))
            using(var csv = new CsvWriter(writer)){
                csv.WriteRecords(linearMocker.values);
            }
        }
    }
}

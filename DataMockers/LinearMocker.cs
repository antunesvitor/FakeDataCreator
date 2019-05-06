using System;
using System.Collections.Generic;

namespace MockingData.DataMockers
{
    public class LinearMocker
    {
        private double _coeficient;
        private double _constant;
        private double _noiseRange;

        public double minValue;
        public double maxValue;
        public long numberSamples;

        public List<data> values;

        public LinearMocker(double coefficient = 0, double constant = 0, double noiseRange = 0, double minimumValue = 0, double maximumValue = 100, long numberOfSamples = 50)
        {
            _coeficient = coefficient;
            _constant = constant;
            _noiseRange = noiseRange;
            this.minValue = minimumValue;
            this.maxValue = maximumValue;
            this.numberSamples = numberOfSamples;
            values = new List<data>();
        }


        public void Mock()
        {
            double block = (maxValue - minValue) / numberSamples;
            Random rn = new Random();
            Console.WriteLine("Starting mocking...");
            Console.WriteLine($"Coefficient for x: {_coeficient}; constant for function: {_constant}");
            Console.WriteLine($"Minimum value for x: {minValue}; Maximum value for x: {maxValue}");
            Console.WriteLine($"noise range between {-1 * _noiseRange} and {_noiseRange}");
            Console.WriteLine($"number of samples: {numberSamples}");
            Console.WriteLine($"block size: {block}");

            for (double localMin = minValue, localMax = minValue + block;
                        localMax < maxValue + block / 2;
                        localMax += block, localMin += block)
            {
                // x será um valor em double aleatório entre localMin e localMax
                double x = localMin + rn.NextDouble() * block;

                // localNoise será algum número aleatório entre 
                double localNoise = (_noiseRange * -1) + rn.NextDouble() * (_noiseRange * 2);

                //      y         =     a * x       +   b       + ruido
                double localValue = _coeficient * x + _constant + localNoise;
                Console.WriteLine($"Local minimum: {localMin}; Local Maximum: {localMax}; x value: {x}; y value: {localValue} with noise {localNoise}");
                values.Add(new data(x, localValue));
            }
        }
    }

    public class data
    {
        public double x { get; set; }
        public double y { get; set; }

        public  data(double x, double y)
        {
            this.x = x;
            this.y = y;
        }
    }


}
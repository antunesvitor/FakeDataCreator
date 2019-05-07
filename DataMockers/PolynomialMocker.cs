using System;
using System.Collections.Generic;

namespace MockingData.DataMockers
{
    public class PolynomialMocker
    {
        private double[]  _coeficients;
        private double _noiseRange;
        public double minValue;
        public double maxValue;
        public long numberSamples;

        public List<data> values;

        public PolynomialMocker(double[] coefficient, double noiseRange = 0, double minimumValue = 0, double maximumValue = 100, long numberOfSamples = 50)
        {
            _coeficients = coefficient;
            _noiseRange = noiseRange;
            this.minValue = minimumValue;
            this.maxValue = maximumValue;
            this.numberSamples = numberOfSamples;
            values = new List<data>();
        }


        public void Mock()
        {
            double block = (Math.Abs(maxValue - minValue)) / numberSamples;
            Random rn = new Random();
            Console.WriteLine("Starting mocking...");
            Console.WriteLine($"Coefficient for x: {_coeficients}");
            Console.WriteLine($"Minimum value for x: {minValue}; Maximum value for x: {maxValue}");
            Console.WriteLine($"noise range between {-1 * _noiseRange} and {_noiseRange}");
            Console.WriteLine($"number of samples: {numberSamples}");
            Console.WriteLine($"block size: {block}");
            // printFunction();

            for (double localMin = minValue, localMax = minValue + block;
                        localMax < maxValue + block / 2;
                        localMax += block, localMin += block)
            {
                // x será um valor em double aleatório entre localMin e localMax
                double x = localMin + rn.NextDouble() * block;

                // localNoise será algum número aleatório entre 
                double localNoise = (_noiseRange * -1) + rn.NextDouble() * (_noiseRange * 2);
                double localValue  = 0;
                for (int i = 0; i < _coeficients.Length; i++)
                {
                    localValue += _coeficients[i] * Math.Pow(x, _coeficients.Length - i - 1);
                }

                localValue += localNoise;
                 
                // Console.WriteLine($"Local minimum: {localMin}; Local Maximum: {localMax}; x value: {x}; y value: {localValue} with noise {localNoise}");
                values.Add(new data(x, localValue));
            }
        }

        private void printFunction()
        {
            throw new NotImplementedException();
        }
    }
}
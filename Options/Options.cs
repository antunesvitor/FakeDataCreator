using MatthiWare.CommandLine.Core.Attributes;
namespace MockingData.Utils
{
    public class Options
    {
        [Name("C", "coefficient"), Description("The constant multiplier of x")]
        public double coefficient { get; set; }
        [Name("c","constant"), Description("The isolated constant")]
        public double constant { get; set; }
        [Name("n","noiserange"), Description("The noise limit")]
        public double noiseRange { get; set; }
        [Name("m", "minimum"), Description("The minimum value of x, the starting point of x")]
        public double minimumValue { get; set; }
        [Name("M","maximum"), Description("The maximum value of x, the ending point of x")]
        public double  maximumValue { get; set; }
        [Name("s","samples"), Description("The number of samples or records")]
        public int numberOfSamples { get; set; }
        [Required, Name("p","path"), Description("The path where the CSV file will be stored")]
        public string path { get; set; }
        [Name("f","filename"),Description("The name of the file")]
        public string filename { get; set; }
    }
}
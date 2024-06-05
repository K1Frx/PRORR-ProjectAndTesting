using PRORR.Utility;

namespace PRORR.ProgramConfiguration
{
    public class Task
    {
        public Polynomial Polynomial { get; set; }
        public FloatRange[] Ranges { get; set; }

        public static Task? GetTaskFromFile(string taskFileName)
        {
            try
            {
                string[] lines = File.ReadAllLines(taskFileName);
                string[] ranges = lines.Take(new Range(3, lines.Length)).ToArray();

                Polynomial polynomial = new Polynomial(ParseArray(lines[1]), ParseArray(lines[2]));
                FloatRange[] floatRanges = ranges.Select(GetRange).ToArray();

                return new Task
                {
                    Polynomial = polynomial,
                    Ranges = floatRanges
                };
            }
            catch (Exception e)
            {
                Console.WriteLine($"Invalid task file: {e.Message}");
                return null;
            }
        }

        private static float[] ParseArray(string line)
        {
            return line.Split(' ').Select(float.Parse).ToArray();
        }

        private static FloatRange GetRange(string range)
        {
            string[] parts = range.Split(' ');
            return new FloatRange(float.Parse(parts[0]), float.Parse(parts[1]));
        }
    }
}

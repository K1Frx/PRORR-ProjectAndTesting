using PRORR.Interfaces;

namespace PRORR.Implementation
{
    public class RandomGenerator : IRandomGenerator
    {
        private Random random;

        public RandomGenerator(Random random)
        {
            this.random = random;
        }

        public float Next(float minValue, float maxValue)
        {
            return (float)random.NextDouble() * (maxValue - minValue) + minValue;
        }

        public int Next(int minValue, int maxValue)
        {
            return random.Next(minValue, maxValue);
        }

        public float NextGaussian(float mean, float stdDev)
        {
            double u1 = 1.0 - random.NextDouble();
            double u2 = 1.0 - random.NextDouble();
            double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2);
            double randNormal = mean + stdDev * randStdNormal;
            return (float)randNormal;
        }

        public int NextWeighted(float[] weights)
        {
            float totalWeight = weights.Sum();
            float randomValue = (float)random.NextDouble() * totalWeight;

            for (int i = 0; i < weights.Length; i++)
            {
                randomValue -= weights[i];
                if (randomValue <= 0)
                {
                    return i;
                }
            }
            throw new InvalidOperationException("The sum of the weights must be greater than zero.");
        }
    }
}

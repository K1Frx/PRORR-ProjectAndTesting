using PRORR.Interfaces;
using PRORR.Utility;

namespace PRORR.Implementation
{
    public class RandomIndividualGenerator : IIndividualGenerator
    {
        private IRandomGenerator randomGenerator;
        private FloatRange[] ranges;

        public RandomIndividualGenerator(IRandomGenerator randomGenerator, FloatRange[] ranges)
        {
            this.randomGenerator = randomGenerator;
            this.ranges = ranges;
        }

        public Individual Create(int geneCount)
        {
            Individual individual = new Individual(geneCount);
            for (int i = 0; i < geneCount; i++)
            {
                individual.Genes[i] = randomGenerator.Next(ranges[i].Min, ranges[i].Max);
            }
            return individual;
        }
    }
}

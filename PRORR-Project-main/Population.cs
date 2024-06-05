using PRORR.Interfaces;

namespace PRORR
{
    public class Population
    {
        public List<Individual> Individuals { get; private set; }

        public Population(int populationSize, int geneCount, IIndividualGenerator individualGenerator)
        {
            Individuals = new List<Individual>(populationSize);
            for (int i = 0; i < populationSize; i++)
            {
                Individuals.Add(individualGenerator.Create(geneCount));
            }
        }

        public Population(int populationSize)
        {
            Individuals = new List<Individual>(new Individual[populationSize]);
        }
    }
}

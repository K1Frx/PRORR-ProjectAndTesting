using PRORR.Interfaces;

namespace PRORR.Implementation
{
    public class RouletteCrossover : ICrossover
    {
        private IRandomGenerator randomGenerator;
        private IMutationController mutationController;
        private int iteration = 0;
        private int threads = 1;

        public RouletteCrossover(int threads, IRandomGenerator randomGenerator, IMutationController mutationController)
        {
            this.mutationController = mutationController;
            this.threads = threads;
            this.randomGenerator = randomGenerator;
        }

        public Population Crossover(Population population)
        {
            Population newPopulation = new Population(population.Individuals.Count);
            float[] fitnesses = population.Individuals.Select(i => i.Fitness).ToArray();

            Parallel.For(0, population.Individuals.Count, new ParallelOptions { MaxDegreeOfParallelism = threads }, i =>
            {
                Individual parent1 = population.Individuals[randomGenerator.NextWeighted(fitnesses)];
                Individual parent2 = population.Individuals[randomGenerator.NextWeighted(fitnesses)];
                newPopulation.Individuals[i] = GetIndividual(parent1, parent2);
            });

            iteration++;
            return newPopulation;
        }

        private Individual GetIndividual(Individual parent1, Individual parent2)
        {
            float[] genes = new float[parent1.Genes.Length];
            for (int i = 0; i < genes.Length; i++)
            {
                genes[i] = randomGenerator.Next(0, 1) < 0.5f ? parent1.Genes[i] : parent2.Genes[i];
            }

            Individual individual = new Individual(genes);

            for (int i = 0; i < genes.Length; i++)
            {
                if (randomGenerator.Next(0, 1) < mutationController.GetMutationRate(iteration))
                {
                    mutationController.Mutate(individual);
                }
            }
            return individual;
        }
    }
}

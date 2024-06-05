using PRORR.Implementation;
using PRORR.Interfaces;

namespace PRORR.ProgramConfiguration
{
    public class Configuration
    {
        public int PopulationSize { get; set; }
        public int ChromosomeLength { get; set; }
        public int MaxGenerations { get; set; }
        public IIndividualGenerator IndividualGenerator { get; set; }
        public IEvaluator Evaluator { get; set; }
        public ICrossover Crossover { get; set; }
        public IRandomGenerator RandomGenerator { get; set; }

        public static Configuration? GetConfiguration(string[] args)
        {
            if (args.Length != 3)
            {
                Console.WriteLine("Invalid number of arguments");
                return null;
            }

            int threads;
            if (!int.TryParse(args[0], out threads) || threads < 1)
            {
                Console.WriteLine("Invalid number of threads");
                return null;
            }

            string taskFileName = args[1];
            Task? task = Task.GetTaskFromFile(taskFileName);
            if (task == null)
            {
                return null;
            }

            string configurationFileName = args[2];
            AlgorithmConfiguration? algorithmConfiguration = AlgorithmConfiguration.LoadFromJson(configurationFileName);
            if (algorithmConfiguration == null)
            {
                return null;
            }

            IRandomGenerator randomGenerator = new RandomGenerator(new Random());
            IMutationController mutationController = new DefaultMutationController(algorithmConfiguration.MutationRate, algorithmConfiguration.MutationMean, algorithmConfiguration.MutationStdDev, randomGenerator);

            Configuration configuration = new Configuration();
            configuration.MaxGenerations = algorithmConfiguration.MaxGenerations;
            configuration.PopulationSize = algorithmConfiguration.PopulationSize;
            configuration.ChromosomeLength = task.Ranges.Length;
            configuration.IndividualGenerator = new RandomIndividualGenerator(randomGenerator, task.Ranges);
            configuration.Crossover = new RouletteCrossover(threads, randomGenerator, mutationController);
            configuration.Evaluator = new PolynomialEvaluator(threads, task.Polynomial);
            return configuration;
        }
    }
}

using PRORR;
using PRORR.Implementation;
using PRORR.Interfaces;

namespace Tests
{
    public class MutationControllerUnitTests
    {
        IMutationController mutationController;
        IRandomGenerator randomGenerator;
        float mutation_rate;
        float mean;
        float stddev;

        [SetUp]
        public void Setup()
        {
            Random random = new Random(44);
            IRandomGenerator randomGenerator = new RandomGenerator(random);
            mutation_rate = 0.1f;
            mean = 0f;
            stddev = 1f;
            mutationController = new DefaultMutationController(mutation_rate, mean, stddev, randomGenerator);
        }

        [Test]
        public void GetMutationRate_Returns_CorrectValue_WhenIterationIsZero()
        {
            // Arrange
            int iteration = 0;
            float expected = 0.1f;

            // Act
            float result = mutationController.GetMutationRate(iteration);

            // Assert
            Assert.That( result, Is.EqualTo(expected));
        }

        [Test]
        public void GetMutationRate_Returns_CorrectValue_WhenIterationIs1000()
        {
            // Arrange
            int iteration = 1000;
            float expected = 0.1f * (1 - (float)iteration / 1000);

            // Act
            float result = mutationController.GetMutationRate(iteration);

            // Assert
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void Mutate_UpdatesIndividualGenes()
        {
            // Arrange
            Individual individual = new Individual(new float[] { 1, 2, 3, 4 });

            // Act
            Individual mutated_individual = new Individual(new float[] { 1, 2, 3, 4 });
            mutationController.Mutate(mutated_individual);

            // Assert
            Assert.That(mutated_individual, Is.Not.EqualTo(individual));

        }
    }
}
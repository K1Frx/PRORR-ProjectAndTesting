using PRORR;
using PRORR.Implementation;
using PRORR.Interfaces;
using PRORR.Utility;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Text.Unicode;
using System.Threading.Tasks;

namespace Tests
{
    internal class GeneticAlgorithmUnitTests
    {
        IEvaluator polynomialEvaluator;
        Polynomial polynomial;
        IIndividualGenerator individualGenerator;
        Population population;
        IRandomGenerator randomGenerator;
        FloatRange[] floatRange;
        ICrossover crossover;

        [SetUp]
        public void Setup()
        {
            Random random = new Random(44);
            randomGenerator = new RandomGenerator(random);
        }

        [Test]
        public void EvaluatePopulation_Returns_CorrectValue_WhenThreeArguments_OneThread()
        {
            // Arrange
            floatRange = new FloatRange[] { new FloatRange(0, 10), new FloatRange(0, 10), new FloatRange(0, 10) };
            polynomial = new Polynomial(new float[] { 1, 2, 3 }, new float[] { 1, 2, 3 });
            individualGenerator = new RandomIndividualGenerator(randomGenerator, floatRange);
            population = new Population(10, 3, individualGenerator);
            float[] expected = new float[] { 0.0018401401f, 0.09506047f, 0.00352725f, 0.0005708391f, 0.00068329566f, 0.010711588f, 0.001100657f, 0.004154981f, 0.0006789678f, 0.0019158638f};
            int threads = 1;
            polynomialEvaluator = new PolynomialEvaluator(threads, polynomial);

            // Act
            polynomialEvaluator.EvaluatePopulation(population);

            Individual[] population_array = population.Individuals.ToArray();
            population.Individuals.ForEach(i => { Console.Write($"{i.ToString()}, "); });

            float[] actual = new float[10];
            for (int i = 0; i < actual.Length; i++) {
                actual[i] = population_array[i].Fitness;
            }

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void EvaluatePopulation_Returns_CorrectValue_WhenThreeArguments_FiveThreads()
        {
            // Arrange
            floatRange = new FloatRange[] { new FloatRange(0, 10), new FloatRange(0, 10), new FloatRange(0, 10) };
            polynomial = new Polynomial(new float[] { 1, 2, 3 }, new float[] { 1, 2, 3 });
            individualGenerator = new RandomIndividualGenerator(randomGenerator, floatRange);
            population = new Population(10, 3, individualGenerator);
            float[] expected = new float[] { 0.0018401401f, 0.09506047f, 0.00352725f, 0.0005708391f, 0.00068329566f, 0.010711588f, 0.001100657f, 0.004154981f, 0.0006789678f, 0.0019158638f };
            int threads = 5;
            polynomialEvaluator = new PolynomialEvaluator(threads, polynomial);

            // Act
            polynomialEvaluator.EvaluatePopulation(population);

            Individual[] population_array = population.Individuals.ToArray();
            population.Individuals.ForEach(i => { Console.Write($"{i.ToString()}, "); });

            float[] actual = new float[10];
            for (int i = 0; i < actual.Length; i++)
            {
                actual[i] = population_array[i].Fitness;
            }

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void EvaluatePopulation_Returns_CorrectValue_WhenOneArgument_FiveThreads()
        {
            // Arrange
            floatRange = new FloatRange[] { new FloatRange(0, 10) };
            polynomial = new Polynomial(new float[] { 1 }, new float[] { 1 });
            individualGenerator = new RandomIndividualGenerator(randomGenerator, floatRange);
            population = new Population(10, 1, individualGenerator);
            float[] expected = new float[] { 0.14026089f, 0.13741086f, 0.19102407f, 0.10530194f, 1.4004483f, 9.6262245f, 0.1611547f, 0.19368899f, 0.23748197f, 0.20050015f };
            int threads = 5;
            polynomialEvaluator = new PolynomialEvaluator(threads, polynomial);

            // Act
            polynomialEvaluator.EvaluatePopulation(population);

            Individual[] population_array = population.Individuals.ToArray();
            population.Individuals.ForEach(i => { Console.Write($"{i.ToString()}, "); });

            float[] actual = new float[10];
            for (int i = 0; i < actual.Length; i++)
            {
                actual[i] = population_array[i].Fitness;
            }

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }
        [Test]
        public void Create_Returns_CorrectValue_WhenOneGene()
        {
            // Arrange 
            floatRange = new FloatRange[] { new FloatRange(0, 10) };
            polynomial = new Polynomial(new float[] { 1 }, new float[] { 1 });
            individualGenerator = new RandomIndividualGenerator(randomGenerator, floatRange);
            int gene_count = 1;
            float[] genes = { 7.129571f };
            Individual expected = new Individual(genes);

            // Act
            Individual actual = individualGenerator.Create(gene_count);

            // Assert
            Assert.That(actual.Genes, Is.EqualTo(expected.Genes));

        }

        [Test]
        public void Create_Returns_CorrectValue_WhenFiveGene()
        {
            // Arrange 
            floatRange = new FloatRange[] { new FloatRange(0, 10) , new FloatRange(0, 10) , new FloatRange(0, 10) , new FloatRange(0, 10) , new FloatRange(0, 10) };
            polynomial = new Polynomial(new float[] { 1 }, new float[] { 1 });
            individualGenerator = new RandomIndividualGenerator(randomGenerator, floatRange);
            int gene_count = 5;
            float[] genes = { 7.129571f, 7.2774453f, 5.2349424f, 9.496501f, 0.714057f };
            Individual expected = new Individual(genes);

            // Act
            Individual actual = individualGenerator.Create(gene_count);

            // Assert
            Assert.That(actual.Genes, Is.EqualTo(expected.Genes));
        }

        [Test]
        public void Crossover_Returns_DifferentValue_OneThread()
        {
            // Arrange
            floatRange = new FloatRange[] { new FloatRange(0, 10), new FloatRange(0, 10), new FloatRange(0, 10) };
            polynomial = new Polynomial(new float[] { 1, 2, 3 }, new float[] { 1, 2, 3 });
            individualGenerator = new RandomIndividualGenerator(randomGenerator, floatRange);
            population = new Population(100, 3, individualGenerator);
            IMutationController mutationController = new DefaultMutationController(0.1f, 0f, 0.01f, randomGenerator);
            int threads = 1;
            crossover = new RouletteCrossover(threads, randomGenerator, mutationController);

            // Act
            Population new_population = crossover.Crossover(population);

            // Assert
            Assert.That(new_population, Is.Not.EqualTo(population));
        }

        [Test]
        public void Crossover_Returns_DifferentValue_FiveThread()
        {
            // Arrange
            floatRange = new FloatRange[] { new FloatRange(0, 10), new FloatRange(0, 10), new FloatRange(0, 10) };
            polynomial = new Polynomial(new float[] { 1, 2, 3 }, new float[] { 1, 2, 3 });
            individualGenerator = new RandomIndividualGenerator(randomGenerator, floatRange);
            population = new Population(100, 3, individualGenerator);
            IMutationController mutationController = new DefaultMutationController(0.1f, 0f, 0.01f, randomGenerator);
            int threads = 5;
            crossover = new RouletteCrossover(threads, randomGenerator, mutationController);

            // Act
            Population new_population = crossover.Crossover(population);

            // Assert
            Assert.That(new_population, Is.Not.EqualTo(population));
        }

        [Test]
        public void Calculate_Returns_CorrectValue_OneArgument()
        {
            // Arrange
            polynomial = new Polynomial(new float[] { 1 }, new float[] { 2 });
            int argument = 5;
            float expected = argument * argument;

            // Act
            float actual = polynomial.Calculate(new float[] { (float)argument });

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void Calculate_Returns_CorrectValue_FiveArguments()
        {
            // Arrange
            polynomial = new Polynomial(new float[] { 1,2,3,4,5 }, new float[] { 2,2,2,2,2 });
            float[] arguments = { 1, 2, 3, 4, 5 };
            float expected = 225;

            // Act
            float actual = polynomial.Calculate(arguments);

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void Calculate_Returns_CorrectValue_EightArguments()
        {
            // Arrange
            polynomial = new Polynomial(new float[] { 1, 2, 3, 4, 5, 6, 7, 8 }, new float[] { 2, 3, 4, 5, 2, 3, 4, 5});
            float[] arguments = { 1, 2, 3, 4, 3, 2, 1, 5};
            float expected = 29456;

            // Act
            float actual = polynomial.Calculate(arguments);

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

    }
}

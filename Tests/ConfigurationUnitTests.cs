using PRORR.ProgramConfiguration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    internal class ConfigurationUnitTests
    {
        AlgorithmConfiguration algorithmConfiguration;

        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void ReadFromJSON_Returns_CorrectValue_WhenCorrectPath()
        {
            // Arrange
            string path = "C:\\Users\\Kamil\\Desktop\\Nowy folder\\PRORR-ProjectAndTesting\\Tests\\files\\configuration";
            float expectedMaxGenerations = 1;
            float expectedMutationStdDev = 1;
            float expectedMutationMean = 1;
            float expectedPopulationSize = 1;
            float expectedMutationRate = 1;

            // Act
            AlgorithmConfiguration algorithmConfiguration = AlgorithmConfiguration.LoadFromJson(path);

            // Assert
            Assert.That(algorithmConfiguration.MaxGenerations, Is.EqualTo(expectedMaxGenerations));
            Assert.That(algorithmConfiguration.MutationStdDev, Is.EqualTo(expectedMutationStdDev));
            Assert.That(algorithmConfiguration.MutationMean, Is.EqualTo(expectedMutationMean));
            Assert.That(algorithmConfiguration.PopulationSize, Is.EqualTo(expectedPopulationSize));
            Assert.That(algorithmConfiguration.MutationRate, Is.EqualTo(expectedMutationRate));
        }

        [Test]
        public void GetConfiguration_Returns_CorrectValue_WhenCorrectPath()
        {
            // Arrange
            string[] args = { "1", "C:\\Users\\Kamil\\Desktop\\Nowy folder\\PRORR-ProjectAndTesting\\PRORR-Project-main\\example\\task" , "C:\\Users\\Kamil\\Desktop\\Nowy folder\\PRORR-ProjectAndTesting\\PRORR-Project-main\\example\\config" };

            // Act
            Configuration configuration = Configuration.GetConfiguration(args);

            // Assert
            Assert.That(configuration, Is.Not.Null);
        }
    }
}

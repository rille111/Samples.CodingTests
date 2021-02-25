using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AlienLanguageInterpreter.Models;
using AlienLanguageInterpreter.Services;

namespace AlienLanguageInterpreter.Tests.Components
{
    [TestClass]
    public class ConfigParserTests
    {
        [TestMethod]
        public void When_providing_input_then_config_parser_should_extract_it()
        {
            // Arrange
            var L = 1;
            var D = 2;
            var N = 3;
            var userInput = string.Format("{0} {1} {2}", L, D, N);
            ConfigParser parser = new ConfigParser(userInput);

            // Act
            var config = parser.Parse();

            // Assert
            Assert.AreEqual(L, config.L_WordLength);
            Assert.AreEqual(D, config.D_WordsCount);
            Assert.AreEqual(N, config.N_TestCasesCount);
        }
    }
}

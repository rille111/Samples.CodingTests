using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AlienLanguageInterpreter.Models;
using AlienLanguageInterpreter.Services;
using System.Collections.Generic;
using AlienLanguageInterpreter.Interfaces;
using Moq;

namespace AlienLanguageInterpreter.Tests.Specs
{
    // Alternativt sätt att skriva tester i 'BDD' stil.
    // SpecFlow skulle vara mycket snyggare att skriva detta i, så detta är fattig-mans SpecFlows. :)
    [TestClass]
    public class LanguageComputerTests
    {
        InputConfig _config;
        Mock<IUserInterface> _ui;
        LanguageComputer _computer;
        List<TestCaseResult> _testResults;

        [TestMethod]
        public void When_computer_executes_then_test_cases_should_be_right()
        {
            // Arrange
            Given_a_user_interface();
            Given_a_3_5_4_configuration();
            Given_a_computer_with_that_config();

            // Act
            When_computer_executes();

            // Assert
            Then_test_cases_should_be_right();
        }

        private void Given_a_3_5_4_configuration()
        {
            _config = new InputConfig();
            _config.L_WordLength = 3;
            _config.D_WordsCount = 5;
            _config.N_TestCasesCount = 4;
        }

        private void Given_a_user_interface()
        {
            _ui = new Mock<IUserInterface>(MockBehavior.Strict);
            _ui.Setup(p => p
                .CollectWords(5))
                .Returns(new List<string> { "abc", "bca", "dac", "dbc", "cba" });
            _ui.Setup(p => p
                .CollectTestCases(4))
                .Returns(new List<string> { "(ab)(bc)(ca)", "abc", "(abc)(abc)(abc)", "(zyx)bc" });
        }

        private void Given_a_computer_with_that_config()
        {
            _computer = new LanguageComputer(_config);
            _computer.AlienWords = _ui.Object.CollectWords(_config.D_WordsCount);
            _computer.TestCases = _ui.Object.CollectTestCases(_config.N_TestCasesCount);
        }

        private void When_computer_executes()
        {
            _testResults = _computer.ExecuteTestsCases();
        }


        private void Then_test_cases_should_be_right()
        {
            Assert.AreEqual(4, _testResults.Count);
            Assert.AreEqual(1, _testResults[0].CaseNumber);
            Assert.AreEqual(2, _testResults[1].CaseNumber);
            Assert.AreEqual(3, _testResults[2].CaseNumber);
            Assert.AreEqual(4, _testResults[3].CaseNumber);

            _ui.VerifyAll();

            Assert.AreEqual(2, _testResults[0].MatchingWords.Count);
            Assert.AreEqual(1, _testResults[1].MatchingWords.Count);
            Assert.AreEqual(3, _testResults[2].MatchingWords.Count);
            Assert.AreEqual(0, _testResults[3].MatchingWords.Count);


        }


    }
}

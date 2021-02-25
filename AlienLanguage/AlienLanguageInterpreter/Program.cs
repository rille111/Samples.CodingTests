using AlienLanguageInterpreter.Interfaces;
using AlienLanguageInterpreter.Models;
using AlienLanguageInterpreter.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlienLanguageInterpreter
{
    class Program
    {
        static InputConfig config = null;
        static LanguageComputer langComputer = null;
        static ConfigParser configParser = null;
        // Låtsas att vi resolvar IUserInterface med önskad IoC Container. Pga tiden skipper jag IoC. :)
        static IUserInterface ui;

        static void Main(string[] args)
        {
            try
            {
                CollectConfiguration();
                CollectWordsAndTestcases();
                ExecuteTestCases();
                WaitAndExit();
            }
            catch (Exception ex)
            {
                ui.Print(ex.Message);
                ui.WaitUntilKeyPress();
                Environment.Exit(1);
            }
        }

        private static void CollectConfiguration()
        {
            ui = new ConsoleUserInterface();
            var input = ui.CollectInput();
            configParser = new ConfigParser(input);
            config = configParser.Parse();
        }
        
        private static void CollectWordsAndTestcases()
        {
            langComputer = new LanguageComputer(config);
            langComputer.AlienWords = ui.CollectWords(config.D_WordsCount);
            langComputer.TestCases = ui.CollectTestCases(config.N_TestCasesCount);
        }

        private static void ExecuteTestCases()
        {
            var testResults = langComputer.ExecuteTestsCases();
            ui.Print(testResults);
        }

        private static void WaitAndExit()
        {
            ui.Print("Press the 'ANY' key to exit .. now where is it?");
            ui.WaitUntilKeyPress();
            Environment.Exit(0);
        }
    }
}

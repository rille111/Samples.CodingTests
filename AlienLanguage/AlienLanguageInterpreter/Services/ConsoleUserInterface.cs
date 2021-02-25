using AlienLanguageInterpreter.Interfaces;
using AlienLanguageInterpreter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlienLanguageInterpreter.Services
{
    public class ConsoleUserInterface : IUserInterface
    {
        public void Print(List<TestCaseResult> results)
        {
            foreach (var result in results)
            {
                Console.WriteLine(string.Format("Case #{0}: {1}"
                    , result.CaseNumber
                    , result.MatchingWords.Count()));
            }
        }

        public string CollectInput()
        {

            Console.WriteLine("Please provide configuration ...");
            return Console.ReadLine();
        }

        public List<string> CollectWords(int howMany)
        {
            Console.WriteLine("Please input the known alien words ..");
            // Fuskar inte med Resharper, som antagligen hade gjort en snygg oneliner annars. :)
            var words = new List<string>(howMany);
            for (int i = 0; i < howMany; i++)
            {
                words.Add(Console.ReadLine());
            }
            return words;
        }

        public List<string> CollectTestCases(int howMany)
        {
            Console.WriteLine("Please input the test cases ..");
            var words = new List<string>(howMany);
            for (int i = 0; i < howMany; i++)
            {
                words.Add(Console.ReadLine());
            }
            return words;
        }

        public void Print(string what, params object[] parameters)
        {
            Console.WriteLine(what, parameters);
        }

        public void WaitUntilKeyPress()
        {
            Console.ReadKey();
        }
    }
}

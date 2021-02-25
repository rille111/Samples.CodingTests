using AlienLanguageInterpreter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AlienLanguageInterpreter.Services
{
    public class LanguageComputer
    {
        public InputConfig Config { get; set; }
        public List<string> AlienWords { get; set; }
        public List<string> TestCases { get; set; }

        public LanguageComputer(InputConfig config)
        {
            this.Config = config;
            // Väntar bara på Roslyn där man kan använda props med default values, slippa ctor initialisering. :)
            this.TestCases = new List<string>();
            this.AlienWords = new List<string>();
        }

        public List<TestCaseResult> ExecuteTestsCases()
        {
            var results = new List<TestCaseResult>();
            int caseNumber = 0;
            foreach (var testCase in TestCases)
            {
                caseNumber++;
                var result = new TestCaseResult();
                result.CaseNumber = caseNumber;
                result.TestCase = testCase;
                result.MatchingWords = new List<string>();
                results.Add(result);
                foreach (var word in AlienWords)
                {
                    var regexReadyTestCase = "^" + testCase.Replace("(", "[").Replace(")", "]") + "$";
                    if (Regex.Match(word, regexReadyTestCase).Success)
                    {
                        result.MatchingWords.Add(word);
                    }
                }
            }
            return results;
        }
    }
}

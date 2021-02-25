using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlienLanguageInterpreter.Models
{
    public class TestCaseResult
    {
        public int CaseNumber { get; set; }
        public string TestCase { get; set; }
        public List<string> MatchingWords { get; set; }
    }
}

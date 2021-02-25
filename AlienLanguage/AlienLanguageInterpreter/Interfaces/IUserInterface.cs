using AlienLanguageInterpreter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlienLanguageInterpreter.Interfaces
{
    public interface IUserInterface
    {
        void Print(List<TestCaseResult> results);
        string CollectInput();
        List<string> CollectWords(int howMany);
        List<string> CollectTestCases(int howMany);
        void Print(string what,  params object[] parameters);
        void WaitUntilKeyPress();
    }
}

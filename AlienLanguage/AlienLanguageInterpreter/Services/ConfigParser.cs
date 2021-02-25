using AlienLanguageInterpreter.Models;
using System;
using System.Text.RegularExpressions;

namespace AlienLanguageInterpreter.Services
{
    /// <summary>
    ///  Code never lies, Comments do. :)
    /// </summary>
    public class ConfigParser
    {
        private string _userInput;
        private Match _regexResult;
        InputConfig _paramz;
        string _inputValidRegexPattern = @"^([0-9]+)\s([0-9]+)\s([0-9]+)$";

        public ConfigParser(string userInput)
        {
            this._userInput = userInput;
        }

        public InputConfig Parse()
        {
            if (ExtractToRegexSucceeds() && RulesAreValid())
            {
                return BuildParams();
            }
            else
            {
                throw SomeCustomException();
            }
        }

        private Exception SomeCustomException()
        {
            // Bygg en egen exception t.ex ParamsExtractorException
            return new Exception(
              string.Format("Unable to extract params, input was invalid! {0}", this._userInput));
        }

        private InputConfig BuildParams()
        {
            _paramz = new InputConfig
            {
                L_WordLength = int.Parse(_regexResult.Groups[1].Value),
                D_WordsCount = int.Parse(_regexResult.Groups[2].Value),
                N_TestCasesCount = int.Parse(_regexResult.Groups[3].Value)
            };
            return _paramz;
        }

        private bool ExtractToRegexSucceeds()
        {
                _regexResult = Regex.Match(this._userInput, _inputValidRegexPattern);
                return _regexResult.Success && _regexResult.Groups.Count == 4;
        }

        private bool RulesAreValid()
        {
            var L = int.Parse(_regexResult.Groups[1].Value);
            var D = int.Parse(_regexResult.Groups[2].Value);
            var N = int.Parse(_regexResult.Groups[3].Value);

            var valid =
                1 <= L && L <= 10 &&
                1 <= D && D <= 25 &&
                1 <= N && N <= 10;
            return valid;
        }
    }
}

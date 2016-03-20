using Anything.Common;
using System;
using System.Collections.Generic;

namespace Tester
{
    public class Tester : IPlugin
    {
        public IEnumerable<IResult> Process(string input)
        {
            var tokens = input.Trim().Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            if (tokens[0] == "+" && tokens.Length == 3)
            {
                return new IResult[] { new TesterResult(1, tokens[1] + tokens[2]) };
            }

            return null;
        }
    }
}

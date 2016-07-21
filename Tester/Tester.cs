using Anything.Shared;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Primitives;

namespace Tester
{
    [Export(typeof(IPlugin))]
    public class Tester : IPlugin
    {
        public IEnumerable<IResult> Process(string input)
        {
            return null;
        }
    }
}

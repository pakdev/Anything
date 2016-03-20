using System.Collections.Generic;

namespace Anything.Common
{
    public interface IPlugin
    {
        IEnumerable<IResult> Process(string input);
    }
}

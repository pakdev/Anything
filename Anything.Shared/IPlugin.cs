using System.Collections.Generic;

namespace Anything.Shared
{
    public interface IPlugin
    {
        IEnumerable<IResult> Process(string input);
    }
}

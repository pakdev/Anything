using Anything.Common;
using System.Collections.Generic;

namespace Anything.Results
{
    public interface IResultService
    {
        void UpdateResults(IEnumerable<IResult> results);
    }
}

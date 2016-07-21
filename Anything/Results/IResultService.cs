using Anything.Shared;
using System;
using System.Collections.Generic;

namespace Anything.Results
{
    public interface IResultService
    {
        event EventHandler<ResultsUpdatedEventArgs> ResultsUpdated; 

        IEnumerable<IResult> Results { get; } 

        void UpdateResults(IEnumerable<IResult> results);
    }
}

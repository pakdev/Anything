using Anything.Shared;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Anything.Results
{
    public class ResultService : IResultService
    {
        public event EventHandler<ResultsUpdatedEventArgs> ResultsUpdated = delegate {};

        public IEnumerable<IResult> Results { get; private set; }

        public void UpdateResults(IEnumerable<IResult> results)
        {
            if (results == null)
            {
                this.Results = null;
                this.ResultsUpdated(this, new ResultsUpdatedEventArgs(0));
            }
            else
            {
                var resultArray = results.ToArray();
                this.Results = resultArray;
                this.ResultsUpdated(this, new ResultsUpdatedEventArgs(resultArray.Length));
            }
        }
    }
}

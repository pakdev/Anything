using System;

namespace Anything.Results
{
    public class ResultsUpdatedEventArgs : EventArgs
    {
        public ResultsUpdatedEventArgs(int numResults)
        {
            this.NumResults = numResults;
        }

        public int NumResults { get; private set; }
    }
}

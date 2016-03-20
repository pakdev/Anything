using Anything.Common;

namespace Tester
{
    [Plugin(typeof(IPlugin), typeof(TesterResult))]
    public class TesterResult : IResult
    {
        public TesterResult(uint rank, string value)
        {
            this.Rank = rank;
            this.Value = value;
        }

        public uint Rank { get; internal set; }

        public string Value { get; internal set; }
    }
}

using System.Windows.Input;
using Anything.Shared;

namespace Tester
{
    public class TesterResult : IResult
    {
        public TesterResult() : this(0, string.Empty)
        {
        }

        public TesterResult(uint rank, string value)
        {
            this.Rank = rank;
            this.Value = value;
        }

        public ICommand Launch { get; }

        public uint Rank { get; internal set; }

        public string Value { get; internal set; }
    }
}

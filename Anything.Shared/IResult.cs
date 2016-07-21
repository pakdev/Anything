using System.Windows.Input;

namespace Anything.Shared
{
    public interface IResult
    {
        uint Rank { get; }

        ICommand Launch { get; }
    }
}

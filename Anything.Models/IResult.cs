using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anything.Models
{
    public enum ResultType
    {
        App,
        File,
        Folder
    }

    public interface IResult
    {
        uint Rank { get; set; }
        string Name { get; set; }
        string Path { get; set; }
        ResultType Type { get; set; }
    }
}

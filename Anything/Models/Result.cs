using System.Diagnostics;
using System.Threading.Tasks;

namespace Anything.Models
{
    public class Result : IResult
    {
        public uint Rank { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public ResultType Type { get; set; }

        public static Task Open(IResult result)
        {
            ProcessStartInfo startInfo = null;

            switch (result.Type)
            {
                case ResultType.App:
                case ResultType.Folder:
                    startInfo = new ProcessStartInfo
                    {
                        FileName = result.Path
                    };                                              
                    break;
                case ResultType.File:
                    // Open an explorer window with the file selected
                    startInfo = new ProcessStartInfo
                    {
                        FileName = $"explorer /n, /select,{result.Path}"
                    };
                    break;
            }

            var process = new Process { StartInfo = startInfo };

            return Task.Run(() =>
                {
                    process.Start();
                    process.WaitForExit();
                });
        }
    }
}

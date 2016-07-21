using Anything.Shared;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Anything.Models
{
    public enum FsObjectType
    {
        App,
        File,
        Folder,

    }

    public class FsResult
    {
        public uint Rank { get; set; }

        public string Name { get; set; }
        public string Path { get; set; }
        public FsObjectType Type { get; set; }

        public static Task Open(FsResult result)
        {
            ProcessStartInfo startInfo = null;

            switch (result.Type)
            {
                case FsObjectType.App:
                case FsObjectType.Folder:
                    startInfo = new ProcessStartInfo
                    {
                        FileName = result.Path
                    };
                    break;
                case FsObjectType.File:
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

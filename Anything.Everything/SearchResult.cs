using System;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Anything.Everything
{
    public enum SearchResultType
    {
        App,
        File,
        Folder
    }

    public class SearchResult
    {
        public SearchResult(string path, SearchResultType type)
        {
            this.Name = System.IO.Path.GetFileNameWithoutExtension(path);
            var match = Regex.Match(this.Name, @"\d+ - (.+)");
            if (match.Success)
            {
                this.Name = match.Groups[1].Value;
            }
            this.Path = path;
            this.Type = type;
            this.IconPath = path;

            if (this.Path.ToUpperInvariant().EndsWith(".LNK"))
            {
                // follow the lnk to get the real icon
                this.IconPath = this.GetLnkTarget(path);
            }
        }

        public string Name { get; private set; }
        public string Path { get; }
        public string IconPath { get; private set; }
        public SearchResultType Type { get; }

        public Task Open()
        {
            ProcessStartInfo startInfo = null;

            switch (this.Type)
            {
                case SearchResultType.App:
                case SearchResultType.Folder:
                    startInfo = new ProcessStartInfo
                    {
                        FileName = this.Path
                    };
                    break;
                case SearchResultType.File:
                    // Open an explorer window with the file selected
                    startInfo = new ProcessStartInfo
                    {
                        FileName = $"explorer /n, /select,{this.Path}"
                    };
                    break;
                default:
                    return Task.Run(() => { });
            }

            var process = new Process { StartInfo = startInfo };

            return Task.Run(() =>
            {
                process.Start();
                process.WaitForExit();
            });
        }

        // Code comes from: https://blez.wordpress.com/2013/02/18/get-file-shortcuts-target-with-c/
        private string GetLnkTarget(string pathLnk)
        {
            try
            {
                var fileStream = File.Open(pathLnk, FileMode.Open, FileAccess.Read);
                using (var fileReader = new BinaryReader(fileStream))
                {
                    fileStream.Seek(0x14, SeekOrigin.Begin);     // Seek to flags
                    uint flags = fileReader.ReadUInt32();        // Read flags
                    if ((flags & 1) == 1)
                    {                      // Bit 1 set means we have to
                                           // skip the shell item ID list
                        fileStream.Seek(0x4c, SeekOrigin.Begin); // Seek to the end of the header
                        uint offset = fileReader.ReadUInt16();   // Read the length of the Shell item ID list
                        fileStream.Seek(offset, SeekOrigin.Current); // Seek past it (to the file locator info)
                    }

                    long fileInfoStartsAt = fileStream.Position; // Store the offset where the file info
                                                                 // structure begins
                    uint totalStructLength = fileReader.ReadUInt32(); // read the length of the whole struct
                    fileStream.Seek(0xc, SeekOrigin.Current); // seek to offset to base pathname
                    uint fileOffset = fileReader.ReadUInt32(); // read offset to base pathname
                                                               // the offset is from the beginning of the file info struct (fileInfoStartsAt)
                    fileStream.Seek((fileInfoStartsAt + fileOffset), SeekOrigin.Begin); // Seek to beginning of
                                                                                        // base pathname (target)
                    long pathLength = (totalStructLength + fileInfoStartsAt) - fileStream.Position - 2; // read
                                                                                                        // the base pathname. I don't need the 2 terminating nulls.
                    char[] linkTarget = fileReader.ReadChars((int)pathLength); // should be unicode safe
                    var link = new string(linkTarget);

                    int begin = link.IndexOf("\0\0");
                    if (begin > -1)
                    {
                        int end = link.IndexOf("\\\\", begin + 2) + 2;
                        end = link.IndexOf('\0', end) + 1;

                        string firstPart = link.Substring(0, begin);
                        string secondPart = link.Substring(end);

                        return firstPart + secondPart;
                    }
                    else
                    {
                        return link;
                    }
                }
            }
            catch
            {
                return "";
            }
        }
    }
}

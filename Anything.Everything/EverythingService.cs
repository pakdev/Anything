using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;

namespace Anything.Everything
{
    public class EverythingService : IEverythingService
    {
        private readonly Collection<SearchResult> _applications;

        public EverythingService()
        {
            _applications = new Collection<SearchResult>();

            const int bufferSize = 260;
            var buffer = new StringBuilder(bufferSize);

            var startMenu = Environment.GetFolderPath(Environment.SpecialFolder.CommonStartMenu);
            SafeNativeMethods.Everything_SetSearchW($@"""{startMenu}""*.lnk");
            SafeNativeMethods.Everything_QueryW(true);

            for (var i = 0; i < SafeNativeMethods.Everything_GetNumResults(); ++i)
            {
                SafeNativeMethods.Everything_GetResultFullPathNameW(i, buffer, bufferSize);
                var pathFile = buffer.ToString().Trim();

                _applications.Add(new SearchResult(pathFile, SearchResultType.App));
            }
        }

        public IEnumerable<SearchResult> Applications => _applications;
    }
}

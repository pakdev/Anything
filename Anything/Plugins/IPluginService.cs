using Anything.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anything.Services
{
    public interface IPluginService
    {
        List<IPlugin> Plugins { get; }

        Task DiscoverPluginsAsync(string pluginDirectory);

        Task ApplySearchToPlugins(string search);
    }
}

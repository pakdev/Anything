using Anything.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Anything.Plugins
{
    public interface IPluginService
    {
        IEnumerable<Lazy<IPlugin>> Plugins { get; }

        IEnumerable<Lazy<IPluginTemplate>> PluginTemplates { get; }

        Task DiscoverPluginsAsync(string pluginDirectory);

        Task ApplyInputToPluginsAsync(string search);
    }
}

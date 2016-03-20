using Anything.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Anything.Services
{
    public interface IPluginService
    {
        List<IPlugin> Plugins { get; }

        Task DiscoverPluginsAsync(string pluginDirectory);

        Task ApplyInputToPluginsAsync(string search);
    }
}

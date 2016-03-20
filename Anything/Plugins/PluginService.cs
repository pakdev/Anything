using Anything.Common;
using Anything.Results;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Reflection;
using System.Threading.Tasks;

namespace Anything.Services
{
    public class PluginService : IPluginService
    {
        private IResultService _resultService;

        public PluginService(IResultService resultService)
        {
            _resultService = resultService;
        }

        [ImportMany(typeof(IPlugin))]
        public List<IPlugin> Plugins { get; private set; }

        public Task DiscoverPluginsAsync(string pluginDirectory)
        {
            return Task.Run(() =>
                {
                    var catalog = new AggregateCatalog(new AssemblyCatalog(Assembly.GetExecutingAssembly()),
                        new DirectoryCatalog(pluginDirectory, "*.dll"));

                    var container = new CompositionContainer(catalog);
                    container.SatisfyImportsOnce(this);
                });
        }

        public Task ApplyInputToPluginsAsync(string search)
        {
            return Task.Run(() =>
                {
                    foreach (var plugin in this.Plugins)
                    {
                        _resultService.UpdateResults(plugin.Process(search));
                    }
                });
        }
    }
}

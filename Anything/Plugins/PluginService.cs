using Anything.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Anything.Services
{
    public class PluginService : IPluginService
    {
        [ImportMany(typeof(IPlugin))]
        public List<IPlugin> Plugins { get; }

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

        public Task ApplySearchToPlugins(string search)
        {
            throw new NotImplementedException();
        }
    }
}

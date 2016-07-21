using Anything.Results;
using Anything.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Anything.Plugins
{
    public class PluginService : IPluginService
    {
        private readonly IResultService _resultService;

        public PluginService(IResultService resultService)
        {
            _resultService = resultService;
        }

        [ImportMany]
        public IEnumerable<Lazy<IPlugin>> Plugins { get; set; }

        [ImportMany]
        public IEnumerable<Lazy<IPluginTemplate>> PluginTemplates { get; set; }

        public Task DiscoverPluginsAsync(string pluginDirectory)
        {
            return Task.Run(() =>
            {
                var catalog = new AggregateCatalog(
                    new AssemblyCatalog(Assembly.GetExecutingAssembly()),
                    new DirectoryCatalog(pluginDirectory, "*.dll")
                );

                var container = new CompositionContainer(catalog);
                container.SatisfyImportsOnce(this);
            });
        }

        public Task ApplyInputToPluginsAsync(string search)
        {
            return Task.Run(() =>
            {
                IEnumerable<IResult> finalResults = new IResult[] {};
                foreach (var plugin in this.Plugins)
                {
                    var results = plugin.Value.Process(search);
                    if (results != null)
                    {
                        finalResults = finalResults.Union(results);
                    }
                }

                // TODO: Does not work
                //finalResults = this.Plugins
                //    .Select(plugin => plugin.Value.Process(search))
                //    .Aggregate(finalResults, (current, results) =>
                //        results != null ? current.Union(results) : current);

                _resultService.UpdateResults(finalResults);
            });
        }
    }
}

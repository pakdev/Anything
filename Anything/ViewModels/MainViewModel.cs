using Anything.Plugins;
using Anything.Properties;
using Anything.Results;
using GalaSoft.MvvmLight;

namespace Anything.ViewModels
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// See http://www.mvvmlight.net
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel(IPluginService pluginService, IResultService resultService)
        {
            pluginService.DiscoverPluginsAsync(Settings.Default.PluginDirectory).ContinueWith(task =>
            {
                foreach (var pluginTemplate in pluginService.PluginTemplates)
                {
                    pluginTemplate.Value.AddToMergedDictionaries();
                }
            });

            resultService.ResultsUpdated += (sender, e) =>
            {
                this.ShowResults = e.NumResults > 0;
            };
        }

        private bool _showResults;
        public bool ShowResults
        {
            get { return _showResults; }
            set { Set(ref _showResults, value); }
        }
    }
}
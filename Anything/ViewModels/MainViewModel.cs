using Anything.Properties;
using Anything.Services;
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
        private IPluginService _pluginService;

        public MainViewModel(IPluginService pluginService)
        {
            _pluginService = pluginService;
            _pluginService.DiscoverPluginsAsync(Settings.Default.PluginDirectory);
        }

        private string _search;
        public string Search
        {
            get { return _search; }
            set
            {
                if (Set(ref _search, value))
                {
                    // let each plugin have a shot at getting results for the text
                    _pluginService.ApplyInputToPluginsAsync(value);
                }
            }
        }
    }
}
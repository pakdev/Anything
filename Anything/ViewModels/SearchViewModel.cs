using System.Windows.Input;
using Anything.Messages;
using Anything.Plugins;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace Anything.ViewModels
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class SearchViewModel : ViewModelBase
    {
        private readonly IPluginService _pluginService;

        /// <summary>
        /// Initializes a new instance of the SearchViewModel class.
        /// </summary>
        public SearchViewModel(IPluginService pluginService)
        {
            _pluginService = pluginService;

            MessengerInstance.Register<FocusMessage<SearchViewModel>>(this, _ => this.TakeFocusCommand.Execute(null));
        }

        public ICommand TakeFocusCommand { get; set; }

        public ICommand KeyDownCommand
        {
            get
            {
                return new RelayCommand<KeyEventArgs>(e =>
                {
                    if (e.Key == Key.Down || e.Key == Key.Up || e.Key == Key.Enter)
                    {
                        MessengerInstance.Send(new FocusMessage<ResultsViewModel>(e.Key));
                    }
                });
            }
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
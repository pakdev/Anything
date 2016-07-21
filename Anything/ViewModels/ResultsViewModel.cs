using Anything.Results;
using Anything.Shared;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Threading;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Anything.Messages;
using GalaSoft.MvvmLight.Command;

namespace Anything.ViewModels
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class ResultsViewModel : ViewModelBase
    {
        private readonly IResultService _resultService;

        /// <summary>
        /// Initializes a new instance of the ResultsViewModel class.
        /// </summary>
        public ResultsViewModel(IResultService resultService)
        {
            _resultService = resultService;
            _resultService.ResultsUpdated += (sender, e) =>
            {
                
                DispatcherHelper.RunAsync(() =>
                {
                    this.SelectedIndex = 0;
                    this.Results.Clear();
                });

                foreach (var result in _resultService.Results)
                {
                    DispatcherHelper.RunAsync(() => this.Results.Add(result));
                }

                DispatcherHelper.RunAsync(() =>
                {
                    this.SelectedIndex = 0;
                });
            };

            MessengerInstance.Register<FocusMessage<ResultsViewModel>>(this, msg =>
            {
                this.TakeFocusCommand.Execute(null);
                //if (msg.KeyPressed == Key.Down)
                //{
                //    this.SelectedIndex = 1;
                //}
                //else
                //{
                //    this.SelectedIndex = 4;
                //}
            });
        }

        public ICommand TakeFocusCommand { get; set; }

        public ICommand KeyDownCommand
        {
            get
            {
                return new RelayCommand<KeyEventArgs>(e =>
                {
                    if (e.Key == Key.Enter)
                    {
                        this.SelectedResult?.Launch.Execute(null);
                        return;
                    }

                    if (e.Key != Key.Down && e.Key != Key.Up)
                    {
                        MessengerInstance.Send(new FocusMessage<SearchViewModel>(e.Key));
                    }
                });
            }
        }

        private ObservableCollection<IResult> _results; 
        public ObservableCollection<IResult> Results
        {
            get
            {
                _results = _results ?? new ObservableCollection<IResult>();
                return _results;
            }
        }

        private IResult _selectedResult;

        public IResult SelectedResult
        {
            get { return _selectedResult;} 
            set { Set(ref _selectedResult, value); }
        }

        private int _selectedIndex;

        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set { Set(ref _selectedIndex, value); }
        }
    }
}
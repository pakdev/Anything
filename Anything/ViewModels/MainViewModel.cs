using System.Windows;
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
        private string _search;
        public string Search
        {
            get { return _search; }
            set
            {
                Set(ref _search, value);
            }
        }
    }
}